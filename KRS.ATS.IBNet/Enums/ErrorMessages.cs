using System.ComponentModel;
using System;

namespace Krs.Ats.IBNet
{
    [System.ComponentModel.TypeConverter(typeof(Krs.Ats.IBNet.EnumDescConverter))]
    public enum ErrorMessages : int
    {
        [Description("Already connected.")]
        AlreadyConnected = 501,
        [Description("Couldn't connect to TWS.  Confirm that \"Enable ActiveX and Socket Clients\" is enabled on the TWS \"Configure->API\" menu.")]
        ConnectFail = 502,
        [Description("The TWS is out of date and must be upgraded.")]
        UpdateTws = 503,
        [Description("Not connected")]
        NotConnected = 504,
        [Description("Fatal Error: Unknown message id.")]
        UnknownID = 505,
        [Description("Request Market Data Sending Error")]
        FailSendRequestMarket = 510,
        [Description("Cancel Market Data Sending Error")]
        FailSendCancelMarket = 511,
        [Description("Order Sending Error")]
        FailSendOrder = 512,
        [Description("Account Update Request Sending Error")]
        FailSendAccountUpdate = 513,
        [Description("Request For Executions Sending Error")]
        FailSendExecution = 514,
        [Description("Cancel Order Sending Error")]
        FailSendCancelOrder = 515,
        [Description("Request Open Order Sending Error")]
        FailSendOpenOrder = 516,
        [Description("Unknown contract. Verify the contract details supplied.")]
        UnknownContract = 517,
        [Description("Request Contract Data Sending Error")]
        FailSendRequestContract = 518,
        [Description("Request Market Depth Sending Error")]
        FailSendRequestMarketDepth = 519,
        [Description("Cancel Market Depth Sending Error")]
        FailSendCancelMarketDepth = 520,
        [Description("Set Server Log Level Sending Error")]
        FailSendServerLogLevel = 521,
        [Description("FA Information Request Sending Error")]
        FailSendFARequest = 522,
        [Description("FA Information Replace Sending Error")]
        FailSendFAReplace = 523,
        [Description("Request Scanner Subscription Sending Error")]
        FailSendRequestScanner = 524,
        [Description("Cancel Scanner Subscription Sending Error")]
        FailSendCancelScanner = 525,
        [Description("Request Scanner Parameter Sending Error")]
        FailSendRequestScannerParameters = 526,
        [Description("Request Historical Data Sending Error")]
        FailSendRequestHistoricalData = 527,
        [Description("")]
        NoValidId = -1
    }
}