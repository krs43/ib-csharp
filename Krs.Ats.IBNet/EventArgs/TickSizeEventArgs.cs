using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Tick Size Event Arguments
    /// </summary>
    [Serializable()]
    public class TickSizeEventArgs : EventArgs
    {
        private readonly int size;
        private readonly int tickerId;

        private readonly TickType tickType;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="tickerId">The ticker Id that was specified previously in the call to reqMktData().</param>
        /// <param name="tickType">Specifies the type of price.</param>
        /// <param name="size">Specifies the size for the specified field.</param>
        public TickSizeEventArgs(int tickerId, TickType tickType, int size)
        {
            this.tickerId = tickerId;
            this.size = size;
            this.tickType = tickType;
        }

        /// <summary>
        /// The ticker Id that was specified previously in the call to reqMktData().
        /// </summary>
        public int TickerId
        {
            get { return tickerId; }
        }

        /// <summary>
        /// Specifies the type of price.
        /// </summary>
        public TickType TickType
        {
            get { return tickType; }
        }

        /// <summary>
        /// Specifies the size for the specified field.
        /// </summary>
        public int Size
        {
            get { return size; }
        }
    }
}