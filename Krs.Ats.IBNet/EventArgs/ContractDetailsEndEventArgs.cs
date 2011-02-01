using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Contract Details Event Arguments
    /// </summary>
    [Serializable()]
    public class ContractDetailsEndEventArgs : EventArgs
    {
        private int requestId;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="requestId">Request Id</param>
        public ContractDetailsEndEventArgs(int requestId)
        {
            this.requestId = requestId;
        }

        /// <summary>
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public ContractDetailsEndEventArgs()
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