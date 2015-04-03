using System;
using Npgsql;

namespace SpadeStat.Engine
{
	public class HandPlayer : PokerObject
	{
		public int m_HandPlayerId;
		public int m_PlayerId;
		public string m_PlayerNm;
		public int m_HandId;
		public int m_TournamentId;
		public int m_PlayerSeatNum;
		public Decimal m_HandStartChipAmt;
		public string m_GameTypCd;
		public string m_BetTypCd;
		public short m_FirstInFlg;
		public short m_PreFlopRaiseFlg;
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
		public string m_SuitedStsCd;
		public int m_PositionNum;
		public decimal m_MoneyInAmt;
		public decimal m_MoneyWonAmt;
		public short m_VoluntaryMoneyFlg;
		public string m_BlindStsCd;
		public string m_FoldCd;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dbTransaction">Database transaction.</param>
		/// <param name="objID">Object ID</param>
		public HandPlayer(NpgsqlTransaction dbTransaction, int objID) : base(dbTransaction, objID, "HandPlayer", "HandPlayerId") {}

		/// <summary>
		/// Constructor. Creates new object in DB (new ID is generated).
		/// </summary>
		/// <param name="dbTransaction">Database transactions.</param>
		public HandPlayer(NpgsqlTransaction dbTransaction, UUIDGenerator generator) : base(dbTransaction, "HandPlayer", "HandPlayerID", generator) {}

		/// <summary>
		/// Updates its locally cached ID.
		/// </summary>
		override public void OnCreate() 
		{
			m_HandPlayerId = m_objID;
		}

		/// <summary>
		/// Pulls data from the read record into local data members. 
		/// This allows outside classes easy access to the columns.
		/// This method is called after data object is laoded.
		/// </summary>
		override public void OnLoad() 
		{
			m_HandPlayerId = m_objID;
			m_PlayerId = (int) this["PlayerId"];
			m_PlayerNm = (string) this["PlayerNm"];
			m_HandId = (int) this["HandId"];
			m_TournamentId = (int) this["TournamentId"];
			m_PlayerSeatNum = (int) this["PlayerSeatNum"];
			m_HandStartChipAmt = Decimal.Parse((string) this["HandStartChipAmt"]);
			m_GameTypCd = (string) this["GameTypCd"];
			m_BetTypCd = (string) this["BetTypCd"];
			m_FirstInFlg = (short) this["FirstInFlg"];
			m_PreFlopRaiseFlg = (short) this["PreFlopRaiseFlg"];
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
			m_SuitedStsCd = (string) this["SuitedStsCd"];
			m_PositionNum = (int) this["PositionNum"];
			m_MoneyInAmt = (decimal) this["MoneyInAmt"];
			m_MoneyWonAmt = (decimal) this["MoneyWonAmt"];
			m_VoluntaryMoneyFlg = (short) this["VoluntaryMoneyFlg"];
			m_BlindStsCd = (string) this["BlindStsCd"];
			m_FoldCd = (string) this["FoldCd"];
		}

		/// <summary>
		/// Pushes data from the read record into local data members. 
		/// This method is called before data object is saved.
		/// </summary>
		override public void OnSave() 
		{
			this["PlayerId"] = m_PlayerId;
			this["PlayerNm"] = m_PlayerNm;
			this["HandId"] = m_HandId;
			this["TournamentId"] = m_TournamentId;
			this["PlayerSeatNum"] = m_PlayerSeatNum;
			this["HandStartChipAmt"] = m_HandStartChipAmt;
			this["GameTypCd"] = m_GameTypCd;
			this["BetTypCd"] = m_BetTypCd;		
			this["FirstInFlg"] = m_FirstInFlg;
			this["PreFlopRaiseFlg"] = m_PreFlopRaiseFlg;
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
			this["SuitedStsCd"] = m_SuitedStsCd;
			this["PositionNum"] = m_PositionNum;
			this["MoneyInAmt"] = m_MoneyInAmt;
			this["MoneyWonAmt"] = m_MoneyWonAmt;
			this["VoluntaryMoneyFlg"] = m_VoluntaryMoneyFlg;
			this["BlindStsCd"] = m_BlindStsCd;
			this["FoldCd"] = m_FoldCd;
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
			command.CommandText = "delete from handplayer where handid = " + handID.ToString();
			command.ExecuteNonQuery();
		}


		/// <summary>
		/// Calculates SuitedStsCd based on m_AceFlg, m_KingFlg, etc.
		/// </summary>
		public void CalculateSuitedStsCd()
		{
			int mask = 1;
			int matchedSuits = 0;
			bool haveAnyCards = false;
			for (int n = 0; n < 4; n++)
			{
				int sum = 0;
				sum += m_AceFlg & mask;
				sum += m_DeuceFlg & mask;
				sum += m_TreyFlg & mask;
				sum += m_FourFlg & mask;
				sum += m_FiveFlg & mask;
				sum += m_SixFlg & mask;
				sum += m_SevenFlg & mask;
				sum += m_EightFlg & mask;
				sum += m_NineFlg & mask;
				sum += m_TenFlg & mask;
				sum += m_JackFlg & mask;
				sum += m_QueenFlg & mask;
				sum += m_KingFlg & mask;

				if (sum > 0 && sum != mask) // There were at least two cards of the same suit
					matchedSuits++;

				if (sum > 0)
					haveAnyCards = true;

				mask = mask << 1;
			}

			if (matchedSuits == 0)
			{
				if (haveAnyCards)
					m_SuitedStsCd = "OS";
			}
			else if (matchedSuits == 1)
				m_SuitedStsCd = "SS";
			else
				m_SuitedStsCd = "DS";
		}
	}
}
