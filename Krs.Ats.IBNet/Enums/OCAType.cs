using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// OCA Type Options
    /// </summary>
    [Serializable()] 
    public enum OcaType : int
    {
        /// <summary>
        /// Undefined Oca Type
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// 1 = Cancel all remaining orders with block
        /// </summary>
        CancelAll = 1,
        /// <summary>
        /// 2 = Remaining orders are proportionately reduced in size with block
        /// </summary>
        ReduceWithBlock = 2,
        /// <summary>
        /// 3 = Remaining orders are proportionately reduced in size with no block
        /// </summary>
        ReduceWithNoBlock = 3
    }
}