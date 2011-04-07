using System;
using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Generic Ticks
    /// </summary>
    [Serializable()] 
    public enum GenericTickType : int
    {
        /// <summary>
        /// Undefined Generic Tick Type
        /// </summary>
        [Description("")] Undefined = 0,
        /// <summary>
        /// Option Volume
        /// For stocks only.
        /// Returns TickType.OptionCallVolume and TickType.OptionPutVolume 
        /// </summary>
        [Description("OptionVolume")] OptionVolume = 100,
        /// <summary>
        /// Option Open Interest
        /// For stocks only.
        /// Returns TickType.OptionCallOpenInterest and TickType.OptionPutOpenInterest
        /// </summary>
        [Description("OptionOpenInterest")] OptionOpenInterest = 101,
        /// <summary>
        /// Historical Volatility
        /// For stocks only.
        /// Returns TickType.OptionHistoricalVol
        /// </summary>
        [Description("HistoricalVolatility")] HistoricalVolatility = 104,
        /// <summary>
        /// Option Implied Volatility
        /// For stocks only.
        /// Returns TickType.OptionImpliedVol
        /// </summary>
        [Description("OptionImpliedVolatility")] OptionImpliedVolatility = 106,
        /// <summary>
        /// Index Future Premium
        /// Returns TickType.IndexFuturePremium
        /// </summary>
        [Description("IndexFuturePremium")] IndexFuturePremium = 162,
        /// <summary>
        /// Miscellaneous Stats
        /// Returns TickType.Low13Week, TickType.High13Week, TickType.Low26Week, TickType.High26Week, TickType.Low52Week, TickType.High52Week and TickType.AverageVolume
        /// </summary>
        [Description("MiscellaneousStats")] MiscellaneousStats = 165,
        /// <summary>
        /// Mark Price
        /// Used in TWS P/L Computations
        /// Returns TickType.MarkPrice
        /// </summary>
        [Description("MarkPrice")] MarkPrice = 221,
        /// <summary>
        /// Auction Price
        /// Auction values (volume, price and imbalance)
        /// Returns TickType.AuctionVolume, TickType.AuctionPrice, TickType.AuctionImbalance
        /// </summary>
        [Description("AuctionPrice")] AuctionPrice = 225,
        /// <summary>
        /// Shortable Ticks
        /// </summary>
        [Description("Shortable")] Shortable = 236,
        /// <summary>
        /// Real Time Volume Tick Type
        /// </summary>
        [Description("RTVolume")] RealTimeVolume = 233,
        /// <summary>
        /// Inventory Type
        /// </summary>
        [Description("Inventory")] Inventory = 256,
        /// <summary>
        /// Fundamental Ratios Tick Type
        /// </summary>
        [Description("FundamentalRatios")] FundamentalRatios = 258
    }
}