using System;
using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Incoming Tick Types
    /// </summary>
    [Serializable()] 
    public enum TickType : int
    {
        /// <summary>
        /// Bid Size
        /// </summary>
        [Description("BID_SIZE")] BidSize = 0,
        /// <summary>
        /// Bid Price
        /// </summary>
        [Description("BID")] BidPrice = 1,
        /// <summary>
        /// Ask Price
        /// </summary>
        [Description("ASK")] AskPrice = 2,
        /// <summary>
        /// Ask Size
        /// </summary>
        [Description("ASK_SIZE")] AskSize = 3,
        /// <summary>
        /// Last Price
        /// </summary>
        [Description("LAST")] LastPrice = 4,
        /// <summary>
        /// Last Size
        /// </summary>
        [Description("LAST_SIZE")] LastSize = 5,
        /// <summary>
        /// High Price
        /// </summary>
        [Description("HIGH")] HighPrice = 6,
        /// <summary>
        /// Low Price
        /// </summary>
        [Description("LOW")] LowPrice = 7,
        /// <summary>
        /// Volume
        /// </summary>
        [Description("VOLUME")] Volume = 8,
        /// <summary>
        /// Close Price
        /// </summary>
        [Description("CLOSE")] ClosePrice = 9,
        /// <summary>
        /// Bid Option
        /// </summary>
        [Description("BID_OPTION")] BidOption = 10,
        /// <summary>
        /// Ask Option
        /// </summary>
        [Description("ASK_OPTION")] AskOption = 11,
        /// <summary>
        /// Last Option
        /// </summary>
        [Description("LAST_OPTION")] LastOption = 12,
        /// <summary>
        /// Model Option
        /// </summary>
        [Description("MODEL_OPTION")] ModelOption = 13,
        /// <summary>
        /// Open Price
        /// </summary>
        [Description("OPEN")] OpenPrice = 14,
        /// <summary>
        /// Low Price over last 13 weeks
        /// </summary>
        [Description("LOW_13_WEEK")] Low13Week = 15,
        /// <summary>
        /// High Price over last 13 weeks
        /// </summary>
        [Description("HIGH_13_WEEK")] High13Week = 16,
        /// <summary>
        /// Low Price over last 26 weeks
        /// </summary>
        [Description("LOW_26_WEEK")] Low26Week = 17,
        /// <summary>
        /// High Price over last 26 weeks
        /// </summary>
        [Description("HIGH_26_WEEK")] High26Week = 18,
        /// <summary>
        /// Low Price over last 52 weeks
        /// </summary>
        [Description("LOW_52_WEEK")] Low52Week = 19,
        /// <summary>
        /// High Price over last 52 weeks
        /// </summary>
        [Description("HIGH_52_WEEK")] High52Week = 20,
        /// <summary>
        /// Average Volume
        /// </summary>
        [Description("AVG_VOLUME")] AverageVolume = 21,
        /// <summary>
        /// Open Interest
        /// </summary>
        [Description("OPEN_INTEREST")] OpenInterest = 22,
        /// <summary>
        /// Option Historical Volatility
        /// </summary>
        [Description("OPTION_HISTORICAL_VOL")] OptionHistoricalVolatility = 23,
        /// <summary>
        /// Option Implied Volatility
        /// </summary>
        [Description("OPTION_IMPLIED_VOL")] OptionImpliedVolatility = 24,
        /// <summary>
        /// Option Bid Exchange
        /// </summary>
        [Description("OPTION_BID_EXCH")] OptionBidExchange = 25,
        /// <summary>
        /// Option Ask Exchange
        /// </summary>
        [Description("OPTION_ASK_EXCH")] OptionAskExchange = 26,
        /// <summary>
        /// Option Call Open Interest
        /// </summary>
        [Description("OPTION_CALL_OPEN_INTEREST")] OptionCallOpenInterest = 27,
        /// <summary>
        /// Option Put Open Interest
        /// </summary>
        [Description("OPTION_PUT_OPEN_INTEREST")] OptionPutOpenInterest = 28,
        /// <summary>
        /// Option Call Volume
        /// </summary>
        [Description("OPTION_CALL_VOLUME")] OptionCallVolume = 29,
        /// <summary>
        /// Option Put Volume
        /// </summary>
        [Description("OPTION_PUT_VOLUME")] OptionPutVolume = 30,
        /// <summary>
        /// Index Future Premium
        /// </summary>
        [Description("INDEX_FUTURE_PREMIUM")] IndexFuturePremium = 31,
        /// <summary>
        /// Bid Exchange
        /// </summary>
        [Description("BID_EXCH")] BidExchange = 32,
        /// <summary>
        /// Ask Exchange
        /// </summary>
        [Description("ASK_EXCH")] AskExchange = 33,
        /// <summary>
        /// Auction Volume
        /// </summary>
        [Description("AUCTION_VOLUME")] AuctionVolume = 34,
        /// <summary>
        /// Auction Price
        /// </summary>
        [Description("AUCTION_PRICE")] AuctionPrice = 35,
        /// <summary>
        /// Auction Imbalance
        /// </summary>
        [Description("AUCTION_IMBALANCE")] AuctionImbalance = 36,
        /// <summary>
        /// Mark Price
        /// </summary>
        [Description("MARK_PRICE")] MarkPrice = 37,
        /// <summary>
        /// Bid EFP Computation
        /// </summary>
        [Description("BID_EFP_COMPUTATION")] BidEfpComputation = 38,
        /// <summary>
        /// Ask EFP Computation
        /// </summary>
        [Description("ASK_EFP_COMPUTATION")] AskEfpComputation = 39,
        /// <summary>
        /// Last EFP Computation
        /// </summary>
        [Description("LAST_EFP_COMPUTATION")] LastEfpComputation = 40,
        /// <summary>
        /// Open EFP Computation
        /// </summary>
        [Description("OPEN_EFP_COMPUTATION")] OpenEfpComputation = 41,
        /// <summary>
        /// High EFP Computation
        /// </summary>
        [Description("HIGH_EFP_COMPUTATION")] HighEfpComputation = 42,
        /// <summary>
        /// Low EFP Computation
        /// </summary>
        [Description("LOW_EFP_COMPUTATION")] LowEfpComputation = 43,
        /// <summary>
        /// Close EFP Computation
        /// </summary>
        [Description("CLOSE_EFP_COMPUTATION")] CloseEfpComputation = 44,
        /// <summary>
        /// Last Time Stamp
        /// </summary>
        [Description("LAST_TIMESTAMP")] LastTimestamp = 45,
        /// <summary>
        /// Shortable
        /// </summary>
        [Description("SHORTABLE")] Shortable = 46,
        /// <summary>
        /// Fundamental Ratios
        /// </summary>
        [Description("FUNDAMENTAL_RATIOS")] FundamentalRatios = 47,
        /// <summary>
        /// Real Time Volume
        /// </summary>
        [Description("RTVOLUME")] RealTimeVolume = 48,
        /// <summary>
        /// When trading is halted for a contract, TWS receives a special tick: haltedLast=1. When trading is resumed, TWS receives haltedLast=0. A new tick type, HALTED, tick ID = 49, is now available in regular market data via the API to indicate this halted state.
        /// Possible values for this new tick type are:
        /// 0 = Not halted 
        /// 1 = Halted. 
        ///  </summary>
        [Description("HALTED")] Halted = 49,
        /// <summary>
        /// Bond Yield for Bid Price
        /// </summary>
        [Description("BID_YIELD")] BidYield = 50,
        /// <summary>
        /// Bond Yield for Ask Price
        /// </summary>
        [Description("ASK_YIELD")] AskYield = 51,
        /// <summary>
        /// Bond Yield for Last Price
        /// </summary>
        [Description("LAST_YIELD")] LastYield = 52,
        /// <summary>
        /// returns calculated implied volatility as a result of an calculateImpliedVolatility( ) request.
        /// </summary>
        [Description("CUST_OPTION_COMPUTATION")] CustOptionComputation = 53,
        /// <summary>
        /// Trades
        /// </summary>
        [Description("TRADE_COUNT")] TradeCount = 54,
        /// <summary>
        /// Trades per Minute
        /// </summary>
        [Description("TRADE_RATE")] TradeRate = 55,
        /// <summary>
        /// Volume per Minute
        /// </summary>
        [Description("VOLUME_RATE")] VolumeRate = 56,
        /// <summary>
        /// Last Regular Trading Hours Trade
        /// </summary>
        [Description("LAST_RTH_TRADE")] LastRthTrade = 57
    }
}