using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Tick EFP Event Arguments
    /// </summary>
    public class TickEFPEventArgs : EventArgs
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

        private readonly double basisPointsl;
        public double BasisPoints1
        {
            get
            {
                return basisPointsl;
            }
        }

        private readonly string formattedBasisPoints;
        public string FormattedBasisPoints
        {
            get
            {
                return formattedBasisPoints;
            }
        }

        private readonly double impliedFuture;
        public double ImpliedFuture
        {
            get
            {
                return impliedFuture;
            }
        }

        private readonly int holdDays;
        public double HoldDays
        {
            get
            {
                return holdDays;
            }
        }

        private readonly string futureExpiry;
        public string FutureExpiry
        {
            get
            {
                return futureExpiry;
            }
        }

        private readonly double dividendImpact;
        public double DividendImpact
        {
            get
            {
                return dividendImpact;
            }
        }
        private readonly double dividendsToExpiry;
        public double DividendsToExpiry
        {
            get
            {
                return dividendsToExpiry;
            }
        }

        public TickEFPEventArgs(int tickerId, int tickType, double basisPointsl, string formattedBasisPoints, double impliedFuture, int holdDays, string futureExpiry, double dividendImpact, double dividendsToExpiry)
        {
            this.tickerId = tickerId;
            this.dividendsToExpiry = dividendsToExpiry;
            this.dividendImpact = dividendImpact;
            this.futureExpiry = futureExpiry;
            this.holdDays = holdDays;
            this.impliedFuture = impliedFuture;
            this.formattedBasisPoints = formattedBasisPoints;
            this.basisPointsl = basisPointsl;
            this.tickType = tickType;
        }
    }
}
