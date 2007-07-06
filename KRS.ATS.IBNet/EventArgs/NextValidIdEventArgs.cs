using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Next Valid Id Event Arguments
    /// </summary>
    public class NextValidIdEventArgs : EventArgs
    {
        private readonly int orderId;

        /// <summary>
        /// The next available order Id received from TWS upon connection.
        /// Increment all successive orders by one based on this Id.
        /// </summary>
        public int OrderId
        {
            get
            {
                return orderId;
            }
        }

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="orderId">The next available order Id received from TWS upon connection.
        /// Increment all successive orders by one based on this Id.</param>
        public NextValidIdEventArgs(int orderId)
        {
            this.orderId = orderId;
        }
    }
}
