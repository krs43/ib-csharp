using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Used in a combination leg for Short Sale Orders.
    /// </summary>
    [Serializable()]
    public enum ShortSaleSlot : int
    {
        /// <summary>
        /// e.g. retail customer or not SSHORT leg
        /// </summary>
        Unapplicable = 0,
        /// <summary>
        /// Clearing Broker
        /// </summary>
        ClearingBroker = 1,
        /// <summary>
        /// Third Party
        /// </summary>
        ThirdParty = 2
    }
}
