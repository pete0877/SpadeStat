using System;
using Npgsql;

namespace SpadeStat.Engine
{
	public class TournamentHand : PokerObject
	{
		public int m_HandId;
		public int m_TournamentId;
		public string m_GameTypCd;
		public string m_BetTypCd;
		public decimal m_BigBlindAmt;
		public decimal m_SmallBlindAmt;
		public decimal m_BigBetAmt;
		public decimal m_SmallBetAmt;
		public decimal m_AnteAmt;
		public int m_ButtonSeatNum;
		public DateTime m_StartDt;
		public string m_TableId;
		public decimal m_PotAmt;
		public decimal m_RakeAmt;
		public int m_TotalPlayerNum;
		public short m_AceFlg;
		public short m_DeuceFlg;
		public short m_TreyFlg;
		public short m_FourFlg;
		public short m_FiveFlg;
		public short m_SixFlg;
		public short m_SevenFlg;
		public short m_EightFlg;
		public short m_NineFlg;
		public short m_TenFlg;
		public short m_JackFlg;
		public short m_QueenFlg;
		public short m_KingFlg;
		public short m_StealAttemptFlg;
		public string m_FinalRoundCd;
		public string m_NoteTxt;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		/// <param name="objID">Object ID</param>
		public TournamentHand(NpgsqlTransaction dbTransaction, int objID) : base(dbTransaction, objID, "TournamentHand", "HandId") {}

		/// <summary>
		/// Constructor. Creates new object in DB (new ID is generated).
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		public TournamentHand(NpgsqlTransaction dbTransaction, UUIDGenerator generator) : base(dbTransaction, "TournamentHand", "HandId", generator) {}

		/// <summary>
		/// Updates its locally cached ID.
		/// </summary>
		override public void OnCreate() 
		{
			m_HandId = m_objID;
		}

		/// <summary>
		/// Pulls data from the read record into local data members. 
		/// This allows outside classes easy access to the columns.
		/// This method is called after data object is laoded.
		/// </summary>
		override public void OnLoad() 
		{
			m_HandId = m_objID;
			m_TournamentId = (int) this["TournamentId"];
			m_GameTypCd = (string) this["GameTypCd"];
			m_BigBlindAmt = (decimal) this["BigBlindAmt"];
			m_SmallBlindAmt = (decimal) this["SmallBlindAmt"];
			m_BigBetAmt = (decimal) this["BigBetAmt"];
			m_SmallBetAmt = (decimal) this["SmallBetAmt"];
			m_AnteAmt = (decimal) this["AnteAmt"];
			m_ButtonSeatNum = (int) this["ButtonSeatNum"];
			if (this["StartDt"] != null)
				m_StartDt = (DateTime) this["StartDt"];
			m_TableId = (string) this["TableId"];
			m_PotAmt = (decimal) this["PotAmt"];
			m_RakeAmt = (decimal) this["RakeAmt"];
			m_BetTypCd = (string) this["BetTypCd"];
			m_TotalPlayerNum = (int) this["TotalPlayerNum"];
			m_AceFlg = (short) this["AceFlg"];
			m_DeuceFlg = (short) this["DeuceFlg"];
			m_TreyFlg = (short) this["TreyFlg"];
			m_FourFlg = (short) this["FourFlg"];
			m_FiveFlg = (short) this["FiveFlg"];
			m_SixFlg = (short) this["SixFlg"];
			m_SevenFlg = (short) this["SevenFlg"];
			m_EightFlg = (short) this["EightFlg"];
			m_NineFlg = (short) this["NineFlg"];
			m_TenFlg = (short) this["TenFlg"];
			m_JackFlg = (short) this["JackFlg"];
			m_QueenFlg = (short) this["QueenFlg"];
			m_KingFlg = (short) this["KingFlg"];
			m_StealAttemptFlg = (short) this["StealAttemptFlg"];
			m_FinalRoundCd = (string) this["FinalRoundCd"];
			m_NoteTxt = (string) this["NoteTxt"];			
		}

		/// <summary>
		/// Pushes data from the read record into local data members. 
		/// This method is called before data object is saved.
		/// </summary>
		override public void OnSave() 
		{
			this["TournamentId"] = m_TournamentId;
			this["GameTypCd"] = m_GameTypCd;
			this["BigBlindAmt"] = m_BigBlindAmt;
			this["SmallBlindAmt"] = m_SmallBlindAmt;
			this["BigBetAmt"] = m_BigBetAmt;
			this["SmallBetAmt"] = m_SmallBetAmt;
			this["AnteAmt"] = m_AnteAmt;
			this["ButtonSeatNum"] = m_ButtonSeatNum;
			if (m_StartDt.Ticks != 0)
				this["StartDt"] = m_StartDt;
			this["TableId"] = m_TableId;
			this["PotAmt"] = m_PotAmt;
			this["RakeAmt"] = m_RakeAmt;
			this["BetTypCd"] = m_BetTypCd;	
			this["TotalPlayerNum"] = m_TotalPlayerNum;
			this["AceFlg"] = m_AceFlg;
			this["DeuceFlg"] = m_DeuceFlg;
			this["TreyFlg"] = m_TreyFlg;
			this["FourFlg"] = m_FourFlg;
			this["FiveFlg"] = m_FiveFlg;
			this["SixFlg"] = m_SixFlg;
			this["SevenFlg"] = m_SevenFlg;
			this["EightFlg"] = m_EightFlg;
			this["NineFlg"] = m_NineFlg;
			this["TenFlg"] = m_TenFlg;
			this["JackFlg"] = m_JackFlg;
			this["QueenFlg"] = m_QueenFlg;
			this["KingFlg"] = m_KingFlg;			
			this["StealAttemptFlg"] = m_StealAttemptFlg;
			this["FinalRoundCd"] = m_FinalRoundCd;
			this["NoteTxt"] = m_NoteTxt;

		}

	}
}
