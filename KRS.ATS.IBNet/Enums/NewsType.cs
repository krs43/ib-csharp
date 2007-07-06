using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// News Message Type
    /// </summary>
    public enum NewsType : int 
    {
        /// <summary>
        /// Reqular news bulletin
        /// </summary>
        Regular = 1,
        /// <summary>
        /// Exchange no longer available for trading
        /// </summary>
        ExchangeUnavailable = 2,
        /// <summary>
        /// Exchange is available for trading
        /// </summary>
        ExchangeAvailable = 3 
    }
}
