using System;
using System.Collections.Generic;
using System.Text;

namespace KRS.ATS.IBNet
{
    /// <summary>
    /// Bond Contract Details Event Arguments
    /// </summary>
    public class BondContractDetailsEventArgs : EventArgs
    {
        private readonly ContractDetails contractDetails;
        public ContractDetails ContractDetails
        {
            get
            {
                return contractDetails;
            }
        }

        public BondContractDetailsEventArgs(ContractDetails contractDetails)
        {
            this.contractDetails = contractDetails;
        }
    }
}
