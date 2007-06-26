using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Update Market Depth L2 Event Arguments
    /// </summary>
    public class UpdateMktDepthL2EventArgs : EventArgs
    {
        private readonly int tickerId;
        public int TickerId
        {
            get
            {
                return tickerId;
            }
        }
        private readonly int position;
        public int Position
        {
            get
            {
                return position;
            }
        }
        private readonly string marketMaker;
        public string MarketMaker
        {
            get
            {
                return marketMaker;
            }
        }
        private readonly int operation;
        public int Operation
        {
            get
            {
                return operation;
            }
        }
        private readonly int side;
        public int Side
        {
            get
            {
                return side;
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
        private readonly int size;
        public int Size
        {
            get
            {
                return size;
            }
        }

        public UpdateMktDepthL2EventArgs(int tickerId, int position, string marketMaker, int operation, int side, double price, int size)
        {
            this.tickerId = tickerId;
            this.size = size;
            this.price = price;
            this.side = side;
            this.operation = operation;
            this.marketMaker = marketMaker;
            this.position = position;
        }
    }
}
