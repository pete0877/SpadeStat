using System;
using System.Collections;

namespace SpadeStat.Engine
{
	/// <summary>
	/// Common set of functions used through out application
	/// </summary>
	public class Utils
	{
		/// <summary>
		/// Sets card flags on hand player record based on given hand card.
		/// </summary>
		/// <param name="hc">Hand card</param>
		/// <param name="hp">Hand player object</param>
		public static void SetCardFlags(HandCard hc, HandPlayer hp)
		{
			short newCard = 0;
			if (hc.m_CardSuitTxt == "c")
				newCard = 1;
			else if (hc.m_CardSuitTxt == "d")
				newCard = 2;
			else if (hc.m_CardSuitTxt == "h")
				newCard = 4;
			else if (hc.m_CardSuitTxt == "s")
				newCard = 8;

			if (hc.m_CardValTxt == "A")
				hp.m_AceFlg = (short) (hp.m_AceFlg | newCard);
			else if (hc.m_CardValTxt == "2")
				hp.m_DeuceFlg = (short) (hp.m_DeuceFlg | newCard);
			else if (hc.m_CardValTxt == "3")
				hp.m_TreyFlg = (short) (hp.m_TreyFlg | newCard);
			else if (hc.m_CardValTxt == "4")
				hp.m_FourFlg = (short) (hp.m_FourFlg | newCard);
			else if (hc.m_CardValTxt == "5")
				hp.m_FiveFlg = (short) (hp.m_FiveFlg | newCard);
			else if (hc.m_CardValTxt == "6")
				hp.m_SixFlg = (short) (hp.m_SixFlg | newCard);
			else if (hc.m_CardValTxt == "7")
				hp.m_SevenFlg = (short) (hp.m_SevenFlg | newCard);
			else if (hc.m_CardValTxt == "8")
				hp.m_EightFlg = (short) (hp.m_EightFlg | newCard);
			else if (hc.m_CardValTxt == "9")
				hp.m_NineFlg = (short) (hp.m_NineFlg | newCard);
			else if (hc.m_CardValTxt == "T")
				hp.m_TenFlg = (short) (hp.m_TenFlg | newCard);
			else if (hc.m_CardValTxt == "J")
				hp.m_JackFlg = (short) (hp.m_JackFlg | newCard);
			else if (hc.m_CardValTxt == "Q")
				hp.m_QueenFlg = (short) (hp.m_QueenFlg | newCard);
			else if (hc.m_CardValTxt == "K")
				hp.m_KingFlg = (short) (hp.m_KingFlg | newCard);
		}


		/// <summary>
		/// Sets card flags on hand record based on given hand card.
		/// </summary>
		/// <param name="hc">Hand card</param>
		/// <param name="h">Hand</param>
		public static void SetCardFlags(HandCard hc, TournamentHand h)
		{
			short newCard = 0;
			if (hc.m_CardSuitTxt == "c")
				newCard = 1;
			else if (hc.m_CardSuitTxt == "d")
				newCard = 2;
			else if (hc.m_CardSuitTxt == "h")
				newCard = 4;
			else if (hc.m_CardSuitTxt == "s")
				newCard = 8;

			if (hc.m_CardValTxt == "A")
				h.m_AceFlg = (short) (h.m_AceFlg | newCard);
			else if (hc.m_CardValTxt == "2")
				h.m_DeuceFlg = (short) (h.m_DeuceFlg | newCard);
			else if (hc.m_CardValTxt == "3")
				h.m_TreyFlg = (short) (h.m_TreyFlg | newCard);
			else if (hc.m_CardValTxt == "4")
				h.m_FourFlg = (short) (h.m_FourFlg | newCard);
			else if (hc.m_CardValTxt == "5")
				h.m_FiveFlg = (short) (h.m_FiveFlg | newCard);
			else if (hc.m_CardValTxt == "6")
				h.m_SixFlg = (short) (h.m_SixFlg | newCard);
			else if (hc.m_CardValTxt == "7")
				h.m_SevenFlg = (short) (h.m_SevenFlg | newCard);
			else if (hc.m_CardValTxt == "8")
				h.m_EightFlg = (short) (h.m_EightFlg | newCard);
			else if (hc.m_CardValTxt == "9")
				h.m_NineFlg = (short) (h.m_NineFlg | newCard);
			else if (hc.m_CardValTxt == "T")
				h.m_TenFlg = (short) (h.m_TenFlg | newCard);
			else if (hc.m_CardValTxt == "J")
				h.m_JackFlg = (short) (h.m_JackFlg | newCard);
			else if (hc.m_CardValTxt == "Q")
				h.m_QueenFlg = (short) (h.m_QueenFlg | newCard);
			else if (hc.m_CardValTxt == "K")
				h.m_KingFlg = (short) (h.m_KingFlg | newCard);	
		}
	}
}
