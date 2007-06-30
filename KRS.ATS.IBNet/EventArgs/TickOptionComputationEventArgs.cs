using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Tick Option Computation Event Arguments
    /// </summary>
    public class TickOptionComputationEventArgs : EventArgs
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

        private readonly double impliedVol;
        public double ImpliedVol
        {
            get
            {
                return impliedVol;
            }
        }

        private readonly double delta;
        public double Delta
        {
            get
            {
                return delta;
            }
        }

        private readonly double modelPrice;
        public double ModelPrice
        {
            get
            {
                return modelPrice;
            }
        }

        private readonly double pvDividend;
        public double PVDividend
        {
            get
            {
                return pvDividend;
            }
        }

        public TickOptionComputationEventArgs(int tickerId, TickType tickType, double impliedVol, double delta, double modelPrice, double pvDividend)
        {
            this.tickerId = tickerId;
            this.pvDividend = pvDividend;
            this.delta = delta;
            this.modelPrice = modelPrice;
            this.impliedVol = impliedVol;
            this.tickType = tickType;
        }
    }
}
