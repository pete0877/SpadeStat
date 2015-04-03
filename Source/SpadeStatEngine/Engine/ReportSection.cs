using System;
using Npgsql;

namespace SpadeStat.Engine
{
	public class ReportSection : PokerObject
	{
		public int m_SectionID;
		public int m_ReportID;
		public string m_SectionNm;
		public string m_SectionSQL;
		public string m_SectionPage;
		public string m_OrderBy;
		public short m_CritHandComboFlg;
		public short m_CritPositionFlg;
		public short m_CritNumPlayerFlg;
		public short m_CritGameTypFlg;
		public short m_CritBetTypFlg;
		public short m_CritTournamentTypFlg;
		public short m_CritPlayerNmFlg;
		public short m_CritDateRangeFlg;
		public short m_CritLimitRangeFlg;
		public short m_CritTableTypeFlg;
		public short m_CritTounamendIDFlg;
		public short m_CritHandIDFlg;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		/// <param name="objID">Object ID</param>
		public ReportSection(NpgsqlTransaction dbTransaction, int objID) : base(dbTransaction, objID, "ReportSection", "SectionID") {}

		/// <summary>
		/// Constructor. Creates new object in DB (new ID is generated).
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		public ReportSection(NpgsqlTransaction dbTransaction, UUIDGenerator generator) : base(dbTransaction, "ReportSection", "SectionID", generator) {}

		/// <summary>
		/// Updates its locally cached ID.
		/// </summary>
		override public void OnCreate() 
		{
			m_SectionID = m_objID;
		}

		/// <summary>
		/// Pulls data from the read record into local data members. 
		/// This allows outside classes easy access to the columns.
		/// This method is called after data object is laoded.
		/// </summary>
		override public void OnLoad() 
		{
			m_SectionID = m_objID;
			m_ReportID = (int) this["ReportID"];
			m_SectionNm = (string) this["SectionNm"];
			m_SectionSQL = (string) this["SectionSQL"];
			m_SectionPage = (string) this["SectionPage"];
			m_OrderBy = (string) this["OrderBy"];
			m_CritHandComboFlg = (short) this["CritHandComboFlg"];
			m_CritPositionFlg = (short) this["CritPositionFlg"];
			m_CritNumPlayerFlg = (short) this["CritNumPlayerFlg"];
			m_CritGameTypFlg = (short) this["CritGameTypFlg"];
			m_CritBetTypFlg = (short) this["CritBetTypFlg"];
			m_CritTournamentTypFlg = (short) this["CritTournamentTypFlg"];
			m_CritPlayerNmFlg = (short) this["CritPlayerNmFlg"];
			m_CritDateRangeFlg = (short) this["CritDateRangeFlg"];
			m_CritLimitRangeFlg = (short) this["CritLimitRangeFlg"];
			m_CritTableTypeFlg = (short) this["CritTableTypeFlg"];
			m_CritTounamendIDFlg = (short) this["CritTounamendIDFlg"];
			m_CritHandIDFlg = (short) this["CritHandIDFlg"];
		}

		/// <summary>
		/// Pushes data from the read record into local data members. 
		/// This method is called before data object is saved.
		/// </summary>
		override public void OnSave() 
		{
			this["ReportID"] = m_ReportID;
			this["SectionNm"] = m_SectionNm;
			this["SectionSQL"] = m_SectionSQL;
			this["SectionPage"] = m_SectionPage;
			this["OrderBy"] = m_OrderBy;			
			this["CritHandComboFlg"] = m_CritHandComboFlg;
			this["CritPositionFlg"] = m_CritPositionFlg;
			this["CritNumPlayerFlg"] = m_CritNumPlayerFlg;
			this["CritGameTypFlg"] = m_CritGameTypFlg;
			this["CritBetTypFlg"] = m_CritBetTypFlg;
			this["CritTournamentTypFlg"] = m_CritTournamentTypFlg;
			this["CritPlayerNmFlg"] = m_CritPlayerNmFlg;
			this["CritDateRangeFlg"] = m_CritDateRangeFlg;
			this["CritLimitRangeFlg"] = m_CritLimitRangeFlg;
			this["CritTableTypeFlg"] = m_CritTableTypeFlg;
			this["CritTounamendIDFlg"] = m_CritTounamendIDFlg;
			this["CritHandIDFlg"] = m_CritHandIDFlg;
		}
	}
}
