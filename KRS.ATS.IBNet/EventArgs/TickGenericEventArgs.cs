using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    public class TickGenericEventArgs : EventArgs
    {
        private readonly int tickerId;
        public int TickerId
        {
            get
            {
                return tickerId;
            }
        }
        private readonly TickType tickType;
        public TickType TickType
        {
            get
            {
                return tickType;
            }
        }
        private readonly double valueRenamed;
        public double ValueRenamed
        {
            get
            {
                return valueRenamed;
            }
        }

        public TickGenericEventArgs(int tickerId, TickType tickType, double valueRenamed)
        {
            this.tickerId = tickerId;
            this.valueRenamed = valueRenamed;
            this.tickType = tickType;
        }
    }
}
