using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Bond Contract Details Event Arguments
    /// </summary>
    public class BondContractDetailsEventArgs : EventArgs
    {
        private readonly ContractDetails contractDetails;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="contractDetails">This structure contains a full description of the bond contract being looked up.</param>
        public BondContractDetailsEventArgs(ContractDetails contractDetails)
        {
            this.contractDetails = contractDetails;
        }

        /// <summary>
        /// This structure contains a full description of the bond contract being looked up.
        /// </summary>
        public ContractDetails ContractDetails
        {
            get { return contractDetails; }
        }
    }
}