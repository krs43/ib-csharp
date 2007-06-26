using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Tick Price Event Arguments
    /// </summary>
    public class TickPriceEventArgs : EventArgs
    {
        private readonly int tickerId;
        public int TickerId
        {
            get
            {
                return this.tickerId;
            }
        }

        private readonly int field;
        public int Field
        {
            get
            {
                return this.field;
            }
        }

        private readonly double price;
        public double Price
        {
            get
            {
                return price;
            }
        }

        private readonly int canAutoExecute;
        public double CanAutoExecute
        {
            get
            {
                return canAutoExecute;
            }
        }

        public TickPriceEventArgs(int tickerId, int field, double price, int canAutoExecute)
        {
            this.tickerId = tickerId;
            this.canAutoExecute = canAutoExecute;
            this.price = price;
            this.field = field;
        }
    }
}
