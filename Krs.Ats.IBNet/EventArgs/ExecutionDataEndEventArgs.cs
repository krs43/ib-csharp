using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Execution Data End Event Arguments
    /// </summary>
    [Serializable()]
    public class ExecutionDataEndEventArgs : EventArgs
    {
        private int requestId;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="requestId">Request Id</param>
        public ExecutionDataEndEventArgs(int requestId)
        {
            this.requestId = requestId;
        }

        /// <summary>
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public ExecutionDataEndEventArgs()
        {
            
        }

        /// <summary>
        /// Request Id
        /// </summary>
        public int RequestId
        {
            get { return requestId; }
			set { requestId = value; }
		}
    }
}