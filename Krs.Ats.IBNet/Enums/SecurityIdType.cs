using System;
using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Historical Bar Size Requests
    /// </summary>
    [Serializable()]
    public enum SecurityIdType : int
    {
        /// <summary>
        /// No Security Id Type
        /// </summary>
        [Description("")] None,
        /// <summary>
        /// Example: Apple: US0378331005
        /// </summary>
        [Description("ISIN")] ISIN,
        /// <summary>
        /// Example: Apple: 037833100
        /// </summary>
        [Description("CUSIP")] CUSIP,
        /// <summary>
        /// Consists of 6-AN + check digit. Example: BAE: 0263494
        /// </summary>
        [Description("SEDOL")] SEDOL,
        /// <summary>
        /// Consists of exchange-independent RIC Root and a suffix identifying the exchange. Example: AAPL.O for Apple on NASDAQ.
        /// </summary>
        [Description("RIC")] RIC
    }
}
