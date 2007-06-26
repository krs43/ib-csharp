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
        public ContractDetails ContractDetails
        {
            get
            {
                return contractDetails;
            }
        }

        public ContractDetailsEventArgs(ContractDetails contractDetails)
        {
            this.contractDetails = contractDetails;
        }
    }
}
