using System;
using System.Data;
using Npgsql;
using System.Collections;

namespace SpadeStat.Engine
{
	/// <summary>
	/// Summary description for UUIDGenerator.
	/// </summary>
	public class UUIDGenerator
	{
		/// <summary>
		/// Next ID to be used
		/// </summary>
		protected int m_nextID;

		/// <summary>
		/// Flag indicateing if insert or update statement 
		/// </summary>
		protected bool m_new;

		/// <summary>
		/// Cached database connection.
		/// </summary>
		protected NpgsqlTransaction m_dbTransaction;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dbTransaction">Transaction</param>
		public UUIDGenerator(NpgsqlTransaction dbTransaction)
		{
			m_dbTransaction = dbTransaction;
			m_nextID = 0;

			LoadIDCounter();
		}


		/// <summary>
		/// Saves the current counter state to the DB
		/// </summary>
		public void Save()
		{
			if (m_new)
			{		
				NpgsqlCommand insertCommand = m_dbTransaction.Connection.CreateCommand();
				insertCommand.Transaction = m_dbTransaction;
				insertCommand.CommandText = "insert into nextid (nextid) values (" + m_nextID.ToString() + ")";
				insertCommand.ExecuteNonQuery();				
			}
			else
			{
				NpgsqlCommand command = m_dbTransaction.Connection.CreateCommand();
				command.Transaction = m_dbTransaction;
				command.CommandText = "update nextid set nextid = " + m_nextID.ToString();
				command.ExecuteNonQuery();
			}
		}


		/// <summary>
		/// Loads the ID from DB:
		/// </summary>
		protected void LoadIDCounter()
		{
			m_nextID = -1;
			m_new = true;

			NpgsqlCommand findCommand = m_dbTransaction.Connection.CreateCommand();
			findCommand.Transaction = m_dbTransaction;
			findCommand.CommandText = "select nextid from nextid";
			NpgsqlDataReader reader = findCommand.ExecuteReader();
			if (reader.Read())
			{
				m_nextID = reader.GetInt32(0);
				m_new = false;
			}

			reader.Close();
            
			if (m_nextID < 0)
				m_nextID = 1;
		}

	
		/// <summary>
		/// Returns new unique ID
		/// </summary>
		/// <returns>New ID</returns>
		public int GenerateID()
		{
			int result = m_nextID;
			m_nextID++;

			return result;
		}
	}
}
