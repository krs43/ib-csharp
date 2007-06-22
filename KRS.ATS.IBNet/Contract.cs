/*
* Contract.java
*
*/
using System;
using KRS.ATS.IBNet;
using System.Collections;

namespace KRS.ATS.IBNet
{
    public class Contract : ICloneable
    {
        private String symbol;
        private String secType;
        private String expiry;
        private double strike;
        private String right;
        private String multiplier;
        private String exchange;

        private String currency;
        private String localSymbol;
        private String primaryExch; // pick a non-aggregate (ie not the SMART exchange) exchange that the contract trades on.  DO NOT SET TO SMART.
        private bool includeExpired; // can not be set to true for orders.
		
        // COMBOS
        private String comboLegsDescrip; // received in open order version 14 and up for all combos
        private ArrayList comboLegs = ArrayList.Synchronized(new ArrayList(10));
        private object comboLegsSync = new object();
		
        // BOND values
        private String cusip;
        private String ratings;
        private String descAppend;
        private String bondType;
        private String couponType;
        private bool callable;
        private bool putable;
        private double coupon;
        private bool convertible;
        private String maturity;
        private String issueDate;
        private String nextOptionDate;
        private String nextOptionType;
        private bool nextOptionPartial;
        private String notes;
		
        public Contract()
        {
            strike = 0;
        }

        public string Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        public string SecType
        {
            get { return secType; }
            set { secType = value; }
        }

        public string Expiry
        {
            get { return expiry; }
            set { expiry = value; }
        }

        public double Strike
        {
            get { return strike; }
            set { strike = value; }
        }

        public string Right
        {
            get { return right; }
            set { right = value; }
        }

        public string Multiplier
        {
            get { return multiplier; }
            set { multiplier = value; }
        }

        public string Exchange
        {
            get { return exchange; }
            set { exchange = value; }
        }

        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        public string LocalSymbol
        {
            get { return localSymbol; }
            set { localSymbol = value; }
        }

        public string PrimaryExch
        {
            get { return primaryExch; }
            set { primaryExch = value; }
        }

        public bool IncludeExpired
        {
            get { return includeExpired; }
            set { includeExpired = value; }
        }

        public string ComboLegsDescrip
        {
            get { return comboLegsDescrip; }
            set { comboLegsDescrip = value; }
        }

        public ArrayList ComboLegs
        {
            get { return comboLegs; }
            set { comboLegs = value; }
        }

        public string Cusip
        {
            get { return cusip; }
            set { cusip = value; }
        }

        public string Ratings
        {
            get { return ratings; }
            set { ratings = value; }
        }

        public string DescAppend
        {
            get { return descAppend; }
            set { descAppend = value; }
        }

        public string BondType
        {
            get { return bondType; }
            set { bondType = value; }
        }

        public string CouponType
        {
            get { return couponType; }
            set { couponType = value; }
        }

        public bool Callable
        {
            get { return callable; }
            set { callable = value; }
        }

        public bool Putable
        {
            get { return putable; }
            set { putable = value; }
        }

        public double Coupon
        {
            get { return coupon; }
            set { coupon = value; }
        }

        public bool Convertible
        {
            get { return convertible; }
            set { convertible = value; }
        }

        public string Maturity
        {
            get { return maturity; }
            set { maturity = value; }
        }

        public string IssueDate
        {
            get { return issueDate; }
            set { issueDate = value; }
        }

        public string NextOptionDate
        {
            get { return nextOptionDate; }
            set { nextOptionDate = value; }
        }

        public string NextOptionType
        {
            get { return nextOptionType; }
            set { nextOptionType = value; }
        }

