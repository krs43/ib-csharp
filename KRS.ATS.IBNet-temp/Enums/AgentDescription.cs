using System;
using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Used for Rule 80A describes the type of trader.
    /// </summary>
    [Serializable()] 
    public enum AgentDescription
    {
        /// <summary>
        /// An individual
        /// </summary>
        [Description("I")] Individual,
        /// <summary>
        /// An Agency
        /// </summary>
        [Description("A")] Agency,
        /// <summary>
        /// An Agent or Other Member
        /// </summary>
        [Description("W")] AgentOtherMember,
        /// <summary>
        /// Individual PTIA
        /// </summary>
        [Description("J")] IndividualPTIA,
        /// <summary>
        /// Agency PTIA
        /// </summary>
        [Description("U")] AgencyPTIA,
        /// <summary>
        /// Agether or Other Member PTIA
        /// </summary>
        [Description("M")] AgentOtherMemberPTIA,
        /// <summary>
        /// Individual PT
        /// </summary>
        [Description("K")] IndividualPT,
        /// <summary>
        /// Agency PT
        /// </summary>
        [Description("Y")] AgencyPT,
        /// <summary>
        /// Agent Other Member PT
        /// </summary>
        [Description("N")] AgentOtherMemberPT,
        /// <summary>
        /// No Description Provided
        /// </summary>
        [Description("")] None
    }
}