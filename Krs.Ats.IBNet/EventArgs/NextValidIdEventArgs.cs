using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Next Valid Id Event Arguments
    /// </summary>
    [Serializable()]
    public class NextValidIdEventArgs : EventArgs
    {
        private int orderId;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="orderId">The next available order Id received from TWS upon connection.
        /// Increment all successive orders by one based on this Id.</param>
        public NextValidIdEventArgs(int orderId)
        {
            this.orderId = orderId;
        }

        /// <summary>
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public NextValidIdEventArgs()
        {
            
        }

        /// <summary>
        /// The next available order Id received from TWS upon connection.
        /// Increment all successive orders by one based on this Id.
        /// </summary>
        public int OrderId
        {
            get { return orderId; }
			set { orderId = value; }
		}
    }
}