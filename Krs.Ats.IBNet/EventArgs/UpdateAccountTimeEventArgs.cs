using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Update Account Time Event Arguments
    /// </summary>
    public class UpdateAccountTimeEventArgs : EventArgs
    {
        private readonly string timeStamp;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="timeStamp">Current system time on the server side.</param>
        public UpdateAccountTimeEventArgs(string timeStamp)
        {
            this.timeStamp = timeStamp;
        }

        /// <summary>
        /// Current system time on the server side.
        /// </summary>
        public string TimeStamp
        {
            get { return timeStamp; }
        }
    }
}