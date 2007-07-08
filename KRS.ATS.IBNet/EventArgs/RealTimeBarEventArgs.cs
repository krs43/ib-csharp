using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Real Time Bar Event Arguments
    /// </summary>
    public class RealTimeBarEventArgs : EventArgs
    {
        private readonly double close;
        private readonly int count;
        private readonly double high;
        private readonly double low;
        private readonly double open;
        private readonly int reqId;
        private readonly long time;
        private readonly long volume;
        private readonly double wap;

        /// <summary>
        /// Real Time Bar Event Arguments
        /// </summary>
        /// <param name="reqId">The ticker Id of the request to which this bar is responding.</param>
        /// <param name="time">The date-time stamp of the start of the bar.
        /// The format is determined by the reqHistoricalData() formatDate parameter.</param>
        /// <param name="open">Bar opening price.</param>
        /// <param name="high">High price during the time covered by the bar.</param>
        /// <param name="low">Low price during the time covered by the bar.</param>
        /// <param name="close">Bar closing price.</param>
        /// <param name="volume">Volume during the time covered by the bar.</param>
        /// <param name="wap">Weighted average price during the time covered by the bar.</param>
        /// <param name="count">When TRADES historical data is returned, represents the number of trades that occurred during the time period the bar covers.</param>
        public RealTimeBarEventArgs(int reqId, long time, double open, double high, double low, double close,
                                    long volume, double wap, int count)
        {
            this.reqId = reqId;
            this.time = time;
            this.open = open;
            this.high = high;
            this.low = low;
            this.close = close;
            this.volume = volume;
            this.wap = wap;
            this.count = count;
        }

        /// <summary>
        /// The ticker Id of the request to which this bar is responding.
        /// </summary>
        public int ReqId
        {
            get { return reqId; }
        }

        /// <summary>
        /// The date-time stamp of the start of the bar.
        /// The format is determined by the reqHistoricalData() formatDate parameter.
        /// </summary>
        public long Time
        {
            get { return time; }
        }

        /// <summary>
        /// Bar opening price.
        /// </summary>
        public double Open
        {
            get { return open; }
        }

        /// <summary>
        /// High price during the time covered by the bar.
        /// </summary>
        public double High
        {
            get { return high; }
        }

        /// <summary>
        /// Low price during the time covered by the bar.
        /// </summary>
        public double Low
        {
            get { return low; }
        }

        /// <summary>
        /// Bar closing price.
        /// </summary>
        public double Close
        {
            get { return close; }
        }

        /// <summary>
        /// Volume during the time covered by the bar.
        /// </summary>
        public long Volume
        {
            get { return volume; }
        }

        /// <summary>
        /// Weighted average price during the time covered by the bar.
        /// </summary>
        public double Wap
        {
            get { return wap; }
        }

        /// <summary>
        /// When TRADES historical data is returned, represents the number of trades that occurred during the time period the bar covers.
        /// </summary>
        public int Count
        {
            get { return count; }
        }
    }
}