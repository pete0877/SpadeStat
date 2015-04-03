using System;
using Npgsql;
using System.IO;
using System.Collections;

namespace SpadeStat.Engine
{
	public delegate void ProgressEventHandler(object sender, EventArgs e);

	/// <summary>
	/// Class responsible for impoting a Poker file into given database.
	/// </summary>
	public class ImportPokerStars
	{
		/// <summary>
		/// Progress event sender.
		/// </summary>		
		public event ProgressEventHandler Changed;

		/// <summary>
		/// Connection to the DB where the files should be imported.
		/// </summary>
		protected NpgsqlConnection m_dbConnection;

		/// <summary>
		/// Transaction used to import the file
		/// </summary>
		protected NpgsqlTransaction m_dbTransaction;

		/// <summary>
		/// Number of lines in the import file
		/// </summary>
		protected int m_totalLines;

		/// <summary>
		/// Number of lines already processed
		/// </summary>
		protected int m_processedLines;

		/// <summary>
		/// Bet order counter
		/// </summary>
		protected int m_betOrderNum;

		/// <summary>
		/// Hash player name -> player Id
		/// </summary>
		protected Hashtable m_playerIDs;	

		/// <summary>
		/// Hash player name -> hand player Id
		/// </summary>
		protected Hashtable m_handPlayerIDs;

		/// <summary>
		/// Hash player name -> hand player object
		/// </summary>
		protected Hashtable m_handPlayers;

		/// <summary>
		/// Hash seat number -> player name
		/// </summary>
		protected Hashtable m_seatPlayerIDs;

		/// <summary>
		/// Flag indicating if for particular hand, big blind has been posted yet
		/// </summary>
		protected bool m_bBigBlindFound;

		/// <summary>
		/// Flag indicating if for particular hand the first call or raise after big blind has been found
		/// </summary>
		protected bool m_bFirstCallRaiseAfterBigBlindFound;

		/// <summary>
		/// ID counter
		/// </summary>
		protected UUIDGenerator m_generator;		

		/// <summary>
		/// Number of hands imported
		/// </summary>
		protected int m_importedHands;

		/// <summary>
		/// Number of tournaments imported
		/// </summary>
		protected int m_importedTours;

		/// <summary>
		/// Number of files imported
		/// </summary>
		protected int m_importedFiles;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dbConnection">Target database connection.</param>
		public ImportPokerStars(NpgsqlConnection dbConnection)
		{
			m_dbConnection = dbConnection;
			m_importedHands = 0;
			m_importedTours = 0;
			m_importedFiles = 0;
		}

		public int GetImportedHands() { return m_importedHands; }
		public int GetImportedTours() { return m_importedTours; }
		public int GetImportedFiles() { return m_importedFiles; }

		// Invoke the Changed event; called whenever list changes
		protected virtual void OnProgressChanged(EventArgs e) 
		{
			if (Changed != null)
				Changed(this, e);
		}

		/// <summary>
		/// Returns percentage indicating current progress.
		/// </summary>
		/// <returns></returns>
		public int GetProgressPercentage()
		{
			if (m_totalLines == 0)
				return 100;

			return (100 * m_processedLines) / m_totalLines;
		}

		/// <summary>
		/// Imports given file into the databse using the connection given through constructor.
		/// Decides on what type of file is being imported and executes appropriate action.
		/// </summary>
		/// <param name="filePath">Full file path to the import file</param>
		public void Import(string filePath)
		{
			try
			{				

				m_dbConnection.Open();
				m_dbTransaction = m_dbConnection.BeginTransaction();

				m_generator = new UUIDGenerator(m_dbTransaction);

				FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false);
				if (fileStream == null)
					throw new Exception("Could not open " + filePath);

				StreamReader streamReader = new StreamReader(fileStream);
				if (streamReader == null)
					throw new Exception("Could not open " + filePath);

				char[] content = streamReader.ReadToEnd().ToCharArray();
				string contentString = new string(content);
				string[] contentLines = contentString.Split('\n');

				// Replave bad strings in all lines:
				IEnumerator itr = contentLines.GetEnumerator();
				int lineCount = 0;
				while (itr.MoveNext())
				{
					string line = (string) itr.Current;
					line = line.Replace("(qualified for the target tournament)", "{qualified for the target tournament}");
					line = line.Replace("out of hand (moved from another table into small blind)", "");
															
					contentLines[lineCount] = line;
					lineCount++;
				}

				m_totalLines = contentLines.Length;
				m_processedLines = 0;

				try
				{
					// Determine type of file and import:
					if (filePath.IndexOf("PokerStars Tournament History Request") >= 0)
						ImportTournamentHistory(contentLines);
					else if (filePath.IndexOf("PokerStars Hand History Request") >= 0)
						ImportHandHistory(contentLines);
					else
						throw new Exception("Don't know how to import the file. Please make sure filename contains 'PokerStars Tournament History Request' or 'PokerStars Hand History Request'");

				}
				catch (Exception errorMessage)
				{
					streamReader.Close();
					fileStream.Close();

					throw new Exception("Could not import " + filePath + ":\n\n" + errorMessage);
				}

				m_generator.Save();

				m_dbTransaction.Commit();
				m_dbConnection.Close();

				m_importedFiles++;

				streamReader.Close();
				fileStream.Close();
			}	
			catch (Exception error)
			{
				m_dbTransaction.Rollback();
				m_dbConnection.Close();

				throw error;
			}
		}		

