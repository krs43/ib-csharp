using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Contract Details Event Arguments
    /// </summary>
    [Serializable()]
    public class FundamentalDetailsEventArgs : EventArgs
    {
        private string data;
        private int requestId;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="requestId">Request Id</param>
        /// <param name="data">Xml Data</param>
        public FundamentalDetailsEventArgs(int requestId, string data)
        {
            this.requestId = requestId;
            this.data = data;
        }

        /// <summary>
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public FundamentalDetailsEventArgs()
        {
            
        }

        /// <summary>
        /// Xml Data
        /// </summary>
        public string Data
        {
            get { return data; }
			set { data = value; }
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