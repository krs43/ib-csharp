using System;
using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Describes wether a security was bought or sold in an execution.
    /// The past tense equivalent of ActionSide.
    /// </summary>
    [Serializable()] 
    public enum ExecutionSide
    {
        /// <summary>
        /// Securities were bought.
        /// </summary>
        [Description("BOT")] Bought,
        /// <summary>
        /// Securities were sold.
        /// </summary>
        [Description("SLD")] Sold
    }
}