		/// <summary>
		/// Imports tournament information from given content.
		/// </summary>
		/// <param name="content">File content</param>
		protected void ImportTournamentHistory(string[] contentLines)
		{
			Tournament t = null;
			int position = -1;
			int importedPlayers = 0;
			int pendingPlayers = 0;
			int buyInLineNumber = 0;
			bool foundFinishedDate = false;
			Hashtable lineStringConversionMap = new Hashtable();

			IEnumerator itr = contentLines.GetEnumerator();			
			while (itr.MoveNext())
			{				
				// Notify listeners of progress:
				m_processedLines++;				
				if (m_processedLines % 100 == 0)
					OnProgressChanged(EventArgs.Empty);

				string line = (string) itr.Current;

				try
				{
					line = FormatTournamentFileLine(line);
					// When commaDistanceFromEnd is -1 then there are no commas in the line
					// When commaDistanceFromEnd is 0 then the last comma is the last character in the string.
					// When commaDistanceFromEnd is 1 then the last comma is the second last character in the string.
					// etc
					int commaDistanceFromEnd = -1;
					for (int n = line.Length - 1; n > 0; n--)
					{
						if (line[n] == ',')
						{
							commaDistanceFromEnd = line.Length - n - 1; 
							break;
						}
					}
					line = line.Replace(",", "");
				

					string[] words = line.Split(' ');
				
					// > Tournament History for Tournament <TournamentId>...
					if (line.IndexOf("Tournament History for Tournament") >= 0)
					{
						int tournamentID = int.Parse(words[4]);
						position = 0;  // Line containing 'Tournament History for Tournament' is considered to be at position 0 (line count-wise)

						t = new Tournament(m_dbTransaction, tournamentID);
						t.Initialize();

						TournamentPlayer.ResetTournament(m_dbTransaction, tournamentID);
					}	
		
					// > PokerStars Tournament <TournamentId>, <BetTypCd: Limit|No Limit|Pot Limit> <GameTypCd: Hold'em|Omaha|Omaha Hi/Lo|7 Card Stud|7 Card Stud Hi/Lo>
					if (position == 1 && words.Length >= 5)
					{
						int offSet = 0;
						t.m_BetTypCd = words[3];
						if (t.m_BetTypCd == "Pot" || t.m_BetTypCd == "No")
						{
							offSet = 1;
							t.m_BetTypCd += " " + words[4];
						}

						// Start at position 4 + offSet and collect all known words:
						string gameTypCd = "";
						int n = 4 + offSet;
						while (true)
						{
							string word = words[n];
							if (
								word != "Holdem" &&
								word != "Omaha" &&
								word != "Hi/Lo" &&
								word != "7" &&
								word != "Card" &&
								word != "Stud"
							)
								break;

							if (gameTypCd == "")
								gameTypCd = word;
							else
								gameTypCd += " " + word;

							n++;
							if (n + 1 > words.Length)
								break;
						}
						t.m_GameTypCd = gameTypCd;
					}

					// > {Buy-In: <BuyInAmt>[/<FeeAmt>]}|{Freeroll}
					if (line.IndexOf("Buy-In: ") == 0 && words.Length >= 2)
					{
						buyInLineNumber = position;
						t.m_BuyInAmt = 0;
						t.m_FeeAmt = 0;
						string buyIn = words[1];
						string[] parts = buyIn.Split('/');
						t.m_BuyInAmt = decimal.Parse(parts[0]);						
						if (parts.Length > 1)
							t.m_FeeAmt = decimal.Parse(parts[1]);
					}

					// > {Buy-In: <BuyInAmt>[/<FeeAmt>]}|{Freeroll}
					if (line == "Freeroll")
					{
						buyInLineNumber = position;
						t.m_BuyInAmt = 0;
						t.m_FeeAmt = 0;
					}

					// > <TotalPlayerNum> players
					if (buyInLineNumber > 0 && position == buyInLineNumber + 1 && words.Length >= 2 && line.IndexOf(" players") > 0)
					{
						t.m_TotalPlayerNum = int.Parse(words[0]);
						pendingPlayers = t.m_TotalPlayerNum;

						if (t.m_TotalPlayerNum == 2)
							t.m_TournamentTypCd = "HeadUp";
						else if (t.m_TotalPlayerNum > 2 && t.m_TotalPlayerNum < 19)
							t.m_TournamentTypCd = "SNG";
						else 
							t.m_TournamentTypCd = "Multi";
					}

					// > Total Prize Pool: <TotalPrizeFundAmt> 
					if (buyInLineNumber > 0 && words.Length >= 4 && line.IndexOf("Total Prize Pool: ") == 0)
						t.m_TotalPrizeFundAmt = decimal.Parse(words[3]);

					// > Tournament started - <StartDt> 
					if (buyInLineNumber > 0 && words.Length >= 7 && line.IndexOf("Tournament started - ") == 0)
					{
						string dateTimeTxt = words[3] + " " + words[5];						
						if (dateTimeTxt.IndexOf("0/00/00") < 0)
							t.m_StartDt = DateTime.Parse(dateTimeTxt);
					}

					// > Tournament finished - <EndDt> 
					if (buyInLineNumber > 0 && words.Length >= 7 && line.IndexOf("Tournament finished - ") == 0)
					{
						string dateTimeTxt = words[3] + " " + words[5];
						if (dateTimeTxt.IndexOf("0/00/00") < 0)
							t.m_EndDt = DateTime.Parse(dateTimeTxt);					
						foundFinishedDate = true;
					}


					// >   <PlacementNum>: <PlayerNm> (<LocationTxt>), [<WinningAmt>][ (qualified for the target tournament)]
					if (foundFinishedDate && importedPlayers < pendingPlayers && words.Length >= 3 && line.IndexOf("Tournament finished") < 0)
					{						
						// First attemp to parse the <PlacementNum>. If sucesffull, we know we are on a line with the syntax specified above
						int placementNum = 0;
						bool parseSucessful = true;
						try
						{
							placementNum = int.Parse(words[0].Replace(":", ""));
						}
						catch (Exception e) 
						{
							parseSucessful = false;
						}
						
						if (parseSucessful)
						{
							// Location and player name are found by going backwards and counting number of close-parens and open-parens characters.
							// We quit looking for begining of location when we find even number of close and open parens. Whatever is in between the first 
							// open and last close parens will be the location. String to the left of the first open paren is the player name.
							int firstColanPos = line.IndexOf(':');
							int firstParenPos = -1;
							int lastParenPos = -1;
							int perenDepth = 0;
							int distanceFromEnd = 0;

							for (int n = line.Length - 1; n > firstColanPos; n--)
							{
								if (line[n] == ')' && distanceFromEnd >= commaDistanceFromEnd)
								{
									perenDepth++;
									if (firstParenPos < 0)
										firstParenPos = n;
								}
								else if (line[n] == '(' && distanceFromEnd >= commaDistanceFromEnd)
								{
									perenDepth--;
									lastParenPos = n;
								}

								if (perenDepth == 0 && firstParenPos > 0)
									break;

								distanceFromEnd++;
							}

							string playerNm = line.Substring(firstColanPos + 2, (lastParenPos - 1) - (firstColanPos + 2));
							string locationTxt = line.Substring(lastParenPos + 1, (firstParenPos - 1) - (lastParenPos + 1) + 1);

							// Convert the player name so that it doesn't include any special characters:
							bool conversionWasPerformed = false;
							string originalPlayerNm = string.Copy(playerNm);
							if (playerNm.IndexOf(':') >= 0)
							{
								conversionWasPerformed = true;
								playerNm = playerNm.Replace(':', '|');
							}
							if (playerNm.IndexOf('(') >= 0)
							{
								conversionWasPerformed = true;
								playerNm = playerNm.Replace('(', '|');
							}
							if (playerNm.IndexOf(')') >= 0)
							{
								conversionWasPerformed = true;
								playerNm = playerNm.Replace(')', '|');
							}
							if (playerNm.IndexOf('[') >= 0)
							{
								conversionWasPerformed = true;
								playerNm = playerNm.Replace('[', '|');
							}
							if (playerNm.IndexOf(']') >= 0)
							{
								conversionWasPerformed = true;
								playerNm = playerNm.Replace(']', '|');
							}

							if (conversionWasPerformed)
								lineStringConversionMap[originalPlayerNm] = playerNm;

							// Conver the location to not include any special characters
							conversionWasPerformed = false;

							string originalLocationTxt = string.Copy(locationTxt);
							if (locationTxt.IndexOf(':') >= 0)
							{
								conversionWasPerformed = true;
								locationTxt = locationTxt.Replace(':', '|');
							}
							if (locationTxt.IndexOf('(') >= 0)
							{
								conversionWasPerformed = true;
								locationTxt = locationTxt.Replace('(', '|');
							}
							if (locationTxt.IndexOf(')') >= 0)
							{
								conversionWasPerformed = true;
								locationTxt = locationTxt.Replace(')', '|');
							}
							if (locationTxt.IndexOf('[') >= 0)
							{
								conversionWasPerformed = true;
								locationTxt = locationTxt.Replace('[', '|');
							}
							if (locationTxt.IndexOf(']') >= 0)
							{
								conversionWasPerformed = true;
								locationTxt = locationTxt.Replace(']', '|');
							}

							if (conversionWasPerformed)
								lineStringConversionMap[originalLocationTxt] = locationTxt;

							
							decimal winningAmt = 0;
							
							// Find won amount if any. This can be done by traveling forward starting with the last
							// ')' in the string found and going forward until encountering the next space. We go
							// forward by two positions from last ')' to get over ") "
							for (int n = firstParenPos + 2; n < line.Length; n++)
							{
								if (line[n] == ' ')
								{
									string winningAmtTxt = line.Substring(firstParenPos + 2, n - (firstParenPos + 2));

									try
									{
										winningAmt = decimal.Parse(winningAmtTxt);
										break;
									}
									catch (Exception e) 
									{
									}
								}
							}							

							// Find the player based on user ID:
							int playerID = Player.FindPlayerID(m_dbTransaction, playerNm);
							Player p = null;
							if (playerID < 0)
								p = new Player(m_dbTransaction, m_generator);
							else
								p = new Player(m_dbTransaction, playerID);

							p.Initialize();
							p.m_PlayerNm = playerNm;
							p.m_LocationTxt = locationTxt;						
							p.Save();

							// Create tournament participation record:
							TournamentPlayer tp = new TournamentPlayer(m_dbTransaction, m_generator);
							tp.Initialize();
							tp.m_PlayerID = p.m_PlayerID;						
							tp.m_PlayerNm = p.m_PlayerNm;
							tp.m_TournamentID = t.m_TournamentID;
							tp.m_WinningAmt = winningAmt;
							tp.m_PlacementNum = placementNum;

							if (line.IndexOf("{qualified for the target tournament}") > 0)
								tp.m_SatelliteSeatFlg = 1;

							tp.Save();

							importedPlayers++;
						}
					}

					// Position is incremented only when we encountered a non-ignored line:
					// > [... added to the prize pool by PokerStars.com]
					if (position >= 0 && line.IndexOf("added to the prize pool by PokerStars.com") < 0)
						position++;
				}
				catch (Exception parseError)
				{
					throw new Exception("While parsing line '" + line + "' found following problem:\n" + parseError.Message);
				}
			}

			if (position < 0)
				throw new Exception("Could not find tournament ID");

			if (t != null)
				t.Save();

			m_importedTours++;
		}

