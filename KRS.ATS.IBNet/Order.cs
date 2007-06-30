/*
* Order.java
*
*/
using System;

namespace Krs.Ats.IBNet
{
    public class Order
    {
        #region Private Variables
        // main order fields
        private int orderId;
        private int clientId;
        private int permId;
        private ActionSide action;
        private int totalQuantity;
        private OrderType orderType;
        private double lmtPrice;
        private double auxPrice;
		
        // extended order fields
        private TimeInForce tif; // "Time in Force" - DAY, GTC, etc.
        private String ocaGroup; // one cancels all group name
        private OcaType ocaType; // 1 = CANCEL_WITH_BLOCK, 2 = REDUCE_WITH_BLOCK, 3 = REDUCE_NON_BLOCK
        private String orderRef;
        private bool transmit; // if false, order will be created but not transmited
        private int parentId; // Parent order Id, to associate Auto STP or TRAIL orders with the original order.
        private bool blockOrder;
        private bool sweepToFill;
        private int displaySize;
        private int triggerMethod; // 0=Default, 1=Double_Bid_Ask, 2=Last, 3=Double_Last, 4=Bid_Ask, 7=Last_or_Bid_Ask, 8=Mid-point
        private bool ignoreRth;
        private bool hidden;
        private String goodAfterTime; // FORMAT: 20060505 08:00:00 {time zone}
        private String goodTillDate; // FORMAT: 20060505 08:00:00 {time zone}
        private bool rthOnly;
        private bool overridePercentageConstraints;
        private String rule80A; // Individual = 'I', Agency = 'A', AgentOtherMember = 'W', IndividualPTIA = 'J', AgencyPTIA = 'U', AgentOtherMemberPTIA = 'M', IndividualPT = 'K', AgencyPT = 'Y', AgentOtherMemberPT = 'N'
        private bool allOrNone;
        private int minQty;
        private double percentOffset; // REL orders only
        private double trailStopPrice; // for TRAILLIMIT orders only
        private String sharesAllocation; // deprecated
		
        // Financial advisors only 
        private String faGroup;
        private String faProfile;
        private String faMethod;
        private String faPercentage;
		
        // Institutional orders only
        private String account;
        private String settlingFirm;
        private String openClose; // O=Open, C=Close
        private OrderOrigin origin; // 0=Customer, 1=Firm
        private int shortSaleSlot; // 1 if you hold the shares, 2 if they will be delivered from elsewhere.  Only for Action="SSHORT
        private String designatedLocation; // set when slot=2 only.
		
        // SMART routing only
        private double discretionaryAmt;
        private bool eTradeOnly;
        private bool firmQuoteOnly;
        private double nbboPriceCap;
		
        // BOX or VOL ORDERS ONLY
        private AuctionStrategy auctionStrategy; // 1=AUCTION_MATCH, 2=AUCTION_IMPROVEMENT, 3=AUCTION_TRANSPARENT
		
        // BOX ORDERS ONLY
        private double startingPrice;
        private double stockRefPrice;
        private double delta;
		
        // pegged to stock or VOL orders
        private double stockRangeLower;
        private double stockRangeUpper;
		
        // VOLATILITY ORDERS ONLY
        private double volatility;
        private int volatilityType; // 1=daily, 2=annual
        private int continuousUpdate;
        private int referencePriceType; // 1=Average, 2 = BidOrAsk
        private OrderType deltaNeutralOrderType;
        private double deltaNeutralAuxPrice;
		
        // COMBO ORDERS ONLY
        private double basisPoints; // EFP orders only
        private int basisPointsType; // EFP orders only
        #endregion

