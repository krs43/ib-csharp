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
        /// <summary>
        /// The ticker Id that was specified previously in the call to reqMktData().
        /// </summary>
        public int TickerId
        {
            get
            {
                return this.tickerId;
            }
        }

        private readonly TickType tickType;
        /// <summary>
        /// Specifies the type of price.
        /// </summary>
        public TickType TickType
        {
            get
            {
                return this.tickType;
            }
        }

        private readonly double price;
        /// <summary>
        /// Specifies the price for the specified field.
        /// </summary>
        public double Price
        {
            get
            {
                return price;
            }
        }

        private readonly bool canAutoExecute;
        /// <summary>
        /// specifies whether the price tick is available for automatic execution.
        /// </summary>
        /// <remarks>Possible values are:
        /// 0 = not eligible for automatic execution
        /// 1 = eligible for automatic execution</remarks>
        public bool CanAutoExecute
        {
            get
            {
                return canAutoExecute;
            }
        }

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="tickerId">The ticker Id that was specified previously in the call to reqMktData().</param>
        /// <param name="tickType">Specifies the type of price.</param>
        /// <param name="price">Specifies the price for the specified field.</param>
        /// <param name="canAutoExecute">specifies whether the price tick is available for automatic execution.</param>
        public TickPriceEventArgs(int tickerId, TickType tickType, double price, bool canAutoExecute)
        {
            this.tickerId = tickerId;
            this.canAutoExecute = canAutoExecute;
            this.price = price;
            this.tickType = tickType;
        }
    }
}