		/// <summary>
		/// Imports hands information from given content.
		/// </summary>
		/// <param name="content">File content</param>
		protected void ImportHandHistory(string[] contentLines)
		{
			// First initialize global player name -> player id registry to simplify player searches throughout the whole file:
			m_playerIDs = new Hashtable(); 

			// Scroll through the lines until '**********' is found and line containing 'PokerStars Game' is followed. then determine game type and call appropriate handler:
			IEnumerator itr = contentLines.GetEnumerator();
			while (itr.MoveNext())
			{
				string line = (string) itr.Current;
				line = FormatHandFileLine(line, null);

				if (line.IndexOf("**********") == 0)
				{
					itr.MoveNext();
					line = (string) itr.Current;
					line = FormatHandFileLine(line, null);

					string word = null;
					if (line.IndexOf("PokerStars Game") == 0)
					{
						string gameTypCd = "";
						Tournament t = null;
						TournamentHand h = null;
						try
						{
							// PokerStars Game #<HandId>: Tournament #<TournamentId>, <GameTypCd: Hold'em|Omaha|Omaha Hi/Lo> <BetTypCd: Limit|No Limit|Pot Limit> - ... ( IF No Limit|Pot Limit {<SmallBlindAmt>/<BigBlindAmt>} ELSE {<SmallBetAmt>/<BigBetAmt>}) - <StartDt(hand)>
							// First grab information from the line we are already on (common for all formats):
							string[] words = line.Split(' ');
							if (words.Length < 11)
								continue;

							// #<HandId>:
							string handIDTxt = words[2];
							handIDTxt = handIDTxt.Remove(0, 1);
							handIDTxt = handIDTxt.Remove(handIDTxt.Length - 1, 1);
							int handID = int.Parse(handIDTxt);
							h = new TournamentHand(m_dbTransaction, handID);
							h.Initialize();

							int parseOffset = 0;
							if (line.IndexOf("Tournament #") > 0)
							{
								// #<TournamentId>,
								string tournamentIDTxt = words[4];
								tournamentIDTxt = tournamentIDTxt.Remove(0, 1);
								tournamentIDTxt = tournamentIDTxt.Remove(tournamentIDTxt.Length - 1, 1);
								int tournamentID = int.Parse(tournamentIDTxt);
								t = new Tournament(m_dbTransaction, tournamentID);
								t.Initialize();

								h.m_TournamentId = t.m_TournamentID;
							}
							else
							{
								parseOffset = -1; // Would be -2 but there is a double-space
								h.m_TournamentId = 0;
							}
							
							h.m_TotalPlayerNum = 0;
							
							m_bBigBlindFound = false;
							m_bFirstCallRaiseAfterBigBlindFound = false;


							// <GameTypCd: .....
							int gameTypCdWordCount = 0;
							gameTypCd = "";
							while (true)
							{
								word = words[parseOffset + 5 + gameTypCdWordCount];
								if (
									word != "Hold'em" &&
									word != "Omaha" &&
									word != "Hi/Lo" &&
									word != "7" &&
									word != "Card" &&
									word != "Stud"
									)
									break;

								if (gameTypCd == "")
									gameTypCd = word;
								else
									gameTypCd += " " + word;

								gameTypCdWordCount++;
								if (parseOffset + 5 + gameTypCdWordCount >= words.Length) 
									break;
							}
							h.m_GameTypCd = gameTypCd;

							// <BetTypCd: .....
							int betTypCdWordCount = 0;
							string betTypCd = "";
							while (true)
							{
								word = words[parseOffset + 5 + betTypCdWordCount + gameTypCdWordCount];
								if (
									word != "Limit" &&
									word != "No" &&
									word != "Pot"
									)
									break;

								if (betTypCd == "")
									betTypCd = word;
								else
									betTypCd += " " + word;

								betTypCdWordCount++;
								if (parseOffset + 5 + betTypCdWordCount + gameTypCdWordCount >= words.Length) 
									break;
							}
							h.m_BetTypCd = betTypCd;

							// ( IF No Limit|Pot Limit {<SmallBlindAmt>/<BigBlindAmt>} ELSE {<SmallBetAmt>/<BigBetAmt>})
							// Find next word with '(' in it:
							int startSearch = parseOffset + 5 + betTypCdWordCount + gameTypCdWordCount;						
							decimal smallBlindAmt = 0;
							decimal bigBlindAmt = 0;
							decimal smallBetAmt = 0;
							decimal bigBetAmt = 0;
							while (true)
							{
								if (startSearch == words.Length)
									break;

								word = words[startSearch];
								if (word.IndexOf("(") == 0)
								{
									decimal firstAmount = 0;
									decimal secondAmount = 0;
									word = word.Remove(0, 1);
									word = word.Remove(word.Length - 1, 1);									
									string[] parts = word.Split('/');
									if (parts.Length < 2)
										break;
									firstAmount = decimal.Parse(parts[0]);
									secondAmount = decimal.Parse(parts[1]);
									if (betTypCd == "No Limit" || betTypCd == "Pot Limit")
									{
										smallBlindAmt = firstAmount;
										bigBlindAmt = secondAmount;							
									}
									else
									{
										smallBetAmt = firstAmount;
										bigBetAmt = secondAmount;								
									}
									break;
								}
								startSearch++;
							}
							h.m_SmallBetAmt = smallBetAmt;
							h.m_BigBetAmt = bigBetAmt;
							h.m_SmallBlindAmt = smallBlindAmt;
							h.m_BigBlindAmt = bigBlindAmt;

							// At this point startSearch points to (20/40) word. Need to skip one and we get the date, skip another one and we get the time:
							int datePosition = startSearch + 2;
							string startTs = words[datePosition] + " " + words[datePosition + 2];
							DateTime startDt = new DateTime();
							if (startTs.IndexOf("0/00/00") < 0)
							{							
								startDt = DateTime.Parse(startTs);
								h.m_StartDt = startDt;                       
							}

							// Reset existing records for this hand:
							HandCard.ResetHand(m_dbTransaction, handID);
							HandPlayer.ResetHand(m_dbTransaction, handID);
							HandPlayerAction.ResetHand(m_dbTransaction, handID);
						}
						catch (Exception parseError)
						{
							throw new Exception("While parsing line '" + line + "' found following problem:\n" + parseError.Message);
						}

						m_handPlayerIDs = new Hashtable();
						m_seatPlayerIDs = new Hashtable();
						m_handPlayers = new Hashtable();
						
						m_betOrderNum = 0;

						

						if 
						(
							gameTypCd == "Hold'em" ||
							gameTypCd == "Omaha" ||
							gameTypCd == "Omaha Hi/Lo"
						)
							ImportHandOmanaHoldemGame(t, h, itr);
						else if 
						(
							gameTypCd == "7 Card Stud" ||
							gameTypCd == "7 Card Stud Hi/Lo"
						)
							ImportHandStudGame(t, h, itr);

						h.Save();

						m_importedHands++;

						if (t != null)
							t.Save();
					}
				}
			}
		}


