using System;
using Krs.Ats.IBNet;
using System.Collections;

namespace Krs.Ats.IBNet
{
    public class Contract : ICloneable
    {
        #region Private Variables
        private String symbol;
        private SecurityType secType;
        private String expiry;
        private double strike;
        private RightType right;
        private String multiplier;
        private String exchange;

        private String currency;
        private String localSymbol;
        private String primaryExch; // pick a non-aggregate (ie not the SMART exchange) exchange that the contract trades on.  DO NOT SET TO SMART.
        private bool includeExpired; // can not be set to true for orders.
		
        // COMBOS
        private String comboLegsDescrip; // received in open order version 14 and up for all combos
        private ArrayList comboLegs = ArrayList.Synchronized(new ArrayList(10));
		
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
        #endregion

        #region Constructors
        public Contract() :
            this(null, SecurityType.Undefined, null, null, null)
        {
            
        }

        /// <summary>
        /// Futures Contract Constructor
        /// </summary>
        /// <param name="Symbol"></param>
        /// <param name="Exchange"></param>
        /// <param name="SecType"></param>
        /// <param name="Currency"></param>
        /// <param name="Expiry"></param>
        public Contract(string symbol, string exchange, SecurityType secType, string currency, string expiry) :
            this(symbol, secType, expiry, 0, RightType.Undefined, null, exchange, currency, null, null)
        {
            
        }

        /// <summary>
        /// Constructor for Futures Contracts
        /// </summary>
        /// <param name="localSymbol">This is the local exchange symbol of the underlying asset.</param>
        /// <param name="secType">This is the security type.</param>
        /// <param name="exchange">The order destination, such as Smart.</param>
        /// <param name="currency">Specifies the currency. Ambiguities may require that this field be specified,
        /// for example, when SMART is the exchange and IBM is being requested
        /// (IBM can trade in GBP or USD).  Given the existence of this kind of ambiguity,
        /// it is a good idea to always specify the currency.</param>
        public Contract(String localSymbol, SecurityType secType, String exchange, String currency, String expiry) :
            this(null, secType, expiry, 0, RightType.Undefined, null, exchange, currency, localSymbol, null)
        {
        }

        public Contract(String symbol, SecurityType secType, String expiry, double strike, RightType right,
                        String multiplier, string exchange, string currency, string localSymbol, string primaryExch)
        {
            this.symbol = symbol;
            this.secType = secType;
            this.expiry = expiry;
            this.strike = strike;
            this.right = right;
            this.multiplier = multiplier;
            this.exchange = exchange;

            this.currency = currency;
            this.localSymbol = localSymbol;
            this.primaryExch = primaryExch;
        }
        #endregion

