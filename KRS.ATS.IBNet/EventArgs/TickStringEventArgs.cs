using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
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
        private readonly TickType tickType;
        public TickType TickType
        {
            get
            {
                return tickType;
            }
        }
        private readonly string valueRenamed;
        public string ValueRenamed
        {
            get
            {
                return valueRenamed;
            }
        }

        public TickStringEventArgs(int tickerId, TickType tickType, string valueRenamed)
        {
            this.tickerId = tickerId;
            this.valueRenamed = valueRenamed;
            this.tickType = tickType;
        }
    }
}
