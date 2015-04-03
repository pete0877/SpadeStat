using System;
using Npgsql;

namespace SpadeStat.Engine
{
	public class Report : PokerObject
	{
		public int m_ReportID;
		public string m_ReportNm;
		public string m_ReportPage;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		/// <param name="objID">Object ID</param>
		public Report(NpgsqlTransaction dbTransaction, int objID) : base(dbTransaction, objID, "Report", "ReportID") {}


		/// <summary>
		/// Constructor. Creates new object in DB (new ID is generated).
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		public Report(NpgsqlTransaction dbTransaction, UUIDGenerator generator) : base(dbTransaction, "Report", "ReportID", generator) {}


		/// <summary>
		/// Updates its locally cached ID.
		/// </summary>
		override public void OnCreate() 
		{
			m_ReportID = m_objID;
		}


		/// <summary>
		/// Pulls data from the read record into local data members. 
		/// This allows outside classes easy access to the columns.
		/// This method is called after data object is laoded.
		/// </summary>
		override public void OnLoad() 
		{
			m_ReportID = m_objID;
			m_ReportNm = (string) this["ReportNm"];
			m_ReportPage = (string) this["ReportPage"];			
		}


		/// <summary>
		/// Pushes data from the read record into local data members. 
		/// This method is called before data object is saved.
		/// </summary>
		override public void OnSave() 
		{
			this["ReportNm"] = m_ReportNm;
			this["ReportPage"] = m_ReportPage;
		}

		/// <summary>
		/// Looks for report based on report name and returns the ID if found. 
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		/// <param name="reportNm">Replrt name</param>
		/// <returns>Report ID if the record is found. If it is not, returns -1</returns>
		public static int FindReportID(NpgsqlTransaction dbTransaction, string reportNm)
		{
			int result = -1;

			NpgsqlCommand command = dbTransaction.Connection.CreateCommand();
			command.Transaction = dbTransaction;
			command.CommandText = "select reportid from report where reportnm = '" + reportNm.Replace("'", "''") + "'";
			NpgsqlDataReader reader = command.ExecuteReader();

			if (reader.Read())
				result = reader.GetInt32(0);
			
			reader.Close();

			return result;
		}
	}
}
