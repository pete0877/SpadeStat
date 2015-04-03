using System;
using Npgsql;

namespace SpadeStat.Engine
{
	public class HandPlayerCombo : PokerObject
	{
		public int m_ComboId;
		public int m_HandPlayerId;
		public string m_HandComboTxt;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		/// <param name="objID">Object ID</param>
		public HandPlayerCombo(NpgsqlTransaction dbTransaction, int objID) : base(dbTransaction, objID, "HandPlayerCombo", "ComboId") {}

		/// <summary>
		/// Constructor. Creates new object in DB (new ID is generated).
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		public HandPlayerCombo(NpgsqlTransaction dbTransaction, UUIDGenerator generator) : base(dbTransaction, "HandPlayerCombo", "ComboId", generator) {}

		/// <summary>
		/// Updates its locally cached ID.
		/// </summary>
		override public void OnCreate() 
		{
			m_ComboId = m_objID;
		}

		/// <summary>
		/// Pulls data from the read record into local data members. 
		/// This allows outside classes easy access to the columns.
		/// This method is called after data object is laoded.
		/// </summary>
		override public void OnLoad() 
		{
			m_ComboId = m_objID;
			m_HandPlayerId = (int) this["HandPlayerId"];
			m_HandComboTxt = (string) this["HandComboTxt"];			
		}

		/// <summary>
		/// Pushes data from the read record into local data members. 
		/// This method is called before data object is saved.
		/// </summary>
		override public void OnSave() 
		{
			this["HandPlayerId"] = m_HandPlayerId;
			this["HandComboTxt"] = m_HandComboTxt;
		}

		/// <summary>
		/// Removes all records from its table related to the given hand player record.
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		/// <param name="handPlayerID">Hand player ID</param>
		public static void ResetCombo(NpgsqlTransaction dbTransaction, int handPlayerID)
		{
			NpgsqlCommand command = dbTransaction.Connection.CreateCommand();
			command.Transaction = dbTransaction;
			command.CommandText = "delete from HandPlayerCombo where handplayerid = " + handPlayerID.ToString();
			command.ExecuteNonQuery();
		}
	}
}
