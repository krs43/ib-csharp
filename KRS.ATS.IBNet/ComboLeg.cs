using System;
using System.Globalization;

namespace Krs.Ats.IBNet
{
    public class ComboLeg
    {
        #region Private Variables
        private int conId;
        private int ratio;
        private ActionSide action; // BUY/SELL
        private String exchange;
        private ComboOpenClose openClose;
        #endregion

        #region Constructors
        public ComboLeg()
        {
            
        }
		
        public ComboLeg(int conId, int ratio, ActionSide action, String exchange, ComboOpenClose openClose)
        {
            this.conId = conId;
            this.ratio = ratio;
            this.action = action;
            this.exchange = exchange;
            this.openClose = openClose;
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
        public ActionSide Action
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
        public  override bool Equals(Object obj)
        {
            if (this == obj)
            {
                return true;
            }
            else if (obj == null)
            {
                return false;
            }
			
            ComboLeg other = (ComboLeg) obj;
            String thisExchange = exchange ?? "";
			
            return (action == other.action &&
                    String.Compare(thisExchange, other.exchange, true, CultureInfo.InvariantCulture) == 0 &&
                    conId == other.conId &&
                    ratio == other.ratio &&
                    openClose == other.openClose);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}