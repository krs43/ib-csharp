using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Exec Details Event Arguments
    /// </summary>
    [Serializable()]
    public class ExecDetailsEventArgs : EventArgs
    {
        private readonly Contract contract;
        private readonly Execution execution;
        private readonly int orderId;
        private readonly int requestId;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="requestId">The request Id for the Execution Details.</param>
        /// <param name="orderId">The order Id that was specified previously in the call to placeOrder().</param>
        /// <param name="contract">This structure contains a full description of the contract that was executed.</param>
        /// <param name="execution">This structure contains addition order execution details.</param>
        public ExecDetailsEventArgs(int requestId, int orderId, Contract contract, Execution execution)
        {
            this.requestId = requestId;
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
        /// Request Id
        /// </summary>
        public int RequestId
        {
            get { return requestId; }
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