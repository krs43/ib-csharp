using System;

namespace Krs.Ats.IBNet
{
    public class ContractDetails
    {
        #region Private Variables
        private Contract summary;
        private String marketName;
        private String tradingClass;
        private int conid;
        private double minTick;
        private String multiplier;
        private int priceMagnifier;
        private String orderTypes;
        private String validExchanges;
        #endregion

        #region Constructors

        public ContractDetails() :
            this(new Contract(), null, null, 0, 0, null, null, null)
        {
            
        }
		
        public ContractDetails(Contract summary, System.String marketName, String tradingClass, int conid, double minTick, System.String multiplier, System.String orderTypes, System.String validExchanges)
        {
            this.summary = summary;
            this.marketName = marketName;
            this.tradingClass = tradingClass;
            this.conid = conid;
            this.minTick = minTick;
            this.multiplier = multiplier;
            this.orderTypes = orderTypes;
            this.validExchanges = validExchanges;
        }
        #endregion

        #region Properties
        /// <summary>
        /// A contract summary.
        /// </summary>
        public Contract Summary
        {
            get { return summary; }
            set { summary = value; }
        }
        /// <summary>
        /// The market name for this contract.
        /// </summary>
        public string MarketName
        {
            get { return marketName; }
            set { marketName = value; }
        }
        /// <summary>
        /// The trading class name for this contract.
        /// </summary>
        public string TradingClass
        {
            get { return tradingClass; }
            set { tradingClass = value; }
        }
        /// <summary>
        /// The unique contract identifier.
        /// </summary>
        public int Conid
        {
            get { return conid; }
            set { conid = value; }
        }
        /// <summary>
        /// The minimum price tick.
        /// </summary>
        public double MinTick
        {
            get { return minTick; }
            set { minTick = value; }
        }
        /// <summary>
        /// The order size multiplier.
        /// </summary>
        public string Multiplier
        {
            get { return multiplier; }
            set { multiplier = value; }
        }
        /// <summary>
        /// Allows execution and strike prices to be reported consistently with
        /// market data, historical data and the order price, i.e. Z on LIFFE is
        /// reported in index points and not GBP.
        /// </summary>
        public int PriceMagnifier
        {
            get { return priceMagnifier; }
            set { priceMagnifier = value; }
        }
        /// <summary>
        /// The list of valid order types for this contract.
        /// </summary>
        public string OrderTypes
        {
            get { return orderTypes; }
            set { orderTypes = value; }
        }
        /// <summary>
        /// The list of exchanges this contract is traded on.
        /// </summary>
        public string ValidExchanges
        {
            get { return validExchanges; }
            set { validExchanges = value; }
        }
        #endregion
    }
}