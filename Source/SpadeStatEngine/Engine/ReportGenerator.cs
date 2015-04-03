using System;
using Npgsql;
using System.IO;
using System.Collections;

namespace SpadeStat.Engine
{
	/// <summary>
	/// Summary description for ReportGenerator.
	/// </summary>
	public class ReportGenerator
	{
		public enum ReportOutputType { HTML, CSV };

		/// <summary>
		/// Class which will be able to receive debug messages.
		/// </summary>
		public DebugFeeder m_debugFeeder;

		/// <summary>
		/// Connection to the DB where all queries are ran
		/// </summary>
		protected NpgsqlConnection m_dbConnection;

		/// <summary>
		/// DB transaction object aqcuired from the database connection
		/// </summary>
		protected NpgsqlTransaction m_dbTransaction;

		/// <summary>
		/// HTML string being written as final HTML content
		/// </summary>
		protected string m_outputContent;

		/// <summary>
		/// Where clause criterions indexed by field names of the ReportCriteria class (e.g. 'crithandcomboflg')
		/// </summary>
		protected Hashtable m_criterions;

		/// <summary>
		/// Report definition record.
		/// </summary>
		protected Report m_report;

		/// <summary>
		/// Full path to directory where the execution of the program is taking place.
		/// </summary>
		protected string m_workingPath;

		/// <summary>
		/// Output type currently being generated
		/// </summary>
		protected ReportOutputType m_outputType;

		/// <summary>
		/// List of loaded report sections
		/// </summary>
		protected ArrayList m_lstSectionIDs;

		/// <summary>
		/// Global field to be used to order all sections that can be order by the field
		/// </summary>
		protected string m_orderBy;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dbConnection">Database connection</param>
		public ReportGenerator(NpgsqlConnection dbConnection)
		{
			m_dbConnection = dbConnection;
			m_criterions = null;
		}

		/// <summary>
		/// Sets internal criterions hash
		/// </summary>
		/// <param name="m_criterions"></param>
		public void SetCriterions(Hashtable criterions)
		{
			m_criterions = criterions;
		}


		/// <summary>
		/// Set internal representation of sorting order
		/// </summary>
		/// <param name="orderBy">Sorting field</param>
		public void SetOrderBy(string orderBy)
		{
			m_orderBy = orderBy;
		}		


		/// <summary>
		/// Generates report of given id.
		/// </summary>
		/// <param name="reportID">Report ID</param>
		/// <param name="workingPath">workingPath</param>
		/// <returns>Relative path to HTML template that was generated as result</returns>
		public string GenerateReport(int reportID, string workingPath, ReportOutputType outputType)
		{
			m_workingPath = workingPath;
			m_outputType = outputType;
			m_outputContent = "";

			m_dbConnection.Open();
			m_dbTransaction = m_dbConnection.BeginTransaction();

			// Read in the report and then read the main HTML page referenced by the report:
			m_report = new Report(m_dbTransaction, reportID);
			m_report.Initialize();

			string outputFile = null;
			string outputFilePath = null;
			if (m_outputType == ReportOutputType.HTML)
			{
				outputFile = "Temp\\output.htm";
				outputFilePath = m_workingPath + "\\" + "Temp\\output.htm";

				LoadReportPage();				
				LoadReportSections();

				IEnumerator itr = m_lstSectionIDs.GetEnumerator();
				while (itr.MoveNext())
				{
					int sectionID = (int) itr.Current;
					RenderHTMLSection(sectionID);
				}
			}
			else
			{
				outputFile = "Temp\\output.csv";
				outputFilePath = m_workingPath + "\\" + "Temp\\output.csv";
				
				LoadReportSections();

				// Only process the first section
				if (m_lstSectionIDs.Count > 0)
				{
					int sectionID = (int) m_lstSectionIDs[0];
					RenderCSVSection(sectionID);
				}				
			}			

			// Finally write the generated output to the output file:
			FileStream reportFile = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
			StreamWriter outputStream = new StreamWriter(reportFile);
			outputStream.WriteLine(m_outputContent);							
			outputStream.Flush();
			outputStream.Close();

			m_dbConnection.Close();

			return outputFile;
		}


