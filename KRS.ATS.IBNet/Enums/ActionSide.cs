using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Order  Action Side. Specifies whether securities should be bought or sold.
    /// </summary>
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
        [Description("")] Undefined
        /* DEPRECATED
         * [Description("SSHORT")]
         * SShort*/
    }
}