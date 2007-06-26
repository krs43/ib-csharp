namespace Krs.Ats.IBNet
{
    /// <summary>
    /// incoming msg id's
    /// </summary>
    public enum IncomingMessage : int
    {
        ERROR = -1,
        TICK_PRICE = 1,
        TICK_SIZE = 2,
        ORDER_STATUS = 3,
        ERR_MSG = 4,
        OPEN_ORDER = 5,
        ACCT_VALUE = 6,
        PORTFOLIO_VALUE = 7,
        ACCT_UPDATE_TIME = 8,
        NEXT_VALID_ID = 9,
        CONTRACT_DATA = 10,
        EXECUTION_DATA = 11,
        MARKET_DEPTH = 12,
        MARKET_DEPTH_L2 = 13,
        NEWS_BULLETINS = 14,
        MANAGED_ACCTS = 15,
        RECEIVE_FA = 16,
        HISTORICAL_DATA = 17,
        BOND_CONTRACT_DATA = 18,
        SCANNER_PARAMETERS = 19,
        SCANNER_DATA = 20,
        TICK_OPTION_COMPUTATION = 21,
        TICK_GENERIC = 45,
        TICK_STRING = 46,
        TICK_EFP = 47
    }
}