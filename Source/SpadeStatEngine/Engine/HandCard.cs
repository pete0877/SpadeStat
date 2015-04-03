using System;
using Npgsql;

namespace SpadeStat.Engine
{
	public class HandCard : PokerObject, IComparable
	{
		public int m_CardId;
		public int m_HandPlayerId;
		public int m_PlayerId;
		public string m_PlayerNm;
		public int m_HandId;
		public string m_CardValTxt;
		public string m_CardSuitTxt;
		public string m_TypCd;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		/// <param name="objID">Object ID</param>
		public HandCard(NpgsqlTransaction dbTransaction, int objID) : base(dbTransaction, objID, "HandCard", "CardId") {}

		/// <summary>
		/// Constructor. Creates new object in DB (new ID is generated).
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		public HandCard(NpgsqlTransaction dbTransaction, UUIDGenerator generator) : base(dbTransaction, "HandCard", "CardId", generator) {}

		/// <summary>
		/// Updates its locally cached ID.
		/// </summary>
		override public void OnCreate() 
		{
			m_CardId = m_objID;
		}

		/// <summary>
		/// Pulls data from the read record into local data members. 
		/// This allows outside classes easy access to the columns.
		/// This method is called after data object is laoded.
		/// </summary>
		override public void OnLoad() 
		{
			m_CardId = m_objID;
			m_HandPlayerId = (int) this["HandPlayerId"];
			m_PlayerId = (int) this["PlayerId"];
			m_PlayerNm = (string) this["PlayerNm"];
			m_HandId = (int) this["HandId"];
			m_CardValTxt = (string) this["CardValTxt"];
			m_CardSuitTxt = (string) this["CardSuitTxt"];
			m_TypCd = (string) this["TypCd"];
		}

		/// <summary>
		/// Pushes data from the read record into local data members. 
		/// This method is called before data object is saved.
		/// </summary>
		override public void OnSave() 
		{
			this["HandPlayerId"] = m_HandPlayerId;
			this["PlayerId"] = m_PlayerId;
			this["PlayerNm"] = m_PlayerNm;
			this["HandId"] = m_HandId;
			this["CardValTxt"] = m_CardValTxt;
			this["CardSuitTxt"] = m_CardSuitTxt;
			this["TypCd"] = m_TypCd;
		}

		/// <summary>
		/// Returns relative value of self within the deck.
		/// values are as follows:
		///	  A - 1
		///   2 - 2
		///   3 - 3
		///   .....
		///   10 - 10
		///   J - 11
		///   Q - 12
		///   K - 13
		/// If card is not set, value will be zero.
		/// </summary>
		/// <returns>Value of self</returns>
		public int GetRelativeValue()
		{
			switch (m_CardValTxt)
			{
				case "A": return 1;
				case "2": return 2;
				case "3": return 3;
				case "4": return 4;
				case "5": return 5;
				case "6": return 6;
				case "7": return 7;
				case "8": return 8;
				case "9": return 9;
				case "T": return 10;
				case "J": return 11;
				case "Q": return 12;
				case "K": return 13;				
			}			
				
			return 0;
		}
		

		/// <summary>
		/// Compares the current instance with another object of the same type.
		/// See IComparable.CompareTo()
		/// </summary>
		public int CompareTo(object obj)
		{
			HandCard otherCard = (HandCard) obj;
			int otherCardValue = otherCard.GetRelativeValue();
			int myCardValue = GetRelativeValue();

			if (myCardValue == otherCardValue)
				return 0;
			else if (myCardValue < otherCardValue)
				return -1;
			else
				return 1;
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
			command.CommandText = "delete from handcard where handid = " + handID.ToString();
			command.ExecuteNonQuery();
		}
	}
}
