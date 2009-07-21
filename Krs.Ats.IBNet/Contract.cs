using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Class to describe a financial security.
    /// </summary>
    /// <seealso href="http://www.interactivebrokers.com/php/apiUsersGuide/apiguide/java/contract.htm">Interactive Brokers Contract Documentation</seealso>
    [Serializable()]
    public class Contract
    {
        #region Private Variables

        private readonly Collection<ComboLeg> comboLegs = new Collection<ComboLeg>();

        private int contractId;

        private String comboLegsDescription; // received in open order version 14 and up for all combos
        private String currency;
        private String exchange;
        private String expiry;
        private bool includeExpired; // can not be set to true for orders.
        private String localSymbol;
        private String multiplier;
        private SecurityIdType secIdType;        // CUSIP;SEDOL;ISIN;RIC
        private String secId;

        private String primaryExchange;
                       // pick a non-aggregate (ie not the SMART exchange) exchange that the contract trades on.  DO NOT SET TO SMART.

        private RightType right;
        private SecurityType securityType;
        private double strike;
        private String symbol;

        private UnderlyingComponent underlyingComponent;

        #endregion

        #region Constructors

        ///<summary>
        /// Undefined Contract Constructor
        ///</summary>
        public Contract() :
            this(0, null, SecurityType.Undefined, null, 0, RightType.Undefined, null, null, null, null, null, SecurityIdType.None, string.Empty)
        {
        }

        /// <summary>
        /// Futures Contract Constructor
        /// </summary>
        /// <param name="symbol">This is the symbol of the underlying asset.</param>
        /// <param name="exchange">The order destination, such as Smart.</param>
        /// <param name="securityType">This is the security type.</param>
        /// <param name="currency">Specifies the currency.</param>
        /// <param name="expiry">The expiration date. Use the format YYYYMM.</param>
        public Contract(string symbol, string exchange, SecurityType securityType, string currency, string expiry) :
            this(0, symbol, securityType, expiry, 0, RightType.Undefined, null, exchange, currency, null, null, SecurityIdType.None, string.Empty)
        {
        }

        /// <summary>
        /// Indice Contract Constructor
        /// </summary>
        /// <param name="symbol">This is the symbol of the underlying asset.</param>
        /// <param name="exchange">The order destination, such as Smart.</param>
        /// <param name="securityType">This is the security type.</param>
        /// <param name="currency">Specifies the currency.</param>
        public Contract(string symbol, string exchange, SecurityType securityType, string currency)
            :
            this(0, symbol, securityType, null, 0, RightType.Undefined, null, exchange, currency, null, null, SecurityIdType.None, string.Empty)
        {
        }

        /// <summary>
        /// Default Contract Constructor
        /// </summary>
        /// <param name="contractId">The unique contract identifier.</param>
        /// <param name="symbol">This is the symbol of the underlying asset.</param>
        /// <param name="securityType">This is the security type.</param>
        /// <param name="expiry">The expiration date. Use the format YYYYMM.</param>
        /// <param name="strike">The strike price.</param>
        /// <param name="right">Specifies a Put or Call.</param>
        /// <param name="multiplier">Allows you to specify a future or option contract multiplier.
        /// This is only necessary when multiple possibilities exist.</param>
        /// <param name="exchange">The order destination, such as Smart.</param>
        /// <param name="currency">Specifies the currency.</param>
        /// <param name="localSymbol">This is the local exchange symbol of the underlying asset.</param>
        /// <param name="primaryExchange">Identifies the listing exchange for the contract (do not list SMART).</param>
        /// <param name="secIdType">Security identifier, when querying contract details or when placing orders.</param>
        /// <param name="secId">Unique identifier for the secIdType.</param>
        public Contract(int contractId, String symbol, SecurityType securityType, String expiry, double strike, RightType right,
                        String multiplier, string exchange, string currency, string localSymbol, string primaryExchange,
                        SecurityIdType secIdType, string secId)
        {
            this.contractId = contractId;
            this.symbol = symbol;
            this.securityType = securityType;
            this.expiry = expiry;
            this.strike = strike;
            this.right = right;
            this.multiplier = multiplier;
            this.exchange = exchange;

            this.currency = currency;
            this.localSymbol = localSymbol;
            this.primaryExchange = primaryExchange;
            this.secIdType = secIdType;
            this.secId = secId;
        }

        /// <summary>
        /// Get a Contract by its unique contractId
        /// </summary>
        /// <param name="contractId"></param>
        public Contract(int contractId)
        {
            this.contractId = contractId;
        }

        #endregion

        #region Properties

        /// <summary>
        /// This is the symbol of the underlying asset.
        /// </summary>
        public string Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        /// <summary>
        /// This is the security type.
        /// </summary>
        /// <remarks>Valid security types are:
        /// <list type="bullet">
        /// <item>Stock</item>
        /// <item>Option</item>
        /// <item>Future</item>
        /// <item>Indice</item>
        /// <item>Option on Future</item>
        /// <item>Cash</item>
        /// <item>Bag</item>
        /// <item>Bond</item>
        /// </list>
        /// </remarks>
        /// <seealso cref="IBNet.SecurityType"/>
        public SecurityType SecurityType
        {
            get { return securityType; }
            set { securityType = value; }
        }

        /// <summary>
        /// The expiration date. Use the format YYYYMM.
        /// </summary>
        public string Expiry
        {
            get { return expiry; }
            set { expiry = value; }
        }

        /// <summary>
        /// The strike price.
        /// </summary>
        public double Strike
        {
            get { return strike; }
            set { strike = value; }
        }

        /// <summary>
        /// Specifies a Put or Call.
        /// </summary>
        /// <remarks>Valid values are:
        /// <list type="bullet">
        /// <item>Put - the right to sell a security.</item>
        /// <item>Call - the right to buy a security.</item>
        /// </list>
        /// </remarks>
        /// <seealso cref="RightType"/>
        public RightType Right
        {
            get { return right; }
            set { right = value; }
        }

        /// <summary>
        /// Allows you to specify a future or option contract multiplier.
        /// This is only necessary when multiple possibilities exist.
        /// </summary>
        public string Multiplier
        {
            get { return multiplier; }
            set { multiplier = value; }
        }

        /// <summary>
        /// The order destination, such as Smart.
        /// </summary>
        public string Exchange
        {
            get { return exchange; }
            set { exchange = value; }
        }

        /// <summary>
        /// Specifies the currency.
        /// </summary>
        /// <remarks>
        /// Ambiguities may require that this field be specified,
        /// for example, when SMART is the exchange and IBM is being requested
        /// (IBM can trade in GBP or USD).  Given the existence of this kind of ambiguity,
        /// it is a good idea to always specify the currency.
        /// </remarks>
        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        /// <summary>
        /// This is the local exchange symbol of the underlying asset.
        /// </summary>
        public string LocalSymbol
        {
            get { return localSymbol; }
            set { localSymbol = value; }
        }

        /// <summary>
        /// Identifies the listing exchange for the contract (do not list SMART).
        /// </summary>
        public string PrimaryExchange
        {
            get { return primaryExchange; }
            set { primaryExchange = value; }
        }

        /// <summary>
        /// If set to true, contract details requests and historical data queries
        /// can be performed pertaining to expired contracts.
        /// 
        /// Historical data queries on expired contracts are limited to the
        /// last year of the contracts life, and are initially only supported for
        /// expired futures contracts,
        /// </summary>
        public bool IncludeExpired
        {
            get { return includeExpired; }
            set { includeExpired = value; }
        }

        /// <summary>
        /// Description for combo legs
        /// </summary>
        public string ComboLegsDescription
        {
            get { return comboLegsDescription; }
            set { comboLegsDescription = value; }
        }

        /// <summary>
        /// Dynamic memory structure used to store the leg definitions for this contract.
        /// </summary>
        public Collection<ComboLeg> ComboLegs
        {
            get { return comboLegs; }
        }

        /// <summary>
        /// The unique contract identifier.
        /// </summary>
        public int ContractId
        {
            get { return contractId; }
            set { contractId = value; }
        }

        /// <summary>
        /// Underlying Component
        /// </summary>
        public UnderlyingComponent UnderlyingComponent
        {
            get { return underlyingComponent; }
            set { underlyingComponent = value; }
        }

        /// <summary>
        /// Security identifier, when querying contract details or when placing orders. Supported identifiers are:
        /// ISIN (Example: Apple: US0378331005)
        /// CUSIP (Example: Apple: 037833100)
        /// SEDOL (Consists of 6-AN + check digit. Example: BAE: 0263494)
        /// RIC (Consists of exchange-independent RIC Root and a suffix identifying the exchange. Example: AAPL.O for Apple on NASDAQ.)
        /// </summary>
        public SecurityIdType SecIdType
        {
            get { return secIdType; }
            set { secIdType = value; }
        } 

        /// <summary>
        /// Unique identifier for the secIdType.
        /// </summary>
        public String SecId
        {
            get
            {
                return secId;
            }
            set
            {
                secId = value;
            }
        }

        #endregion
    }
}