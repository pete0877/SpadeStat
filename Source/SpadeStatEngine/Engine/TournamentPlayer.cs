using System;
using Npgsql;

namespace SpadeStat.Engine
{
	public class TournamentPlayer : PokerObject
	{
		public int m_TournamentPlayerID;
		public int m_TournamentID;
		public int m_PlayerID;
		public string m_PlayerNm;
		public int m_PlacementNum;
		public decimal m_WinningAmt;
		public short m_SatelliteSeatFlg;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		/// <param name="objID">Object ID</param>
		public TournamentPlayer(NpgsqlTransaction dbTransaction, int objID) : base(dbTransaction, objID, "TournamentPlayer", "TournamentPlayerID") {}

		/// <summary>
		/// Constructor. Creates new object in DB (new ID is generated).
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		public TournamentPlayer(NpgsqlTransaction dbTransaction, UUIDGenerator generator) : base(dbTransaction, "TournamentPlayer", "TournamentPlayerID", generator) {}

		/// <summary>
		/// Updates its locally cached ID.
		/// </summary>
		override public void OnCreate() 
		{
			m_TournamentPlayerID = m_objID;
		}

		/// <summary>
		/// Pulls data from the read record into local data members. 
		/// This allows outside classes easy access to the columns.
		/// This method is called after data object is laoded.
		/// </summary>
		override public void OnLoad() 
		{
			m_TournamentPlayerID = m_objID;
			m_TournamentID = (int) this["TournamentId"];
			m_PlayerID = (int) this["PlayerId"];
			m_PlayerNm = (string) this["PlayerNm"];
			m_PlacementNum = (int) this["PlacementNum"];
			m_WinningAmt = (decimal) this["WinningAmt"];
			m_SatelliteSeatFlg = (short) this["SatelliteSeatFlg"];
		}

		/// <summary>
		/// Pushes data from the read record into local data members. 
		/// This method is called before data object is saved.
		/// </summary>
		override public void OnSave() 
		{
			this["TournamentId"] = m_TournamentID;
			this["PlayerId"] = m_PlayerID;
			this["PlayerNm"] = m_PlayerNm;
			this["PlacementNum"] = m_PlacementNum;
			this["WinningAmt"] = m_WinningAmt;
			this["SatelliteSeatFlg"] = m_SatelliteSeatFlg;
		}

		/// <summary>
		/// Removes all participation records for given tournament.
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		/// <param name="tournamentID">Tournament ID</param>
		public static void ResetTournament(NpgsqlTransaction dbTransaction, int tournamentID)
		{
			NpgsqlCommand command = dbTransaction.Connection.CreateCommand();
			command.Transaction = dbTransaction;
			command.CommandText = "delete from tournamentplayer where tournamentid = " + tournamentID.ToString();
			command.ExecuteNonQuery();
		}

		/// <summary>
		/// Looks for tournament-player record based on player ID and tourmanent ID.
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		/// <param name="playerID">Player ID</param>
		/// <returns>Player ID if the record is found. If it is not, returns -1</returns>
		public static int FindTournamentPlayerID(NpgsqlTransaction dbTransaction, int playerID, int tournamentID)
		{
			int result = -1;

			NpgsqlCommand command = dbTransaction.Connection.CreateCommand();
			command.Transaction = dbTransaction;
			command.CommandText = "select tournamentplayerid from tournamentplayer where playerid = " + playerID.ToString() + " and tournamentid = " + tournamentID.ToString();
			NpgsqlDataReader reader = command.ExecuteReader();

			if (reader.Read())
				result = reader.GetInt32(0);
			
			reader.Close();

			return result;
		}
	}
}