		/// <summary>
		/// Internal method used for reading the main report page template.
		/// </summary>
		protected void LoadReportPage()
		{
			string pageFilePath = m_workingPath + "\\Templates\\" + m_report.m_ReportPage;

			FileStream pageFile = new FileStream(pageFilePath, FileMode.Open, FileAccess.Read, FileShare.None, 4096, true);
			StreamReader inputStream = new StreamReader(pageFile);
			m_outputContent = inputStream.ReadToEnd();
			inputStream.Close();
		}


		/// <summary>
		/// Internal method used for loading report sections (generating them).
		/// </summary>
		protected void LoadReportSections()
		{
			NpgsqlCommand command = m_dbTransaction.Connection.CreateCommand();
			command.Transaction = m_dbTransaction;
			command.CommandText = "select SectionID from ReportSection where ReportID = " + m_report.m_ReportID.ToString() + " order by SectionID";
			NpgsqlDataReader reader = command.ExecuteReader();

			m_lstSectionIDs = new ArrayList();
			while(reader.Read())
			{
				int sectionID = reader.GetInt32(0);
				m_lstSectionIDs.Add(sectionID);				
			}
			reader.Close();
		}


		/// <summary>
		/// Internal method used for processing section of given ID.
		/// </summary>
		/// <param name="sectionID">Section ID</param>
		protected void RenderHTMLSection(int sectionID)
		{
			ReportSection section = new ReportSection(m_dbTransaction, sectionID);
			section.Initialize();	

			string sectionOutput = "";
			string sectionSource = "";

			string sectionSourcePath = m_workingPath + "\\Templates\\" + section.m_SectionPage;

			FileStream sectionFile = new FileStream(sectionSourcePath, FileMode.Open, FileAccess.Read, FileShare.None, 4096, true);
			StreamReader inputStream = new StreamReader(sectionFile);
			sectionSource = inputStream.ReadToEnd();
			inputStream.Close();
			sectionFile.Close();

			NpgsqlCommand command = m_dbTransaction.Connection.CreateCommand();
			command.Transaction = m_dbTransaction;
			string sectionSQL = ComposeSectionSQL(section);
			command.CommandText = sectionSQL;
			NpgsqlDataReader reader = command.ExecuteReader();

			bool haveSchema = false;
			int valuesLength = 0;
			Hashtable columnPositions = new Hashtable();			
			object[] values = null;
			bool isOnEvenRow = false;
			int rowCount = 0;
			while(reader.Read())
			{
				rowCount++;

				if (values == null)
					values = new object[reader.FieldCount];

				reader.GetValues(values);

				if (!haveSchema)
				{				
					for (int count = 0; count < reader.FieldCount; count++) 
					{
						string columnName = reader.GetName(count);
						columnPositions[columnName.ToLower()] = count;
					}

					valuesLength = reader.FieldCount;				

					haveSchema = true;
				}

				string sectionSourceParsed = string.Copy(sectionSource);

				// For each column, perform substitution within the section page string:
				IDictionaryEnumerator itr = columnPositions.GetEnumerator();
				while (itr.MoveNext())
				{
					string column = (string) itr.Key;
					int position = (int) itr.Value;
					object objectVal = values[position];
					string stringVal = objectVal.ToString();

					sectionSourceParsed = sectionSourceParsed.Replace("{" + column + "}", stringVal);					
				}

				if (isOnEvenRow)				
					sectionSourceParsed = sectionSourceParsed.Replace("{_evenrow_}", "1");					
				else
					sectionSourceParsed = sectionSourceParsed.Replace("{_evenrow_}", "0");

				sectionSourceParsed = sectionSourceParsed.Replace("{_rowcount_}", rowCount.ToString());

				sectionOutput += sectionSourceParsed;

				isOnEvenRow = !isOnEvenRow;
			}
			reader.Close();

			m_outputContent = m_outputContent.Replace("{" + section.m_SectionNm + "}", sectionOutput);
		}