        #region Constructor
        public Order()
        {
            openClose = "O";
            origin = OrderOrigin.Customer;
            transmit = true;
            designatedLocation = "";
            minQty = System.Int32.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            percentOffset = System.Double.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            nbboPriceCap = System.Double.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            startingPrice = System.Double.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            stockRefPrice = System.Double.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            delta = System.Double.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            stockRangeLower = System.Double.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            stockRangeUpper = System.Double.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            volatility = System.Double.MaxValue;
            volatilityType = System.Int32.MaxValue;
            deltaNeutralOrderType = IBNet.OrderType.None;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            deltaNeutralAuxPrice = System.Double.MaxValue;
            referencePriceType = System.Int32.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            trailStopPrice = System.Double.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            basisPoints = System.Double.MaxValue;
            basisPointsType = System.Int32.MaxValue;
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
        /// The order type. identifies the order type. Valid values are:
        /// MKT
        /// MKTCLS
        /// LMT
        /// LMTCLS
        /// PEGMKT
        /// STP
        /// STPLMT
        /// TRAIL
        /// REL
        /// VWAP
        /// TRAILLIMIT
        /// VOL
        /// </summary>
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
        public double LmtPrice
        {
            get { return lmtPrice; }
            set { lmtPrice = value; }
        }

        /// <summary>
        /// This is the STOP price for stop-limit orders, and the offset amount for
        /// relative orders. In all other cases, specify zero.
        /// </summary>
        public double AuxPrice
        {
            get { return auxPrice; }
            set { auxPrice = value; }
        }

        /// <summary>
        /// The time in force. Valid values are: DAY, GTC, IOC, GTD.
        /// </summary>
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
        /// Tells how to handle remaining orders in an OCA group when one order or part of an order executes. Valid values include:
        /// 1 = Cancel all remaining orders with block
        /// 2 = Remaining orders are proportionately reduced in size with block
        /// 3 = Remaining orders are proportionately reduced in size with no block
        /// If you use a value "with block"gives your order has overfill protection. This means  that only one order in the group will be routed at a time to remove the possibility of an overfill.
        /// </summary>
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
        /// Specifies how Simulated Stop, Stop-Limit and Trailing Stop orders are triggered. Valid values are:
        /// 0 - the default value. The "double bid/ask" method will be used for orders for OTC stocks and US options. All other orders will used the "last" method.
        /// 1 - use "double bid/ask" method, where stop orders are triggered based on two consecutive bid or ask prices.
        /// 2 - "last" method, where stop orders are triggered based on the last price.
        /// 3 double last method.
        /// 4 bid/ask method.
        /// 7 last or bid/ask method.
        /// 8 mid-point method.
        /// </summary>
        public int TriggerMethod
        {
            get { return triggerMethod; }
            set { triggerMethod = value; }
        }

        /// <summary>
        /// If set to true, allows triggering of orders outside of regular trading hours.
        /// </summary>
        public bool IgnoreRth
        {
            get { return ignoreRth; }
            set { ignoreRth = value; }
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
        /// The trade's "Good After Time,"
        /// format "YYYYMMDD hh:mm:ss (optional time zone)" 
        /// Use an empty String if not applicable.
        /// </summary>
        public string GoodAfterTime
        {
            get { return goodAfterTime; }
            set { goodAfterTime = value; }
        }

        /// <summary>
        /// You must enter a tif value of GTD. The trade's "Good Till Date," format is:
        /// YYYYMMDD hh:mm:ss (optional time zone)
        /// Use an empty String if not applicable.
        /// </summary>
        public string GoodTillDate
        {
            get { return goodTillDate; }
            set { goodTillDate = value; }
        }

        /// <summary>
        /// Regular trading hours only.
        /// yes=1, no=0
        /// </summary>
        public bool RthOnly
        {
            get { return rthOnly; }
            set { rthOnly = value; }
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
        /// Individual = 'I'
        /// Agency = 'A',
        /// AgentOtherMember = 'W'
        /// IndividualPTIA = 'J'
        /// AgencyPTIA = 'U'
        /// AgentOtherMemberPTIA = 'M'
        /// IndividualPT = 'K'
        /// AgencyPT = 'Y'
        /// AgentOtherMemberPT = 'N' 
        /// </summary>
        public string Rule80A
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
        public double TrailStopPrice
        {
            get { return trailStopPrice; }
            set { trailStopPrice = value; }
        }

        /// <summary>
        /// Deprecated. Upgrade to new FA functionality must be done. 
        /// </summary>
        public string SharesAllocation
        {
            get { return sharesAllocation; }
            set { sharesAllocation = value; }
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
        public string FAMethod
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
        /// Specifies whether the order is an open or close order.
        /// For institutional customers only. Valid values are O, C.
        /// </summary>
        public string OpenClose
        {
            get { return openClose; }
            set { openClose = value; }
        }

        /// <summary>
        /// The order origin. For institutional customers only.
        /// Valid values are 0 = customer, 1 = firm
        /// </summary>
        public OrderOrigin Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        /// <summary>
        /// Values are 1 or 2.
        /// </summary>
        public int ShortSaleSlot
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
        public double DiscretionaryAmt
        {
            get { return discretionaryAmt; }
            set { discretionaryAmt = value; }
        }

        /// <summary>
        /// Trade with electronic quotes.
        /// yes = 1, no = 0
        /// </summary>
        public bool ETradeOnly
        {
            get { return eTradeOnly; }
            set { eTradeOnly = value; }
        }

        /// <summary>
        /// Trade with firm quotes.
        /// yes = 1, no = 0
        /// </summary>
        public bool FirmQuoteOnly
        {
            get { return firmQuoteOnly; }
            set { firmQuoteOnly = value; }
        }

        /// <summary>
        /// The maximum Smart order distance from the NBBO.
        /// </summary>
        public double NbboPriceCap
        {
            get { return nbboPriceCap; }
            set { nbboPriceCap = value; }
        }
        
        /// <summary>
        /// match = 1
        /// improvement = 2
        /// transparent = 3
        /// For BOX exchange only.
        /// </summary>
        public AuctionStrategy AuctionStrategy
        {
            get { return auctionStrategy; }
            set { auctionStrategy = value; }
        }

        /// <summary>
        /// The starting price. Valid on BOX orders only.
        /// </summary>
        public double StartingPrice
        {
            get { return startingPrice; }
            set { startingPrice = value; }
        }

        /// <summary>
        /// The stock reference price. The reference price is used for VOL orders
        /// to compute the limit price sent to an exchange (whether or not Continuous
        /// Update is selected), and for price range monitoring.
        /// </summary>
        public double StockRefPrice
        {
            get { return stockRefPrice; }
            set { stockRefPrice = value; }
        }

        /// <summary>
        /// The stock delta. Valid on BOX orders only.
        /// </summary>
        public double Delta
        {
            get { return delta; }
            set { delta = value; }
        }

        /// <summary>
        /// The lower value for the acceptable underlying stock price range.
        /// For price improvement option orders on BOX and VOL orders with dynamic management.
        /// </summary>
        public double StockRangeLower
        {
            get { return stockRangeLower; }
            set { stockRangeLower = value; }
        }

        /// <summary>
        /// The upper value for the acceptable underlying stock price range.
        /// For price improvement option orders on BOX and VOL orders with dynamic management.
        /// </summary>
        public double StockRangeUpper
        {
            get { return stockRangeUpper; }
            set { stockRangeUpper = value; }
        }

        /// <summary>
        /// What the price is, computed via TWSs Options Analytics.
        /// For VOL orders, the limit price sent to an exchange is not editable,
        /// as it is the output of a function.  Volatility is expressed as a percentage.
        /// </summary>
        public double Volatility
        {
            get { return volatility; }
            set { volatility = value; }
        }

        /// <summary>
        /// How the volatility is calculated.
        /// Daily = 1
        /// Annual = 2
        /// </summary>
        public int VolatilityType
        {
            get { return volatilityType; }
            set { volatilityType = value; }
        }

        /// <summary>
        /// Used for dynamic management of volatility orders. Determines whether TWS is
        /// supposed to update the order price as the underlying moves.  If selected,
        /// the limit price sent to an exchange is modified by TWS if the computed price
        /// of the option changes enough to warrant doing so.  This is very helpful in
        /// keeping the limit price sent to the exchange up to date as the underlying price changes.
        /// </summary>
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
        public double BasisPoints
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
        #endregion

        #region Object Override
        public  override bool Equals(System.Object obj)
        {
            if (this == obj)
                return true;
            else if (obj == null)
                return false;
			
            Order other = (Order) obj;
			
            if (permId == other.permId)
            {
                return true;
            }
			
            bool firstSetEquals = orderId == other.orderId && clientId == other.clientId && totalQuantity == other.totalQuantity && lmtPrice == other.lmtPrice && auxPrice == other.auxPrice && origin == other.origin && transmit == other.transmit && parentId == other.parentId && blockOrder == other.blockOrder && sweepToFill == other.sweepToFill && displaySize == other.displaySize && triggerMethod == other.triggerMethod && ignoreRth == other.ignoreRth && hidden == other.hidden && discretionaryAmt == other.discretionaryAmt && shortSaleSlot == other.shortSaleSlot && (System.Object) designatedLocation == (System.Object) other.designatedLocation && ocaType == other.ocaType && rthOnly == other.rthOnly && allOrNone == other.allOrNone && minQty == other.minQty && percentOffset == other.percentOffset && eTradeOnly == other.eTradeOnly && firmQuoteOnly == other.firmQuoteOnly && nbboPriceCap == other.nbboPriceCap && auctionStrategy == other.auctionStrategy && startingPrice == other.startingPrice && stockRefPrice == other.stockRefPrice && delta == other.delta && stockRangeLower == other.stockRangeLower && stockRangeUpper == other.stockRangeUpper && volatility == other.volatility && volatilityType == other.volatilityType && deltaNeutralAuxPrice == other.deltaNeutralAuxPrice && continuousUpdate == other.continuousUpdate && referencePriceType == other.referencePriceType && trailStopPrice == other.trailStopPrice;
			
            if (!firstSetEquals)
            {
                return false;
            }
            else
            {
                String thisOcaGroup = ocaGroup ?? "";
                String thisAccount = account ?? "";
                String thisOpenClose = openClose ?? "";
                String thisOrderRef = orderRef ?? "";
                String thisRule80A = rule80A ?? "";
                String thisSettlingFirm = settlingFirm ?? "";

                String otherOcaGroup = other.ocaGroup ?? "";
                String otherAccount = other.account ?? "";
                String otherOpenClose = other.openClose ?? "";
                String otherOrderRef = other.orderRef ?? "";
                String otherOrderGoodAfterTime = other.goodAfterTime ?? "";
                String otherOrderGoodTillDate = other.goodTillDate ?? "";
                String otherRule80A = other.rule80A ?? "";
                String otherSettlingFirm = other.settlingFirm ?? "";

                return action.Equals(other.action) && orderType.Equals(other.orderType) && tif.Equals(other.tif) && thisOcaGroup.Equals(otherOcaGroup) && thisAccount.Equals(otherAccount) && thisOpenClose.Equals(otherOpenClose) && thisOrderRef.Equals(otherOrderRef) && otherOrderGoodAfterTime.Equals(otherOrderGoodAfterTime) && otherOrderGoodTillDate.Equals(otherOrderGoodTillDate) && thisRule80A.Equals(otherRule80A) && thisSettlingFirm.Equals(otherSettlingFirm) && deltaNeutralOrderType.Equals(other.deltaNeutralOrderType);
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}