		/// <summary>
		/// Imports omaha and holdem games
		/// </summary>
		/// <param name="t">Initialized trounament object</param>
		/// <param name="h">Initialized hand object</param>
		/// <param name="itr">Line enumerator</param>
		protected void ImportHandOmanaHoldemGame(Tournament t, TournamentHand h, IEnumerator itr)
		{			
			string	bettingRoundTypCd = "PreFlop";
			bool	preDeal = true;
			int sectionDepth = 0; // Number of lines we are into given section:			
			string handCardTypCdPrefix = "c";
			if (h.m_GameTypCd == "Hold'em")
				handCardTypCdPrefix = "h";
			else if (h.m_GameTypCd.IndexOf("Omaha") >= 0)
				handCardTypCdPrefix = "o";

			string openHandPlayerNm = "";

			Hashtable playerPositions = new Hashtable();
			int playerPosition = 0;
			int buttonPosition = 0;
			Hashtable lineStringConversionMap = new Hashtable(); 			

			while (itr.MoveNext())
			{
				string line = (string) itr.Current;
				line = FormatHandFileLine(line, lineStringConversionMap);

				if (line.Length == 0)
					break;

				// Check for empty space at the end of the line
				if (line[line.Length - 1] == ' ')
					line = line.Remove(line.Length - 1, 1); // Remove space at the end of the line

				if (line.Length == 0)
					break;

				try
				{

					string[] words = line.Split(' ');


					sectionDepth++; // The first line after '*** '-line is always at 1;

					m_processedLines++;
					if (m_processedLines % 100 == 0)
						OnProgressChanged(EventArgs.Empty);

					// Check if section is changing:
					if (line.IndexOf("*** ") == 0)
					{
						if (line.IndexOf("HOLE CARDS") > 0)
						{
							preDeal = false;

							// As soon as we enter this section, we need to calculate positions for all players (with respect to the button position)
							int shiftPosition = h.m_TotalPlayerNum - buttonPosition - 1;
							IDictionaryEnumerator itrPlayers = playerPositions.GetEnumerator();
							while (itrPlayers.MoveNext())
							{
								string playerNm = (string) itrPlayers.Key;
								int position = (int) itrPlayers.Value;
								position = h.m_TotalPlayerNum - position;
								position = position - shiftPosition;
								if (position < 1)
									position = h.m_TotalPlayerNum + position;

								HandPlayer hp = (HandPlayer) m_handPlayers[playerNm];
								hp.m_PositionNum = position;
							}
						}
						else if (line.IndexOf("FLOP") > 0)
						{
							bettingRoundTypCd = "Flop";
							m_betOrderNum = 0;
						}
						else if (line.IndexOf("TURN") > 0)
						{
							bettingRoundTypCd = "Turn";
							m_betOrderNum = 0;
						}
						else if (line.IndexOf("RIVER") > 0)	
						{
							bettingRoundTypCd = "River";
							m_betOrderNum = 0;
						}
						else if (line.IndexOf("SHOW DOWN") > 0)
						{
							bettingRoundTypCd = "Show Down";
							m_betOrderNum = 0;
						}
						else if (line.IndexOf("SUMMARY") > 0)
						{
							bettingRoundTypCd = "Summary";
							m_betOrderNum = 0;
						}

						sectionDepth = 0;						

						continue;
					}

					if (preDeal)
					{
						if (sectionDepth == 1 && words[0] == "Table")
						{
							// Table '<TableNm(can have spaces)>' ... Seat #<ButtonSeatNum> is the button
							// Starting with second word scroll until we found two words with ' in it:
							int offSet = 1;
							int foundQuotes = 0;
							string tableID = "";
							while (foundQuotes < 2 && offSet < words.Length)
							{
								string word = words[offSet];
								if (word.Length > 0)
								{
									if (tableID.Length == 0)
										tableID = word;
									else
										tableID += " " + word;

									if (word[0] == '\'')
										foundQuotes++;

									if (word[word.Length - 1] == '\'')
										foundQuotes++;
								}

								offSet++;
							}
							tableID = tableID.Replace("'", "");
							h.m_TableId = tableID;							

							// Now we just need to find the button seat number .. it the first word that begins with # after offset:
							int buttonSeatNum = 0;
							while (offSet < words.Length)
							{
								string word = words[offSet];
								if (word.IndexOf('#') == 0)
								{
									word = word.Remove(0, 1);
									buttonSeatNum = int.Parse(word);
									break;
								}
								offSet++;
							}
							h.m_ButtonSeatNum = buttonSeatNum;
						}
						else if (sectionDepth > 1 && words[0] == "Seat")
						{
							// Seat <PlayerSeatNum>: <PlayerNm> (<HandStartChipAmt> in chips)
							
							// Find player seat number:
							string word = words[1];
							word = word.Remove(word.Length - 1, 1);
							int playerSeatNum = int.Parse(word);

							// Find player name:

							// Begin by finding the first ':'
							int firstColanPos = line.IndexOf(':');
							// Going backwards, find the '(' before <HandStartChipAmt>
							int parenBeforeChipAmtPos = -1;
							int lastSeenSpacePos = -1;
							for (int n = line.Length - 1; n > 0; n--)
							{
								if (line[n] == ' ')
									lastSeenSpacePos = n;
								else if (line[n] == '(')
								{
									parenBeforeChipAmtPos = n;
									break;
								}
							}

							// The player name is string between positions  (firstColanPos + 2)  and  (parenBeforeChipAmtPos - 1)
							if (firstColanPos < 0 || parenBeforeChipAmtPos < 0)
								throw new Exception("Line which was expected to carry player seat and name is invalid: " + line);

							int playerNmLen = (parenBeforeChipAmtPos - 1) - (firstColanPos + 2);
							string playerNm = line.Substring(firstColanPos + 2, playerNmLen);

							// Convert the player name so that it doesn't include any special characters:
							bool conversionWasPerformed = false;
							string originalPlayerNm = string.Copy(playerNm);
							if (playerNm.IndexOf(':') >= 0)
							{
								conversionWasPerformed = true;
								playerNm = playerNm.Replace(':', '|');
							}
							if (playerNm.IndexOf('(') >= 0)
							{
								conversionWasPerformed = true;
								playerNm = playerNm.Replace('(', '|');
							}
							if (playerNm.IndexOf(')') >= 0)
							{
								conversionWasPerformed = true;
								playerNm = playerNm.Replace(')', '|');
							}
							if (playerNm.IndexOf('[') >= 0)
							{
								conversionWasPerformed = true;
								playerNm = playerNm.Replace('[', '|');
							}
							if (playerNm.IndexOf(']') >= 0)
							{
								conversionWasPerformed = true;
								playerNm = playerNm.Replace(']', '|');
							}

							if (conversionWasPerformed)
								lineStringConversionMap[originalPlayerNm] = playerNm;

							// Record name of the player on the given seat number:
							m_seatPlayerIDs[playerSeatNum] = playerNm;

							// Prepare data for calculating position:
							playerPositions[playerNm] = playerPosition;
							if (h.m_ButtonSeatNum == playerSeatNum)
								buttonPosition = playerPosition;

							playerPosition++;

							/*
								After the player import is compelted, if following were the seats
									seat 1: A
									seat 2: B
									seat 5: C
									seat 6: D
									seat 7: E
									seat 9: F
								with button on seat 5,
								
								Then the playerPositions will store
									playerPositions[A] = 0  5
									playerPositions[B] = 1  4
									playerPositions[C] = 2  3
									playerPositions[D] = 3  2
									playerPositions[E] = 4  1
									playerPositions[F] = 5  0  
								and the buttonPosition will be equal to 2 and h.m_TotalPlayerNum will be 6
								
								In order to calculate position for any player, we need to do following:
									1. reverse counting order 
										playerPositions[A] = 5
										playerPositions[B] = 4
										playerPositions[C] = 3
										playerPositions[D] = 2
										playerPositions[E] = 1
										playerPositions[F] = 0  									
									2. account of offset of the buttonPosition		(2)
										playerPositions[A] = 5
										playerPositions[B] = 4
										playerPositions[C] = 1
										playerPositions[D] = 0
										playerPositions[E] = 5
										playerPositions[F] = 0  																
							*/

							decimal handStartChipAmt = 0;
							if (lastSeenSpacePos > 0)
							{
								string handStartChipAmtText = line.Substring(parenBeforeChipAmtPos + 1, lastSeenSpacePos - (parenBeforeChipAmtPos + 1));
								handStartChipAmt = decimal.Parse(handStartChipAmtText);
							}

							// Attempt to find the player:
							TournamentPlayer tp = null;
							int playerID = -1;
							if (m_playerIDs.Contains(playerNm))
								playerID = (int) m_playerIDs[playerNm];
							else
								playerID = Player.FindPlayerID(m_dbTransaction, playerNm);

							Player p = null;
							if (playerID < 0)
							{
								p = new Player(m_dbTransaction, m_generator);
								
								if (t != null)
									tp = new TournamentPlayer(m_dbTransaction, m_generator);
							}
							else
							{										
								p = new Player(m_dbTransaction, playerID);

								if (t != null)
								{
									int tournamentPlayerID = TournamentPlayer.FindTournamentPlayerID(m_dbTransaction, playerID, t.m_TournamentID);
									if (tournamentPlayerID >= 0)
										tp = new TournamentPlayer(m_dbTransaction, tournamentPlayerID);
									else
										tp = new TournamentPlayer(m_dbTransaction, m_generator);
								}
							}

							p.Initialize();
							p.m_PlayerNm = playerNm;						
							p.Save();

							m_playerIDs[playerNm] = p.m_PlayerID;

							if (tp != null)
							{
								tp.Initialize();
								tp.m_PlayerID = p.m_PlayerID;
								tp.m_TournamentID = t.m_TournamentID;
								tp.m_PlayerNm = p.m_PlayerNm;
								tp.Save();
							}

							HandPlayer hp = new HandPlayer(m_dbTransaction, m_generator);
							hp.Initialize();
							hp.m_GameTypCd = h.m_GameTypCd;
							hp.m_BetTypCd = h.m_BetTypCd;
							hp.m_HandId = h.m_HandId;
							hp.m_PlayerId = p.m_PlayerID;
							hp.m_HandStartChipAmt = handStartChipAmt;
							hp.m_PlayerNm = playerNm;
							hp.m_PlayerSeatNum = playerSeatNum;

							if (t != null)
								hp.m_TournamentId = t.m_TournamentID;
							else
								hp.m_TournamentId = 0;

							m_handPlayerIDs[playerNm] = hp.m_HandPlayerId;
							m_handPlayers[playerNm] = hp;
							
							// In tournament hands if player is sitting out we still count him as part of the hand
							// In non-tournament hands if player is sitting out, we don't count him/her towards
							// the total player count for the hand:
							if (line.IndexOf(") is sitting out") == (line.Length - 16))
							{
								if (t != null)
									h.m_TotalPlayerNum++;
							}
							else
								h.m_TotalPlayerNum++;
						}
						else 
							ImportHandAction(t, h, line, words, bettingRoundTypCd);					
					} 
					else if (
						bettingRoundTypCd == "PreFlop" ||
						bettingRoundTypCd == "Flop" ||
						bettingRoundTypCd == "Turn" ||
						bettingRoundTypCd == "River" ||
						bettingRoundTypCd == "Show Down"
						)
					{
						if (bettingRoundTypCd == "PreFlop" && line.IndexOf("Dealt to ") == 0)
						{
							// Dealt to <PlayerNm> \[<HoleCard#>*\]
							int playerNmOffSet = 2;
							string playerNm = "";
							while (playerNmOffSet < words.Length && words[playerNmOffSet].IndexOf("[") < 0)
							{
								if (playerNm == "")
									playerNm = words[playerNmOffSet];
								else
									playerNm += " " + words[playerNmOffSet];

								playerNmOffSet++;
							}							

							if (m_playerIDs.Contains(playerNm))
							{
								int playerID = (int) m_playerIDs[playerNm];
								int handPlayerID = (int) m_handPlayerIDs[playerNm];		
								HandPlayer hp = (HandPlayer) m_handPlayers[playerNm];

								openHandPlayerNm = playerNm;
					
								// playerNmOffSet at this moment points to the first hand card:
								int holeCardPosition = playerNmOffSet;
								int holeCardNumber = 0;
								ArrayList cards = new ArrayList();
								while (holeCardPosition < words.Length)
								{
									holeCardNumber++;									
									string holeCardNumberText = holeCardNumber.ToString();

									string holeCard = words[holeCardPosition];
									holeCard = holeCard.Replace("[", "");
									holeCard = holeCard.Replace("]", "");

									if (holeCard.Length != 2)
										throw new Exception("Hole card " + holeCardNumberText + " [" + holeCard + "] is invalid");
									
									HandCard hc = new HandCard(m_dbTransaction, m_generator);
									hc.Initialize();
									hc.m_HandId = h.m_HandId;
									hc.m_HandPlayerId = handPlayerID;
									hc.m_PlayerId = playerID;
									hc.m_PlayerNm = playerNm;
									hc.m_TypCd = handCardTypCdPrefix + holeCardNumberText;
									hc.m_CardValTxt = holeCard.Substring(0, 1);
									hc.m_CardSuitTxt = holeCard.Substring(1, 1);
									hc.Save();	

									Utils.SetCardFlags(hc, hp);

									cards.Add(hc);

									holeCardPosition++;
								}

								CreateCardCombos(cards, hp);
							}
						}
						else
							ImportHandAction(t, h, line, words, bettingRoundTypCd);
					}
					else if (bettingRoundTypCd == "Summary")
					{
						if (sectionDepth == 1 && words.Length >= 6)
						{
							// Total pot <PotAmt> | Rake <RakeAmt>
							string potAmtTxt = words[2];
							string rakeAmtTxt = words[words.Length - 1];
							decimal potAmt = decimal.Parse(potAmtTxt);
							decimal rakeAmt = decimal.Parse(rakeAmtTxt);
							h.m_PotAmt = potAmt;
							h.m_RakeAmt = rakeAmt;
						}
						else
						{
							if (sectionDepth == 2 && words.Length >= 2 && words[0] == "Board" && words[1][0] == '[')
							{
								// [Board \[<BoardCard#>\]]
								int boardCardPosition = 1;
								while (true)
								{
									string boardCardPositionTxt = boardCardPosition.ToString();
									string boardCard = string.Copy(words[boardCardPosition]);
									boardCard = boardCard.Replace("[", "");
									boardCard = boardCard.Replace("]", "");
									HandCard hc = new HandCard(m_dbTransaction, m_generator);
									hc.Initialize();
									hc.m_HandId = h.m_HandId;
									hc.m_HandPlayerId = -1;
									hc.m_PlayerId = -1;
									hc.m_PlayerNm = "";
									hc.m_TypCd = "b" + boardCardPositionTxt;
									hc.m_CardValTxt = boardCard.Substring(0, 1);
									hc.m_CardSuitTxt = boardCard.Substring(1, 1);
									hc.Save();	

									Utils.SetCardFlags(hc, h);

									if (words[boardCardPosition].IndexOf("]") > 0)
										break;

									boardCardPosition++;
								}							
							}
							else if (words.Length > 3 && words[0] == "Seat" && words[1].IndexOf(":") > 0 && (line.IndexOf("showed") >= 0 || line.IndexOf("mucked") >= 0))
							{
								// Seat <PlayerSeatNum>: <PlayerNm> [showed|mucked] \[<HoleCard#>*\] // Only if not 'Dealt to'. Look for 'showed [' and 'mucked [' as the landmarks for player's name span.
								// Start with seconds word that ends with : and start player name collection from there until hitting (
								int offSet = 2;
								string playerNm = "";
								while (offSet < words.Length)
								{
									string word = words[offSet];
									if (
										word.Length > 0 && word[0] == '(' ||
										( 
											offSet + 1 < words.Length &&
											(word == "showed" || word == "mucked") &&
											words[offSet + 1][0] == '['
										)
									)
										break;
									
									if (playerNm.Length == 0)
										playerNm = word;
									else
										playerNm += " " + word;

									offSet++;
								}

								if (openHandPlayerNm != playerNm)
								{
									// Find the first [ in the list of words, starting with offSet

									int playerID = (int) m_playerIDs[playerNm];
									int handPlayerID = (int) m_handPlayerIDs[playerNm];
									HandPlayer hp = (HandPlayer) m_handPlayers[playerNm];

									bool foundFirstCard = false;
									int cardCount = 0;

									ArrayList cards = new ArrayList();
									while (offSet < words.Length)
									{
										string word = words[offSet];
										if (word.IndexOf("[") >= 0)
											foundFirstCard = true;

										if (foundFirstCard)
										{
											string playerCard = string.Copy(word);
											playerCard = playerCard.Replace("[", "");
											playerCard = playerCard.Replace("]", "");

											if (playerCard.Length == 2)
											{
												cardCount++;

												HandCard hc = new HandCard(m_dbTransaction, m_generator);
												hc.Initialize();
												hc.m_HandId = h.m_HandId;
												hc.m_HandPlayerId = handPlayerID;
												hc.m_PlayerId = playerID;
												hc.m_PlayerNm = playerNm;
												hc.m_TypCd = handCardTypCdPrefix + cardCount.ToString();
												hc.m_CardValTxt = playerCard.Substring(0, 1);
												hc.m_CardSuitTxt = playerCard.Substring(1, 1);
												hc.Save();			
							
												Utils.SetCardFlags(hc, hp);

												cards.Add(hc);

											}											
										}

										if (word.IndexOf("]") > 0)
											break;

										offSet++;									
									}								

									CreateCardCombos(cards, hp);
								}
							}
						}
					}
				}
				catch (Exception parseError)
				{
					throw new Exception("While parsing line '" + line + "' found following problem:\n" + parseError.Message);
				}			
			}

			// Save all hand player objects:
			IDictionaryEnumerator itrHandPlayers = m_handPlayers.GetEnumerator();
			while (itrHandPlayers.MoveNext())
			{
				string playerNm = (string) itrHandPlayers.Key;
				HandPlayer hp = (HandPlayer) itrHandPlayers.Value;
				hp.CalculateSuitedStsCd();
				hp.Save();
			}

			OnProgressChanged(EventArgs.Empty);
		}


