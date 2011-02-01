using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Error Event Arguments
    /// </summary>
    [Serializable()]
    public class ErrorEventArgs : EventArgs
    {
        private ErrorMessage errorCode;
        private string errorMsg;
        private int tickerId;

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
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public ErrorEventArgs()
        {
            
        }

        /// <summary>
        /// This is the orderId or tickerId of the request that generated the error.
        /// </summary>
        public int TickerId
        {
            get { return tickerId; }
			set { tickerId = value; }
		}

        /// <summary>
        /// Error codes are documented in the Error Codes topic.
        /// </summary>
        /// <seealso cref="ErrorMessage"/>
        public ErrorMessage ErrorCode
        {
            get { return errorCode; }
			set { errorCode = value; }
		}

        /// <summary>
        /// This is the textual description of the error, also documented in the Error Codes topic.
        /// </summary>
        /// <seealso cref="ErrorMessage"/>
        public string ErrorMsg
        {
            get { return errorMsg; }
			set { errorMsg = value; }
		}
    }
}