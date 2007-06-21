using System;
using System.Collections.Generic;
using System.Text;

namespace KRS.ATS.IBNet
{
    /// <summary>
    /// Open Order Event Arguments
    /// </summary>
    public class OpenOrderEventArgs : EventArgs
    {
        private readonly int orderId;
        public int OrderId
        {
            get
            {
                return orderId;
            }
        }

        private readonly Contract contract;
        public Contract Contract
        {
            get
            {
                return contract;
            }
        }

        private readonly Order order;
        public Order Order
        {
            get
            {
                return order;
            }
        }

        public OpenOrderEventArgs(int orderId, Contract contract, Order order)
        {
            this.orderId = orderId;
            this.order = order;
            this.contract = contract;
        }
    }
}
