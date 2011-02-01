using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Execution Data End Event Arguments
    /// </summary>
    [Serializable()]
    public class DeltaNuetralValidationEventArgs : EventArgs
    {
        private int requestId;
        private UnderComp underComp;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="requestId">Request Id</param>
        /// <param name="underComp">Underlying Component</param>
        public DeltaNuetralValidationEventArgs(int requestId, UnderComp underComp)
        {
            this.requestId = requestId;
            this.underComp = underComp;
        }

        /// <summary>
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public DeltaNuetralValidationEventArgs()
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

        /// <summary>
        /// Underlying Component
        /// </summary>
        public UnderComp UnderComp
        {
            get { return underComp; }
			set { underComp = value; }
		}
    }
}