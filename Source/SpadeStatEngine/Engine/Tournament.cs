using System;
using Npgsql;

namespace SpadeStat.Engine
{
	public class Tournament : PokerObject
	{
		public int m_TournamentID;
		public DateTime m_StartDt;
		public DateTime m_EndDt;
		public string m_TournamentTypCd;
		public string m_GameTypCd;
		public decimal m_BuyInAmt;
		public decimal m_FeeAmt;
		public short m_RebuyFlg;
		public string m_RebuySpecTxt;
		public string m_BetTypCd;
		public int m_TotalPlayerNum;
		public decimal m_TotalPrizeFundAmt;
		public string m_NoteTxt;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		/// <param name="objID">Object ID</param>
		public Tournament(NpgsqlTransaction dbTransaction, int objID) : base(dbTransaction, objID, "Tournament", "TournamentID") {}

		/// <summary>
		/// Constructor. Creates new object in DB (new ID is generated).
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		public Tournament(NpgsqlTransaction dbTransaction, UUIDGenerator generator) : base(dbTransaction, "Tournament", "TournamentID", generator) {}

		/// <summary>
		/// Updates its locally cached ID.
		/// </summary>
		override public void OnCreate() 
		{
			m_TournamentID = m_objID;
		}

		/// <summary>
		/// Pulls data from the read record into local data members. 
		/// This allows outside classes easy access to the columns.
		/// This method is called after data object is laoded.
		/// </summary>
		override public void OnLoad() 
		{
			m_TournamentID = m_objID;
			if (this["StartDt"] != null)
				m_StartDt = (DateTime) this["StartDt"];
			if (this["EndDt"] != null)
				m_EndDt = (DateTime) this["EndDt"];
			m_TournamentTypCd = (string) this["TournamentTypCd"];
			m_GameTypCd = (string) this["GameTypCd"];
			m_BuyInAmt = (decimal) this["BuyInAmt"];
			m_FeeAmt = (decimal) this["FeeAmt"];
			m_RebuyFlg = (short) this["RebuyFlg"];
			m_RebuySpecTxt = (string) this["RebuySpecTxt"];
			m_BetTypCd = (string) this["BetTypCd"];
			m_TotalPlayerNum = (int) this["TotalPlayerNum"];
			m_TotalPrizeFundAmt = (decimal) this["TotalPrizeFundAmt"];		
			m_NoteTxt = (string) this["NoteTxt"];
		}

		/// <summary>
		/// Pushes data from the read record into local data members. 
		/// This method is called before data object is saved.
		/// </summary>
		override public void OnSave() 
		{
			if (m_StartDt.Ticks != 0)
				this["StartDt"] = m_StartDt;
			if (m_EndDt.Ticks != 0)
				this["EndDt"] = m_EndDt;
			this["TournamentTypCd"] = m_TournamentTypCd;
			this["GameTypCd"] = m_GameTypCd;
			this["BuyInAmt"] = m_BuyInAmt;
			this["FeeAmt"] = m_FeeAmt;
			this["RebuyFlg"] = m_RebuyFlg;
			this["RebuySpecTxt"] = m_RebuySpecTxt;
			this["BetTypCd"] = m_BetTypCd;
			this["TotalPlayerNum"] = m_TotalPlayerNum;
			this["TotalPrizeFundAmt"] = m_TotalPrizeFundAmt;			
			this["NoteTxt"] = m_NoteTxt;
		}
	}
}
