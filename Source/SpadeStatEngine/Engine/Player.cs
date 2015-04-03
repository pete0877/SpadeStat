using System;
using Npgsql;

namespace SpadeStat.Engine
{
	public class Player : PokerObject
	{
		public int m_PlayerID;
		public string m_PlayerNm;
		public string m_LocationTxt;
		public string m_PlayerNoteTxt;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		/// <param name="objID">Object ID</param>
		public Player(NpgsqlTransaction dbTransaction, int objID) : base(dbTransaction, objID, "Player", "PlayerID") {}

		/// <summary>
		/// Constructor. Creates new object in DB (new ID is generated).
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		public Player(NpgsqlTransaction dbTransaction, UUIDGenerator generator) : base(dbTransaction, "Player", "PlayerID", generator) {}

		/// <summary>
		/// Updates its locally cached ID.
		/// </summary>
		override public void OnCreate() 
		{
			m_PlayerID = m_objID;
		}

		/// <summary>
		/// Pulls data from the read record into local data members. 
		/// This allows outside classes easy access to the columns.
		/// This method is called after data object is laoded.
		/// </summary>
		override public void OnLoad() 
		{
			m_PlayerID = m_objID;
			m_PlayerNm = (string) this["PlayerNm"];
			m_LocationTxt = (string) this["LocationTxt"];
			m_PlayerNoteTxt = (string) this["PlayerNoteTxt"];
		}

		/// <summary>
		/// Pushes data from the read record into local data members. 
		/// This method is called before data object is saved.
		/// </summary>
		override public void OnSave() 
		{
			this["PlayerNm"] = m_PlayerNm;
			this["LocationTxt"] = m_LocationTxt;
			if (m_PlayerNoteTxt != null && m_PlayerNoteTxt.Length > 255)
				m_PlayerNoteTxt = m_PlayerNoteTxt.Substring(0, 255);

			this["PlayerNoteTxt"] = m_PlayerNoteTxt;
		}

		/// <summary>
		/// Looks for player based on username and return his/her player ID if found. 
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		/// <param name="playerNm">Player name (username)</param>
		/// <returns>Player ID if the record is found. If it is not, returns -1</returns>
		public static int FindPlayerID(NpgsqlTransaction dbTransaction, string playerNm)
		{
			int result = -1;

			NpgsqlCommand command = dbTransaction.Connection.CreateCommand();
			command.Transaction = dbTransaction;
			command.CommandText = "select playerid from player where playernm = '" + playerNm.Replace("'", "''") + "'";
			NpgsqlDataReader reader = command.ExecuteReader();

			if (reader.Read())
				result = reader.GetInt32(0);
			
			reader.Close();

			return result;
		}
	}
}
