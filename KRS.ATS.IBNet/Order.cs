/*
* Order.java
*
*/
using System;

namespace KRS.ATS.IBNet
{
    public class Order
    {
        public const int CUSTOMER = 0;
        public const int FIRM = 1;
        public const char OPT_UNKNOWN = '?';
        public const char OPT_BROKER_DEALER = 'b';
        public const char OPT_CUSTOMER = 'c';
        public const char OPT_FIRM = 'f';
        public const char OPT_ISEMM = 'm';
        public const char OPT_FARMM = 'n';
        public const char OPT_SPECIALIST = 'y';
        public const int AUCTION_MATCH = 1;
        public const int AUCTION_IMPROVEMENT = 2;
        public const int AUCTION_TRANSPARENT = 3;
        public const System.String EMPTY_STR = "";
		
        // main order fields
        public int m_orderId;
        public int m_clientId;
        public int m_permId;
        public System.String m_action;
        public int m_totalQuantity;
        public System.String m_orderType;
        public double m_lmtPrice;
        public double m_auxPrice;
		
        // extended order fields
        public System.String m_tif; // "Time in Force" - DAY, GTC, etc.
        public System.String m_ocaGroup; // one cancels all group name
        public int m_ocaType; // 1 = CANCEL_WITH_BLOCK, 2 = REDUCE_WITH_BLOCK, 3 = REDUCE_NON_BLOCK
        public System.String m_orderRef;
        public bool m_transmit; // if false, order will be created but not transmited
        public int m_parentId; // Parent order Id, to associate Auto STP or TRAIL orders with the original order.
        public bool m_blockOrder;
        public bool m_sweepToFill;
        public int m_displaySize;
        public int m_triggerMethod; // 0=Default, 1=Double_Bid_Ask, 2=Last, 3=Double_Last, 4=Bid_Ask, 7=Last_or_Bid_Ask, 8=Mid-point
        public bool m_ignoreRth;
        public bool m_hidden;
        public System.String m_goodAfterTime; // FORMAT: 20060505 08:00:00 {time zone}
        public System.String m_goodTillDate; // FORMAT: 20060505 08:00:00 {time zone}
        public bool m_rthOnly;
        public bool m_overridePercentageConstraints;
        public System.String m_rule80A; // Individual = 'I', Agency = 'A', AgentOtherMember = 'W', IndividualPTIA = 'J', AgencyPTIA = 'U', AgentOtherMemberPTIA = 'M', IndividualPT = 'K', AgencyPT = 'Y', AgentOtherMemberPT = 'N'
        public bool m_allOrNone;
        public int m_minQty;
        public double m_percentOffset; // REL orders only
        public double m_trailStopPrice; // for TRAILLIMIT orders only
        public System.String m_sharesAllocation; // deprecated
		
        // Financial advisors only 
        public System.String m_faGroup;
        public System.String m_faProfile;
        public System.String m_faMethod;
        public System.String m_faPercentage;
		
        // Institutional orders only
        public System.String m_account;
        public System.String m_settlingFirm;
        public System.String m_openClose; // O=Open, C=Close
        public int m_origin; // 0=Customer, 1=Firm
        public int m_shortSaleSlot; // 1 if you hold the shares, 2 if they will be delivered from elsewhere.  Only for Action="SSHORT
        public System.String m_designatedLocation; // set when slot=2 only.
		
        // SMART routing only
        public double m_discretionaryAmt;
        public bool m_eTradeOnly;
        public bool m_firmQuoteOnly;
        public double m_nbboPriceCap;
		
        // BOX or VOL ORDERS ONLY
        public int m_auctionStrategy; // 1=AUCTION_MATCH, 2=AUCTION_IMPROVEMENT, 3=AUCTION_TRANSPARENT
		
        // BOX ORDERS ONLY
        public double m_startingPrice;
        public double m_stockRefPrice;
        public double m_delta;
		
        // pegged to stock or VOL orders
        public double m_stockRangeLower;
        public double m_stockRangeUpper;
		
        // VOLATILITY ORDERS ONLY
        public double m_volatility;
        public int m_volatilityType; // 1=daily, 2=annual
        public int m_continuousUpdate;
        public int m_referencePriceType; // 1=Average, 2 = BidOrAsk
        public System.String m_deltaNeutralOrderType;
        public double m_deltaNeutralAuxPrice;
		
        // COMBO ORDERS ONLY
        public double m_basisPoints; // EFP orders only
        public int m_basisPointsType; // EFP orders only
		
        public Order()
        {
            m_openClose = "O";
            m_origin = CUSTOMER;
            m_transmit = true;
            m_designatedLocation = EMPTY_STR;
            m_minQty = System.Int32.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            m_percentOffset = System.Double.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            m_nbboPriceCap = System.Double.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            m_startingPrice = System.Double.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            m_stockRefPrice = System.Double.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            m_delta = System.Double.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            m_stockRangeLower = System.Double.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            m_stockRangeUpper = System.Double.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            m_volatility = System.Double.MaxValue;
            m_volatilityType = System.Int32.MaxValue;
            m_deltaNeutralOrderType = EMPTY_STR;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            m_deltaNeutralAuxPrice = System.Double.MaxValue;
            m_referencePriceType = System.Int32.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            m_trailStopPrice = System.Double.MaxValue;
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            m_basisPoints = System.Double.MaxValue;
            m_basisPointsType = System.Int32.MaxValue;
        }
		
