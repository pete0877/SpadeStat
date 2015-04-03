using System;
using System.Data;
using System.Collections;
using Npgsql;

namespace SpadeStat.Engine
{
	/// <summary>
	/// Base data object class. Handles basic communication with database.
	/// </summary>
	public class PokerObject
	{
		/// <summary>
		/// Maximum number of columns that the object can store:
		/// </summary>
		static public int	COLUMN_COUNT_LIMIT = 100;

		/// <summary>
		/// Cached database connection.
		/// </summary>
		protected NpgsqlTransaction m_dbTransaction;

		/// <summary>
		/// Name of the table where the object is stored.
		/// </summary>
		protected string m_table;

		/// <summary>
		/// Name of the primary key column.
		/// </summary>
		protected string m_keyColumn;

		/// <summary>
		/// ID of the data object.
		/// </summary>
		protected int m_objID;

		/// <summary>
		/// Flag for if the object is being created from scratch (new record in db)
		/// </summary>
		protected bool m_isNew;

		/// <summary>
		/// List of values stored in the DB record.
		/// </summary>
		protected object[] m_values;

		/// <summary>
		/// Index of column positions (m_columnPositions[string column name] will give you position where it is stored)
		/// </summary>
		protected Hashtable m_columnPositions;

		/// <summary>
		/// Flag indicating if the object was initialized properly.
		/// </summary>
		protected bool m_initialized;

		/// <summary>
		/// Current length of m_values;
		/// </summary>
		protected int m_valuesLength;


		/// <summary>
		/// Base constructor
		/// </summary>
		/// <param name="dbTransaction">Database transaction</param>
		/// <param name="objID">Object ID (cannot be null, even if the record is going to be created in the DB)</param>
		/// <param name="table">Name of the table where object is stored</param>
		/// <param name="keyColumn">Name of the primary key column</param>
		public PokerObject(NpgsqlTransaction dbTransaction, int objID, string table, string keyColumn)
		{
			m_dbTransaction = dbTransaction;
			m_table = table;
			m_objID = objID;
			m_keyColumn = keyColumn;
			
			m_isNew = false; // This might change after Initialize() finds if the record exists or not.
		}

		/// <summary>
		/// Base constructor
		/// </summary>
		/// <param name="dbTransaction">Database transaction to be used to load and save the object</param>
		/// <param name="table">Name of the table where object is stored</param>
		/// <param name="keyColumn">Name of the primary key column</param>
		public PokerObject(NpgsqlTransaction dbTransaction, string table, string keyColumn, UUIDGenerator generator)
		{
			m_dbTransaction = dbTransaction;
			m_table = table;			
			m_keyColumn = keyColumn;

			// Generate ID:
			m_objID = generator.GenerateID();

			m_isNew = true;
		}

		virtual public void Initialize()
		{
			if (m_initialized)
				return;

			if (m_dbTransaction == null || 
				m_table == null || m_table == "" || 
				m_keyColumn == null || m_keyColumn == "")
					throw new Exception("Poker object cannot be initialized.");

			m_values = new object[COLUMN_COUNT_LIMIT];
			m_valuesLength = 0;
			m_columnPositions = new Hashtable();
			
			if (!m_isNew) // If we already know that we will have a new object, then there is no need to attempt to load it:
			{
				// Attempt to load the object from DB:				
				NpgsqlCommand command = m_dbTransaction.Connection.CreateCommand();
				command.Transaction = m_dbTransaction;
				command.CommandText = "select * from " + m_table + " where " + m_keyColumn + " = " + m_objID.ToString();
				NpgsqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
				
				if (reader.Read())
				{
					reader.GetValues(m_values);
					for (int count = 0; count < reader.FieldCount; count++) 
					{
						string columnName = reader.GetName(count);
						m_columnPositions[columnName.ToLower()] = count;
					}

					m_valuesLength = reader.FieldCount;
		
					m_isNew = false;
				}
				else
					m_isNew = true;

				reader.Close();
			}

			// Initialization completed
			m_initialized = true;

			// Pull data from the record into cached local data members. This method is always overwritten in derived classes.
			if (!m_isNew)
				OnLoad();
			else
				OnCreate();
		}