		/// <summary>
		/// Imports stud game (hand)
		/// </summary>
		/// <param name="t">Initialized trounament object</param>
		/// <param name="h">Initialized hand object</param>
		/// <param name="itr">Line enumerator</param>
		protected void ImportHandStudGame(Tournament t, TournamentHand h, IEnumerator itr)
		{	
			string bettingRoundTypCd = "PreDeal";
			int sectionDepth = 0; // Number of lines we are into given section:			
			string handCardTypCdPrefix = "s";
			string openHandPlayerNm = "";
			int betRoundCardNumbers = 0;
		
			Hashtable lineStringConversionMap = new Hashtable(); 			

			while (itr.MoveNext())
			{
				string line = (string) itr.Current;
				line = FormatHandFileLine(line, lineStringConversionMap);

				if (line.Length == 0)
					break;
				
				// Check for empty space at the end of the line
				if (line[line.Length - 1] == ' ')
					line = line.Remove(line.Length - 1, 1); // Remove space at the end of the line

				if (line.Length == 0)
					break;

				try
				{
					string[] words = line.Split(' ');

					sectionDepth++; // The first line after '*** '-line is always at 1;

					m_processedLines++;
					if (m_processedLines % 100 == 0)
						OnProgressChanged(EventArgs.Empty);

					// Check if section is changing:
					if (line.IndexOf("*** ") == 0)
					{
						if (line.IndexOf("3rd STREET") > 0)
						{
							bettingRoundTypCd = "3rd Street";
							betRoundCardNumbers = 3;
						}
						else if (line.IndexOf("4th STREET") > 0)
						{
							bettingRoundTypCd = "4th Street";
							betRoundCardNumbers = 4;
						}
						else if (line.IndexOf("5th STREET") > 0)
						{
							bettingRoundTypCd = "5th Street";
							betRoundCardNumbers = 5;
						}
						else if (line.IndexOf("6th STREET") > 0)						
						{
							bettingRoundTypCd = "6th Street";
							betRoundCardNumbers = 6;
						}
						else if (line.IndexOf("RIVER") > 0)
						{
							bettingRoundTypCd = "River";
							betRoundCardNumbers = 7;
						}
						else if (line.IndexOf("SHOW DOWN") > 0)
							bettingRoundTypCd = "Show Down";
						else if (line.IndexOf("SUMMARY") > 0)
							bettingRoundTypCd = "Summary";

						sectionDepth = 0;
						m_betOrderNum = 0;

						continue;
					}

					if (bettingRoundTypCd == "PreDeal")
					{
						if (sectionDepth == 1 && words[0] == "Table")
						{
							// Table '<TableNm(can have spaces)>' ... Seat #<ButtonSeatNum> is the button
							// Starting with second word scroll until we found two words with ' in it:
							int offSet = 1;
							int foundQuotes = 0;
							string tableID = "";
							while (foundQuotes < 2 && offSet < words.Length)
							{
								string word = words[offSet];
								if (word.Length > 0)
								{
									if (tableID.Length == 0)
										tableID = word;
									else
										tableID += " " + word;

									if (word[0] == '\'')
										foundQuotes++;

									if (word[word.Length - 1] == '\'')
										foundQuotes++;
								}

								offSet++;
							}
							tableID = tableID.Replace("'", "");
							h.m_TableId = tableID;							
							h.m_ButtonSeatNum = 0;
						}
						else if (sectionDepth > 1 && words[0] == "Seat")
						{
							// Seat <PlayerSeatNum>: <PlayerNm> (<HandStartChipAmt> in chips)
							
							// Find player seat number:
							string word = words[1];
							word = word.Remove(word.Length - 1, 1);
							int playerSeatNum = int.Parse(word);

							// Find player name:

							// Begin by finding the first ':'
							int firstColanPos = line.IndexOf(':');
							// Going backwards, find the '(' before <HandStartChipAmt>
							int parenBeforeChipAmtPos = -1;
							int lastSeenSpacePos = -1;
							for (int n = line.Length - 1; n > 0; n--)
							{
								if (line[n] == ' ')
									lastSeenSpacePos = n;
								else if (line[n] == '(')
								{
									parenBeforeChipAmtPos = n;
									break;
								}
							}

							// The player name is string between positions  (firstColanPos + 2)  and  (parenBeforeChipAmtPos - 1)
							if (firstColanPos < 0 || parenBeforeChipAmtPos < 0)
								throw new Exception("Line which was expected to carry player seat and name is invalid: " + line);

							int playerNmLen = (parenBeforeChipAmtPos - 1) - (firstColanPos + 2);
							string playerNm = line.Substring(firstColanPos + 2, playerNmLen);

							// Convert the player name so that it doesn't include any special characters:
							bool conversionWasPerformed = false;
							string originalPlayerNm = string.Copy(playerNm);
							if (playerNm.IndexOf(':') >= 0)
							{
								conversionWasPerformed = true;
								playerNm = playerNm.Replace(':', '|');
							}
							if (playerNm.IndexOf('(') >= 0)
							{
								conversionWasPerformed = true;
								playerNm = playerNm.Replace('(', '|');
							}
							if (playerNm.IndexOf(')') >= 0)
							{
								conversionWasPerformed = true;
								playerNm = playerNm.Replace(')', '|');
							}
							if (playerNm.IndexOf('[') >= 0)
							{
								conversionWasPerformed = true;
								playerNm = playerNm.Replace('[', '|');
							}
							if (playerNm.IndexOf(']') >= 0)
							{
								conversionWasPerformed = true;
								playerNm = playerNm.Replace(']', '|');
							}


							if (conversionWasPerformed)
								lineStringConversionMap[originalPlayerNm] = playerNm;

							// Record name of the player on the given seat number:
							m_seatPlayerIDs[playerSeatNum] = playerNm;

							decimal handStartChipAmt = 0;
							if (lastSeenSpacePos > 0)
							{
								string handStartChipAmtText = line.Substring(parenBeforeChipAmtPos + 1, lastSeenSpacePos - (parenBeforeChipAmtPos + 1));
								handStartChipAmt = decimal.Parse(handStartChipAmtText);
							}

							// Attempt to find the player:
							TournamentPlayer tp = null;
							int playerID = -1;
							if (m_playerIDs.Contains(playerNm))
								playerID = (int) m_playerIDs[playerNm];
							else
								playerID = Player.FindPlayerID(m_dbTransaction, playerNm);

							Player p = null;
							if (playerID < 0)
							{
								p = new Player(m_dbTransaction, m_generator);
								if (t != null)
									tp = new TournamentPlayer(m_dbTransaction, m_generator);
							}
							else
							{										
								p = new Player(m_dbTransaction, playerID);

								if (t != null)
								{
									int tournamentPlayerID = TournamentPlayer.FindTournamentPlayerID(m_dbTransaction, playerID, t.m_TournamentID);
									if (tournamentPlayerID >= 0)
										tp = new TournamentPlayer(m_dbTransaction, tournamentPlayerID);
									else
										tp = new TournamentPlayer(m_dbTransaction, m_generator);
								}
							}

							p.Initialize();
							p.m_PlayerNm = playerNm;						
							p.Save();

							m_playerIDs[playerNm] = p.m_PlayerID;

							if (tp != null)
							{
								tp.Initialize();
								tp.m_PlayerID = p.m_PlayerID;
								tp.m_TournamentID = t.m_TournamentID;
								tp.m_PlayerNm = p.m_PlayerNm;
								tp.Save();
							}

							HandPlayer hp = new HandPlayer(m_dbTransaction, m_generator);
							hp.Initialize();
							hp.m_GameTypCd = h.m_GameTypCd;
							hp.m_BetTypCd = h.m_BetTypCd;
							hp.m_HandId = h.m_HandId;
							hp.m_PlayerId = p.m_PlayerID;
							hp.m_HandStartChipAmt = handStartChipAmt;
							hp.m_PlayerNm = playerNm;
							hp.m_PlayerSeatNum = playerSeatNum;
							if (t != null)
								hp.m_TournamentId = t.m_TournamentID;
							else
								hp.m_TournamentId = 0;
							
							m_handPlayerIDs[playerNm] = hp.m_HandPlayerId;
							m_handPlayers[playerNm] = hp;

							// In tournament hands if player is sitting out we still count him as part of the hand
							// In non-tournament hands if player is sitting out, we don't count him/her towards
							// the total player count for the hand:
							if (line.IndexOf(") is sitting out") == (line.Length - 16))
							{
								if (t != null)
									h.m_TotalPlayerNum++;
							}
							else
								h.m_TotalPlayerNum++;
						}
						else 
							ImportHandAction(t, h, line, words, bettingRoundTypCd);					
					} 
					else if (
						bettingRoundTypCd == "3rd Street" ||
						bettingRoundTypCd == "4th Street" ||
						bettingRoundTypCd == "5th Street" ||
						bettingRoundTypCd == "6th Street" ||
						bettingRoundTypCd == "River" ||
						bettingRoundTypCd == "Show Down"
						)
					{
						// Dealt to's can appear anywhere except for in show-down
						if (bettingRoundTypCd != "Show Down" && line.IndexOf("Dealt to ") == 0)
						{
							// Dealt to <PlayerNm> \[<HoleCard#>*\]
							int playerNmOffSet = 2;
							string playerNm = "";
							while (playerNmOffSet < words.Length && words[playerNmOffSet].IndexOf("[") != 0)
							{
								if (playerNm == "")
									playerNm = words[playerNmOffSet];
								else
									playerNm += " " + words[playerNmOffSet];

								playerNmOffSet++;
							}							

							if (m_playerIDs.Contains(playerNm))
							{
								int playerID = (int) m_playerIDs[playerNm];
								int handPlayerID = (int) m_handPlayerIDs[playerNm];
								HandPlayer hp = (HandPlayer) m_handPlayers[playerNm];

								// playerNmOffSet currently points at the first instance of word that begins with [
								// What we need to do is to find the second instance .. if it exists .. and use it instead 
								// of the first instance. If second instance is found, then the first instance contains 
								// already known cards (existing) and second instance contains new cards. If the second 
								// instance is not found, then the first instance contains new cards.

								// We also need to count number of cards in the first set.
								// If there are 3 of them, and there is no second set, then the 
								// card numbers will be 1,2,3. If there is no second set and 
								// there is only one card in the first set, then that card's number
								// is 3. the next one would be 4, etc.
								int firstCardSetPosition = playerNmOffSet;
								int secondCardSetPosition = playerNmOffSet + 1;
								bool foundSecond = false;
								int countFirstSetCards = 0;
								while (secondCardSetPosition < words.Length)
								{
									string word = words[secondCardSetPosition];
									if (word.IndexOf("[") == 0)
									{
										foundSecond = true;
										break;
									}
									secondCardSetPosition++;
									countFirstSetCards++;
								}

								// If there were only one card in the first set, then countFirstSetCards would be 0
								int holeCardNumberShift = 0;
								if (!foundSecond && countFirstSetCards > 0)
									holeCardNumberShift = -2;

								int newCardSetPosition = firstCardSetPosition;
								if (foundSecond)
									newCardSetPosition = secondCardSetPosition;								

								// Now we can start importing cards starting with newCardSetPosition
								int holeCardNumber = 0;
								ArrayList cards = new ArrayList();
								while (newCardSetPosition < words.Length)
								{									
									string holeCardNumberText = (betRoundCardNumbers + holeCardNumberShift + holeCardNumber).ToString();

									string holeCard = string.Copy(words[newCardSetPosition]);
									holeCard = holeCard.Replace("[", "");
									holeCard = holeCard.Replace("]", "");

									if (holeCard.Length != 2)
										throw new Exception("Hole card " + holeCardNumberText + " [" + holeCard + "] is invalid");

									HandCard hc = new HandCard(m_dbTransaction, m_generator);
									hc.Initialize();
									hc.m_HandId = h.m_HandId;
									hc.m_HandPlayerId = handPlayerID;
									hc.m_PlayerId = playerID;
									hc.m_PlayerNm = playerNm;
									hc.m_TypCd = handCardTypCdPrefix + holeCardNumberText;
									hc.m_CardValTxt = holeCard.Substring(0, 1);
									hc.m_CardSuitTxt = holeCard.Substring(1, 1);
									hc.Save();	

									Utils.SetCardFlags(hc, hp);

									cards.Add(hc);

									if (words[newCardSetPosition].IndexOf("]") > 0)
										break;
							
									newCardSetPosition++;
									holeCardNumber++;
								}
								
								// If we are on 3rd street, and we imported 3 cards, then the player must be 
								// the open-hand player. We need to momorize the player name because we treat
								// this player's summary info differently:
								if (holeCardNumber == 2 && bettingRoundTypCd == "3rd Street")  // holeCardNumber is 0 if there is 1 card.
									openHandPlayerNm = playerNm;
							}
						}
						else
							ImportHandAction(t, h, line, words, bettingRoundTypCd);
					}
					else if (bettingRoundTypCd == "Summary")
					{
						if (sectionDepth == 1 && words.Length >= 6)
						{
							// Total pot <PotAmt> | Rake <RakeAmt>
							string potAmtTxt = words[2];
							string rakeAmtTxt = words[words.Length - 1];
							decimal potAmt = decimal.Parse(potAmtTxt);
							decimal rakeAmt = decimal.Parse(rakeAmtTxt);
							h.m_PotAmt = potAmt;
							h.m_RakeAmt = rakeAmt;
						}
						else
						{
							if (sectionDepth == 2 && words.Length >= 2 && words[0] == "Board" && words[1][0] == '[')
							{
								// [Board \[<BoardCard#>\]]
								int boardCardPosition = 1;
								while (true)
								{
									string boardCardPositionTxt = boardCardPosition.ToString();
									string boardCard = string.Copy(words[boardCardPosition]);
									boardCard = boardCard.Replace("[", "");
									boardCard = boardCard.Replace("]", "");
									HandCard hc = new HandCard(m_dbTransaction, m_generator);
									hc.Initialize();
									hc.m_HandId = h.m_HandId;
									hc.m_HandPlayerId = -1;
									hc.m_PlayerId = -1;
									hc.m_PlayerNm = "";
									hc.m_TypCd = "b" + boardCardPositionTxt;
									hc.m_CardValTxt = boardCard.Substring(0, 1);
									hc.m_CardSuitTxt = boardCard.Substring(1, 1);
									hc.Save();	

									Utils.SetCardFlags(hc, h);

									if (words[boardCardPosition].IndexOf("]") > 0)
										break;

									boardCardPosition++;
								}							
							}
							else if (words.Length > 3 && words[0] == "Seat" && words[1].IndexOf(":") > 0 && (line.IndexOf("showed") >= 0 || line.IndexOf("mucked") >= 0))
							{
								// Seat <PlayerSeatNum>: <PlayerNm> [showed|mucked] \[<HoleCard#>*\] // Only if not 'Dealt to'. Look for 'showed [' and 'mucked [' as the landmarks for player's name span.
								// Start with seconds word that ends with : and start player name collection from there until hitting (
								int offSet = 2;
								string playerNm = "";
								while (offSet < words.Length)
								{
									string word = words[offSet];
									if (
										word.Length > 0 && 
										word[0] == '(' 
										||
										( 
											offSet + 1 < words.Length &&
											(word == "showed" || word == "mucked") &&
											words[offSet + 1][0] == '['
										)
									)
										break;
									
									if (playerNm.Length == 0)
										playerNm = word;
									else
										playerNm += " " + word;

									offSet++;
								}

								if (openHandPlayerNm != playerNm)
								{
									// Find the first [ in the list of words, starting with offSet

									int playerID = (int) m_playerIDs[playerNm];
									int handPlayerID = (int) m_handPlayerIDs[playerNm];
									HandPlayer hp = (HandPlayer) m_handPlayers[playerNm];

									bool foundFirstCard = false;
									int cardCount = 0;

									ArrayList cards = new ArrayList();

									while (offSet < words.Length)
									{
										string word = words[offSet];
										if (word.IndexOf("[") >= 0)
											foundFirstCard = true;

										if (foundFirstCard)
										{
											string playerCard = string.Copy(word);
											playerCard = playerCard.Replace("[", "");
											playerCard = playerCard.Replace("]", "");

											if (playerCard.Length == 2)
											{
												cardCount++;

												if (
													cardCount == 1 ||
													cardCount == 2 ||
													cardCount == 7 
													)
												{
													HandCard hc = new HandCard(m_dbTransaction, m_generator);
													hc.Initialize();
													hc.m_HandId = h.m_HandId;
													hc.m_HandPlayerId = handPlayerID;
													hc.m_PlayerId = playerID;
													hc.m_PlayerNm = playerNm;
													hc.m_TypCd = handCardTypCdPrefix + cardCount.ToString();
													hc.m_CardValTxt = playerCard.Substring(0, 1);
													hc.m_CardSuitTxt = playerCard.Substring(1, 1);
													hc.Save();		
					
													Utils.SetCardFlags(hc, hp);

													cards.Add(hc);
												}
											}											
										}

										if (word.IndexOf("]") > 0)
											break;

										offSet++;									
									}	
								}
							}
						}
					}
				}
				catch (Exception parseError)
				{
					throw new Exception("While parsing line '" + line + "' found following problem:\n" + parseError.Message);
				}
			}

			// Save all hand player objects:
			IDictionaryEnumerator itrHandPlayers = m_handPlayers.GetEnumerator();
			while (itrHandPlayers.MoveNext())
			{
				string playerNm = (string) itrHandPlayers.Key;
				HandPlayer hp = (HandPlayer) itrHandPlayers.Value;
				hp.CalculateSuitedStsCd();
				hp.Save();
			}

			OnProgressChanged(EventArgs.Empty);
		}


