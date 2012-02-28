using System;
using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Outgoing Message Ids
    /// </summary>
    [Serializable()]
    public enum OutgoingMessage
    {
        /// <summary>
        /// Undefined Outgoing Message
        /// </summary>
        [Description("")] Undefined = 0,
        /// <summary>
        /// Request Market Data
        /// </summary>
        [Description("REQ_MKT_DATA")] RequestMarketData = 1,
        /// <summary>
        /// Cancel Market Data
        /// </summary>
        [Description("CANCEL_MKT_DATA")] CancelMarketData = 2,
        /// <summary>
        /// Place Order
        /// </summary>
        [Description("PLACE_ORDER")] PlaceOrder = 3,
        /// <summary>
        /// Cancel Order
        /// </summary>
        [Description("CANCEL_ORDER")] CancelOrder = 4,
        /// <summary>
        /// Request Open Orders
        /// </summary>
        [Description("REQ_OPEN_ORDERS")] RequestOpenOrders = 5,
        /// <summary>
        /// Request Account Data
        /// </summary>
        [Description("REQ_ACCOUNT_DATA")] RequestAccountData = 6,
        /// <summary>
        /// Request Executions
        /// </summary>
        [Description("REQ_EXECUTIONS")] RequestExecutions = 7,
        /// <summary>
        /// Request IDS
        /// </summary>
        [Description("REQ_IDS")] RequestIds = 8,
        /// <summary>
        /// Request Contract Data
        /// </summary>
        [Description("REQ_CONTRACT_DATA")] RequestContractData = 9,
        /// <summary>
        /// Request Market Depth
        /// </summary>
        [Description("REQ_MKT_DEPTH")] RequestMarketDepth = 10,
        /// <summary>
        /// Cancel Market Depth
        /// </summary>
        [Description("CANCEL_MKT_DEPTH")] CancelMarketDepth = 11,
        /// <summary>
        /// Request News Bullestins
        /// </summary>
        [Description("REQ_NEWS_BULLETINS")] RequestNewsBulletins = 12,
        /// <summary>
        /// Cancel News Bulletins
        /// </summary>
        [Description("CANCEL_NEWS_BULLETINS")] CancelNewsBulletins = 13,
        /// <summary>
        /// Set Server Log Level
        /// </summary>
        [Description("SET_SERVER_LOGLEVEL")] SetServerLogLevel = 14,
        /// <summary>
        /// Request Auto Open Orders
        /// </summary>
        [Description("REQ_AUTO_OPEN_ORDERS")] RequestAutoOpenOrders = 15,
        /// <summary>
        /// Request All Open Orders
        /// </summary>
        [Description("REQ_ALL_OPEN_ORDERS")] RequestAllOpenOrders = 16,
        /// <summary>
        /// Request Managed Accounts
        /// </summary>
        [Description("REQ_MANAGED_ACCTS")] RequestManagedAccounts = 17,
        /// <summary>
        /// Request Financial Advisor
        /// </summary>
        [Description("REQ_FA")] RequestFA = 18,
        /// <summary>
        /// Replace Financial Advisor
        /// </summary>
        [Description("REPLACE_FA")] ReplaceFA = 19,
        /// <summary>
        /// Request Historical Data
        /// </summary>
        [Description("REQ_HISTORICAL_DATA")] RequestHistoricalData = 20,
        /// <summary>
        /// Exercise Options
        /// </summary>
        [Description("EXERCISE_OPTIONS")] ExerciseOptions = 21,
        /// <summary>
        /// Request Scanner Subscription
        /// </summary>
        [Description("REQ_SCANNER_SUBSCRIPTION")] RequestScannerSubscription = 22,
        /// <summary>
        /// Cancel Scanner Subscription
        /// </summary>
        [Description("CANCEL_SCANNER_SUBSCRIPTION")] CancelScannerSubscription = 23,
        /// <summary>
        /// Request Scanner Parameters
        /// </summary>
        [Description("REQ_SCANNER_PARAMETERS")] RequestScannerParameters = 24,
        /// <summary>
        /// Cancel Historical Data
        /// </summary>
        [Description("CANCEL_HISTORICAL_DATA")] CancelHistoricalData = 25,
        /// <summary>
        /// Request Current Time
        /// </summary>
        [Description("REQ_CURRENT_TIME")] RequestCurrentTime = 49,
        /// <summary>
        /// Request Real Time Bars
        /// </summary>
        [Description("REQ_REAL_TIME_BARS")] RequestRealTimeBars = 50,
        /// <summary>
        /// Cancel Real Time Bars
        /// </summary>
        [Description("CANCEL_REAL_TIME_BARS")] CancelRealTimeBars = 51,
        /// <summary>
        /// Request Fundamental Data
        /// </summary>
        [Description("REQ_FUNDAMENTAL_DATA")] RequestFundamentalData = 52,
        /// <summary>
        /// Cancel Fundamental Data
        /// </summary>
        [Description("CANCEL_FUNDAMENTAL_DATA")] CancelFundamentalData = 53,
        /// <summary>
        /// Request Calculated Implied Volatility
        /// </summary>
        [Description("REQ_CALC_IMPLIED_VOLAT")] RequestCalcImpliedVolatility = 54,
        /// <summary>
        /// Request Calculated Option Price
        /// </summary>
        [Description("REQ_CALC_OPTION_PRICE")] RequestCalcOptionPrice = 55,
        /// <summary>
        /// Cancel Calculated Implied Volatility
        /// </summary>
        [Description("CANCEL_CALC_IMPLIED_VOLAT")] CancelCalcImpliedVolatility = 56,
        /// <summary>
        /// Cancel Calculated Option Price
        /// </summary>
        [Description("CANCEL_CALC_OPTION_PRICE")] CancelCalcOptionPrice = 57,
        /// <summary>
        /// Request Global Cancel
        /// </summary>
        [Description("REQ_GLOBAL_CANCEL")] RequestGlobalCancel = 58,
        /// <summary>
        /// Request Market Data Type
        /// </summary>
        [Description("REQ_MARKET_DATA_TYPE")] RequestMarketDataType = 59
    }
}