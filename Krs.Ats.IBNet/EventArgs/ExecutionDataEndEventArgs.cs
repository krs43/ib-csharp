using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Execution Data End Event Arguments
    /// </summary>
    [Serializable()]
    public class ExecutionDataEndEventArgs : EventArgs
    {
        private readonly int requestId;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="requestId">Request Id</param>
        public ExecutionDataEndEventArgs(int requestId)
        {
            this.requestId = requestId;
        }

        /// <summary>
        /// Request Id
        /// </summary>
        public int RequestId
        {
            get { return requestId; }
        }
    }
}