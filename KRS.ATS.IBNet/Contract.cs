using System;
using System.Collections.Generic;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Class to describe a financial security.
    /// </summary>
    /// <seealso href="http://www.interactivebrokers.com/php/webhelp/Interoperability/Socket_Client_Java/java_properties.htm#Contract">Interactive Brokers Contract Documentation</seealso>
    public class Contract
    {
        #region Private Variables

        private readonly List<ComboLeg> comboLegs = new List<ComboLeg>();

        // BOND values
        private String bondType;
        private bool callable;
        private String comboLegsDescrip; // received in open order version 14 and up for all combos
        private bool convertible;
        private double coupon;
        private String couponType;
        private String currency;
        private String cusip;
        private String descriptionAppend;
        private String exchange;
        private String expiry;
        private bool includeExpired; // can not be set to true for orders.
        private String issueDate;
        private String localSymbol;
        private String maturity;
        private String multiplier;
        private String nextOptionDate;
        private bool nextOptionPartial;
        private String nextOptionType;
        private String notes;

        private String primaryExch;
                       // pick a non-aggregate (ie not the SMART exchange) exchange that the contract trades on.  DO NOT SET TO SMART.

        private bool putable;
        private String ratings;
        private RightType right;
        private SecurityType securityType;
        private double strike;
        private String symbol;

        #endregion

        #region Constructors

        ///<summary>
        /// Undefined Contract Constructor
        ///</summary>
        public Contract() :
            this(null, SecurityType.Undefined, null, 0, RightType.Undefined, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Futures Contract Constructor
        /// </summary>
        /// <param name="symbol">This is the symbol of the underlying asset.</param>
        /// <param name="exchange">The order destination, such as Smart.</param>
        /// <param name="securityType">This is the security type.</param>
        /// <param name="currency">Specifies the currency.</param>
        /// <param name="expiry">The expiration date. Use the format YYYYMM.</param>
        public Contract(string symbol, string exchange, SecurityType securityType, string currency, string expiry) :
            this(symbol, securityType, expiry, 0, RightType.Undefined, null, exchange, currency, null, null)
        {
        }

        /// <summary>
        /// Default Contract Constructor
        /// </summary>
        /// <param name="symbol">This is the symbol of the underlying asset.</param>
        /// <param name="securityType">This is the security type.</param>
        /// <param name="expiry">The expiration date. Use the format YYYYMM.</param>
        /// <param name="strike">The strike price.</param>
        /// <param name="right">Specifies a Put or Call.</param>
        /// <param name="multiplier">Allows you to specify a future or option contract multiplier.
        /// This is only necessary when multiple possibilities exist.</param>
        /// <param name="exchange">The order destination, such as Smart.</param>
        /// <param name="currency">Specifies the currency.</param>
        /// <param name="localSymbol">This is the local exchange symbol of the underlying asset.</param>
        /// <param name="primaryExch">Identifies the listing exchange for the contract (do not list SMART).</param>
        public Contract(String symbol, SecurityType securityType, String expiry, double strike, RightType right,
                        String multiplier, string exchange, string currency, string localSymbol, string primaryExch)
        {
            this.symbol = symbol;
            this.securityType = securityType;
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
        /// This is the security type.
        /// </summary>
        /// <remarks>Valid security types are:
        /// <list type="bullet">
        /// <item>Stock</item>
        /// <item>Option</item>
        /// <item>Future</item>
        /// <item>Indice</item>
        /// <item>Option on Future</item>
        /// <item>Cash</item>
        /// <item>Bag</item>
        /// <item>Bond</item>
        /// </list>
        /// </remarks>
        /// <seealso cref="IBNet.SecurityType"/>
        public SecurityType SecurityType
        {
            get { return securityType; }
            set { securityType = value; }
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
        /// Specifies a Put or Call.
        /// </summary>
        /// <remarks>Valid values are:
        /// <list type="bullet">
        /// <item>Put - the right to sell a security.</item>
        /// <item>Call - the right to buy a security.</item>
        /// </list>
        /// </remarks>
        /// <seealso cref="RightType"/>
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
        /// Specifies the currency.
        /// </summary>
        /// <remarks>
        /// Ambiguities may require that this field be specified,
        /// for example, when SMART is the exchange and IBM is being requested
        /// (IBM can trade in GBP or USD).  Given the existence of this kind of ambiguity,
        /// it is a good idea to always specify the currency.
        /// </remarks>
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
        /// Historical data queries on expired contracts are limited to the
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
        public List<ComboLeg> ComboLegs
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
        /// are from Moody's and SP respectively.
        /// </summary>
        public string Ratings
        {
            get { return ratings; }
            set { ratings = value; }
        }

        /// <summary>
        /// For Bonds. A description string containing further descriptive information about the bond.
        /// </summary>
        public string DescriptionAppend
        {
            get { return descriptionAppend; }
            set { descriptionAppend = value; }
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
    }
}