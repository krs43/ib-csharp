using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Tick Size Event Arguments
    /// </summary>
    public class TickSizeEventArgs : EventArgs
    {
        private readonly int tickerId;
        /// <summary>
        /// The ticker Id that was specified previously in the call to reqMktData().
        /// </summary>
        public int TickerId
        {
            get { return tickerId; }
        }

        private readonly TickType tickerType;
        /// <summary>
        /// Specifies the type of price.
        /// </summary>
        public TickType TickerType
        {
            get { return tickerType; }
        }

        private readonly int size;
        /// <summary>
        /// Specifies the size for the specified field.
        /// </summary>
        public int Size
        {
            get { return size; }
        }

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="tickerId">The ticker Id that was specified previously in the call to reqMktData().</param>
        /// <param name="tickerType">Specifies the type of price.</param>
        /// <param name="size">Specifies the size for the specified field.</param>
        public TickSizeEventArgs(int tickerId, TickType tickerType, int size)
        {
            this.tickerId = tickerId;
            this.size = size;
            this.tickerType = tickerType;
        }
    }
}
