using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    public enum OCAType : int
    {
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
