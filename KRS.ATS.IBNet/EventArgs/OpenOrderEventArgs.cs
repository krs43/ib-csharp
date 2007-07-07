using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Open Order Event Arguments
    /// </summary>
    public class OpenOrderEventArgs : EventArgs
    {
        private readonly Contract contract;
        private readonly Order order;
        private readonly int orderId;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="orderId">The order Id assigned by TWS. Used to cancel or update the order.</param>
        /// <param name="contract">Describes the contract for the open order.</param>
        /// <param name="order">Gives the details of the open order.</param>
        public OpenOrderEventArgs(int orderId, Contract contract, Order order)
        {
            this.orderId = orderId;
            this.order = order;
            this.contract = contract;
        }

        /// <summary>
        /// The order Id assigned by TWS. Used to cancel or update the order.
        /// </summary>
        public int OrderId
        {
            get { return orderId; }
        }

        /// <summary>
        /// Describes the contract for the open order.
        /// </summary>
        public Contract Contract
        {
            get { return contract; }
        }

        /// <summary>
        /// Gives the details of the open order.
        /// </summary>
        public Order Order
        {
            get { return order; }
        }
    }
}