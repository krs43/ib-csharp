using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Time frame for Volatility
    /// </summary>
    public enum VolatilityType : int
    {
        /// <summary>
        /// Undefined Volatility
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Daily Average Volatility
        /// </summary>
        Daily = 1,
        /// <summary>
        /// Annual Average Volatility
        /// </summary>
        Annual = 2
    }
}
