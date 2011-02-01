using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Current Time Event Arguments
    /// </summary>
    [Serializable()]
    public class CurrentTimeEventArgs : EventArgs
    {
        private DateTime time;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="time">Current system time on the server side</param>
        public CurrentTimeEventArgs(DateTime time)
        {
            this.time = time;
        }

        /// <summary>
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public CurrentTimeEventArgs()
        {
            
        }

        /// <summary>
        /// Current system time on the server side in UTC
        /// </summary>
        public DateTime Time
        {
            get { return time; }
			set { time = value; }
		}
    }
}