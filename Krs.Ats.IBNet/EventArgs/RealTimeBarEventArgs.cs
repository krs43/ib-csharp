using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Real Time Bar Event Arguments
    /// </summary>
    [Serializable()]
    public class RealTimeBarEventArgs : EventArgs
    {
        private decimal close;
        private int count;
        private decimal high;
        private decimal low;
        private decimal open;
        private int requestId;
        private long time;
        private long volume;
        private double wap;

        /// <summary>
        /// Real Time Bar Event Arguments
        /// </summary>
        /// <param name="requestId">The ticker Id of the request to which this bar is responding.</param>
        /// <param name="time">The date-time stamp of the start of the bar.
        /// The format is determined by the reqHistoricalData() formatDate parameter.</param>
        /// <param name="open">Bar opening price.</param>
        /// <param name="high">High price during the time covered by the bar.</param>
        /// <param name="low">Low price during the time covered by the bar.</param>
        /// <param name="close">Bar closing price.</param>
        /// <param name="volume">Volume during the time covered by the bar.</param>
        /// <param name="wap">Weighted average price during the time covered by the bar.</param>
        /// <param name="count">When TRADES historical data is returned, represents the number of trades that occurred during the time period the bar covers.</param>
        public RealTimeBarEventArgs(int requestId, long time, decimal open, decimal high, decimal low, decimal close,
                                    long volume, double wap, int count)
        {
            this.requestId = requestId;
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
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public RealTimeBarEventArgs()
        {
            
        }

        /// <summary>
        /// The ticker Id of the request to which this bar is responding.
        /// </summary>
        public int RequestId
        {
            get { return requestId; }
			set { requestId = value; }
		}

        /// <summary>
        /// The date-time stamp of the start of the bar.
        /// The format is determined by the reqHistoricalData() formatDate parameter.
        /// </summary>
        public long Time
        {
            get { return time; }
			set { time = value; }
		}

        /// <summary>
        /// Bar opening price.
        /// </summary>
        public decimal Open
        {
            get { return open; }
			set { open = value; }
		}

        /// <summary>
        /// High price during the time covered by the bar.
        /// </summary>
        public decimal High
        {
            get { return high; }
			set { high = value; }
		}

        /// <summary>
        /// Low price during the time covered by the bar.
        /// </summary>
        public decimal Low
        {
            get { return low; }
			set { low = value; }
		}

        /// <summary>
        /// Bar closing price.
        /// </summary>
        public decimal Close
        {
            get { return close; }
			set { close = value; }
		}

        /// <summary>
        /// Volume during the time covered by the bar.
        /// </summary>
        public long Volume
        {
            get { return volume; }
			set { volume = value; }
		}

        /// <summary>
        /// Weighted average price during the time covered by the bar.
        /// </summary>
        public double Wap
        {
            get { return wap; }
			set { wap = value; }
		}

        /// <summary>
        /// When TRADES historical data is returned, represents the number of trades that occurred during the time period the bar covers.
        /// </summary>
        public int Count
        {
            get { return count; }
			set { count = value; }
		}
    }
}