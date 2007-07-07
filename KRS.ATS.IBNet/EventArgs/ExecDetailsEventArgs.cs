using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Exec Details Event Arguments
    /// </summary>
    public class ExecDetailsEventArgs : EventArgs
    {
        private readonly Contract contract;
        private readonly Execution execution;
        private readonly int orderId;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="orderId">The order Id that was specified previously in the call to placeOrder().</param>
        /// <param name="contract">This structure contains a full description of the contract that was executed.</param>
        /// <param name="execution">This structure contains addition order execution details.</param>
        public ExecDetailsEventArgs(int orderId, Contract contract, Execution execution)
        {
            this.orderId = orderId;
            this.execution = execution;
            this.contract = contract;
        }

        /// <summary>
        /// The order Id that was specified previously in the call to placeOrder().
        /// </summary>
        public int OrderId
        {
            get { return orderId; }
        }

        /// <summary>
        /// This structure contains a full description of the contract that was executed.
        /// </summary>
        /// <seealso cref="Contract"/>
        public Contract Contract
        {
            get { return contract; }
        }

        /// <summary>
        /// This structure contains addition order execution details.
        /// </summary>
        /// <seealso cref="Execution"/>
        public Execution Execution
        {
            get { return execution; }
        }
    }
}