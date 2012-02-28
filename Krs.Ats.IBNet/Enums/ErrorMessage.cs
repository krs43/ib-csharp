using System;
using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Contains all of the standard Interactive Brokers error messages.
    /// </summary>
    [Serializable()] 
    public enum ErrorMessage : int
    {
        /// <summary>
        /// Undefined Error Message
        /// </summary>
        [Description("")] Undefined = 0,
        /// <summary>
        /// Already connected.
        /// </summary>
        [Description("Already connected.")] AlreadyConnected = 501,
        /// <summary>
        /// Couldn't connect to TWS.  Confirm that \"Enable ActiveX and Socket Clients\" is enabled on the TWS \"Configure->API\" menu.
        /// </summary>
        [Description(
            "Couldn't connect to TWS.  Confirm that \"Enable ActiveX and Socket Clients\" is enabled on the TWS \"Configure->API\" menu."
            )] ConnectFail = 502,
        /// <summary>
        /// The TWS is out of date and must be upgraded.
        /// </summary>
        [Description("The TWS is out of date and must be upgraded.")] UpdateTws = 503,
        /// <summary>
        /// Not connected
        /// </summary>
        [Description("Not connected")] NotConnected = 504,
        /// <summary>
        /// Fatal Error: Unknown message id.
        /// </summary>
        [Description("Fatal Error: Unknown message id.")] UnknownId = 505,
        /// <summary>
        /// Request Market Data Sending Error
        /// </summary>
        [Description("Request Market Data Sending Error")] FailSendRequestMarket = 510,
        /// <summary>
        /// Cancel Market Data Sending Error
        /// </summary>
        [Description("Cancel Market Data Sending Error")] FailSendCancelMarket = 511,
        /// <summary>
        /// Order Sending Error
        /// </summary>
        [Description("Order Sending Error")] FailSendOrder = 512,
        /// <summary>
        /// Account Update Request Sending Error
        /// </summary>
        [Description("Account Update Request Sending Error")] FailSendAccountUpdate = 513,
        /// <summary>
        /// Request For Executions Sending Error
        /// </summary>
        [Description("Request For Executions Sending Error")] FailSendExecution = 514,
        /// <summary>
        /// Cancel Order Sending Error
        /// </summary>
        [Description("Cancel Order Sending Error")] FailSendCancelOrder = 515,
        /// <summary>
        /// Request Open Order Sending Error
        /// </summary>
        [Description("Request Open Order Sending Error")] FailSendOpenOrder = 516,
        /// <summary>
        /// Unknown contract. Verify the contract details supplied.
        /// </summary>
        [Description("Unknown contract. Verify the contract details supplied.")] UnknownContract = 517,
        /// <summary>
        /// Request Contract Data Sending Error
        /// </summary>
        [Description("Request Contract Data Sending Error")] FailSendRequestContract = 518,
        /// <summary>
        /// Request Market Depth Sending Error
        /// </summary>
        [Description("Request Market Depth Sending Error")] FailSendRequestMarketDepth = 519,
        /// <summary>
        /// Cancel Market Depth Sending Error
        /// </summary>
        [Description("Cancel Market Depth Sending Error")] FailSendCancelMarketDepth = 520,
        /// <summary>
        /// Set Server Log Level Sending Error
        /// </summary>
        [Description("Set Server Log Level Sending Error")] FailSendServerLogLevel = 521,
        /// <summary>
        /// FA Information Request Sending Error
        /// </summary>
        [Description("FA Information Request Sending Error")] FailSendFARequest = 522,
        /// <summary>
        /// FA Information Replace Sending Error
        /// </summary>
        [Description("FA Information Replace Sending Error")] FailSendFAReplace = 523,
        /// <summary>
        /// Request Scanner Subscription Sending Error
        /// </summary>
        [Description("Request Scanner Subscription Sending Error")] FailSendRequestScanner = 524,
        /// <summary>
        /// Cancel Scanner Subscription Sending Error
        /// </summary>
        [Description("Cancel Scanner Subscription Sending Error")] FailSendCancelScanner = 525,
        /// <summary>
        /// Request Scanner Parameter Sending Error
        /// </summary>
        [Description("Request Scanner Parameter Sending Error")] FailSendRequestScannerParameters = 526,
        /// <summary>
        /// Request Historical Data Sending Error
        /// </summary>
        [Description("Request Historical Data Sending Error")] FailSendRequestHistoricalData = 527,
        /// <summary>
        /// Cancel Historical Data Sending Error
        /// </summary>
        [Description("Cancel Historical Data Sending Error")] FailSendCancelHistoricalData = 528,
        /// <summary>
        /// Request Real-time Bar Data Sending Error
        /// </summary>
        [Description("Request Real-time Bar Data Sending Error")] FailSendRequestRealTimeBars = 529,
        /// <summary>
        /// Cancel Real-time Bar Data Sending Error
        /// </summary>
        [Description("Cancel Real-time Bar Data Sending Error")] FailSendCancelRealTimeBars = 530,
        /// <summary>
        /// Request Current Time Sending Error
        /// </summary>
        [Description("Request Current Time Sending Error")] FailSendRequestCurrentTime = 531,
        /// <summary>
        /// Failed to send fundamental data
        /// </summary>
        [Description("Request Fundamental Data Sending Error")] FailSendRequestFundData = 532,
        /// <summary>
        /// Cancel Fundamental Data Sending Error
        /// </summary>
        [Description("Cancel Fundamental Data Sending Error")] FailSendCancelFundData = 533,
        /// <summary>
        /// Failed to send Request to Calculate Implied Volatility
        /// </summary>
        [Description("Request Calculate Implied Volatility Sending Error")] FailSendReqCalcImpliedVolatility = 534,
        /// <summary>
        /// Request Calculate Option Price Sending Error
        /// </summary>
        [Description("Request Calculate Option Price Sending Error")] FailSendRequestCalcOptionPrice = 535,
        /// <summary>
        /// Cancel Calculate Implied Volatility Sending Error
        /// </summary>
        [Description("Cancel Calculate Implied Volatility Sending Error")] FailSendCancelCalculateImpliedVolatility = 536,
        /// <summary>
        /// Cancel Calculate Option Price Sending Error
        /// </summary>
        [Description("Cancel Calculate Option Price Sending Error")] FailSendCancelCalculateOptionPrice = 537,
        /// <summary>
        /// Request Global Cancel Sending Error
        /// </summary>
        [Description("Request Global Cancel Sending Error")] FailSendRequestGlobalCancel = 538,
        /// <summary>
        /// Request Market Data Type Sending Error
        /// </summary>
        [Description("Request Market Data Type Sending Error")] FailSendRequestMarketDataType = 539,
        /// <summary>
        /// No Valid ID for error message
        /// </summary>
        [Description("No Valid Id")] NoValidId = -1
    }
}