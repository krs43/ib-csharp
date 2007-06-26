using System;

namespace Krs.Ats.IBNet
{
    public class ComboLeg
    {
        #region Private Variables
        private int conId;
        private int ratio;
        private ActionType action; // BUY/SELL
        private String exchange;
        private ComboOpenClose openClose;
        #endregion

        #region Constructors
        public ComboLeg()
        {
            conId = 0;
            ratio = 0;
            openClose = 0;
        }
		
        public ComboLeg(int ConId, int Ratio, ActionType Action, String Exchange, ComboOpenClose OpenClose)
        {
            conId = ConId;
            ratio = Ratio;
            action = Action;
            exchange = Exchange;
            openClose = OpenClose;
        }

        #endregion

        #region Properties
        /// <summary>
        /// The unique contract identifier specifying the security.
        /// </summary>
        public int ConId
        {
            get { return conId; }
            set { conId = value; }
        }
        /// <summary>
        /// Select the relative number of contracts for the leg you are constructing.
        /// To help determine the ratio for a specific combination order, refer to the
        /// Interactive Analytics section of the User's Guide.
        /// </summary>
        public int Ratio
        {
            get { return ratio; }
            set { ratio = value; }
        }
        /// <summary>
        /// The side (buy or sell) for the leg you are constructing.
        /// </summary>
        public ActionType Action
        {
            get { return action; }
            set { action = value; }
        }
        /// <summary>
        /// The exchange to which the complete combination order will be routed.
        /// </summary>
        public string Exchange
        {
            get { return exchange; }
            set { exchange = value; }
        }
        /// <summary>
        /// openClose - Specifies whether the order is an open or close order. Valid values are:
        /// Same - (0) same as the parent security. This is the only option for retail customers.
        /// Open - (1) this option is only valid for institutional customers.
        /// Close - (2) this option is only valid for institutional customers.
        /// Unknown - (3) ??
        /// </summary>
        public ComboOpenClose OpenClose
        {
            get { return openClose; }
            set { openClose = value; }
        }

        #endregion

        #region Object Overrides
        public  override bool Equals(Object p_other)
        {
            if (this == p_other)
            {
                return true;
            }
            else if (p_other == null)
            {
                return false;
            }
			
            ComboLeg l_theOther = (ComboLeg) p_other;
            String l_thisExchange = exchange ?? "";
			
            return (action == l_theOther.action &&
                    String.Compare(l_thisExchange, l_theOther.exchange, true) == 0 &&
                    conId == l_theOther.conId &&
                    ratio == l_theOther.ratio &&
                    openClose == l_theOther.openClose);
        }

        public override int GetHashCode()
        {
            return action.GetHashCode() ^ exchange.GetHashCode() ^ conId.GetHashCode() ^ ratio.GetHashCode() ^ openClose.GetHashCode();
        }
        #endregion
    }
}