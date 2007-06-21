using System;
using System.Collections.Generic;
using System.Text;

namespace KRS.ATS.IBNet
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
        private readonly int field;
        public int Field
        {
            get { return field; }
        }
        private readonly int size;
        public int Size
        {
            get { return size; }
        }

        public TickSizeEventArgs(int tickerId, int field, int size)
        {
            this.tickerId = tickerId;
            this.size = size;
            this.field = field;
        }
    }
}