		/// <summary>
		/// Renders given section as CVS file
		/// </summary>
		/// <param name="sectionID">Section ID</param>
		protected void RenderCSVSection(int sectionID)
		{
			ReportSection section = new ReportSection(m_dbTransaction, sectionID);
			section.Initialize();	

			string sectionOutput = "";

			NpgsqlCommand command = m_dbTransaction.Connection.CreateCommand();
			command.Transaction = m_dbTransaction;
			string sectionSQL = ComposeSectionSQL(section);
			command.CommandText = sectionSQL;
			NpgsqlDataReader reader = command.ExecuteReader();

			bool haveSchema = false;
			object[] values = null;
			while(reader.Read())
			{
				if (values == null)
					values = new object[reader.FieldCount];

				reader.GetValues(values);

				if (!haveSchema)
				{				
					for (int count = 0; count < reader.FieldCount; count++) 
					{
						string columnName = reader.GetName(count);
						if (count > 0)
                            sectionOutput += ",\"" + columnName + "\"";
						else
							sectionOutput += "\"" + columnName + "\"";
					}
					haveSchema = true;
					sectionOutput += "\n";
				}

				// For each column export the data:
				IEnumerator itr = values.GetEnumerator();
				bool firstColumn = true;
				while (itr.MoveNext())
				{
					string columnValue = itr.Current.ToString();
					columnValue	= columnValue.Replace("\"", "\\\"");
					if (firstColumn)
						sectionOutput += "\"" + columnValue + "\"";
					else
						sectionOutput += ",\"" + columnValue + "\"";

					firstColumn = false;
				}
				sectionOutput += "\n";
			}
			reader.Close();

			m_outputContent = sectionOutput;
		}


		/// <summary>
		/// Composes and returns the SQL statement that should be used to generate section content.
		/// </summary>
		/// <param name="section">Section object</param>
		/// <returns></returns>
		protected string ComposeSectionSQL(ReportSection section)
		{
			string whereClause = "";
			if (m_criterions != null)
			{
				if (section.m_CritHandComboFlg > 0 && m_criterions.Contains("crithandcomboflg"))
					whereClause += " AND (" + m_criterions["crithandcomboflg"] + ") ";
				if (section.m_CritPositionFlg > 0 && m_criterions.Contains("critpositionflg"))
					whereClause += " AND (" + m_criterions["critpositionflg"] + ") ";
				if (section.m_CritNumPlayerFlg > 0 && m_criterions.Contains("critnumplayerflg"))
					whereClause += " AND (" + m_criterions["critnumplayerflg"] + ") ";
				if (section.m_CritGameTypFlg > 0 && m_criterions.Contains("critgametypflg"))
					whereClause += " AND (" + m_criterions["critgametypflg"] + ") ";
				if (section.m_CritBetTypFlg > 0 && m_criterions.Contains("critbettypflg"))
					whereClause += " AND (" + m_criterions["critbettypflg"] + ") ";
				if (section.m_CritTournamentTypFlg > 0 && m_criterions.Contains("crittournamenttypflg"))
					whereClause += " AND (" + m_criterions["crittournamenttypflg"] + ") ";
				if (section.m_CritPlayerNmFlg > 0 && m_criterions.Contains("critplayernmflg"))
					whereClause += " AND (" + m_criterions["critplayernmflg"] + ") ";
				if (section.m_CritDateRangeFlg > 0 && m_criterions.Contains("critdaterangeflg"))
					whereClause += " AND (" + m_criterions["critdaterangeflg"] + ") ";
				if (section.m_CritLimitRangeFlg > 0 && m_criterions.Contains("critlimitrangeflg"))
					whereClause += " AND (" + m_criterions["critlimitrangeflg"] + ") ";
				if (section.m_CritTableTypeFlg > 0 && m_criterions.Contains("crittabletypeflg"))
					whereClause += " AND (" + m_criterions["crittabletypeflg"] + ") ";
				if (section.m_CritTounamendIDFlg > 0 && m_criterions.Contains("crittounamendidflg"))
					whereClause += " AND (" + m_criterions["crittounamendidflg"] + ") ";
				if (section.m_CritHandIDFlg > 0 && m_criterions.Contains("crithandidflg"))
					whereClause += " AND (" + m_criterions["crithandidflg"] + ") ";
			}

			string parsedSectionSQL = section.m_SectionSQL.Replace("{where}", whereClause);

			// compose order by section
			string orderByClause = "";
			string[] specs = section.m_OrderBy.Split(',');
			IEnumerator itr = specs.GetEnumerator();
			while (itr.MoveNext())
			{
				string spec = (string) itr.Current;
				string[] attributes = spec.Split('|');
				string field = attributes[0].Replace(" ", "");
				if (field == m_orderBy)
				{
					orderByClause = " order by " + field + " ";
					break;
				}
			}

			parsedSectionSQL = parsedSectionSQL.Replace("{orderby}", orderByClause);


			m_debugFeeder.FeedDebugInfo(parsedSectionSQL);

			return parsedSectionSQL;	
		}
	}
}

