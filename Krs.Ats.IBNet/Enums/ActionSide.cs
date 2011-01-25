using System;
using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Order  Action Side. Specifies whether securities should be bought or sold.
    /// </summary>
    [Serializable()]
    public enum ActionSide
    {
        /// <summary>
        /// Security is to be bought.
        /// </summary>
        [Description("BUY")] Buy,
        /// <summary>
        /// Security is to be sold.
        /// </summary>
        [Description("SELL")] Sell,
        /// <summary>
        /// Undefined
        /// </summary>
        [Description("")] Undefined,
        /// <summary>
        /// Sell Short as part of a combo leg
        /// </summary>
        [Description("SSHORT")] SShort,
        /// <summary>
        /// Short Sale Exempt action.
        /// SSHORTX allows some orders to be marked as exempt from the new SEC Rule 201
        /// </summary>
        [Description("SSHORTX")] SShortX
    }
}