        public bool NextOptionPartial
        {
            get { return nextOptionPartial; }
            set { nextOptionPartial = value; }
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public virtual Object Clone()
        {
            Contract retval = (Contract) MemberwiseClone();
            retval.comboLegs = (ArrayList) retval.comboLegs.Clone();
            return retval;
        }
		
        public Contract(String p_symbol, String p_secType, String p_expiry, double p_strike, String p_right, String p_multiplier, String p_exchange, String p_currency, String p_localSymbol, System.Collections.ArrayList p_comboLegs, String p_primaryExch, bool p_includeExpired, String p_cusip, String p_ratings, String p_descAppend, String p_bondType, String p_couponType, bool p_callable, bool p_putable, double p_coupon, bool p_convertible, String p_maturity, String p_issueDate, String p_nextOptionDate, String p_nextOptionType, bool p_nextOptionPartial, String p_notes)
        {
            symbol = p_symbol;
            secType = p_secType;
            expiry = p_expiry;
            strike = p_strike;
            right = p_right;
            multiplier = p_multiplier;
            exchange = p_exchange;
            currency = p_currency;
            includeExpired = p_includeExpired;
            localSymbol = p_localSymbol;
            comboLegs = p_comboLegs;
            primaryExch = p_primaryExch;
            cusip = p_cusip;
            ratings = p_ratings;
            descAppend = p_descAppend;
            bondType = p_bondType;
            couponType = p_couponType;
            callable = p_callable;
            putable = p_putable;
            coupon = p_coupon;
            convertible = p_convertible;
            maturity = p_maturity;
            issueDate = p_issueDate;
            nextOptionDate = p_nextOptionDate;
            nextOptionType = p_nextOptionType;
            nextOptionPartial = p_nextOptionPartial;
            notes = p_notes;
        }
		
        public  override bool Equals(Object p_other)
        {
            if (p_other == null || !(p_other is Contract) || comboLegs.Count != ((Contract) p_other).comboLegs.Count)
            {
                return false;
            }
            else if (this == p_other)
            {
                return true;
            }
			
            Contract l_theOther = (Contract) p_other;
            bool l_bContractEquals;
			
            String l_thisSecType = secType ?? "";
            String l_otherSecType = l_theOther.secType ?? "";
			
            if (!l_thisSecType.Equals(l_otherSecType))
            {
                l_bContractEquals = false;
            }
            else
            {
                String l_thisSymbol = symbol ?? "";
                String l_thisExchange = exchange ?? "";
                String l_thisPrimaryExch = primaryExch ?? "";
                String l_thisCurrency = currency ?? "";
				
                String l_otherSymbol = l_theOther.symbol ?? "";
                String l_otherExchange = l_theOther.exchange ?? "";
                String l_otherPrimaryExch = l_theOther.primaryExch ?? "";
                String l_otherCurrency = l_theOther.currency ?? "";
				
                l_bContractEquals = l_thisSymbol.Equals(l_otherSymbol) && l_thisExchange.Equals(l_otherExchange) && l_thisPrimaryExch.Equals(l_otherPrimaryExch) && l_thisCurrency.Equals(l_otherCurrency);
				
                if (l_bContractEquals)
                {
                    if (l_thisSecType.Equals("BOND"))
                    {
                        l_bContractEquals = (putable == l_theOther.putable) && (callable == l_theOther.callable) && (convertible == l_theOther.convertible) && (coupon == l_theOther.coupon) && (nextOptionPartial == l_theOther.nextOptionPartial);
                        if (l_bContractEquals)
                        {
                            String l_thisCusip = cusip ?? "";
                            String l_thisRatings = ratings ?? "";
                            String l_thisDescAppend = descAppend ?? "";
                            String l_thisBondType = bondType ?? "";
                            String l_thisCouponType = couponType ?? "";
                            String l_thisMaturity = maturity ?? "";
                            String l_thisIssueDate = issueDate ?? "";
							
                            String l_otherCusip = l_theOther.cusip ?? "";
                            String l_otherRatings = l_theOther.ratings ?? "";
                            String l_otherDescAppend = l_theOther.descAppend ?? "";
                            String l_otherBondType = l_theOther.bondType ?? "";
                            String l_otherCouponType = l_theOther.couponType ?? "";
                            String l_otherMaturity = l_theOther.maturity ?? "";
                            String l_otherIssueDate = l_theOther.issueDate ?? "";
                            String l_otherOptionDate = l_theOther.nextOptionDate ?? "";
                            String l_otherOptionType = l_theOther.nextOptionType ?? "";
                            String l_otherNotes = l_theOther.notes ?? "";
                            l_bContractEquals = l_thisCusip.Equals(l_otherCusip) && l_thisRatings.Equals(l_otherRatings) && l_thisDescAppend.Equals(l_otherDescAppend) && l_thisBondType.Equals(l_otherBondType) && l_thisCouponType.Equals(l_otherCouponType) && l_thisMaturity.Equals(l_otherMaturity) && l_thisIssueDate.Equals(l_otherIssueDate) && l_otherOptionDate.Equals(l_otherOptionDate) && l_otherOptionType.Equals(l_otherOptionType) && l_otherNotes.Equals(l_otherNotes);
                        }
                    }
                    else
                    {
                        String l_thisExpiry = expiry ?? "";
                        String l_thisRight = right ?? "";
                        String l_thisMultiplier = multiplier ?? "";
                        String l_thisLocalSymbol = localSymbol ?? "";
						
                        String l_otherExpiry = l_theOther.expiry ?? "";
                        String l_otherRight = l_theOther.right ?? "";
                        String l_otherMultiplier = l_theOther.multiplier ?? "";
                        String l_otherLocalSymbol = l_theOther.localSymbol ?? "";
						
                        l_bContractEquals = l_thisExpiry.Equals(l_otherExpiry) && strike == l_theOther.strike && l_thisRight.Equals(l_otherRight) && l_thisMultiplier.Equals(l_otherMultiplier) && l_thisLocalSymbol.Equals(l_otherLocalSymbol);
                    }
                }
            }
			
            if (l_bContractEquals && comboLegs.Count > 0)
            {
                // compare the combo legs
                bool[] alreadyMatchedSecondLeg = new bool[comboLegs.Count];
                for (int ctr1 = 0; ctr1 < comboLegs.Count; ctr1++)
                {
                    ComboLeg l_thisComboLeg = (ComboLeg) comboLegs[ctr1];
                    bool l_bLegsEqual = false;
                    for (int ctr2 = 0; ctr2 < l_theOther.comboLegs.Count; ctr2++)
                    {
                        if (alreadyMatchedSecondLeg[ctr2])
                        {
                            continue;
                        }
                        if (l_thisComboLeg.Equals(l_theOther.comboLegs[ctr2]))
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
        public override int GetHashCode()
        {
            return symbol.GetHashCode() ^ secType.GetHashCode() ^ expiry.GetHashCode() ^ strike.GetHashCode() ^
                   right.GetHashCode() ^ multiplier.GetHashCode() ^ exchange.GetHashCode() ^ currency.GetHashCode() ^
                   localSymbol.GetHashCode() ^ primaryExch.GetHashCode() ^ includeExpired.GetHashCode();
        }
    }
}