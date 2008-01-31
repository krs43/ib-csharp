using System;
using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Financial Advisor Data Message
    /// </summary>
    [Serializable()] 
    public enum FADataType : int
    {
        /// <summary>
        /// Undefined FA Message Type
        /// </summary>
        [Description("")] Undefined = 0,
        /// <summary>
        /// Financial Advisor Groups
        /// </summary>
        [Description("GROUPS")] Groups = 1,
        /// <summary>
        /// Financial Advisor Profiles
        /// </summary>
        [Description("PROFILES")] Profiles = 2,
        /// <summary>
        /// Financial Advisor Aliases
        /// </summary>
        [Description("ALIASES")] Aliases = 3
    }
}