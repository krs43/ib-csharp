/*
* Contract.java
*
*/
using System;
using KRS.ATS.IBNet;

namespace KRS.ATS.IBNet
{
    public class Contract : ICloneable
    {
        public String m_symbol;
        public String m_secType;
        public String m_expiry;
        public double m_strike;
        public String m_right;
        public String m_multiplier;
        public String m_exchange;
		
        public String m_currency;
        public String m_localSymbol;
        public String m_primaryExch; // pick a non-aggregate (ie not the SMART exchange) exchange that the contract trades on.  DO NOT SET TO SMART.
        public bool m_includeExpired; // can not be set to true for orders.
		
        // COMBOS
        public String m_comboLegsDescrip; // received in open order version 14 and up for all combos
        public System.Collections.ArrayList m_comboLegs = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
		
        // BOND values
        public String m_cusip;
        public String m_ratings;
        public String m_descAppend;
        public String m_bondType;
        public String m_couponType;
        public bool m_callable;
        public bool m_putable;
        public double m_coupon;
        public bool m_convertible;
        public String m_maturity;
        public String m_issueDate;
        public String m_nextOptionDate;
        public String m_nextOptionType;
        public bool m_nextOptionPartial;
        public String m_notes;
		
        public Contract()
        {
            m_strike = 0;
        }
		
        public virtual Object Clone()
        {
            Contract retval = (Contract) MemberwiseClone();
            retval.m_comboLegs = (System.Collections.ArrayList) retval.m_comboLegs.Clone();
            return retval;
        }
		
        public Contract(String p_symbol, String p_secType, String p_expiry, double p_strike, String p_right, String p_multiplier, String p_exchange, String p_currency, String p_localSymbol, System.Collections.ArrayList p_comboLegs, String p_primaryExch, bool p_includeExpired, String p_cusip, String p_ratings, String p_descAppend, String p_bondType, String p_couponType, bool p_callable, bool p_putable, double p_coupon, bool p_convertible, String p_maturity, String p_issueDate, String p_nextOptionDate, String p_nextOptionType, bool p_nextOptionPartial, String p_notes)
        {
            m_symbol = p_symbol;
            m_secType = p_secType;
            m_expiry = p_expiry;
            m_strike = p_strike;
            m_right = p_right;
            m_multiplier = p_multiplier;
            m_exchange = p_exchange;
            m_currency = p_currency;
            m_includeExpired = p_includeExpired;
            m_localSymbol = p_localSymbol;
            m_comboLegs = p_comboLegs;
            m_primaryExch = p_primaryExch;
            m_cusip = p_cusip;
            m_ratings = p_ratings;
            m_descAppend = p_descAppend;
            m_bondType = p_bondType;
            m_couponType = p_couponType;
            m_callable = p_callable;
            m_putable = p_putable;
            m_coupon = p_coupon;
            m_convertible = p_convertible;
            m_maturity = p_maturity;
            m_issueDate = p_issueDate;
            m_nextOptionDate = p_nextOptionDate;
            m_nextOptionType = p_nextOptionType;
            m_nextOptionPartial = p_nextOptionPartial;
            m_notes = p_notes;
        }
		
