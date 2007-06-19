/*
* ContractDetails.java
*
*/
using System;
using KRS.ATS.IBNet;

namespace KRS.ATS.IBNet
{
    public class ContractDetails
    {
        public Contract m_summary;
        public System.String m_marketName;
        public System.String m_tradingClass;
        public int m_conid;
        public double m_minTick;
        public System.String m_multiplier;
        public int m_priceMagnifier;
        public System.String m_orderTypes;
        public System.String m_validExchanges;
		
        public ContractDetails()
        {
            m_summary = new Contract();
            m_conid = 0;
            m_minTick = 0;
        }
		
        public ContractDetails(Contract p_summary, System.String p_marketName, System.String p_tradingClass, int p_conid, double p_minTick, System.String p_multiplier, System.String p_orderTypes, System.String p_validExchanges)
        {
            m_summary = p_summary;
            m_marketName = p_marketName;
            m_tradingClass = p_tradingClass;
            m_conid = p_conid;
            m_minTick = p_minTick;
            m_multiplier = p_multiplier;
            m_orderTypes = p_orderTypes;
            m_validExchanges = p_validExchanges;
        }
    }
}