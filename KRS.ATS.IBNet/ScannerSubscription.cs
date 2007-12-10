using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Scanner Subscription details to pass to Interactive Brokers
    /// </summary>
    [Serializable()]
    public class ScannerSubscription
    {
        #region Private Variables

        private double abovePrice = Double.MaxValue;
        private int aboveVolume = Int32.MaxValue;
        private int averageOptionVolumeAbove = Int32.MaxValue;
        private double belowPrice = Double.MaxValue;
        private double couponRateAbove = Double.MaxValue;
        private double couponRateBelow = Double.MaxValue;
        private String excludeConvertible;
        private String instrument;
        private String locationCode;
        private double marketCapAbove = Double.MaxValue;
        private double marketCapBelow = Double.MaxValue;
        private String maturityDateAbove;
        private String maturityDateBelow;
        private String moodyRatingAbove;
        private String moodyRatingBelow;
        private int numberOfRows = -1; //No row number specified
        private String scanCode;
        private String scannerSettingPairs;
        private String spRatingAbove;
        private String spRatingBelow;
        private String stockTypeFilter;

        #endregion

        #region Properties

        /// <summary>
        /// Defines the number of rows of data to return for a query.
        /// </summary>
        public int NumberOfRows
        {
            get { return numberOfRows; }
            set { numberOfRows = value; }
        }

        /// <summary>
        /// Defines the instrument type for the scan.
        /// </summary>
        public string Instrument
        {
            get { return instrument; }
            set { instrument = value; }
        }

        /// <summary>
        /// The location, currently the only valid location is US stocks.
        /// </summary>
        public string LocationCode
        {
            get { return locationCode; }
            set { locationCode = value; }
        }

        /// <summary>
        /// Can be left blank. 
        /// </summary>
        public string ScanCode
        {
            get { return scanCode; }
            set { scanCode = value; }
        }

        /// <summary>
        /// Filter out contracts with a price lower than this value.
        /// Can be left blank.
        /// </summary>
        public double AbovePrice
        {
            get { return abovePrice; }
            set { abovePrice = value; }
        }

        /// <summary>
        /// Filter out contracts with a price higher than this value.
        /// Can be left blank. 
        /// </summary>
        public double BelowPrice
        {
            get { return belowPrice; }
            set { belowPrice = value; }
        }

        /// <summary>
        /// Filter out contracts with a volume lower than this value.
        /// Can be left blank.
        /// </summary>
        public int AboveVolume
        {
            get { return aboveVolume; }
            set { aboveVolume = value; }
        }

        /// <summary>
        /// Can leave empty. 
        /// </summary>
        public int AverageOptionVolumeAbove
        {
            get { return averageOptionVolumeAbove; }
            set { averageOptionVolumeAbove = value; }
        }

        /// <summary>
        /// Filter out contracts with a market cap lower than this value.
        /// Can be left blank.
        /// </summary>
        public double MarketCapAbove
        {
            get { return marketCapAbove; }
            set { marketCapAbove = value; }
        }

        /// <summary>
        /// Filter out contracts with a market cap above this value.
        /// Can be left blank.
        /// </summary>
        public double MarketCapBelow
        {
            get { return marketCapBelow; }
            set { marketCapBelow = value; }
        }

        /// <summary>
        /// Filter out contracts with a Moody rating below this value.
        /// Can be left blank.
        /// </summary>
        public string MoodyRatingAbove
        {
            get { return moodyRatingAbove; }
            set { moodyRatingAbove = value; }
        }

        /// <summary>
        /// Filter out contracts with a Moody rating above this value.
        /// Can be left blank.
        /// </summary>
        public string MoodyRatingBelow
        {
            get { return moodyRatingBelow; }
            set { moodyRatingBelow = value; }
        }

        /// <summary>
        /// Filter out contracts with an SP rating below this value.
        /// Can be left blank.
        /// </summary>
        public string SPRatingAbove
        {
            get { return spRatingAbove; }
            set { spRatingAbove = value; }
        }

        /// <summary>
        /// Filter out contracts with an SP rating above this value.
        /// Can be left blank.
        /// </summary>
        public string SPRatingBelow
        {
            get { return spRatingBelow; }
            set { spRatingBelow = value; }
        }

        /// <summary>
        /// Filter out contracts with a maturity date earlier than this value.
        /// Can be left blank.
        /// </summary>
        public string MaturityDateAbove
        {
            get { return maturityDateAbove; }
            set { maturityDateAbove = value; }
        }

        /// <summary>
        /// Filter out contracts with a maturity date later than this value.
        /// Can be left blank.
        /// </summary>
        public string MaturityDateBelow
        {
            get { return maturityDateBelow; }
            set { maturityDateBelow = value; }
        }

        /// <summary>
        /// Filter out contracts with a coupon rate lower than this value.
        /// Can be left blank.
        /// </summary>
        public double CouponRateAbove
        {
            get { return couponRateAbove; }
            set { couponRateAbove = value; }
        }

        /// <summary>
        /// Filter out contracts with a coupon rate higher than this value.
        /// Can be left blank.
        /// </summary>
        public double CouponRateBelow
        {
            get { return couponRateBelow; }
            set { couponRateBelow = value; }
        }

        /// <summary>
        /// Filter out convertible bonds.
        /// Can be left blank.
        /// </summary>
        public string ExcludeConvertible
        {
            get { return excludeConvertible; }
            set { excludeConvertible = value; }
        }

        /// <summary>
        /// Can leave empty. For example, a pairing "Annual, true" used on the
        /// "top Option Implied Vol % Gainers" scan would return annualized volatilities.
        /// </summary>
        public string ScannerSettingPairs
        {
            get { return scannerSettingPairs; }
            set { scannerSettingPairs = value; }
        }

        /// <summary>
        /// ALL (excludes nothing)
        /// STOCK (excludes ETFs)
        /// ETF (includes ETFs)
        /// </summary>
        public string StockTypeFilter
        {
            get { return stockTypeFilter; }
            set { stockTypeFilter = value; }
        }

        #endregion
    }
}