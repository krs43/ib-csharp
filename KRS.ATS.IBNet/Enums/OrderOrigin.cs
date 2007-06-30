using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Order Origin Fields
    /// </summary>
    public enum OrderOrigin : int
    {
        /// <summary>
        /// Order originated from the customer
        /// </summary>
        Customer = 0,
        /// <summary>
        /// Order originated from teh firm
        /// </summary>
        Firm = 1
    }
}
