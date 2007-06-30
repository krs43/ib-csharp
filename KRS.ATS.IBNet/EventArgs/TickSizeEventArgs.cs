using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Tick Size Event Arguments
    /// </summary>
    public class TickSizeEventArgs : EventArgs
    {
        private readonly int tickerId;
        public int TickerId
        {
            get { return tickerId; }
        }
        private readonly TickType tickerType;
        public TickType TickerType
        {
            get { return tickerType; }
        }
        private readonly int size;
        public int Size
        {
            get { return size; }
        }

        public TickSizeEventArgs(int tickerId, TickType tickerType, int size)
        {
            this.tickerId = tickerId;
            this.size = size;
            this.tickerType = tickerType;
        }
    }
}
