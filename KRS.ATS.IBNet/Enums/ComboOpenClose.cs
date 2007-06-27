using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Retail Customers are restricted to "SAME"
    /// Institutional Customers may use "SAME", "OPEN", "CLOSE", "UNKNOWN"
    /// </summary>
    public enum ComboOpenClose : int
    {
        [Description("SAME")]
        /// <summary>
        /// open/close leg value is same as combo
        /// This value is always used for retail accounts
        /// </summary>
        Same = 0,
        /// <summary>
        /// Institutional Accounts Only
        /// </summary>
        [Description("OPEN")]
        Open = 1,
        /// <summary>
        /// Institutional Accounts Only
        /// </summary>
        [Description("CLOSE")]
        Close = 2,
        /// <summary>
        /// Institutional Accounts Only
        /// </summary>
        [Description("UNKNOWN")]
        Unknown = 3,
    }
}
