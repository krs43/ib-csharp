using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Open Order Event Arguments
    /// </summary>
    [Serializable()]
    public class OpenOrderEventArgs : EventArgs
    {
        private Contract contract;
        private Order order;
        private int orderId;
        private OrderState orderState;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="orderId">The order Id assigned by TWS. Used to cancel or update the order.</param>
        /// <param name="contract">Describes the contract for the open order.</param>
        /// <param name="order">Gives the details of the open order.</param>
        /// <param name="orderState">The openOrder() callback with the new OrderState() object will now be invoked each time TWS receives commission information for a trade.</param>
        public OpenOrderEventArgs(int orderId, Contract contract, Order order, OrderState orderState)
        {
            this.orderId = orderId;
            this.order = order;
            this.contract = contract;
            this.orderState = orderState;
        }

        ///<summary>
        /// Parameterless OpenOrderEventArgs Constructor
        ///</summary>
        public OpenOrderEventArgs()
        {
            this.orderId = -1;
            this.order = new Order();
            this.contract = new Contract();
            this.orderState = new OrderState();
        }


        /// <summary>
        /// The order Id assigned by TWS. Used to cancel or update the order.
        /// </summary>
        public int OrderId
        {
            get { return orderId; }
			set { orderId = value; }
		}

        /// <summary>
        /// Describes the contract for the open order.
        /// </summary>
        public Contract Contract
        {
            get { return contract; }
			set { contract = value; }
		}

        /// <summary>
        /// Gives the details of the open order.
        /// </summary>
        public Order Order
        {
            get { return order; }
			set { order = value; }
		}

        /// <summary>
        /// The openOrder() callback with the new OrderState() object will
        /// now be invoked each time TWS receives commission information for a trade.
        /// </summary>
        public OrderState OrderState
        {
            get { return orderState; }
			set { orderState = value; }
		}
    }
}