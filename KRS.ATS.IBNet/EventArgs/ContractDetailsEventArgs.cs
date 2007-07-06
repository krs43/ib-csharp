using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Contract Details Event Arguments
    /// </summary>
    public class ContractDetailsEventArgs : EventArgs
    {
        private readonly ContractDetails contractDetails;
        /// <summary>
        /// This structure contains a full description of the contract being looked up.
        /// </summary>
        public ContractDetails ContractDetails
        {
            get
            {
                return contractDetails;
            }
        }

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="contractDetails">This structure contains a full description of the contract being looked up.</param>
        public ContractDetailsEventArgs(ContractDetails contractDetails)
        {
            this.contractDetails = contractDetails;
        }
    }
}
