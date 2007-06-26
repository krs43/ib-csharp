using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Order Status Event Arguments
    /// </summary>
    public class OrderStatusEventArgs : EventArgs
    {
        private readonly int orderId;
        public int OrderId
        {
            get
            {
                return orderId;
            }
        }

        private readonly string status;
        public string Status
        {
            get
            {
                return status;
            }
        }

        private readonly int filled;
        public int Filled
        {
            get
            {
                return filled;
            }
        }

        private readonly int remaining;
        public int Remaining
        {
            get
            {
                return remaining;
            }
        }

        private readonly double avgFillPrice;
        public double AvgFillPrice
        {
            get
            {
                return avgFillPrice;
            }
        }

        private readonly int permId;
        public int PermId
        {
            get
            {
                return permId;
            }
        }

        private readonly int parentId;
        public int ParentId
        {
            get
            {
                return parentId;
            }
        }

        private readonly double lastFillPrice;
        public double LastFillPrice
        {
            get
            {
                return lastFillPrice;
            }
        }

        private readonly int clientId;
        public int ClientId
        {
            get
            {
                return clientId;
            }
        }

        public OrderStatusEventArgs(int orderId, string status, int filled, int remaining, double avgFillPrice, int permId, int parentId, double lastFillPrice, int clientId)
        {
            this.orderId = orderId;
            this.clientId = clientId;
            this.lastFillPrice = lastFillPrice;
            this.parentId = parentId;
            this.permId = permId;
            this.avgFillPrice = avgFillPrice;
            this.remaining = remaining;
            this.filled = filled;
            this.status = status;
        }
    }
}
