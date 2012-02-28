using System;
using System.Collections.ObjectModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Order class passed to Interactive Brokers to place an order.
    /// </summary>
    [Serializable()]
    public class Order
    {
        #region Private Variables

        // main order fields
        private ActionSide action;
        private bool allOrNone;
        private AuctionStrategy auctionStrategy; // 1=AUCTION_MATCH, 2=AUCTION_IMPROVEMENT, 3=AUCTION_TRANSPARENT
        private decimal auxPrice;
        private decimal basisPoints; // EFP orders only
        private int basisPointsType; // EFP orders only

        // extended order fields
        private bool blockOrder;
        private int clientId;
        private int continuousUpdate;
        private double delta;
        private double deltaNeutralAuxPrice;
        private OrderType deltaNeutralOrderType;
        private String designatedLocation; // set when slot=2 only.
        private int deltaNeutralConId;
        private string deltaNeutralSettlingFirm;
        private string deltaNeutralClearingAccount;
        private string deltaNeutralClearingIntent;

        // HEDGE ORDERS ONLY
        private string hedgeType; // 'D' - delta, 'B' - beta, 'F' - FX, 'P' - pair
        private string hedgeParam; // beta value for beta hedge, ratio for pair hedge

        // SMART routing only
        private decimal discretionaryAmt;
        private int displaySize;
        private bool eTradeOnly;
        private bool optOutSmartRouting;

        // Financial advisors only 
        private String faGroup;
        private FinancialAdvisorAllocationMethod faMethod;
        private String faPercentage;
        private String faProfile;
        private bool firmQuoteOnly;
        private String goodAfterTime; // FORMAT: 20060505 08:00:00 {time zone}
        private String goodTillDate; // FORMAT: 20060505 08:00:00 {time zone}
        private bool hidden;
        private bool? outsideRth;
        private decimal limitPrice;
        private int minQty;
        private decimal nbboPriceCap;
        private String ocaGroup; // one cancels all group name
        private OcaType ocaType; // 1 = CANCEL_WITH_BLOCK, 2 = REDUCE_WITH_BLOCK, 3 = REDUCE_NON_BLOCK

        // Institutional orders only
        private String openClose; // O=Open, C=Close
        private int orderId;
        private String orderRef;
        private OrderType orderType;
        private OrderOrigin origin; // 0=Customer, 1=Firm
        private bool overridePercentageConstraints;
        private int parentId; // Parent order Id, to associate Auto STP or TRAIL orders with the original order.
        private double percentOffset; // REL orders only
        private int permId;
        private int referencePriceType; // 1=Average, 2 = BidOrAsk

        private AgentDescription rule80A;
                                 // Individual = 'I', Agency = 'A', AgentOtherMember = 'W', IndividualPTIA = 'J', AgencyPTIA = 'U', AgentOtherMemberPTIA = 'M', IndividualPT = 'K', AgencyPT = 'Y', AgentOtherMemberPT = 'N'


        private ShortSaleSlot shortSaleSlot;
                    // 1 if you hold the shares, 2 if they will be delivered from elsewhere.  Only for Action="SSHORT
        private int exemptCode; //Code for short sale exemption orders

        // BOX ORDERS ONLY
        private decimal startingPrice;

        // pegged to stock or VOL orders
        private double stockRangeLower;
        private double stockRangeUpper;
        private double stockRefPrice;
        private bool sweepToFill;
        private TimeInForce tif; // "Time in Force" - DAY, GTC, etc.
        private int totalQuantity;
        private decimal trailStopPrice; // for TRAILLIMIT orders only
        private bool transmit; // if false, order will be created but not transmited

        private TriggerMethod triggerMethod;
                              // 0=Default, 1=Double_Bid_Ask, 2=Last, 3=Double_Last, 4=Bid_Ask, 7=Last_or_Bid_Ask, 8=Mid-point

        // VOLATILITY ORDERS ONLY
        private double volatility;
        private VolatilityType volatilityType; // 1=daily, 2=annual

        // SCALE ORDERS ONLY
        private int scaleInitLevelSize;
        private int scaleSubsLevelSize;
        private decimal scalePriceIncrement;

        // Clearing info
        private string account; // IB account
        private string settlingFirm;
        private string clearingAccount; // True beneficiary of the order
        private string clearingIntent; // "" (Default), "IB", "Away", "PTA" (PostTrade)

        // ALGO ORDERS ONLY
        private string algoStrategy;
        private Collection<TagValue> algoParams;

        // What-if
        private bool whatIf;

        // Not Held
        private bool notHeld;

        // Smart combo routing params
        private Collection<TagValue> smartComboRoutingParams;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Order()
        {
            openClose = "O";
            origin = OrderOrigin.Customer;
            transmit = true;
            tif = TimeInForce.Day;
            designatedLocation = "";
            minQty = Int32.MaxValue;
            percentOffset = Double.MaxValue;
            nbboPriceCap = decimal.MaxValue;
            startingPrice = decimal.MaxValue;
            stockRefPrice = Double.MaxValue;
            delta = Double.MaxValue;
            stockRangeLower = Double.MaxValue;
            stockRangeUpper = Double.MaxValue;
            volatility = Double.MaxValue;
            volatilityType = VolatilityType.Undefined;
            deltaNeutralOrderType = OrderType.Empty;
            deltaNeutralAuxPrice = Double.MaxValue;
            referencePriceType = Int32.MaxValue;
            trailStopPrice = decimal.MaxValue;
            basisPoints = decimal.MaxValue;
            basisPointsType = Int32.MaxValue;
            scaleInitLevelSize = Int32.MaxValue;
            scaleSubsLevelSize = Int32.MaxValue;
            scalePriceIncrement = decimal.MaxValue;
            faMethod = FinancialAdvisorAllocationMethod.None;
            notHeld = false;
            exemptCode = -1;
            
            optOutSmartRouting = false;
            deltaNeutralConId = 0;
            deltaNeutralOrderType = OrderType.Empty;
            deltaNeutralSettlingFirm = string.Empty;
            deltaNeutralClearingAccount = string.Empty;
            deltaNeutralClearingIntent = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The id for this order.
        /// </summary>
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        /// <summary>
        /// The id of the client that placed this order.
        /// </summary>
        public int ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

        /// <summary>
        /// The TWS id used to identify orders, remains the same over TWS sessions.
        /// </summary>
        public int PermId
        {
            get { return permId; }
            set { permId = value; }
        }

        /// <summary>
        /// Identifies the side. Valid values are: BUY, SELL, SSHORT
        /// </summary>
        public ActionSide Action
        {
            get { return action; }
            set { action = value; }
        }

        /// <summary>
        /// The order quantity.
        /// </summary>
        public int TotalQuantity
        {
            get { return totalQuantity; }
            set { totalQuantity = value; }
        }

        /// <summary>
        /// The order type.
        /// </summary>
        /// <seealso cref="OrderType"/>
        public OrderType OrderType
        {
            get { return orderType; }
            set { orderType = value; }
        }

        /// <summary>
        /// This is the LIMIT price, used for limit, stop-limit and relative orders.
        /// In all other cases specify zero. For relative orders with no limit price,
        /// also specify zero.
        /// </summary>
        public decimal LimitPrice
        {
            get { return limitPrice; }
            set { limitPrice = value; }
        }

        /// <summary>
        /// This is the STOP price for stop-limit orders, and the offset amount for
        /// relative orders. In all other cases, specify zero.
        /// </summary>
        public decimal AuxPrice
        {
            get { return auxPrice; }
            set { auxPrice = value; }
        }

        /// <summary>
        /// The time in force.
        /// </summary>
        /// <remarks>Valid values are: DAY, GTC, IOC, GTD.</remarks>
        /// <seealso cref="TimeInForce"/>
        public TimeInForce Tif
        {
            get { return tif; }
            set { tif = value; }
        }

        /// <summary>
        /// Identifies an OCA (one cancels all) group.
        /// </summary>
        public string OcaGroup
        {
            get { return ocaGroup; }
            set { ocaGroup = value; }
        }

        /// <summary>
        /// Tells how to handle remaining orders in an OCA group when one order or part of an order executes.
        /// </summary>
        /// <remarks>
        /// Valid values include:
        /// <list type="bullet">
        /// <item>1 = Cancel all remaining orders with block.</item>
        /// <item>2 = Remaining orders are proportionately reduced in size with block.</item>
        /// <item>3 = Remaining orders are proportionately reduced in size with no block.</item>
        /// </list>
        /// If you use a value "with block"gives your order has overfill protection. This means  that only one order in the group will be routed at a time to remove the possibility of an overfill.
        /// </remarks>
        /// <seealso cref="OcaType"/>
        public OcaType OcaType
        {
            get { return ocaType; }
            set { ocaType = value; }
        }

        /// <summary>
        /// The order reference. For institutional customers only.
        /// </summary>
        public string OrderRef
        {
            get { return orderRef; }
            set { orderRef = value; }
        }

        /// <summary>
        /// Specifies whether the order will be transmitted by TWS.
        /// If set to false, the order will be created at TWS but will not be sent.
        /// </summary>
        public bool Transmit
        {
            get { return transmit; }
            set { transmit = value; }
        }

        /// <summary>
        /// The order ID of the parent order, used for bracket and auto trailing stop orders.
        /// </summary>
        public int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }

        /// <summary>
        /// If set to true, specifies that the order is an ISE Block order.
        /// </summary>
        public bool BlockOrder
        {
            get { return blockOrder; }
            set { blockOrder = value; }
        }

        /// <summary>
        /// If set to true, specifies that the order is a Sweep-to-Fill order.
        /// </summary>
        public bool SweepToFill
        {
            get { return sweepToFill; }
            set { sweepToFill = value; }
        }

        /// <summary>
        /// The publicly disclosed order size, used when placing Iceberg orders.
        /// </summary>
        public int DisplaySize
        {
            get { return displaySize; }
            set { displaySize = value; }
        }

        /// <summary>
        /// Specifies how Simulated Stop, Stop-Limit and Trailing Stop orders are triggered.
        /// </summary>
        /// <remarks>
        /// Valid values are:
        /// <list type="bullet">
        /// <item>0 - the default value. The "double bid/ask" method will be used for orders for OTC stocks and US options. All other orders will used the "last" method.</item>
        /// <item>1 - use "double bid/ask" method, where stop orders are triggered based on two consecutive bid or ask prices.</item>
        /// <item>2 - "last" method, where stop orders are triggered based on the last price.</item>
        /// <item>3 - double last method.</item>
        /// <item>4 - bid/ask method.</item>
        /// <item>7 - last or bid/ask method.</item>
        /// <item>8 - mid-point method.</item>
        /// </list>
        /// </remarks>
        /// <seealso cref="TriggerMethod"/>
        public TriggerMethod TriggerMethod
        {
            get { return triggerMethod; }
            set { triggerMethod = value; }
        }

        /// <summary>
        /// If set to true, allows triggering of orders outside of regular trading hours.
        /// </summary>
        public bool? OutsideRth
        {
            get { return outsideRth; }
            set { outsideRth = value; }
        }

        /// <summary>
        /// If set to true, the order will not be visible when viewing the market depth.
        /// This option only applies to orders routed to the ISLAND exchange.
        /// </summary>
        public bool Hidden
        {
            get { return hidden; }
            set { hidden = value; }
        }

        /// <summary>
        /// The trade's "Good After Time"
        /// </summary>
        /// <remarks>format "YYYYMMDD hh:mm:ss (optional time zone)" 
        /// Use an empty String if not applicable.</remarks>
        public string GoodAfterTime
        {
            get { return goodAfterTime; }
            set { goodAfterTime = value; }
        }

        /// <summary>
        /// You must enter a Time in Force value of Good Till Date.
        /// </summary>
        /// <remarks>The trade's "Good Till Date," format is:
        /// YYYYMMDD hh:mm:ss (optional time zone)
        /// Use an empty String if not applicable.</remarks>
        public string GoodTillDate
        {
            get { return goodTillDate; }
            set { goodTillDate = value; }
        }

        /// <summary>
        /// If set, allows you to override TWS order price percentage constraints set to
        /// reject orders that deviate too far from the NBBO. This precaution was created
        /// to avoid transmitting orders with an incorrect price. 
        /// </summary>
        public bool OverridePercentageConstraints
        {
            get { return overridePercentageConstraints; }
            set { overridePercentageConstraints = value; }
        }

        /// <summary>
        /// This identifies what type of trader you are.
        /// </summary>
        /// <remarks>Rule80A required you to identify which type of trader you are.</remarks>
        /// <seealso cref="AgentDescription"/>
        public AgentDescription Rule80A
        {
            get { return rule80A; }
            set { rule80A = value; }
        }

        /// <summary>
        /// yes=1, no=0
        /// </summary>
        public bool AllOrNone
        {
            get { return allOrNone; }
            set { allOrNone = value; }
        }

        /// <summary>
        /// Identifies a minimum quantity order type.
        /// </summary>
        public int MinQty
        {
            get { return minQty; }
            set { minQty = value; }
        }

        /// <summary>
        /// The percent offset amount for relative orders.
        /// </summary>
        public double PercentOffset
        {
            get { return percentOffset; }
            set { percentOffset = value; }
        }

        /// <summary>
        /// For TRAILLIMIT orders only
        /// </summary>
        public decimal TrailStopPrice
        {
            get { return trailStopPrice; }
            set { trailStopPrice = value; }
        }

        /// <summary>
        /// The Financial Advisor group the trade will be allocated to -- use an empty String if not applicable.
        /// </summary>
        public string FAGroup
        {
            get { return faGroup; }
            set { faGroup = value; }
        }

        /// <summary>
        /// The Financial Advisor allocation profile the trade will be allocated to -- use an empty String if not applicable.
        /// </summary>
        public string FAProfile
        {
            get { return faProfile; }
            set { faProfile = value; }
        }

        /// <summary>
        /// The Financial Advisor allocation method the trade will be allocated with -- use an empty String if not applicable.
        /// </summary>
        public FinancialAdvisorAllocationMethod FAMethod
        {
            get { return faMethod; }
            set { faMethod = value; }
        }

        /// <summary>
        /// The Financial Advisor percentage concerning the trade's allocation -- use an empty String if not applicable.
        /// </summary>
        public string FAPercentage
        {
            get { return faPercentage; }
            set { faPercentage = value; }
        }

        /// <summary>
        /// Specifies whether the order is an open or close order.
        /// For institutional customers only. Valid values are O, C.
        /// </summary>
        public string OpenClose
        {
            get { return openClose; }
            set { openClose = value; }
        }

        /// <summary>
        /// The order origin.
        /// </summary>
        /// <remarks>For institutional customers only.</remarks>
        public OrderOrigin Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        /// <summary>
        /// ShortSaleSlot of Third Party requires DesignatedLocation to be specified. Non-empty DesignatedLocation values for all other cases will cause orders to be rejected.
        /// </summary>
        public ShortSaleSlot ShortSaleSlot
        {
            get { return shortSaleSlot; }
            set { shortSaleSlot = value; }
        }

        /// <summary>
        /// Use only when shortSaleSlot value = 2.
        /// </summary>
        public string DesignatedLocation
        {
            get { return designatedLocation; }
            set { designatedLocation = value; }
        }

        /// <summary>
        /// The amount off the limit price allowed for discretionary orders.
        /// </summary>
        public decimal DiscretionaryAmt
        {
            get { return discretionaryAmt; }
            set { discretionaryAmt = value; }
        }

        /// <summary>
        /// Trade with electronic quotes.
        /// </summary>
        public bool ETradeOnly
        {
            get { return eTradeOnly; }
            set { eTradeOnly = value; }
        }

        /// <summary>
        /// Trade with firm quotes.
        /// </summary>
        public bool FirmQuoteOnly
        {
            get { return firmQuoteOnly; }
            set { firmQuoteOnly = value; }
        }

        /// <summary>
        /// The maximum Smart order distance from the NBBO.
        /// </summary>
        public decimal NbboPriceCap
        {
            get { return nbboPriceCap; }
            set { nbboPriceCap = value; }
        }

        /// <summary>
        /// The auction strategy.
        /// </summary>
        /// <remarks>For BOX exchange only.</remarks>
        /// <seealso cref="AuctionStrategy"/>
        public AuctionStrategy AuctionStrategy
        {
            get { return auctionStrategy; }
            set { auctionStrategy = value; }
        }

        /// <summary>
        /// The starting price.
        /// </summary>
        /// <remarks>Valid on BOX orders only.</remarks>
        public decimal StartingPrice
        {
            get { return startingPrice; }
            set { startingPrice = value; }
        }

        /// <summary>
        /// The stock reference price.
        /// </summary>
        /// <remarks>The reference price is used for VOL orders
        /// to compute the limit price sent to an exchange (whether or not Continuous
        /// Update is selected), and for price range monitoring.</remarks>
        public double StockRefPrice
        {
            get { return stockRefPrice; }
            set { stockRefPrice = value; }
        }

        /// <summary>
        /// The stock delta.
        /// </summary>
        /// <remarks>Valid on BOX orders only.</remarks>
        public double Delta
        {
            get { return delta; }
            set { delta = value; }
        }

        /// <summary>
        /// The lower value for the acceptable underlying stock price range.
        /// </summary>
        /// <remarks>For price improvement option orders on BOX and VOL orders with dynamic management.</remarks>
        public double StockRangeLower
        {
            get { return stockRangeLower; }
            set { stockRangeLower = value; }
        }

        /// <summary>
        /// The upper value for the acceptable underlying stock price range.
        /// </summary>
        /// <remarks>For price improvement option orders on BOX and VOL orders with dynamic management.</remarks>
        public double StockRangeUpper
        {
            get { return stockRangeUpper; }
            set { stockRangeUpper = value; }
        }

        /// <summary>
        /// What the price is, computed via TWS's Options Analytics.
        /// </summary>
        /// <remarks>For VOL orders, the limit price sent to an exchange is not editable,
        /// as it is the output of a function.  Volatility is expressed as a percentage.</remarks>
        public double Volatility
        {
            get { return volatility; }
            set { volatility = value; }
        }

        /// <summary>
        /// How the volatility is calculated. 
        /// </summary>
        /// <seealso cref="VolatilityType"/>
        public VolatilityType VolatilityType
        {
            get { return volatilityType; }
            set { volatilityType = value; }
        }

        /// <summary>
        /// Used for dynamic management of volatility orders. 
        /// </summary>
        /// <remarks>Determines whether TWS is
        /// supposed to update the order price as the underlying moves.  If selected,
        /// the limit price sent to an exchange is modified by TWS if the computed price
        /// of the option changes enough to warrant doing so.  This is very helpful in
        /// keeping the limit price sent to the exchange up to date as the underlying price changes.</remarks>
        public int ContinuousUpdate
        {
            get { return continuousUpdate; }
            set { continuousUpdate = value; }
        }

        /// <summary>
        /// Used for dynamic management of volatility orders. Set to
        /// 1 = Average of National Best Bid or Ask, or set to
        /// 2 =  National Best Bid when buying a call or selling a put; and National Best Ask when selling a call or buying a put.
        /// </summary>
        public int ReferencePriceType
        {
            get { return referencePriceType; }
            set { referencePriceType = value; }
        }

        /// <summary>
        /// VOL orders only. Enter an order type to instruct TWS to submit a
        /// delta neutral trade on full or partial execution of the VOL order.
        /// For no hedge delta order to be sent, specify NONE.
        /// </summary>
        public OrderType DeltaNeutralOrderType
        {
            get { return deltaNeutralOrderType; }
            set { deltaNeutralOrderType = value; }
        }

        /// <summary>
        /// VOL orders only. Use this field to enter a value if  the value in the
        /// deltaNeutralOrderType field is an order type that requires an Aux price, such as a REL order. 
        /// </summary>
        public double DeltaNeutralAuxPrice
        {
            get { return deltaNeutralAuxPrice; }
            set { deltaNeutralAuxPrice = value; }
        }

        /// <summary>
        /// For EFP orders only
        /// </summary>
        public decimal BasisPoints
        {
            get { return basisPoints; }
            set { basisPoints = value; }
        }

        /// <summary>
        /// For EFP orders only
        /// </summary>
        public int BasisPointsType
        {
            get { return basisPointsType; }
            set { basisPointsType = value; }
        }

        /// <summary>
        /// split order into X buckets
        /// </summary>
        public int ScaleInitLevelSize
        {
            get { return scaleInitLevelSize; }
            set { scaleInitLevelSize = value; }
        }

        /// <summary>
        /// split order so each bucket is of the size X
        /// </summary>
        public int ScaleSubsLevelSize
        {
            get { return scaleSubsLevelSize; }
            set { scaleSubsLevelSize = value; }
        }

        /// <summary>
        /// price increment per bucket
        /// </summary>
        public decimal ScalePriceIncrement
        {
            get { return scalePriceIncrement; }
            set { scalePriceIncrement = value; }
        }

        /// <summary>
        /// The account. For institutional customers only.
        /// </summary>
        public string Account
        {
            get { return account; }
            set { account = value; }
        }

        /// <summary>
        /// Institutional only.
        /// </summary>
        public string SettlingFirm
        {
            get { return settlingFirm; }
            set { settlingFirm = value; }
        }

        /// <summary>
        /// Unknown - assume institutional only.
        /// </summary>
        public string ClearingAccount
        {
            get { return clearingAccount; }
            set { clearingAccount = value; }
        }

        /// <summary>
        /// Unknown - assume institutional only.
        /// </summary>
        public string ClearingIntent
        {
            get { return clearingIntent; }
            set { clearingIntent = value; }
        }

        /// <summary>
        /// Algorithm Strategy
        /// </summary>
        public string AlgoStrategy
        {
            get { return algoStrategy; }
            set { algoStrategy = value; }
        }

        /// <summary>
        /// List of Algorithm Parameters
        /// </summary>
        public Collection<TagValue> AlgoParams
        {
            get { return algoParams; }
            set { algoParams = value; }
        }

        /// <summary>
        /// When this value is set to true, margin and commission data is
        /// received back via a new OrderState() object for the openOrder() callback.
        /// </summary>
        public bool WhatIf
        {
            get { return whatIf; }
            set { whatIf = value; }
        }

        /// <summary>
        /// Not Held
        /// </summary>
        public bool NotHeld
        {
            get { return notHeld; }
            set { notHeld = value; }
        }

        /// <summary>
        /// Exempt Code for Short Sale Exemption Orders
        /// </summary>
        public int ExemptCode
        {
            get { return exemptCode; }
            set { exemptCode = value; }
        }

        /// <summary>
        /// Opt out of smart routing for directly routed ASX orders
        /// </summary>
        public bool OptOutSmartRouting
        {
            get { return optOutSmartRouting; }
            set { optOutSmartRouting = value; }
        }

        public int DeltaNeutralConId
        {
            get { return deltaNeutralConId; }
            set { deltaNeutralConId = value; }
        }

        public string DeltaNeutralSettlingFirm
        {
            get { return deltaNeutralSettlingFirm; }
            set { deltaNeutralSettlingFirm = value; }
        }

        public string DeltaNeutralClearingAccount
        {
            get { return deltaNeutralClearingAccount; }
            set { deltaNeutralClearingAccount = value; }
        }

        public string DeltaNeutralClearingIntent
        {
            get { return deltaNeutralClearingIntent; }
            set { deltaNeutralClearingIntent = value; }
        }

        public string HedgeType
        {
            get { return hedgeType; }
            set { hedgeType = value; }
        }

        public string HedgeParam
        {
            get { return hedgeParam; }
            set { hedgeParam = value; }
        }

        public Collection<TagValue> SmartComboRoutingParams
        {
            get { return smartComboRoutingParams; }
            set { smartComboRoutingParams = value; }
        }

        #endregion
    }
}