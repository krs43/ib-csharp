using System;

namespace Krs.Ats.IBNet
{
    public class ExecutionFilter
    {
        #region Private Variables

        private int clientId;
        private String acctCode;
        private DateTime time;
        private String symbol;
        private SecurityType secType;
        private String exchange;
        private ActionSide side;

        #endregion

        #region Constructors
        public ExecutionFilter()
        {
            clientId = 0;
        }
		
        public ExecutionFilter(int clientId, String acctCode, DateTime time, String symbol, SecurityType secType, String exchange, ActionSide side)
        {
            this.clientId = clientId;
            this.acctCode = acctCode;
            this.time = time;
            this.symbol = symbol;
            this.secType = secType;
            this.exchange = exchange;
            this.side = side;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Filter the results of the ReqExecutions() method based on the clientId.
        /// </summary>
        public int ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

        /// <summary>
        /// Filter the results of the ReqExecutions() method based on an account code.
        /// Note: this is only relevant for Financial Advisor (FA) accounts.
        /// </summary>
        public string AcctCode
        {
            get { return acctCode; }
            set { acctCode = value; }
        }

        /// <summary>
        /// Filter the results of the ReqExecutions() method based on execution reports received after the specified time.
        /// The format for timeFilter is "yyyymmdd-hh:mm:ss"
        /// </summary>
        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }

        /// <summary>
        /// Filter the results of the ReqExecutions() method based on the order symbol.
        /// </summary>
        public string Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        /// <summary>
        /// Filter the results of the ReqExecutions() method based on the order security type.
        /// Note: Refer to the Contract struct for the list of valid security types.
        /// </summary>
        public SecurityType SecType
        {
            get { return secType; }
            set { secType = value; }
        }

        /// <summary>
        /// Filter the results of the ReqExecutions() method based on theorder exchange.
        /// </summary>
        public string Exchange
        {
            get { return exchange; }
            set { exchange = value; }
        }

        /// <summary>
        /// Filter the results of the ReqExecutions() method based on the order action.
        /// Note: Refer to the Order struct for the list of valid order actions.
        /// </summary>
        public ActionSide Side
        {
            get { return side; }
            set { side = value; }
        }

        #endregion

        #region Object Overrides
        public  override bool Equals(System.Object p_other)
        {
            bool l_bRetVal = false;
			
            if (p_other == null)
            {
                l_bRetVal = false;
            }
            else if (this == p_other)
            {
                l_bRetVal = true;
            }
            else
            {
                ExecutionFilter l_theOther = (ExecutionFilter) p_other;
                l_bRetVal = (clientId == l_theOther.clientId && acctCode.ToUpper().Equals(l_theOther.acctCode.ToUpper()) && time.Equals(l_theOther.time) && symbol.ToUpper().Equals(l_theOther.symbol.ToUpper()) && secType.Equals(l_theOther.secType) && exchange.ToUpper().Equals(l_theOther.exchange.ToUpper()) && side.Equals(l_theOther.side));
            }
            return l_bRetVal;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}