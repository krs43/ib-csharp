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
        private readonly int tickType;
        public int TickType
        {
            get
            {
                return tickType;
            }
        }
        private readonly double value_Renamed;
        public double Value_Renamed
        {
            get
            {
                return value_Renamed;
            }
        }

        public TickGenericEventArgs(int tickerId, int tickType, double value_Renamed)
        {
            this.tickerId = tickerId;
            this.value_Renamed = value_Renamed;
            this.tickType = tickType;
        }
    }
}
