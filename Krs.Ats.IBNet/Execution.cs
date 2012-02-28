using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Execution details returned by Interactive Brokers
    /// </summary>
    [Serializable()]
    public class Execution
    {
        #region Private Variables

        private String accountNumber;
        private int clientId;
        private String exchange;
        private String executionId;
        private int liquidation;
        private int orderId;
        private int permId;
        private double price;
        private int shares;
        private ExecutionSide side;
        private String time;
        private int cumQuantity;
        private decimal avgPrice;
        private String orderRef;

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Execution()
        {
        }

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="orderId">The order id.</param>
        /// <param name="clientId">TWS orders have a fixed client id of "0."</param>
        /// <param name="executionId">Unique order execution id.</param>
        /// <param name="time">The order execution time.</param>
        /// <param name="accountNumber">The customer account number.</param>
        /// <param name="exchange">Exchange that executed the order.</param>
        /// <param name="side">Specifies if the transaction was a sale or a purchase.</param>
        /// <param name="shares">The number of shares filled.</param>
        /// <param name="price">The order execution price.</param>
        /// <param name="permId">The TWS id used to identify orders, remains the same over TWS sessions.</param>
        /// <param name="liquidation">Identifies the position as one to be liquidated last should the need arise.</param>
        /// <param name="cumQuantity">Cumulative quantity. Used in regular trades, combo trades and legs of the combo.</param>
        /// <param name="avgPrice">Average price. Used in regular trades, combo trades and legs of the combo.</param>
        /// <param name="orderRef">Order Reference</param>
        public Execution(int orderId, int clientId, String executionId, String time, String accountNumber,
                         String exchange, ExecutionSide side, int shares, double price, int permId, int liquidation,
                         int cumQuantity, decimal avgPrice, string orderRef)
        {
            this.orderId = orderId;
            this.clientId = clientId;
            this.executionId = executionId;
            this.time = time;
            this.accountNumber = accountNumber;
            this.exchange = exchange;
            this.side = side;
            this.shares = shares;
            this.price = price;
            this.permId = permId;
            this.liquidation = liquidation;
            this.cumQuantity = cumQuantity;
            this.avgPrice = avgPrice;
            this.orderRef = orderRef;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The order id. 
        /// </summary>
        /// <remarks>TWS orders have a fixed order id of "0."</remarks>
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        /// <summary>
        /// The id of the client that placed the order.
        /// </summary>
        /// <remarks>TWS orders have a fixed client id of "0."</remarks>
        public int ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

        /// <summary>
        /// Unique order execution id.
        /// </summary>
        public string ExecutionId
        {
            get { return executionId; }
            set { executionId = value; }
        }

        /// <summary>
        /// The order execution time.
        /// </summary>
        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        /// <summary>
        /// The customer account number.
        /// </summary>
        public string AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }

        /// <summary>
        /// Exchange that executed the order.
        /// </summary>
        public string Exchange
        {
            get { return exchange; }
            set { exchange = value; }
        }

        /// <summary>
        /// Specifies if the transaction was a sale or a purchase.
        /// </summary>
        /// <remarks>Valid values are:
        /// <list type="bullet">
        /// <item>Bought</item>
        /// <item>Sold</item>
        /// </list>
        /// </remarks>
        public ExecutionSide Side
        {
            get { return side; }
            set { side = value; }
        }

        /// <summary>
        /// The number of shares filled.
        /// </summary>
        public int Shares
        {
            get { return shares; }
            set { shares = value; }
        }

        /// <summary>
        /// The order execution price.
        /// </summary>
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        /// <summary>
        /// The TWS id used to identify orders, remains the same over TWS sessions.
        /// </summary>
        public int PermId
        {
            get { return permId; }
            set { permId = value; }
        }

        /// <summary>
        /// Identifies the position as one to be liquidated last should the need arise.
        /// </summary>
        public int Liquidation
        {
            get { return liquidation; }
            set { liquidation = value; }
        }

        /// <summary>
        /// Cumulative Quantity
        /// </summary>
        public int CumQuantity
        {
            get { return cumQuantity; }
            set { cumQuantity = value; }
        }

        /// <summary>
        /// Average Price
        /// </summary>
        public decimal AvgPrice
        {
            get { return avgPrice; }
            set { avgPrice = value; }
        }

        public string OrderRef
        {
            get { return orderRef; }
            set { orderRef = value; }
        }

        #endregion
    }
}