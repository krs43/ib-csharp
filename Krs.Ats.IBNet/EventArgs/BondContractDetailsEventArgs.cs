using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Bond Contract Details Event Arguments
    /// </summary>
    [Serializable()]
    public class BondContractDetailsEventArgs : EventArgs
    {
        private readonly ContractDetails contractDetails;
        private readonly int requestId;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="requestId">Request Id</param>
        /// <param name="contractDetails">This structure contains a full description of the bond contract being looked up.</param>
        public BondContractDetailsEventArgs(int requestId, ContractDetails contractDetails)
        {
            this.requestId = requestId;
            this.contractDetails = contractDetails;
        }

        /// <summary>
        /// This structure contains a full description of the bond contract being looked up.
        /// </summary>
        public ContractDetails ContractDetails
        {
            get { return contractDetails; }
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