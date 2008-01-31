using System;
using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Historical Data Request Return Types
    /// </summary>
    [Serializable()] 
    public enum HistoricalDataType
    {
        /// <summary>
        /// Return Trade data only
        /// </summary>
        [Description("TRADES")]
        Trades,
        /// <summary>
        /// Return the mid point between the bid and ask
        /// </summary>
        [Description("MIDPOINT")]
        Midpoint,
        /// <summary>
        /// Return Bid Prices only
        /// </summary>
        [Description("BID")]
        Bid,
        /// <summary>
        /// Return ask prices only
        /// </summary>
        [Description("ASK")]
        Ask,
        /// <summary>
        /// Return Bid / Ask price only
        /// </summary>
        [Description("BID/ASK")]
        BidAsk,
    }
}
