using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Contract Details Event Arguments
    /// </summary>
    [Serializable()]
    public class FundamentalDetailsEventArgs : EventArgs
    {
        private readonly string data;
        private readonly int requestId;

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
        /// Xml Data
        /// </summary>
        public string Data
        {
            get { return data; }
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