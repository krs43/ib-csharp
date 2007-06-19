/*
* ExecutionFilter.java
*
*/
using System;

namespace KRS.ATS.IBNet
{
    public class ExecutionFilter
    {
        public int m_clientId;
        public System.String m_acctCode;
        public System.String m_time;
        public System.String m_symbol;
        public System.String m_secType;
        public System.String m_exchange;
        public System.String m_side;
		
        public ExecutionFilter()
        {
            m_clientId = 0;
        }
		
        public ExecutionFilter(int p_clientId, System.String p_acctCode, System.String p_time, System.String p_symbol, System.String p_secType, System.String p_exchange, System.String p_side)
        {
            m_clientId = p_clientId;
            m_acctCode = p_acctCode;
            m_time = p_time;
            m_symbol = p_symbol;
            m_secType = p_secType;
            m_exchange = p_exchange;
            m_side = p_side;
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
                ExecutionFilter l_theOther = (ExecutionFilter) p_other;
                l_bRetVal = (m_clientId == l_theOther.m_clientId && m_acctCode.ToUpper().Equals(l_theOther.m_acctCode.ToUpper()) && m_time.ToUpper().Equals(l_theOther.m_time.ToUpper()) && m_symbol.ToUpper().Equals(l_theOther.m_symbol.ToUpper()) && m_secType.ToUpper().Equals(l_theOther.m_secType.ToUpper()) && m_exchange.ToUpper().Equals(l_theOther.m_exchange.ToUpper()) && m_side.ToUpper().Equals(l_theOther.m_side.ToUpper()));
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