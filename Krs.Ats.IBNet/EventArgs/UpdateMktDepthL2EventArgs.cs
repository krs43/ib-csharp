using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Update Market Depth L2 Event Arguments
    /// </summary>
    [Serializable()]
    public class UpdateMarketDepthL2EventArgs : EventArgs
    {
        private readonly string marketMaker;
        private readonly MarketDepthOperation operation;
        private readonly int position;
        private readonly decimal price;
        private readonly MarketDepthSide side;
        private readonly int size;
        private readonly int tickerId;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="tickerId">The ticker Id that was specified previously in the call to <see cref="IBClient.RequestMarketDepth"/>.</param>
        /// <param name="position">Specifies the row id of this market depth entry.</param>
        /// <param name="marketMaker">Specifies the exchange hosting this order.</param>
        /// <param name="operation">Identifies the how this order should be applied to the market depth.</param>
        /// <param name="side">Identifies the side of the book that this order belongs to.</param>
        /// <param name="price">The order price.</param>
        /// <param name="size">The order size.</param>
        public UpdateMarketDepthL2EventArgs(int tickerId, int position, string marketMaker, MarketDepthOperation operation,
                                         MarketDepthSide side, decimal price, int size)
        {
            this.tickerId = tickerId;
            this.size = size;
            this.price = price;
            this.side = side;
            this.operation = operation;
            this.marketMaker = marketMaker;
            this.position = position;
        }

        /// <summary>
        /// The ticker Id that was specified previously in the call to <see cref="IBClient.RequestMarketDepth"/>.
        /// </summary>
        public int TickerId
        {
            get { return tickerId; }
        }

        /// <summary>
        /// Specifies the row id of this market depth entry.
        /// </summary>
        public int Position
        {
            get { return position; }
        }

        /// <summary>
        /// Specifies the exchange hosting this order.
        /// </summary>
        public string MarketMaker
        {
            get { return marketMaker; }
        }

        /// <summary>
        /// Identifies the how this order should be applied to the market depth.
        /// </summary>
        /// <seealso cref="MarketDepthOperation"/>
        public MarketDepthOperation Operation
        {
            get { return operation; }
        }

        /// <summary>
        /// Identifies the side of the book that this order belongs to.
        /// </summary>
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