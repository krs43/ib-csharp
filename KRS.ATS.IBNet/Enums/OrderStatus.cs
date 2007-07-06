using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Order Status reported by enum order status.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// indicates that you have transmitted the order, but have not yet received
        /// confirmation that it has been accepted by the order destination.
        /// This order status is not sent by TWS and should be explicitly set by the API developer when an order is submitted.
        /// </summary>
        PendingSubmit,
        /// <summary>
        /// PendingCancel - indicates that you have sent a request to cancel the order
        /// but have not yet received cancel confirmation from the order destination.
        /// At this point, your order is not confirmed canceled. You may still receive
        /// an execution while your cancellation request is pending.
        /// This order status is not sent by TWS and should be explicitly set by the API developer when an order is canceled.
        /// </summary>
        PendingCancel,
        /// <summary>
        /// indicates that a simulated order type has been accepted by the IB system and
        /// that this order has yet to be elected. The order is held in the IB system
        /// (and the status remains DARK BLUE) until the election criteria are met.
        /// At that time the order is transmitted to the order destination as specified
        /// (and the order status color will change).
        /// </summary>
        PreSubmitted,
        /// <summary>
        /// indicates that your order has been accepted at the order destination and is working.
        /// </summary>
        Submitted,
        /// <summary>
        /// indicates that the balance of your order has been confirmed canceled by the IB system.
        /// This could occur unexpectedly when IB or the destination has rejected your order.
        /// </summary>
        Cancelled,
        /// <summary>
        /// The order has been completely filled.
        /// </summary>
        Filled
    }
}
