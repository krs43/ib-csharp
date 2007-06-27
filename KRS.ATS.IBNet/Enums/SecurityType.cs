using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Contract Security Types
    /// </summary>
    public enum SecurityType
    {
        /// <summary>
        /// Stock
        /// </summary>
        [Description("STK")]
        Stock,
        /// <summary>
        /// Option
        /// </summary>
        [Description("OPT")]
        Option,
        /// <summary>
        /// Future
        /// </summary>
        [Description("FUT")]
        Future,
        /// <summary>
        /// Indice
        /// </summary>
        [Description("IND")]
        Indice,
        /// <summary>
        /// 
        /// </summary>
        [Description("FOP")]
        FOP,
        /// <summary>
        /// Cash
        /// </summary>
        [Description("CASH")]
        Cash,
        /// <summary>
        /// For Combination Orders - must use combo leg details
        /// </summary>
        [Description("BAG")]
        Bag,
        /// <summary>
        /// Bond
        /// </summary>
        [Description("BOND")]
        Bond,
        /// <summary>
        /// Undefined Security Type
        /// </summary>
        [Description("")]
        Undefined
    }
}
