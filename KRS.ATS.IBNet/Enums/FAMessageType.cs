using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Financial Advisor Data Message
    /// </summary>
    public enum FAMessageType : int
    {
        /// <summary>
        /// Financial Advisor Groups
        /// </summary>
        [Description("GROUPS")]
        Groups = 1,
        /// <summary>
        /// Financial Advisor Profiles
        /// </summary>
        [Description("PROFILES")]
        Profiles = 2,
        /// <summary>
        /// Financial Advisor Aliases
        /// </summary>
        [Description("ALIASES")]
        Aliases = 3
    }
}
