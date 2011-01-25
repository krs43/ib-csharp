using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Connection Closed Event Arguments
    /// </summary>
    [Serializable()]
    public class ConnectionClosedEventArgs : EventArgs
    {
        /// <summary>
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public ConnectionClosedEventArgs()
        {
            
        }
    }
}