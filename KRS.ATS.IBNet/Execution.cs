/*
* Execution.java
*
*/
using System;

namespace KRS.ATS.IBNet
{
    public class Execution
    {
        public int m_orderId;
        public int m_clientId;
        public System.String m_execId;
        public System.String m_time;
        public System.String m_acctNumber;
        public System.String m_exchange;
        public System.String m_side;
        public int m_shares;
        public double m_price;
        public int m_permId;
        public int m_liquidation;
		
        public Execution()
        {
            m_orderId = 0;
            m_clientId = 0;
            m_shares = 0;
            m_price = 0;
            m_permId = 0;
            m_liquidation = 0;
        }
		
        public Execution(int p_orderId, int p_clientId, System.String p_execId, System.String p_time, System.String p_acctNumber, System.String p_exchange, System.String p_side, int p_shares, double p_price, int p_permId, int p_liquidation)
        {
            m_orderId = p_orderId;
            m_clientId = p_clientId;
            m_execId = p_execId;
            m_time = p_time;
            m_acctNumber = p_acctNumber;
            m_exchange = p_exchange;
            m_side = p_side;
            m_shares = p_shares;
            m_price = p_price;
            m_permId = p_permId;
            m_liquidation = p_liquidation;
        }
		
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
                Execution l_theOther = (Execution) p_other;
                l_bRetVal = m_execId.Equals(l_theOther.m_execId);
            }
            return l_bRetVal;
        }
        //UPGRADE_NOTE: The following method implementation was automatically added to preserve functionality. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1306'"
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}