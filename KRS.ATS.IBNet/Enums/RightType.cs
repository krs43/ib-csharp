using System;
using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Option Right Type (Put or Call)
    /// </summary>
    [Serializable()]
    public enum RightType
    {
        /// <summary>
        /// Option type is a Put (Right to sell)
        /// </summary>
        /// Description tag used to be "PUT"
        [Description("P")] Put,
        /// <summary>
        /// Option type is a Call (Right to buy)
        /// </summary>
        /// Description tag used to be "CALL"
        [Description("C")] Call,
        /// <summary>
        /// Option type is not defined (contract is not an option).
        /// </summary>
        [Description("")] Undefined
    }
}