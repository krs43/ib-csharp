using System;

namespace KRS.ATS.IBNet
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

        public int ConId
        {
            get { return conId; }
            set { conId = value; }
        }

        public int Ratio
        {
            get { return ratio; }
            set { ratio = value; }
        }

        public ActionType Action
        {
            get { return action; }
            set { action = value; }
        }

        public string Exchange
        {
            get { return exchange; }
            set { exchange = value; }
        }

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