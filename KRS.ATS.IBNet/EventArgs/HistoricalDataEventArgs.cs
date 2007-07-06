using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Historical Data Event Arguments
    /// </summary>
    public class HistoricalDataEventArgs : EventArgs
    {
        private readonly int reqId;
        /// <summary>
        /// The ticker Id of the request to which this bar is responding.
        /// </summary>
        public int ReqId
        {
            get
            {
                return reqId;
            }
        }

        private readonly string date;
        /// <summary>
        /// The date-time stamp of the start of the bar.
        /// The format is determined by the reqHistoricalData() formatDate parameter.
        /// </summary>
        public string Date
        {
            get
            {
                return date;
            }
        }

        private readonly double open;

        /// <summary>
        /// Bar opening price.
        /// </summary>
        public double Open
        {
            get
            {
                return open;
            }
        }

        private readonly double high;
        /// <summary>
        /// High price during the time covered by the bar.
        /// </summary>
        public double High
        {
            get
            {
                return high;
            }
        }

        private readonly double low;
        /// <summary>
        /// Low price during the time covered by the bar.
        /// </summary>
        public double Low
        {
            get
            {
                return low;
            }
        }

        private readonly double close;
        /// <summary>
        /// Bar closing price.
        /// </summary>
        public double Close
        {
            get
            {
                return close;
            }
        }

        private readonly int volume;
        /// <summary>
        /// Volume during the time covered by the bar.
        /// </summary>
        public int Volume
        {
            get
            {
                return volume;
            }
        }

        private readonly int count;
        /// <summary>
        /// When TRADES historical data is returned, represents the number of trades that
        /// occurred during the time period the bar covers.
        /// </summary>
        public int Count
        {
            get
            {
                return count;
            }
        }

        private readonly double wap;
        /// <summary>
        /// Weighted average price during the time covered by the bar.
        /// </summary>
        public double Wap
        {
            get
            {
                return wap;
            }
        }

        private readonly bool hasGaps;
        /// <summary>
        /// Whether or not there are gaps in the data.
        /// </summary>
        public bool HasGaps
        {
            get
            {
                return hasGaps;
            }
        }

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="reqId">The ticker Id of the request to which this bar is responding.</param>
        /// <param name="date">The date-time stamp of the start of the bar.
        /// The format is determined by the reqHistoricalData() formatDate parameter.</param>
        /// <param name="open">Bar opening price.</param>
        /// <param name="high">High price during the time covered by the bar.</param>
        /// <param name="low">Low price during the time covered by the bar.</param>
        /// <param name="close">Bar closing price.</param>
        /// <param name="volume">Volume during the time covered by the bar.</param>
        /// <param name="count">When TRADES historical data is returned, represents the number of trades that
        /// occurred during the time period the bar covers.</param>
        /// <param name="wap">Weighted average price during the time covered by the bar.</param>
        /// <param name="hasGaps">Whether or not there are gaps in the data.</param>
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