		/// <summary>
		/// Imports hand action line.
		/// </summary>
		/// <param name="t">Tournament object</param>
		/// <param name="h">Hand object</param>
		/// <param name="line">Full text line</param>
		/// <param name="words">Array or words seperated by ' ' from the full line</param>
		/// <param name="bettingRoundTypCd">Name of the betting round</param>
		void ImportHandAction(Tournament t, TournamentHand h, string line, string[] words, string bettingRoundTypCd)
		{			
			// First ignore the line if it contains a known, bad keyword:
			if (
				line.IndexOf(" said, \"") >= 0 ||
				line.IndexOf(": sits out") == (line.Length - 10) ||
				line.IndexOf(" is sitting out") == (line.Length - 15)
			)
				return;

			// Collect all words until : character:
			int playerNmWordCount = 0;
			string playerNm = "";
			bool foundColan = false;
			while (playerNmWordCount < words.Length)
			{
				string word = words[playerNmWordCount];
				if (playerNm.Length == 0)
					playerNm = word;
				else
					playerNm += " " + word;

				if (word.Length > 0 && word[word.Length - 1] == ':')
				{
					foundColan = true;
					break;
				}
				playerNmWordCount++;
			}
			playerNmWordCount++; // For players with one-word names, this value would be at 0 when exiting the loop

			if (foundColan)
			{
				/* We might be looking at one of following:
				 * <PlayerNm>: <ActionTypCd: folds>
				 * <PlayerNm>: <ActionTypCd: checks>
				 * <PlayerNm>: <ActionTypCd: shows>
				 * <PlayerNm>: <ActionTypCd: calls> <ActionAmt>
				 * <PlayerNm>: <ActionTypCd: raises> ## to <ActionAmt>
				 * <PlayerNm>: <ActionTypCd: mucks hand>
				 * 
				 */

				playerNm = playerNm.Remove(playerNm.Length - 1, 1); // Remove the colan
				// Find the player:
				if (m_playerIDs.Contains(playerNm))
				{
					int playerID = (int) m_playerIDs[playerNm];
					int handPlayerID = (int) m_handPlayerIDs[playerNm];
					HandPlayer hp = (HandPlayer) m_handPlayers[playerNm];

					// Starting with playerNmWordCount, we need to collect all actions:
					int actionPosition = playerNmWordCount;
					string actionTypCd = "";
					while (actionPosition < words.Length)
					{
						string word = words[actionPosition];
						if (
							word == "folds" ||
							word == "checks" ||
							word == "shows" ||
							word == "calls" ||
							word == "mucks" ||
							word == "hand" ||
							word == "posts" ||
							word == "the" ||
							word == "ante" ||
							word == "blind" ||
							word == "big" ||
							word == "small" ||
							word == "bets" ||
							word == "brings-in" ||
							word == "hi" ||
							word == "low" ||
							word == "raises"

						)
						{
							if (actionTypCd.Length == 0)
								actionTypCd = word;
							else 
								actionTypCd += " " + word;
							
							actionPosition++;
						}
						else
							break;
					}

					if (actionTypCd.Length > 0)
					{
						decimal actionAmt = 0;
						decimal moneyInAmt = 0;
						if (
							actionTypCd == "bets" ||
							actionTypCd == "calls" ||
							actionTypCd == "posts the ante" ||
							actionTypCd == "posts small blind" ||
							actionTypCd == "posts big blind" ||
							actionTypCd == "brings-in low" ||
							actionTypCd == "brings-in hi"
						)
						{
							// <PlayerNm>: <ActionTypCd: calls> <ActionAmt>	
							string actionAmtTxt = words[actionPosition];
							actionAmt = decimal.Parse(actionAmtTxt);

							moneyInAmt = actionAmt;

							if (actionTypCd == "posts the ante")
								h.m_AnteAmt = actionAmt;
							else if (actionTypCd == "posts small blind")
								hp.m_BlindStsCd = "SB";
							else if (actionTypCd == "posts big blind")
							{
								hp.m_BlindStsCd = "BB";
								m_bBigBlindFound = true;
							}
							else if (actionTypCd == "calls")
							{
								if (m_bBigBlindFound && !m_bFirstCallRaiseAfterBigBlindFound)
								{
									hp.m_FirstInFlg = 1;
									m_bFirstCallRaiseAfterBigBlindFound = true;

									if (hp.m_PositionNum == 1 || hp.m_PositionNum == 2)
										h.m_StealAttemptFlg = 1;
								}
							}

						}
						else if (actionTypCd == "raises")
						{
							// <PlayerNm>: <ActionTypCd: raises> <raisefromAmt> to <ActionAmt>	
							string raisefromAmtTxt = words[actionPosition + 0];
							decimal raisefromAmt = decimal.Parse(raisefromAmtTxt);

							string actionAmtTxt = words[actionPosition + 2];
							actionAmt = decimal.Parse(actionAmtTxt);

							moneyInAmt = actionAmt - raisefromAmt;

							if (m_bBigBlindFound && !m_bFirstCallRaiseAfterBigBlindFound)
							{
								hp.m_FirstInFlg = 1;
								m_bFirstCallRaiseAfterBigBlindFound = true;

								if (hp.m_PositionNum == 1 || hp.m_PositionNum == 2)
									h.m_StealAttemptFlg = 1;
							}

							if (bettingRoundTypCd == "PreFlop")
								hp.m_PreFlopRaiseFlg = 1;
						}
						else if (actionTypCd == "folds")
							hp.m_FoldCd = bettingRoundTypCd;

						if (actionTypCd == "raises" || actionTypCd == "calls" || actionTypCd == "bets")
						{
							hp.m_VoluntaryMoneyFlg = 1;
							hp.m_MoneyInAmt += moneyInAmt;
						}

						HandPlayerAction hpa = new HandPlayerAction(m_dbTransaction, m_generator);
						hpa.Initialize();
						hpa.m_ActionTypCd = actionTypCd;
						hpa.m_BettingRoundTypCd = bettingRoundTypCd;
						hpa.m_HandId = h.m_HandId;
						hpa.m_HandPlayerId = handPlayerID;
						hpa.m_PlayerId = playerID;
						hpa.m_PlayerNm = playerNm;							
						hpa.m_BetOrderNum = m_betOrderNum;			
						hpa.m_ActionAmt = actionAmt;						
						hpa.Save();

						m_betOrderNum++;
					}
				}
				// Else ignore the line if we can't recognize the player
			}
			else
			{
				// <PlayerNm> <ActionTypCd: collected> <ActionAmt> from ... pot		// BetOrderNum++
				if (
					line.IndexOf("collected") >= 0 &&
					line.IndexOf("from") >= 0 &&
					line.IndexOf("pot") >= 0
					)
				{
					h.m_FinalRoundCd = bettingRoundTypCd;

					// Scroll through the words until finding 'collected'. 
					// All words before that are the player name, the word immidiatelly after the 'collected' is the amount:
					int collectedPos = 0;
					playerNm = "";
					bool foundCollected = false;
					while (collectedPos < words.Length)
					{
						string word = words[collectedPos];
						if (playerNm.Length == 0)
							playerNm = word;
						else
							playerNm += " " + word;

						collectedPos++;

						if (collectedPos < words.Length && words[collectedPos] == "collected")
						{
							foundCollected = true;
							break;						
						}
					}

					if (m_playerIDs.Contains(playerNm) && foundCollected && (collectedPos + 1) < words.Length)
					{
						int playerID = (int) m_playerIDs[playerNm];		
						int handPlayerID = (int) m_handPlayerIDs[playerNm];
		
						string actionAmtTxt = words[collectedPos + 1];
						decimal actionAmt = decimal.Parse(actionAmtTxt);
		
						HandPlayer hp = (HandPlayer) m_handPlayers[playerNm];
						if (hp != null)
							hp.m_MoneyWonAmt += actionAmt;

						HandPlayerAction hpa = new HandPlayerAction(m_dbTransaction, m_generator);
						hpa.Initialize();
						hpa.m_ActionTypCd = "collected";
						hpa.m_BettingRoundTypCd = bettingRoundTypCd;
						hpa.m_HandId = h.m_HandId;
						hpa.m_HandPlayerId = handPlayerID;
						hpa.m_PlayerId = playerID;
						hpa.m_PlayerNm = playerNm;							
						hpa.m_BetOrderNum = m_betOrderNum;		
						hpa.m_ActionAmt = actionAmt;						
						hpa.Save();

						m_betOrderNum++;
					}
					// Else ignore the line if we can't recognize the player	
				}
			}
		}


