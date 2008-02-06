using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Scanner Data Event Arguments
    /// </summary>
    [Serializable()]
    public class ScannerDataEndEventArgs : EventArgs
    {
        private readonly int requestId;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="requestId">The ticker ID of the request to which this row is responding.</param>
        public ScannerDataEndEventArgs(int requestId)
        {
            this.requestId = requestId;
        }

        /// <summary>
        /// The ticker ID of the request to which this row is responding.
        /// </summary>
        public int RequestId
        {
            get { return requestId; }
        }
    }
}