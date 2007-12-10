using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Order Origin Fields
    /// </summary>
    [Serializable()] 
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