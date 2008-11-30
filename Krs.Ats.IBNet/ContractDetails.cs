using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Contract details returned from Interactive Brokers
    /// </summary>
    [Serializable()]
    public class ContractDetails
    {
        #region Private Variables

        private String marketName;
        private double minTick;
        private String orderTypes;
        private int priceMagnifier;
        private Contract summary;
        private String tradingClass;
        private String validExchanges;
        private int underConId;

        // BOND values
        private String cusip;
        private String ratings;
        private String descriptionAppend;
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

        /// <summary>
        /// Default constructor
        /// </summary>
        public ContractDetails() :
            this(new Contract(), null, null, 0, null, null, 0)
        {
        }

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="summary">A contract summary.</param>
        /// <param name="marketName">The market name for this contract.</param>
        /// <param name="tradingClass">The trading class name for this contract.</param>
        /// <param name="minTick">The minimum price tick.</param>
        /// <param name="orderTypes">The list of valid order types for this contract.</param>
        /// <param name="validExchanges">The list of exchanges this contract is traded on.</param>
        /// <param name="underConId">The Underlying Contract Id (for derivatives only)</param>
        public ContractDetails(Contract summary, String marketName, String tradingClass, double minTick,
                               String orderTypes, String validExchanges, int underConId)
        {
            this.summary = summary;
            this.marketName = marketName;
            this.tradingClass = tradingClass;
            this.minTick = minTick;
            this.orderTypes = orderTypes;
            this.validExchanges = validExchanges;
            this.underConId = underConId;
        }

        #endregion

        #region Properties

        /// <summary>
        /// A contract summary.
        /// </summary>
        public Contract Summary
        {
            get { return summary; }
            set { summary = value; }
        }

        /// <summary>
        /// The market name for this contract.
        /// </summary>
        public string MarketName
        {
            get { return marketName; }
            set { marketName = value; }
        }

        /// <summary>
        /// The trading class name for this contract.
        /// </summary>
        public string TradingClass
        {
            get { return tradingClass; }
            set { tradingClass = value; }
        }

        /// <summary>
        /// The minimum price tick.
        /// </summary>
        public double MinTick
        {
            get { return minTick; }
            set { minTick = value; }
        }

        /// <summary>
        /// Allows execution and strike prices to be reported consistently with
        /// market data, historical data and the order price, i.e. Z on LIFFE is
        /// reported in index points and not GBP.
        /// </summary>
        public int PriceMagnifier
        {
            get { return priceMagnifier; }
            set { priceMagnifier = value; }
        }

        /// <summary>
        /// The list of valid order types for this contract.
        /// </summary>
        public string OrderTypes
        {
            get { return orderTypes; }
            set { orderTypes = value; }
        }

        /// <summary>
        /// The list of exchanges this contract is traded on.
        /// </summary>
        public string ValidExchanges
        {
            get { return validExchanges; }
            set { validExchanges = value; }
        }

        /// <summary>
        /// Underlying Contract Id
        /// underConId (underlying contract ID), has been added to the
        /// ContractDetails structure to allow unambiguous identification with the underlying contract
        /// (you no longer have to match by symbol, etc.). This new field applies to derivatives only.
        /// </summary>
        public int UnderConId
        {
            get { return underConId; }
            set { underConId = value; }
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