        #region Properties
        /// <summary>
        /// This is the symbol of the underlying asset.
        /// </summary>
        public string Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }
        /// <summary>
        /// This is the security type. Valid values are:
        /// STK
        /// OPT
        /// FUT
        /// IND
        /// FOP
        /// CASH
        /// BAG
        /// </summary>
        public SecurityType SecType
        {
            get { return secType; }
            set { secType = value; }
        }
        /// <summary>
        /// The expiration date. Use the format YYYYMM.
        /// </summary>
        public string Expiry
        {
            get { return expiry; }
            set { expiry = value; }
        }
        /// <summary>
        /// The strike price.
        /// </summary>
        public double Strike
        {
            get { return strike; }
            set { strike = value; }
        }
        /// <summary>
        /// Specifies a Put or Call. Valid values are: P, PUT, C, CALL.
        /// </summary>
        public RightType Right
        {
            get { return right; }
            set { right = value; }
        }
        /// <summary>
        /// Allows you to specify a future or option contract multiplier.
        /// This is only necessary when multiple possibilities exist.
        /// </summary>
        public string Multiplier
        {
            get { return multiplier; }
            set { multiplier = value; }
        }
        /// <summary>
        /// The order destination, such as Smart.
        /// </summary>
        public string Exchange
        {
            get { return exchange; }
            set { exchange = value; }
        }
        /// <summary>
        /// Specifies the currency. Ambiguities may require that this field be specified,
        /// for example, when SMART is the exchange and IBM is being requested
        /// (IBM can trade in GBP or USD).  Given the existence of this kind of ambiguity,
        /// it is a good idea to always specify the currency.
        /// </summary>
        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }
        /// <summary>
        /// This is the local exchange symbol of the underlying asset.
        /// </summary>
        public string LocalSymbol
        {
            get { return localSymbol; }
            set { localSymbol = value; }
        }
        /// <summary>
        /// Identifies the listing exchange for the contract (do not list SMART).
        /// </summary>
        public string PrimaryExch
        {
            get { return primaryExch; }
            set { primaryExch = value; }
        }
        /// <summary>
        /// If set to true, contract details requests and historical data queries
        /// can be performed pertaining to expired contracts.
        /// 
        /// Note: Historical data queries on expired contracts are limited to the
        /// last year of the contracts life, and are initially only supported for
        /// expired futures contracts,
        /// </summary>
        public bool IncludeExpired
        {
            get { return includeExpired; }
            set { includeExpired = value; }
        }
        /// <summary>
        /// Description for combo legs
        /// </summary>
        public string ComboLegsDescrip
        {
            get { return comboLegsDescrip; }
            set { comboLegsDescrip = value; }
        }
        /// <summary>
        /// Dynamic memory structure used to store the leg definitions for this contract.
        /// </summary>
        public ArrayList ComboLegs
        {
            get { return comboLegs; }
        }
        /// <summary>
        /// For Bonds. The nine-character bond CUSIP or the 12-character SEDOL.
        /// </summary>
        public string Cusip
        {
            get { return cusip; }
            set { cusip = value; }
        }
        /// <summary>
        /// For Bonds. Identifies the credit rating of the issuer. A higher credit
        /// rating generally indicates a less risky investment. Bond ratings
        /// are from Moody's and S&P respectively.
        /// </summary>
        public string Ratings
        {
            get { return ratings; }
            set { ratings = value; }
        }
        /// <summary>
        /// For Bonds. A description string containing further descriptive information about the bond.
        /// </summary>
        public string DescAppend
        {
            get { return descAppend; }
            set { descAppend = value; }
        }
        /// <summary>
        /// For Bonds. The type of bond, such as "CORP."
        /// </summary>
        public string BondType
        {
            get { return bondType; }
            set { bondType = value; }
        }
        /// <summary>
        /// For Bonds. The type of bond coupon, such as "FIXED."
        /// </summary>
        public string CouponType
        {
            get { return couponType; }
            set { couponType = value; }
        }
        /// <summary>
        /// For Bonds. Values are True or False. If true, the bond can be called
        /// by the issuer under certain conditions.
        /// </summary>
        public bool Callable
        {
            get { return callable; }
            set { callable = value; }
        }
        /// <summary>
        /// For Bonds. Values are True or False. If true, the bond can be sold
        /// back to the issuer under certain conditions.
        /// </summary>
        public bool Putable
        {
            get { return putable; }
            set { putable = value; }
        }
        /// <summary>
        /// For Bonds. The interest rate used to calculate the amount you will
        /// receive in interest payments over the course of the year.
        /// </summary>
        public double Coupon
        {
            get { return coupon; }
            set { coupon = value; }
        }
        /// <summary>
        /// For Bonds. Values are True or False.
        /// If true, the bond can be converted to stock under certain conditions.
        /// </summary>
        public bool Convertible
        {
            get { return convertible; }
            set { convertible = value; }
        }
        /// <summary>
        /// For Bonds. The date on which the issuer must repay the face value of the bond.
        /// </summary>
        public string Maturity
        {
            get { return maturity; }
            set { maturity = value; }
        }
        /// <summary>
        /// For Bonds. The date the bond was issued. 
        /// </summary>
        public string IssueDate
        {
            get { return issueDate; }
            set { issueDate = value; }
        }
        /// <summary>
        /// For Bonds, relevant if the bond has embedded options
        /// </summary>
        public string NextOptionDate
        {
            get { return nextOptionDate; }
            set { nextOptionDate = value; }
        }
        /// <summary>
        /// For Bonds, relevant if the bond has embedded options
        /// </summary>
        public string NextOptionType
        {
            get { return nextOptionType; }
            set { nextOptionType = value; }
        }
        /// <summary>
        /// For Bonds, relevant if the bond has embedded options, i.e., is the next option full or partial?
        /// </summary>
        public bool NextOptionPartial
        {
            get { return nextOptionPartial; }
            set { nextOptionPartial = value; }
        }
        /// <summary>
        /// For Bonds, if populated for the bond in IBs database
        /// </summary>
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }
        #endregion

        #region Object Overrides
        public virtual Object Clone()
        {
            Contract retval = (Contract) MemberwiseClone();
            retval.comboLegs = (ArrayList) retval.comboLegs.Clone();
            return retval;
        }
		
        public  override bool Equals(Object obj)
        {
            Contract other = obj as Contract;
            if (other == null || comboLegs.Count != other.comboLegs.Count)
            {
                return false;
            }
            else if (this == obj)
            {
                return true;
            }
            bool bContractEquals;
						
            if (secType != other.secType)
            {
                bContractEquals = false;
            }
            else
            {
                String thisSymbol = symbol ?? "";
                String thisExchange = exchange ?? "";
                String thisPrimaryExch = primaryExch ?? "";
                String thisCurrency = currency ?? "";
				
                String otherSymbol = other.symbol ?? "";
                String otherExchange = other.exchange ?? "";
                String otherPrimaryExch = other.primaryExch ?? "";
                String otherCurrency = other.currency ?? "";
				
                bContractEquals = thisSymbol.Equals(otherSymbol) && thisExchange.Equals(otherExchange) && thisPrimaryExch.Equals(otherPrimaryExch) && thisCurrency.Equals(otherCurrency);
				
                if (bContractEquals)
                {
                    if (secType == SecurityType.Bond)
                    {
                        bContractEquals = (putable == other.putable) && (callable == other.callable) && (convertible == other.convertible) && (coupon == other.coupon) && (nextOptionPartial == other.nextOptionPartial);
                        if (bContractEquals)
                        {
                            String thisCusip = cusip ?? "";
                            String thisRatings = ratings ?? "";
                            String thisDescAppend = descAppend ?? "";
                            String thisBondType = bondType ?? "";
                            String thisCouponType = couponType ?? "";
                            String thisMaturity = maturity ?? "";
                            String thisIssueDate = issueDate ?? "";
							
                            String otherCusip = other.cusip ?? "";
                            String otherRatings = other.ratings ?? "";
                            String otherDescAppend = other.descAppend ?? "";
                            String otherBondType = other.bondType ?? "";
                            String otherCouponType = other.couponType ?? "";
                            String otherMaturity = other.maturity ?? "";
                            String otherIssueDate = other.issueDate ?? "";
                            String otherOptionDate = other.nextOptionDate ?? "";
                            String otherOptionType = other.nextOptionType ?? "";
                            String otherNotes = other.notes ?? "";
                            bContractEquals = thisCusip.Equals(otherCusip) && thisRatings.Equals(otherRatings) && thisDescAppend.Equals(otherDescAppend) && thisBondType.Equals(otherBondType) && thisCouponType.Equals(otherCouponType) && thisMaturity.Equals(otherMaturity) && thisIssueDate.Equals(otherIssueDate) && otherOptionDate.Equals(otherOptionDate) && otherOptionType.Equals(otherOptionType) && otherNotes.Equals(otherNotes);
                        }
                    }
                    else
                    {
                        String thisExpiry = expiry ?? "";
                        String thisRight = ((right == RightType.Undefined) ? "" : right.ToString());
                        String thisMultiplier = multiplier ?? "";
                        String thisLocalSymbol = localSymbol ?? "";
						
                        String otherExpiry = other.expiry ?? "";
                        String otherRight = ((other.right == RightType.Undefined) ? "" : right.ToString());
                        String otherMultiplier = other.multiplier ?? "";
                        String otherLocalSymbol = other.localSymbol ?? "";
						
                        bContractEquals = thisExpiry.Equals(otherExpiry) && strike == other.strike && thisRight.Equals(otherRight) && thisMultiplier.Equals(otherMultiplier) && thisLocalSymbol.Equals(otherLocalSymbol);
                    }
                }
            }
			
            if (bContractEquals && comboLegs.Count > 0)
            {
                // compare the combo legs
                bool[] alreadyMatchedSecondLeg = new bool[comboLegs.Count];
                for (int ctr1 = 0; ctr1 < comboLegs.Count; ctr1++)
                {
                    ComboLeg thisComboLeg = (ComboLeg) comboLegs[ctr1];
                    bool bLegsEqual = false;
                    for (int ctr2 = 0; ctr2 < other.comboLegs.Count; ctr2++)
                    {
                        if (alreadyMatchedSecondLeg[ctr2])
                        {
                            continue;
                        }
                        if (thisComboLeg.Equals(other.comboLegs[ctr2]))
                        {
                            bLegsEqual = alreadyMatchedSecondLeg[ctr2] = true;
                            break;
                        }
                    }
                    if (!bLegsEqual)
                    {
                        // leg on first not matched by any previously unmatched leg on second
                        return false;
                    }
                }
            }
			
            return bContractEquals;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}