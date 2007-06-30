using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// incoming msg id's
    /// </summary>
    public enum IncomingMessage : int
    {
        /// <summary>
        /// Undefined Incoming Message
        /// </summary>
        [Description("")]
        Undefined = 0,
        /// <summary>
        /// Error
        /// </summary>
        [Description("ERROR")]
        Error = -1,
        /// <summary>
        /// Tick Price
        /// </summary>
        [Description("TICK_PRICE")]
        TickPrice = 1,
        /// <summary>
        /// Tick Size
        /// </summary>
        [Description("TICK_SIZE")]
        TickSize = 2,
        /// <summary>
        /// Order Status
        /// </summary>
        [Description("ORDER_STATUS")]
        OrderStatus = 3,
        /// <summary>
        /// Error Message
        /// </summary>
        [Description("ERR_MSG")]
        ErrorMessage = 4,
        /// <summary>
        /// Open Order
        /// </summary>
        [Description("OPEN_ORDER")]
        OpenOrder = 5,
        /// <summary>
        /// Account Value
        /// </summary>
        [Description("ACCT_VALUE")]
        AccountValue = 6,
        /// <summary>
        /// Portfolio Value
        /// </summary>
        [Description("PORTFOLIO_VALUE")]
        PortfolioValue = 7,
        /// <summary>
        /// Account Update Time
        /// </summary>
        [Description("ACCT_UPDATE_TIME")]
        AccountUpdateTime = 8,
        /// <summary>
        /// Next Valid ID
        /// </summary>
        [Description("NEXT_VALID_ID")]
        NextValidId = 9,
        /// <summary>
        /// Contract Data
        /// </summary>
        [Description("CONTRACT_DATA")]
        ContractData = 10,
        /// <summary>
        /// Execution Data
        /// </summary>
        [Description("EXECUTION_DATA")]
        ExecutionData = 11,
        /// <summary>
        /// Market Depth
        /// </summary>
        [Description("MARKET_DEPTH")]
        MarketDepth = 12,
        /// <summary>
        /// Market Depth L2
        /// </summary>
        [Description("MARKET_DEPTH_L2")]
        MarketDepthL2 = 13,
        /// <summary>
        /// News Bulletins
        /// </summary>
        [Description("NEWS_BULLETINS")]
        NewsBulletins = 14,
        /// <summary>
        /// Managed Accounts
        /// </summary>
        [Description("MANAGED_ACCTS")]
        ManagedAccounts = 15,
        /// <summary>
        /// Receive Financial Advice
        /// </summary>
        [Description("RECEIVE_FA")]
        ReceiveFA = 16,
        /// <summary>
        /// Historical Data
        /// </summary>
        [Description("HISTORICAL_DATA")]
        HistoricalData = 17,
        /// <summary>
        /// Bond Contract Data
        /// </summary>
        [Description("BOND_CONTRACT_DATA")]
        BondContractData = 18,
        /// <summary>
        /// Scanner Parameters
        /// </summary>
        [Description("SCANNER_PARAMETERS")]
        ScannerParameters = 19,
        /// <summary>
        /// Scanner Data
        /// </summary>
        [Description("SCANNER_DATA")]
        ScannerData = 20,
        /// <summary>
        /// Tick Option Computation
        /// </summary>
        [Description("TICK_OPTION_COMPUTATION")]
        TickOptionComputation = 21,
        /// <summary>
        /// Tick Generic
        /// </summary>
        [Description("TICK_GENERIC")]
        TickGeneric = 45,
        /// <summary>
        /// Tick String
        /// </summary>
        [Description("TICK_STRING")]
        TickString = 46,
        /// <summary>
        /// Tick Exchange for Physical(EFP)
        /// </summary>
        [Description("TICK_EFP")]
        TickEfp = 47
    }
}