        public  override bool Equals(System.Object p_other)
        {
            if (this == p_other)
                return true;
            else if (p_other == null)
                return false;
			
            Order l_theOther = (Order) p_other;
			
            if (m_permId == l_theOther.m_permId)
            {
                return true;
            }
			
            bool firstSetEquals = m_orderId == l_theOther.m_orderId && m_clientId == l_theOther.m_clientId && m_totalQuantity == l_theOther.m_totalQuantity && m_lmtPrice == l_theOther.m_lmtPrice && m_auxPrice == l_theOther.m_auxPrice && m_origin == l_theOther.m_origin && m_transmit == l_theOther.m_transmit && m_parentId == l_theOther.m_parentId && m_blockOrder == l_theOther.m_blockOrder && m_sweepToFill == l_theOther.m_sweepToFill && m_displaySize == l_theOther.m_displaySize && m_triggerMethod == l_theOther.m_triggerMethod && m_ignoreRth == l_theOther.m_ignoreRth && m_hidden == l_theOther.m_hidden && m_discretionaryAmt == l_theOther.m_discretionaryAmt && m_shortSaleSlot == l_theOther.m_shortSaleSlot && (System.Object) m_designatedLocation == (System.Object) l_theOther.m_designatedLocation && m_ocaType == l_theOther.m_ocaType && m_rthOnly == l_theOther.m_rthOnly && m_allOrNone == l_theOther.m_allOrNone && m_minQty == l_theOther.m_minQty && m_percentOffset == l_theOther.m_percentOffset && m_eTradeOnly == l_theOther.m_eTradeOnly && m_firmQuoteOnly == l_theOther.m_firmQuoteOnly && m_nbboPriceCap == l_theOther.m_nbboPriceCap && m_auctionStrategy == l_theOther.m_auctionStrategy && m_startingPrice == l_theOther.m_startingPrice && m_stockRefPrice == l_theOther.m_stockRefPrice && m_delta == l_theOther.m_delta && m_stockRangeLower == l_theOther.m_stockRangeLower && m_stockRangeUpper == l_theOther.m_stockRangeUpper && m_volatility == l_theOther.m_volatility && m_volatilityType == l_theOther.m_volatilityType && m_deltaNeutralAuxPrice == l_theOther.m_deltaNeutralAuxPrice && m_continuousUpdate == l_theOther.m_continuousUpdate && m_referencePriceType == l_theOther.m_referencePriceType && m_trailStopPrice == l_theOther.m_trailStopPrice;
			
            if (!firstSetEquals)
            {
                return false;
            }
            else
            {
                System.String l_thisAction = m_action != null?m_action:EMPTY_STR;
                System.String l_thisOrderType = m_orderType != null?m_orderType:EMPTY_STR;
                System.String l_thisTif = m_tif != null?m_tif:EMPTY_STR;
                System.String l_thisOcaGroup = m_ocaGroup != null?m_ocaGroup:EMPTY_STR;
                System.String l_thisAccount = m_account != null?m_account:EMPTY_STR;
                System.String l_thisOpenClose = m_openClose != null?m_openClose:EMPTY_STR;
                System.String l_thisOrderRef = m_orderRef != null?m_orderRef:EMPTY_STR;
                System.String l_thisRule80A = m_rule80A != null?m_rule80A:EMPTY_STR;
                System.String l_thisSettlingFirm = m_settlingFirm != null?m_settlingFirm:EMPTY_STR;
                System.String l_thisDeltaNeutralOrderType = m_deltaNeutralOrderType != null?m_deltaNeutralOrderType:EMPTY_STR;
				
                System.String l_otherAction = l_theOther.m_action != null?l_theOther.m_action:EMPTY_STR;
                System.String l_otherOrderType = l_theOther.m_orderType != null?l_theOther.m_orderType:EMPTY_STR;
                System.String l_otherTif = l_theOther.m_tif != null?l_theOther.m_tif:EMPTY_STR;
                System.String l_otherOcaGroup = l_theOther.m_ocaGroup != null?l_theOther.m_ocaGroup:EMPTY_STR;
                System.String l_otherAccount = l_theOther.m_account != null?l_theOther.m_account:EMPTY_STR;
                System.String l_otherOpenClose = l_theOther.m_openClose != null?l_theOther.m_openClose:EMPTY_STR;
                System.String l_otherOrderRef = l_theOther.m_orderRef != null?l_theOther.m_orderRef:EMPTY_STR;
                System.String l_otherOrderGoodAfterTime = l_theOther.m_goodAfterTime != null?l_theOther.m_goodAfterTime:EMPTY_STR;
                System.String l_otherOrderGoodTillDate = l_theOther.m_goodTillDate != null?l_theOther.m_goodTillDate:EMPTY_STR;
                System.String l_otherRule80A = l_theOther.m_rule80A != null?l_theOther.m_rule80A:EMPTY_STR;
                System.String l_otherSettlingFirm = l_theOther.m_settlingFirm != null?l_theOther.m_settlingFirm:EMPTY_STR;
                System.String l_otherDeltaNeutralOrderType = l_theOther.m_deltaNeutralOrderType != null?l_theOther.m_deltaNeutralOrderType:EMPTY_STR;
				
                return l_thisAction.Equals(l_otherAction) && l_thisOrderType.Equals(l_otherOrderType) && l_thisTif.Equals(l_otherTif) && l_thisOcaGroup.Equals(l_otherOcaGroup) && l_thisAccount.Equals(l_otherAccount) && l_thisOpenClose.Equals(l_otherOpenClose) && l_thisOrderRef.Equals(l_otherOrderRef) && l_otherOrderGoodAfterTime.Equals(l_otherOrderGoodAfterTime) && l_otherOrderGoodTillDate.Equals(l_otherOrderGoodTillDate) && l_thisRule80A.Equals(l_otherRule80A) && l_thisSettlingFirm.Equals(l_otherSettlingFirm) && l_thisDeltaNeutralOrderType.Equals(l_otherDeltaNeutralOrderType);
            }
        }
        //UPGRADE_NOTE: The following method implementation was automatically added to preserve functionality. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1306'"
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}