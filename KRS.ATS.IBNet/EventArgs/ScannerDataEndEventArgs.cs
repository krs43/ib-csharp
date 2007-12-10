using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Scanner Data Event Arguments
    /// </summary>
    public class ScannerDataEndEventArgs : EventArgs
    {
        private readonly int reqId;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="reqId">The ticker ID of the request to which this row is responding.</param>
        public ScannerDataEndEventArgs(int reqId)
        {
            this.reqId = reqId;
        }

        /// <summary>
        /// The ticker ID of the request to which this row is responding.
        /// </summary>
        public int ReqId
        {
            get { return reqId; }
        }
    }
}