        public  override bool Equals(Object p_other)
        {
            if (p_other == null || !(p_other is Contract) || m_comboLegs.Count != ((Contract) p_other).m_comboLegs.Count)
            {
                return false;
            }
            else if (this == p_other)
            {
                return true;
            }
			
            Contract l_theOther = (Contract) p_other;
            bool l_bContractEquals;
			
            String l_thisSecType = m_secType ?? "";
            String l_otherSecType = l_theOther.m_secType ?? "";
			
            if (!l_thisSecType.Equals(l_otherSecType))
            {
                l_bContractEquals = false;
            }
            else
            {
                String l_thisSymbol = m_symbol ?? "";
                String l_thisExchange = m_exchange ?? "";
                String l_thisPrimaryExch = m_primaryExch ?? "";
                String l_thisCurrency = m_currency ?? "";
				
                String l_otherSymbol = l_theOther.m_symbol ?? "";
                String l_otherExchange = l_theOther.m_exchange ?? "";
                String l_otherPrimaryExch = l_theOther.m_primaryExch ?? "";
                String l_otherCurrency = l_theOther.m_currency ?? "";
				
                l_bContractEquals = l_thisSymbol.Equals(l_otherSymbol) && l_thisExchange.Equals(l_otherExchange) && l_thisPrimaryExch.Equals(l_otherPrimaryExch) && l_thisCurrency.Equals(l_otherCurrency);
				
                if (l_bContractEquals)
                {
                    if (l_thisSecType.Equals("BOND"))
                    {
                        l_bContractEquals = (m_putable == l_theOther.m_putable) && (m_callable == l_theOther.m_callable) && (m_convertible == l_theOther.m_convertible) && (m_coupon == l_theOther.m_coupon) && (m_nextOptionPartial == l_theOther.m_nextOptionPartial);
                        if (l_bContractEquals)
                        {
                            String l_thisCusip = m_cusip ?? "";
                            String l_thisRatings = m_ratings ?? "";
                            String l_thisDescAppend = m_descAppend ?? "";
                            String l_thisBondType = m_bondType ?? "";
                            String l_thisCouponType = m_couponType ?? "";
                            String l_thisMaturity = m_maturity ?? "";
                            String l_thisIssueDate = m_issueDate ?? "";
							
                            String l_otherCusip = l_theOther.m_cusip ?? "";
                            String l_otherRatings = l_theOther.m_ratings ?? "";
                            String l_otherDescAppend = l_theOther.m_descAppend ?? "";
                            String l_otherBondType = l_theOther.m_bondType ?? "";
                            String l_otherCouponType = l_theOther.m_couponType ?? "";
                            String l_otherMaturity = l_theOther.m_maturity ?? "";
                            String l_otherIssueDate = l_theOther.m_issueDate ?? "";
                            String l_otherOptionDate = l_theOther.m_nextOptionDate ?? "";
                            String l_otherOptionType = l_theOther.m_nextOptionType ?? "";
                            String l_otherNotes = l_theOther.m_notes ?? "";
                            l_bContractEquals = l_thisCusip.Equals(l_otherCusip) && l_thisRatings.Equals(l_otherRatings) && l_thisDescAppend.Equals(l_otherDescAppend) && l_thisBondType.Equals(l_otherBondType) && l_thisCouponType.Equals(l_otherCouponType) && l_thisMaturity.Equals(l_otherMaturity) && l_thisIssueDate.Equals(l_otherIssueDate) && l_otherOptionDate.Equals(l_otherOptionDate) && l_otherOptionType.Equals(l_otherOptionType) && l_otherNotes.Equals(l_otherNotes);
                        }
                    }
                    else
                    {
                        String l_thisExpiry = m_expiry ?? "";
                        String l_thisRight = m_right ?? "";
                        String l_thisMultiplier = m_multiplier ?? "";
                        String l_thisLocalSymbol = m_localSymbol ?? "";
						
                        String l_otherExpiry = l_theOther.m_expiry ?? "";
                        String l_otherRight = l_theOther.m_right ?? "";
                        String l_otherMultiplier = l_theOther.m_multiplier ?? "";
                        String l_otherLocalSymbol = l_theOther.m_localSymbol ?? "";
						
                        l_bContractEquals = l_thisExpiry.Equals(l_otherExpiry) && m_strike == l_theOther.m_strike && l_thisRight.Equals(l_otherRight) && l_thisMultiplier.Equals(l_otherMultiplier) && l_thisLocalSymbol.Equals(l_otherLocalSymbol);
                    }
                }
            }
			
            if (l_bContractEquals && m_comboLegs.Count > 0)
            {
                // compare the combo legs
                bool[] alreadyMatchedSecondLeg = new bool[m_comboLegs.Count];
                for (int ctr1 = 0; ctr1 < m_comboLegs.Count; ctr1++)
                {
                    ComboLeg l_thisComboLeg = (ComboLeg) m_comboLegs[ctr1];
                    bool l_bLegsEqual = false;
                    for (int ctr2 = 0; ctr2 < l_theOther.m_comboLegs.Count; ctr2++)
                    {
                        if (alreadyMatchedSecondLeg[ctr2])
                        {
                            continue;
                        }
                        if (l_thisComboLeg.Equals(l_theOther.m_comboLegs[ctr2]))
                        {
                            l_bLegsEqual = alreadyMatchedSecondLeg[ctr2] = true;
                            break;
                        }
                    }
                    if (!l_bLegsEqual)
                    {
                        // leg on first not matched by any previously unmatched leg on second
                        return false;
                    }
                }
            }
			
            return l_bContractEquals;
        }
    }
}