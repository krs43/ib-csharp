using System;
using System.Collections.Generic;
using System.Text;

namespace KRS.ATS.IBNet
{
    /// <summary>
    /// Exec Details Event Arguments
    /// </summary>
    public class ExecDetailsEventArgs : EventArgs
    {
        private readonly int orderId;
        public int OrderId
        {
            get
            {
                return orderId;
            }
        }
        private readonly Contract contract;
        public Contract Contract
        {
            get
            {
                return contract;
            }
        }

        private readonly Execution execution;
        public Execution Execution
        {
            get
            {
                return execution;
            }
        }

        public ExecDetailsEventArgs(int orderId, Contract contract, Execution execution)
        {
            this.orderId = orderId;
            this.execution = execution;
            this.contract = contract;
        }
    }
}
