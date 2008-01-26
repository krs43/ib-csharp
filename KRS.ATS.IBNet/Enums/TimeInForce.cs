using System;
using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Order Time in Force Values
    /// </summary>
    [Serializable()] 
    public enum TimeInForce
    {
        /// <summary>
        /// Day
        /// </summary>
        [Description("DAY")] Day,
        /// <summary>
        /// Good Till Cancel
        /// </summary>
        [Description("GTC")] GoodTillCancel,
        /// <summary>
        /// You can set the time in force for MARKET or LIMIT orders as IOC. This dictates that any portion of the order not executed immediately after it becomes available on the market will be cancelled.
        /// </summary>
        [Description("IOC")] ImmediateOrCancel,
        /// <summary>
        /// Setting FOK as the time in force dictates that the entire order must execute immediately or be canceled.
        /// </summary>
        [Description("FOK")] FillOrKill,
        /// <summary>
        /// Good Till Date
        /// </summary>
        [Description("GTD")] GoodTillDate,
        /// <summary>
        /// Market On Open
        /// </summary>
        [Description("OPG")] MarketOnOpen,
        /// <summary>
        /// Undefined
        /// </summary>
        [Description("")] Undefined
    }
}