		/// <summary>
		/// Formats import line (removes unnecessary characters, double spaces, etc)
		/// </summary>
		/// <param name="line">Line to be formattted.</param>
		/// <returns>Formatted line</returns>
		protected string FormatTournamentFileLine(string line)
		{
			string result = line;

			// Replace commonly ominted characters:
			result = result.Replace("#", "");
			result = result.Replace("'", "");
			result = result.Replace("$", "");			
			result = result.Replace("%", "");			
			result = result.Replace("\x0d", "");
			result = result.Replace("\x0a", "");
			
			if (result == "")
				return "";
					
			// Remove double spaces
			while (result.IndexOf("  ") >= 0)
				result = result.Replace("  ", " ");
					
			// No result should start with a space:
			if (result.Length > 0 && result[0] == ' ')
				result = result.Remove(0, 1);

			// No result should end with a space:
			if (result.Length > 0 && result[result.Length - 1] == ' ')
				result = result.Remove(result.Length - 1, 1);

			return result;
		}

		/// <summary>
		/// Formats import line (removes unnecessary characters)
		/// </summary>
		/// <param name="line">Line to be formattted.</param>
		/// <returns>Formatted line</returns>
		protected string FormatHandFileLine(string line, Hashtable substitutions)
		{
			string result = line;

			// Replace commonly ominted characters:
			result = result.Replace("$", "");			
			if (result.Length > 0)
				result = result.Remove(result.Length - 1, 1); // Remove \n at the end

			if (substitutions != null)
			{
				IDictionaryEnumerator itr = substitutions.GetEnumerator();
				while (itr.MoveNext())
				{
					string originalString = (string) itr.Key;
					string newString = (string) itr.Value;

					result = result.Replace(originalString, newString);			
				}
			}

			return result;
		}


