using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    public class HistoricalDataEventArgs : EventArgs
    {
        private readonly int reqId;
        public int ReqId
        {
            get
            {
                return reqId;
            }
        }
        private readonly string date;
        public string Date
        {
            get
            {
                return date;
            }
        }
        private readonly double open;
        public double Open
        {
            get
            {
                return open;
            }
        }
        private readonly double high;
        public double High
        {
            get
            {
                return high;
            }
        }
        private readonly double low;
        public double Low
        {
            get
            {
                return low;
            }
        }
        private readonly double close;
        public double Close
        {
            get
            {
                return close;
            }
        }
        private readonly int volume;
        public int Volume
        {
            get
            {
                return volume;
            }
        }
        private readonly int count;
        public int Count
        {
            get
            {
                return count;
            }
        }
        private readonly double wap;
        public double WAP
        {
            get
            {
                return wap;
            }
        }
        private readonly bool hasGaps;
        public bool HasGaps
        {
            get
            {
                return hasGaps;
            }
        }

        public HistoricalDataEventArgs(int reqId, string date, double open, double high, double low, double close, int volume, int count, double wap, bool hasGaps)
        {
            this.reqId = reqId;
            this.hasGaps = hasGaps;
            this.wap = wap;
            this.count = count;
            this.volume = volume;
            this.close = close;
            this.low = low;
            this.high = high;
            this.open = open;
            this.date = date;
        }
    }
}
