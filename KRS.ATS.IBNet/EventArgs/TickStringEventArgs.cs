using System;
using System.Collections.Generic;
using System.Text;

namespace KRS.ATS.IBNet
{
    public class TickStringEventArgs : EventArgs
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
        private readonly string value_Renamed;
        public string Value_Renamed
        {
            get
            {
                return value_Renamed;
            }
        }

        public TickStringEventArgs(int tickerId, int tickType, string value_Renamed)
        {
            this.tickerId = tickerId;
            this.value_Renamed = value_Renamed;
            this.tickType = tickType;
        }
    }
}