		/// <summary>
		/// Column value indexer.
		/// </summary>
		public object this[string columnName]
		{
			get
			{
				if (!m_initialized || columnName == null || columnName == "")
					return null;

				object o = m_columnPositions[columnName.ToLower()];
				if (o == null)
					return null;

				int columnPosition = (int) o;
				if ((columnPosition + 1 > m_values.Length))
					return null;

				object val = m_values[columnPosition];
				if (val.GetType().ToString() == "System.DBNull")
					return null;

				return val;
			}
			set
			{
				if (!m_initialized || columnName == null || columnName == "")
					return;

				object o = m_columnPositions[columnName.ToLower()];
				if (o == null)
				{
					// Value for this column does not exist yet
					int insertSpot = m_valuesLength;
					m_valuesLength++;

					if ((insertSpot + 1 > m_values.Length))
						return;

					m_values[insertSpot] = value;
					m_columnPositions[columnName.ToLower()] = insertSpot;
				
				}
				else
				{
					// Value for this column already exists so we overwrite.
					int columnPosition = (int) o;
					if ((columnPosition + 1 > m_values.Length))
						return;

					m_values[columnPosition] = value;
				}				
			}		
		}


		/// <summary>
		/// Saves the data object into the database
		/// </summary>
		public virtual void Save()
		{
			if (!m_initialized)
				return;

			OnSave();

			if (m_isNew)
			{
				// Will need 
				string columnsSQL = "";
				string valuesSQL = "";

				// Start off by putting in the primary key column and value:
				columnsSQL = m_keyColumn;
				valuesSQL = m_objID.ToString();

				IDictionaryEnumerator itr = m_columnPositions.GetEnumerator();
				while (itr.MoveNext())
				{
					string columnName = (string) itr.Key;
					if (columnName == m_keyColumn)
						continue;

					int columnPosition = (int) itr.Value;
					object o = m_values[columnPosition];
					columnsSQL += ", " + columnName;

					if (o == null)
						valuesSQL += ", null";	
					else
					{
						string valueString = o.ToString();
						valueString = valueString.Replace("'", "''");
						valuesSQL += ", '" + valueString + "'";				
					}
				}

				NpgsqlCommand command = m_dbTransaction.Connection.CreateCommand();
				command.Transaction = m_dbTransaction;
				command.CommandText = "insert into " + m_table + " (" + columnsSQL + ") values (" + valuesSQL + ")";
				command.ExecuteNonQuery();
			}
			else
			{
				string sqlStatement = "update " + m_table + " set ";
				
				IDictionaryEnumerator itr = m_columnPositions.GetEnumerator();
				bool firstColumn = true;
				while (itr.MoveNext())
				{
					string columnName = (string) itr.Key;
					int columnPosition = (int) itr.Value;
					object o = m_values[columnPosition];

					if (m_keyColumn != columnName)
					{
						if (firstColumn)
							sqlStatement += columnName;
						else
							sqlStatement += ", " + columnName;

						if (o == null || o.GetType().ToString() == "System.DBNull")
							sqlStatement += " = null";
						else
						{
							string valueString = o.ToString();
							valueString = valueString.Replace("'", "''");
							sqlStatement += " = '" + valueString + "'";
						}

						firstColumn = false;
					}
				}
				sqlStatement += " where " + m_keyColumn + " = " + m_objID.ToString();
				
				NpgsqlCommand command = m_dbTransaction.Connection.CreateCommand();
				command.Transaction = m_dbTransaction;
				command.CommandText = sqlStatement;
				command.ExecuteNonQuery();			
			}
		}

		/// <summary>
		/// On-load handler.
		/// </summary>
		public virtual void OnLoad() {}

		/// <summary>
		/// On-create handler.
		/// </summary>
		public virtual void OnCreate() {}

		/// <summary>
		/// On-save handler.
		/// </summary>
		public virtual void OnSave() {}
	}
}
