using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Update Market Depth Event Arguments
    /// </summary>
    [Serializable()]
    public class UpdateMarketDepthEventArgs : EventArgs
    {
        private readonly MarketDepthOperation operation;
        private readonly int position;
        private readonly decimal price;
        private readonly MarketDepthSide side;
        private readonly int size;
        private readonly int tickerId;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="tickerId">The ticker Id that was specified previously in the call to reqMktDepth().</param>
        /// <param name="position">Specifies the row Id of this market depth entry.</param>
        /// <param name="operation">Identifies how this order should be applied to the market depth.</param>
        /// <param name="side">Identifies the side of the book that this order belongs to.</param>
        /// <param name="price">The order price.</param>
        /// <param name="size">The order size.</param>
        public UpdateMarketDepthEventArgs(int tickerId, int position, MarketDepthOperation operation, MarketDepthSide side,
                                       decimal price, int size)
        {
            this.tickerId = tickerId;
            this.size = size;
            this.price = price;
            this.side = side;
            this.operation = operation;
            this.position = position;
        }

        /// <summary>
        /// The ticker Id that was specified previously in the call to reqMktDepth().
        /// </summary>
        public int TickerId
        {
            get { return tickerId; }
        }

        /// <summary>
        /// Specifies the row Id of this market depth entry.
        /// </summary>
        public int Position
        {
            get { return position; }
        }

        /// <summary>
        /// Identifies how this order should be applied to the market depth.
        /// </summary>
        /// <seealso cref="MarketDepthOperation"/>
        public MarketDepthOperation Operation
        {
            get { return operation; }
        }

        /// <summary>
        /// Identifies the side of the book that this order belongs to.
        /// </summary>
        /// <seealso cref="MarketDepthSide"/>
        public MarketDepthSide Side
        {
            get { return side; }
        }

        /// <summary>
        /// The order price.
        /// </summary>
        public decimal Price
        {
            get { return price; }
        }

        /// <summary>
        /// The order size.
        /// </summary>
        public int Size
        {
            get { return size; }
        }
    }
}