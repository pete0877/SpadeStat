using System;
using Npgsql;

namespace SpadeStat.Engine
{
	public class HandPlayerAction : PokerObject
	{
		public int m_ActionId;
		public int m_HandId;
		public int m_PlayerId;
		public string m_PlayerNm;
		public int m_HandPlayerId;
		public string m_BettingRoundTypCd;
		public int m_BetOrderNum;
		public string m_ActionTypCd;
		public decimal m_ActionAmt;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		/// <param name="objID">Object ID</param>
		public HandPlayerAction(NpgsqlTransaction dbTransaction, int objID) : base(dbTransaction, objID, "HandPlayerAction", "ActionId") {}

		/// <summary>
		/// Constructor. Creates new object in DB (new ID is generated).
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		public HandPlayerAction(NpgsqlTransaction dbTransaction, UUIDGenerator generator) : base(dbTransaction, "HandPlayerAction", "ActionId", generator) {}

		/// <summary>
		/// Updates its locally cached ID.
		/// </summary>
		override public void OnCreate() 
		{
			m_ActionId = m_objID;
		}

		/// <summary>
		/// Pulls data from the read record into local data members. 
		/// This allows outside classes easy access to the columns.
		/// This method is called after data object is laoded.
		/// </summary>
		override public void OnLoad() 
		{
			m_ActionId = m_objID;
			m_HandId = (int) this["HandId"];
			m_PlayerId = (int) this["PlayerId"];
			m_PlayerNm = (string) this["PlayerNm"];
			m_HandPlayerId = (int) this["HandPlayerId"];
			m_BettingRoundTypCd = (string) this["BettingRoundTypCd"];
			m_BetOrderNum = (int) this["BetOrderNum"];
			m_ActionTypCd = (string) this["ActionTypCd"];
			m_ActionAmt = decimal.Parse((string) this["ActionAmt"]);
		}

		/// <summary>
		/// Pushes data from the read record into local data members. 
		/// This method is called before data object is saved.
		/// </summary>
		override public void OnSave() 
		{
			this["HandId"] = m_HandId;
			this["PlayerId"] = m_PlayerId;
			this["PlayerNm"] = m_PlayerNm;
			this["HandPlayerId"] = m_HandPlayerId;
			this["BettingRoundTypCd"] = m_BettingRoundTypCd;
			this["BetOrderNum"] = m_BetOrderNum;
			this["ActionTypCd"] = m_ActionTypCd;
			this["ActionAmt"] = m_ActionAmt;
		}

		/// <summary>
		/// Removes all records from its table related to the given hand.
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		/// <param name="handID">Hand ID</param>
		public static void ResetHand(NpgsqlTransaction dbTransaction, int handID)
		{
			NpgsqlCommand command = dbTransaction.Connection.CreateCommand();
			command.Transaction = dbTransaction;
			command.CommandText = "delete from handplayeraction where handid = " + handID.ToString();
			command.ExecuteNonQuery();
		}
	}
}
