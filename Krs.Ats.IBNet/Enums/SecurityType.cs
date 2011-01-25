using System;
using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Contract Security Types
    /// </summary>
    [Serializable()] 
    public enum SecurityType
    {
        /// <summary>
        /// Stock
        /// </summary>
        [Description("STK")] Stock,
        /// <summary>
        /// Option
        /// </summary>
        [Description("OPT")] Option,
        /// <summary>
        /// Future
        /// </summary>
        [Description("FUT")] Future,
        /// <summary>
        /// Indice
        /// </summary>
        [Description("IND")] Index,
        /// <summary>
        /// FOP = options on futures
        /// </summary>
        [Description("FOP")] FutureOption,
        /// <summary>
        /// Cash
        /// </summary>
        [Description("CASH")] Cash,
        /// <summary>
        /// For Combination Orders - must use combo leg details
        /// </summary>
        [Description("BAG")] Bag,
        /// <summary>
        /// Bond
        /// </summary>
        [Description("BOND")] Bond,
        /// <summary>
        /// Warrant
        /// </summary>
        [Description("WAR")] Warrant,
        /// <summary>
        /// Undefined Security Type
        /// </summary>
        [Description("")] Undefined
    }
}