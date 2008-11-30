using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Execution Data End Event Arguments
    /// </summary>
    [Serializable()]
    public class DeltaNuetralValidationEventArgs : EventArgs
    {
        private readonly int requestId;
        private readonly UnderComp underComp;

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
        /// Request Id
        /// </summary>
        public int RequestId
        {
            get { return requestId; }
        }

        /// <summary>
        /// Underlying Component
        /// </summary>
        public UnderComp UnderComp
        {
            get { return underComp; }
        }
    }
}