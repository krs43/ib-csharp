using System;
using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Real Time Bar Type - "What to Show"
    /// </summary>
    [Serializable()]
    public enum RealTimeBarType
    {
        /// <summary>
        /// Trades
        /// </summary>
        [Description("TRADES")]
        Trades,
        /// <summary>
        /// Bid
        /// </summary>
        [Description("BID")]
        Bid,
        /// <summary>
        /// Ask
        /// </summary>
        [Description("ASK")]
        Ask,
        /// <summary>
        /// Mid Point
        /// </summary>
        [Description("MIDPOINT")]
        Midpoint
    }
}
