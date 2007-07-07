using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Option Right Type (Put or Call)
    /// </summary>
    public enum RightType
    {
        /// <summary>
        /// Option type is a Put (Right to sell)
        /// </summary>
        [Description("PUT")] Put,
        /// <summary>
        /// Option type is a Call (Right to buy)
        /// </summary>
        [Description("CALL")] Call,
        /// <summary>
        /// Option type is not defined (contract is not an option).
        /// </summary>
        [Description("")] Undefined
    }
}