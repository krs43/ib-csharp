using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Current Time Event Arguments
    /// </summary>
    public class CurrentTimeEventArgs : EventArgs
    {
        private readonly long time;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="time">Current system time on the server side</param>
        public CurrentTimeEventArgs(long time)
        {
            this.time = time;
        }

        /// <summary>
        /// Current system time on the server side
        /// </summary>
        public long Time
        {
            get { return time; }
        }
    }
}