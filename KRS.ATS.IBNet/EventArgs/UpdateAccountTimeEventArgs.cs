using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Update Account Time Event Arguments
    /// </summary>
    public class UpdateAccountTimeEventArgs : EventArgs
    {
        private readonly string timeStamp;
        /// <summary>
        /// Current system time on the server side.
        /// </summary>
        public string TimeStamp
        {
            get
            {
                return timeStamp;
            }
        }

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="timeStamp">Current system time on the server side.</param>
        public UpdateAccountTimeEventArgs(string timeStamp)
        {
            this.timeStamp = timeStamp;
        }
    }
}
