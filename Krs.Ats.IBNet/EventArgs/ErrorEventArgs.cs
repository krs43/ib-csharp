using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Error Event Arguments
    /// </summary>
    [Serializable()]
    public class ErrorEventArgs : EventArgs
    {
        private readonly ErrorMessage errorCode;
        private readonly string errorMsg;
        private readonly int tickerId;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="tickerId">This is the orderId or tickerId of the request that generated the error.</param>
        /// <param name="errorCode">Error codes are documented in the Error Codes topic.</param>
        /// <param name="errorMsg">This is the textual description of the error, also documented in the Error Codes topic.</param>
        public ErrorEventArgs(int tickerId, ErrorMessage errorCode, string errorMsg)
        {
            this.tickerId = tickerId;
            this.errorMsg = errorMsg;
            this.errorCode = errorCode;
        }

        /// <summary>
        /// This is the orderId or tickerId of the request that generated the error.
        /// </summary>
        public int TickerId
        {
            get { return tickerId; }
        }

        /// <summary>
        /// Error codes are documented in the Error Codes topic.
        /// </summary>
        /// <seealso cref="ErrorMessage"/>
        public ErrorMessage ErrorCode
        {
            get { return errorCode; }
        }

        /// <summary>
        /// This is the textual description of the error, also documented in the Error Codes topic.
        /// </summary>
        /// <seealso cref="ErrorMessage"/>
        public string ErrorMsg
        {
            get { return errorMsg; }
        }
    }
}