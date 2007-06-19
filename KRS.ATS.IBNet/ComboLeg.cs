/*
* ComboLeg.java
*
*/
using System;

namespace KRS.ATS.IBNet
{
    public class ComboLeg
    {
        public const int SAME = 0; // open/close leg value is same as combo
        public const int OPEN = 1;
        public const int CLOSE = 2;
        public const int UNKNOWN = 3;
		
        public int m_conId;
        public int m_ratio;
        public System.String m_action; // BUY/SELL
        public System.String m_exchange;
        public int m_openClose;
		
        public ComboLeg()
        {
            m_conId = 0;
            m_ratio = 0;
            m_openClose = 0;
        }
		
        public ComboLeg(int p_ConId, int p_Ratio, System.String p_Action, System.String p_exchange, int p_openClose)
        {
            m_conId = p_ConId;
            m_ratio = p_Ratio;
            m_action = p_Action;
            m_exchange = p_exchange;
            m_openClose = p_openClose;
        }
		
        public  override bool Equals(System.Object p_other)
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
            System.String l_thisAction = m_action != null?m_action:"";
            System.String l_thisExchange = m_exchange != null?m_exchange:"";
			
            return (System.String.Compare(l_thisAction, l_theOther.m_action, true) == 0 && System.String.Compare(l_thisExchange, l_theOther.m_exchange, true) == 0 && m_conId == l_theOther.m_conId && m_ratio == l_theOther.m_ratio && m_openClose == l_theOther.m_openClose);
        }
        //UPGRADE_NOTE: The following method implementation was automatically added to preserve functionality. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1306'"
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}