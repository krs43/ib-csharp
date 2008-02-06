using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Update Account Time Event Arguments
    /// </summary>
    [Serializable()]
    public class UpdateAccountTimeEventArgs : EventArgs
    {
        private readonly string timestamp;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="timestamp">Current system time on the server side.</param>
        public UpdateAccountTimeEventArgs(string timestamp)
        {
            this.timestamp = timestamp;
        }

        /// <summary>
        /// Current system time on the server side.
        /// </summary>
        public string Timestamp
        {
            get { return timestamp; }
        }
    }
}