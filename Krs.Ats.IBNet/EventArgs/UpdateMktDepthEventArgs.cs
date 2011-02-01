using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Update Market Depth Event Arguments
    /// </summary>
    [Serializable()]
    public class UpdateMarketDepthEventArgs : EventArgs
    {
        private MarketDepthOperation operation;
        private int position;
        private decimal price;
        private MarketDepthSide side;
        private int size;
        private int tickerId;

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
		/// Uninitialized Constructor for Serialization
		/// </summary>
        public UpdateMarketDepthEventArgs()
		{
			
		}

        /// <summary>
        /// The ticker Id that was specified previously in the call to reqMktDepth().
        /// </summary>
        public int TickerId
        {
            get { return tickerId; }
			set { tickerId = value; }
		}

        /// <summary>
        /// Specifies the row Id of this market depth entry.
        /// </summary>
        public int Position
        {
            get { return position; }
			set { position = value; }
		}

        /// <summary>
        /// Identifies how this order should be applied to the market depth.
        /// </summary>
        /// <seealso cref="MarketDepthOperation"/>
        public MarketDepthOperation Operation
        {
            get { return operation; }
			set { operation = value; }
		}

        /// <summary>
        /// Identifies the side of the book that this order belongs to.
        /// </summary>
        /// <seealso cref="MarketDepthSide"/>
        public MarketDepthSide Side
        {
            get { return side; }
			set { side = value; }
		}

        /// <summary>
        /// The order price.
        /// </summary>
        public decimal Price
        {
            get { return price; }
			set { price = value; }
		}

        /// <summary>
        /// The order size.
        /// </summary>
        public int Size
        {
            get { return size; }
			set { size = value; }
		}
    }
}