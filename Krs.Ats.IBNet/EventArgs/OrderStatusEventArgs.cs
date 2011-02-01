using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Order Status Event Arguments
    /// </summary>
    [Serializable()]
    public class OrderStatusEventArgs : EventArgs
    {
        private decimal averageFillPrice;
        private int clientId;
        private int filled;
        private decimal lastFillPrice;
        private int orderId;
        private int parentId;
        private int permId;
        private int remaining;
        private string whyHeld;

        private OrderStatus status;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="orderId">The order Id that was specified previously in the call to placeOrder().</param>
        /// <param name="status">The order status.</param>
        /// <param name="filled">Specifies the number of shares that have been executed.</param>
        /// <param name="remaining">Specifies the number of shares still outstanding.</param>
        /// <param name="averageFillPrice">The average price of the shares that have been executed.
        /// This parameter is valid only if the filled parameter value
        /// is greater than zero. Otherwise, the price parameter will be zero.</param>
        /// <param name="permId">The TWS id used to identify orders. Remains the same over TWS sessions.</param>
        /// <param name="parentId">The order ID of the parent order, used for bracket and auto trailing stop orders.</param>
        /// <param name="lastFillPrice">The last price of the shares that have been executed. This parameter is valid
        /// only if the filled parameter value is greater than zero. Otherwise, the price parameter will be zero.</param>
        /// <param name="clientId">The ID of the client (or TWS) that placed the order.
        /// The TWS orders have a fixed clientId and orderId of 0 that distinguishes them from API orders.</param>
        /// <param name="whyHeld">This field is used to identify an order held when TWS is trying to locate shares for a short sell.
        /// The value used to indicate this is 'locate'.</param>
        public OrderStatusEventArgs(int orderId, OrderStatus status, int filled, int remaining, decimal averageFillPrice,
                                    int permId, int parentId, decimal lastFillPrice, int clientId, string whyHeld)
        {
            this.orderId = orderId;
            this.clientId = clientId;
            this.lastFillPrice = lastFillPrice;
            this.parentId = parentId;
            this.permId = permId;
            this.averageFillPrice = averageFillPrice;
            this.remaining = remaining;
            this.filled = filled;
            this.status = status;
            this.whyHeld = whyHeld;
        }

        ///<summary>
        /// Parameterless OrderStatusEventArgs Constructor for serialization
        ///</summary>
        public OrderStatusEventArgs()
        {
            this.orderId = -1;
            this.clientId = -1;
            this.lastFillPrice = -1;
            this.parentId = -1;
            this.permId = -1;
            this.averageFillPrice = -1;
            this.remaining = -1;
            this.filled = -1;
            this.status = OrderStatus.Error; //OrderStatus.None;
            this.whyHeld = "";
        }
        
        /// <summary>
        /// The order Id that was specified previously in the call to placeOrder().
        /// </summary>
        public int OrderId
        {
            get { return orderId; }
			set { orderId = value; }
		}

        /// <summary>
        /// The order status.
        /// </summary>
        /// <remarks>Possible values include:
        /// <list type="table">
        /// <listheader>
        /// <term>Status</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>PendingSubmit</term>
        /// <description>indicates that you have transmitted the order, but have not yet received confirmation that it has been accepted by the order destination. This order status is not sent by TWS and should be explicitly set by the API developer when an order is submitted.</description>
        /// </item>
        /// <item>
        /// <term>PendingCancel</term>
        /// <description>Indicates that you have sent a request to cancel the order but have not yet received cancel confirmation from the order destination. At this point, your order is not confirmed canceled. You may still receive an execution while your cancellation request is pending. This order status is not sent by TWS and should be explicitly set by the API developer when an order is canceled.</description>
        /// </item>
        /// <item>
        /// <term>PreSubmitted</term>
        /// <description>Indicates that a simulated order type has been accepted by the IB system and that this order has yet to be elected. The order is held in the IB system (and the status remains DARK BLUE) until the election criteria are met. At that time the order is transmitted to the order destination as specified (and the order status color will change).</description>
        /// </item>
        /// <item>
        /// <term>Submitted</term>
        /// <description>Indicates that your order has been accepted at the order destination and is working.</description>
        /// </item>
        /// <item>
        /// <term>Cancelled</term>
        /// <description>Indicates that the balance of your order has been confirmed canceled by the IB system. This could occur unexpectedly when IB or the destination has rejected your order.</description>
        /// </item>
        /// <item>
        /// <term>Filled</term>
        /// <description>The order has been completely filled.</description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <seealso cref="OrderStatus"/>
        public OrderStatus Status
        {
            get { return status; }
			set { status = value; }
		}

        /// <summary>
        /// Specifies the number of shares that have been executed.
        /// </summary>
        public int Filled
        {
            get { return filled; }
			set { filled = value; }
		}

        /// <summary>
        /// Specifies the number of shares still outstanding.
        /// </summary>
        public int Remaining
        {
            get { return remaining; }
			set { remaining = value; }
		}

        /// <summary>
        /// The average price of the shares that have been executed.
        /// This parameter is valid only if the filled parameter value
        /// is greater than zero. Otherwise, the price parameter will be zero.
        /// </summary>
        public decimal AverageFillPrice
        {
            get { return averageFillPrice; }
			set { averageFillPrice = value; }
		}

        /// <summary>
        /// The TWS id used to identify orders. Remains the same over TWS sessions.
        /// </summary>
        public int PermId
        {
            get { return permId; }
			set { permId = value; }
		}

        /// <summary>
        /// The order ID of the parent order, used for bracket and auto trailing stop orders.
        /// </summary>
        public int ParentId
        {
            get { return parentId; }
			set { parentId = value; }
		}

        /// <summary>
        /// The last price of the shares that have been executed. This parameter is valid
        /// only if the filled parameter value is greater than zero. Otherwise, the price parameter will be zero.
        /// </summary>
        public decimal LastFillPrice
        {
            get { return lastFillPrice; }
			set { lastFillPrice = value; }
		}

        /// <summary>
        /// The ID of the client (or TWS) that placed the order.
        /// The TWS orders have a fixed clientId and orderId of 0 that distinguishes them from API orders.
        /// </summary>
        public int ClientId
        {
            get { return clientId; }
			set { clientId = value; }
		}

        /// <summary>
        /// This field is used to identify an order held when TWS is trying to locate shares for a short sell.
        /// The value used to indicate this is 'locate'.
        /// </summary>
        /// <remarks>This field is supported starting with TWS release 872.</remarks>
        public string WhyHeld
        {
            get { return whyHeld; }
			set { whyHeld = value; }
		}
    }
}