using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Order Time in Force Values
    /// </summary>
    public enum TimeInForce
    {
        /// <summary>
        /// Day
        /// </summary>
        [Description("DAY")]
        Day,
        /// <summary>
        /// Good Till Close
        /// </summary>
        [Description("GTC")]
        GoodTillClose,
        /// <summary>
        /// You can set the time in force for MARKET or LIMIT orders as IOC. This dictates that any portion of the order not executed immediately after it becomes available on the market will be cancelled.
        /// </summary>
        [Description("IOC")]
        ImmediateOrCancel,
        /// <summary>
        /// Good Till Date
        /// </summary>
        [Description("GTD")]
        GoodTillDate
    }
}
