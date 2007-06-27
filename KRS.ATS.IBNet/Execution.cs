using System;

namespace Krs.Ats.IBNet
{
    public class Execution
    {
        #region Private Variables

        private int orderId;
        private int clientId;
        private String execId;
        private String time;
        private String acctNumber;
        private String exchange;
        private ExecutionSide side;
        private int shares;
        private double price;
        private int permId;
        private int liquidation;
        #endregion

        #region Constructors
        public Execution()
        {

        }
		
        public Execution(int orderId, int clientId, String execId, String time, String acctNumber, String exchange, ExecutionSide side, int shares, double price, int permId, int liquidation)
        {
            this.orderId = orderId;
            this.clientId = clientId;
            this.execId = execId;
            this.time = time;
            this.acctNumber = acctNumber;
            this.exchange = exchange;
            this.side = side;
            this.shares = shares;
            this.price = price;
            this.permId = permId;
            this.liquidation = liquidation;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The order id.
        /// Note: TWS orders have a fixed order id of "0."
        /// </summary>
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        /// <summary>
        /// The id of the client that placed the order.
        /// Note: TWS orders have a fixed client id of "0."
        /// </summary>
        public int ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

        /// <summary>
        /// Unique order execution id.
        /// </summary>
        public string ExecId
        {
            get { return execId; }
            set { execId = value; }
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
        public string AcctNumber
        {
            get { return acctNumber; }
            set { acctNumber = value; }
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
        /// Specifies if the transaction was a sale or a purchase. Valid values are:
        /// BOT
        /// SLD
        /// </summary>
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

        #endregion

        #region Object Overrides
        public  override bool Equals(Object obj)
        {
            bool retVal = false;
			
            if (obj == null)
            {
                retVal = false;
            }
            else if (this == obj)
            {
                retVal = true;
            }
            else
            {
                Execution other = (Execution) obj;
                retVal = execId.Equals(other.execId);
            }
            return retVal;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}