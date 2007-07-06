using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Update Market Depth Event Arguments
    /// </summary>
    public class UpdateMktDepthEventArgs : EventArgs
    {
        private readonly int tickerId;
        /// <summary>
        /// The ticker Id that was specified previously in the call to reqMktDepth().
        /// </summary>
        public int TickerId
        {
            get
            {
                return tickerId;
            }
        }

        private readonly int position;
        /// <summary>
        /// Specifies the row Id of this market depth entry.
        /// </summary>
        public int Position
        {
            get
            {
                return position;
            }
        }

        private readonly MarketDepthOperation operation;
        /// <summary>
        /// Identifies how this order should be applied to the market depth.
        /// </summary>
        /// <seealso cref="MarketDepthOperation"/>
        public MarketDepthOperation Operation
        {
            get
            {
                return operation;
            }
        }

        private readonly MarketDepthSide side;
        /// <summary>
        /// Identifies the side of the book that this order belongs to.
        /// </summary>
        /// <seealso cref="MarketDepthSide"/>
        public MarketDepthSide Side
        {
            get
            {
                return side;
            }
        }

        private readonly double price;
        /// <summary>
        /// The order price.
        /// </summary>
        public double Price
        {
            get
            {
                return price;
            }
        }

        private readonly int size;
        /// <summary>
        /// The order size.
        /// </summary>
        public int Size
        {
            get
            {
                return size;
            }
        }

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="tickerId">The ticker Id that was specified previously in the call to reqMktDepth().</param>
        /// <param name="position">Specifies the row Id of this market depth entry.</param>
        /// <param name="operation">Identifies how this order should be applied to the market depth.</param>
        /// <param name="side">Identifies the side of the book that this order belongs to.</param>
        /// <param name="price">The order price.</param>
        /// <param name="size">The order size.</param>
        public UpdateMktDepthEventArgs(int tickerId, int position, MarketDepthOperation operation, MarketDepthSide side, double price, int size)
        {
            this.tickerId = tickerId;
            this.size = size;
            this.price = price;
            this.side = side;
            this.operation = operation;
            this.position = position;
        }
    }
}