		/// <summary>
		/// Function responsible for creating card combinations. 
		/// 
		/// E.g. for: Ac2s3c4h:
		///   Axxx
		///   2xxx
		///   3xxx
		///   4xxx
		///   A2xx
		///   A3xx
		///   A4xx
		///   23xx
		///   24xx
		///   34xx
		///   A23x
		///   A24x
		///   A34x
		///   234x
		///   A234
		/// 
		/// </summary>
		/// <param name="cards">List of cards</param>
		/// <param name="hp">Hand player record</param>
		public void CreateCardCombos(ArrayList cards, HandPlayer hp)
		{
			// Remove existing records:
			HandPlayerCombo.ResetCombo(m_dbTransaction, hp.m_HandPlayerId);

			cards.Sort();
			object[] sortedCards = cards.ToArray();
			Hashtable combos = new Hashtable();

			long limit = 1 << (sortedCards.Length);
			for (long c = 1; c < limit; c++)
			{
				// Find which cards are included:
				string combination = "";
				for (int n = 0; n < sortedCards.Length; n++)
				{
					long mapCheck = (1 << n);
					if ((c & mapCheck) > 0)
					{
						HandCard hc = (HandCard) sortedCards[n];
						combination += hc.m_CardValTxt;
					}
				}

				combos[combination] = true;
			}

			IDictionaryEnumerator itr = combos.GetEnumerator();
			while (itr.MoveNext())
			{
				string combo = (string) itr.Key;

				while (combo.Length < sortedCards.Length)
					combo += "x";					

				HandPlayerCombo hcc = new HandPlayerCombo(m_dbTransaction, m_generator);
				hcc.Initialize();
				hcc.m_HandComboTxt = combo;
				hcc.m_HandPlayerId = hp.m_HandPlayerId;
				hcc.Save();
			}
		}
	}
}
