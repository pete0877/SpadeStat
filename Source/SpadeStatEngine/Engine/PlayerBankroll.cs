using System;
using Npgsql;

namespace SpadeStat.Engine
{
	public class PlayerBankroll : PokerObject
	{
		public int m_BankrollId;		
		public int m_PlayerId;
		public string m_PlayerNm;
		public DateTime m_StartDt;
		public DateTime m_EndDt;
		public decimal m_NetChangeAmt;
		public decimal m_NetTimeAmt;
		public string m_TypeCd;
		public string m_SessionNoteTxt;		
		public int	m_HandCt;
		public string m_GameTypCd;
		public string m_BetTypCd;
		public decimal m_BBAmt;
		public string m_SiteCd;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		/// <param name="objID">Object ID</param>
		public PlayerBankroll(NpgsqlTransaction dbTransaction, int objID) : base(dbTransaction, objID, "PlayerBankroll", "BankrollId") {}

		/// <summary>
		/// Constructor. Creates new object in DB (new ID is generated).
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		public PlayerBankroll(NpgsqlTransaction dbTransaction, UUIDGenerator generator) : base(dbTransaction, "PlayerBankroll", "BankRollID", generator) {}

		/// <summary>
		/// Updates its locally cached ID.
		/// </summary>
		override public void OnCreate() 
		{
			m_BankrollId = m_objID;
		}

		/// <summary>
		/// Pulls data from the read record into local data members. 
		/// This allows outside classes easy access to the columns.
		/// This method is called after data object is laoded.
		/// </summary>
		override public void OnLoad() 
		{
			m_BankrollId = m_objID;		
			m_PlayerId = (int) this["PlayerId"];
			m_PlayerNm = (string) this["PlayerNm"];
			if (this["StartDt"] != null)
				m_StartDt = (DateTime) this["StartDt"];
			if (this["EndDt"] != null)
				m_EndDt = (DateTime) this["EndDt"];

			m_NetChangeAmt = (decimal) this["NetChangeAmt"];
			m_NetTimeAmt = (decimal) this["NetTimeAmt"];
			m_TypeCd = (string) this["TypeCd"];
			m_SessionNoteTxt = (string) this["SessionNoteTxt"];
			m_HandCt = (int) this["HandCt"];
			m_GameTypCd = (string) this["GameTypCd"];
			m_BetTypCd = (string) this["BetTypCd"];
			m_BBAmt = (decimal) this["BBAmt"];
			m_SiteCd = (string) this["SiteCd"];
		}

		/// <summary>
		/// Pushes data from the read record into local data members. 
		/// This method is called before data object is saved.
		/// </summary>
		override public void OnSave() 
		{
			this["PlayerId"] = m_PlayerId;
			this["PlayerNm"] = m_PlayerNm;
			this["StartDt"] = m_StartDt;
			this["EndDt"] = m_EndDt;
			this["NetChangeAmt"] = m_NetChangeAmt;
			this["NetTimeAmt"] = m_NetTimeAmt;
			this["TypeCd"] = m_TypeCd;
			this["SessionNoteTxt"] = m_SessionNoteTxt;
			this["HandCt"] = m_HandCt;
			this["GameTypCd"] = m_GameTypCd;
			this["BetTypCd"] = m_BetTypCd;
			this["BBAmt"] = m_BBAmt;
			this["SiteCd"] = m_SiteCd;
		}
	}
}
