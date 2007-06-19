using System;
using KRS.ATS.IBNet;

namespace KRS.ATS.IBNet
{
    public class EWrapperMsgGenerator:AnyWrapperMsgGenerator
    {
        public const System.String SCANNER_PARAMETERS = "SCANNER PARAMETERS:";
        public const System.String FINANCIAL_ADVISOR = "FA:";
		
        static public System.String tickPrice(int tickerId, int field, double price, int canAutoExecute)
        {
            return "id=" + tickerId + "  " + TickType.getField(field) + "=" + price + " " + ((canAutoExecute != 0)?" canAutoExecute":" noAutoExecute");
        }
		
        static public System.String tickSize(int tickerId, int field, int size)
        {
            return "id=" + tickerId + "  " + TickType.getField(field) + "=" + size;
        }
		
        static public System.String tickOptionComputation(int tickerId, int field, double impliedVol, double delta, double modelPrice, double pvDividend)
        {
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            System.String toAdd = "id=" + tickerId + "  " + TickType.getField(field) + ": vol = " + ((impliedVol >= 0 && impliedVol != System.Double.MaxValue)?impliedVol.ToString():"N/A") + " delta = " + ((System.Math.Abs(delta) <= 1)?delta.ToString():"N/A");
            if (field == TickType.MODEL_OPTION)
            {
                //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                toAdd += (": modelPrice = " + ((modelPrice >= 0 && modelPrice != System.Double.MaxValue)?modelPrice.ToString():"N/A"));
                //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                toAdd += (": pvDividend = " + ((pvDividend >= 0 && pvDividend != System.Double.MaxValue)?pvDividend.ToString():"N/A"));
            }
            return toAdd;
        }
		
        static public System.String tickGeneric(int tickerId, int tickType, double value_Renamed)
        {
            return "id=" + tickerId + "  " + TickType.getField(tickType) + "=" + value_Renamed;
        }
		
        static public System.String tickString(int tickerId, int tickType, System.String value_Renamed)
        {
            return "id=" + tickerId + "  " + TickType.getField(tickType) + "=" + value_Renamed;
        }
		
        static public System.String tickEFP(int tickerId, int tickType, double basisPoints, System.String formattedBasisPoints, double impliedFuture, int holdDays, System.String futureExpiry, double dividendImpact, double dividendsToExpiry)
        {
            return "id=" + tickerId + "  " + TickType.getField(tickType) + ": basisPoints = " + basisPoints + "/" + formattedBasisPoints + " impliedFuture = " + impliedFuture + " holdDays = " + holdDays + " futureExpiry = " + futureExpiry + " dividendImpact = " + dividendImpact + " dividends to expiry = " + dividendsToExpiry;
        }
		
        static public System.String orderStatus(int orderId, System.String status, int filled, int remaining, double avgFillPrice, int permId, int parentId, double lastFillPrice, int clientId)
        {
            return "order status: orderId=" + orderId + " clientId=" + clientId + " permId=" + permId + " status=" + status + " filled=" + filled + " remaining=" + remaining + " avgFillPrice=" + avgFillPrice + " lastFillPrice=" + lastFillPrice + " parent Id=" + parentId;
        }
		
        static public System.String openOrder(int orderId, Contract contract, Order order)
        {
            System.String msg = "open order: orderId=" + orderId + " action=" + order.m_action + " quantity=" + order.m_totalQuantity + " symbol=" + contract.m_symbol + " exchange=" + contract.m_exchange + " secType=" + contract.m_secType + " type=" + order.m_orderType + " lmtPrice=" + order.m_lmtPrice + " auxPrice=" + order.m_auxPrice + " TIF=" + order.m_tif + " localSymbol=" + contract.m_localSymbol + " client Id=" + order.m_clientId + " parent Id=" + order.m_parentId + " permId=" + order.m_permId + " ignoreRth=" + order.m_ignoreRth + " hidden=" + order.m_hidden + " discretionaryAmt=" + order.m_discretionaryAmt + " triggerMethod=" + order.m_triggerMethod + " goodAfterTime=" + order.m_goodAfterTime + " goodTillDate=" + order.m_goodTillDate + " account=" + order.m_account + " allocation=" + order.m_sharesAllocation + " faGroup=" + order.m_faGroup + " faMethod=" + order.m_faMethod + " faPercentage=" + order.m_faPercentage + " faProfile=" + order.m_faProfile + " shortSaleSlot=" + order.m_shortSaleSlot + " designatedLocation=" + order.m_designatedLocation + " ocaGroup=" + order.m_ocaGroup + " ocaType=" + order.m_ocaType + " rthOnly=" + order.m_rthOnly + " rule80A=" + order.m_rule80A + " settlingFirm=" + order.m_settlingFirm + " allOrNone=" + order.m_allOrNone + " minQty=" + order.m_minQty + " percentOffset=" + order.m_percentOffset + " eTradeOnly=" + order.m_eTradeOnly + " firmQuoteOnly=" + order.m_firmQuoteOnly + " nbboPriceCap=" + order.m_nbboPriceCap + " auctionStrategy=" + order.m_auctionStrategy + " startingPrice=" + order.m_startingPrice + " stockRefPrice=" + order.m_stockRefPrice + " delta=" + order.m_delta + " stockRangeLower=" + order.m_stockRangeLower + " stockRangeUpper=" + order.m_stockRangeUpper + " volatility=" + order.m_volatility + " volatilityType=" + order.m_volatilityType + " deltaNeutralOrderType=" + order.m_deltaNeutralOrderType + " deltaNeutralAuxPrice=" + order.m_deltaNeutralAuxPrice + " continuousUpdate=" + order.m_continuousUpdate + " referencePriceType=" + order.m_referencePriceType + 
                                " trailStopPrice=" + order.m_trailStopPrice;
			
            if ("BAG".Equals(contract.m_secType))
            {
                if (contract.m_comboLegsDescrip != null)
                {
                    msg += (" comboLegsDescrip=" + contract.m_comboLegsDescrip);
                }
                //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                if (order.m_basisPoints != System.Double.MaxValue)
                {
                    msg += (" basisPoints=" + order.m_basisPoints);
                    msg += (" basisPointsType=" + order.m_basisPointsType);
                }
            }
            return msg;
        }
		
        static public System.String updateAccountValue(System.String key, System.String value_Renamed, System.String currency, System.String accountName)
        {
            return "updateAccountValue: " + key + " " + value_Renamed + " " + currency + " " + accountName;
        }
		
        static public System.String updatePortfolio(Contract contract, int position, double marketPrice, double marketValue, double averageCost, double unrealizedPNL, double realizedPNL, System.String accountName)
        {
            System.String msg = "updatePortfolio: " + contractMsg(contract) + position + " " + marketPrice + " " + marketValue + " " + averageCost + " " + unrealizedPNL + " " + realizedPNL + " " + accountName;
            return msg;
        }
		
        static public System.String updateAccountTime(System.String timeStamp)
        {
            return "updateAccountTime: " + timeStamp;
        }
		
        static public System.String nextValidId(int orderId)
        {
            return "Next Valid Order ID: " + orderId;
        }
		
        static public System.String contractDetails(ContractDetails contractDetails)
        {
            Contract contract = contractDetails.m_summary;
            System.String msg = " ---- Contract Details begin ----\n" + contractMsg(contract) + contractDetailsMsg(contractDetails) + " ---- Contract Details End ----\n";
            return msg;
        }
		
        private static System.String contractDetailsMsg(ContractDetails contractDetails)
        {
            System.String msg = "marketName = " + contractDetails.m_marketName + "\n" + "tradingClass = " + contractDetails.m_tradingClass + "\n" + "conid = " + contractDetails.m_conid + "\n" + "minTick = " + contractDetails.m_minTick + "\n" + "multiplier = " + contractDetails.m_multiplier + "\n" + "price magnifier = " + contractDetails.m_priceMagnifier + "\n" + "orderTypes = " + contractDetails.m_orderTypes + "\n" + "validExchanges = " + contractDetails.m_validExchanges + "\n";
            return msg;
        }
		
        static public System.String contractMsg(Contract contract)
        {
            System.String msg = " ---- Contract Details begin ----\n" + "symbol = " + contract.m_symbol + "\n" + "secType = " + contract.m_secType + "\n" + "expiry = " + contract.m_expiry + "\n" + "strike = " + contract.m_strike + "\n" + "right = " + contract.m_right + "\n" + "exchange = " + contract.m_exchange + "\n" + "currency = " + contract.m_currency + "\n" + "localSymbol = " + contract.m_localSymbol + "\n";
            return msg;
        }
		
        static public System.String bondContractDetails(ContractDetails contractDetails)
        {
            Contract contract = contractDetails.m_summary;
            System.String msg = " ---- Bond Contract Details begin ----\n" + "symbol = " + contract.m_symbol + "\n" + "secType = " + contract.m_secType + "\n" + "cusip = " + contract.m_cusip + "\n" + "coupon = " + contract.m_coupon + "\n" + "maturity = " + contract.m_maturity + "\n" + "issueDate = " + contract.m_issueDate + "\n" + "ratings = " + contract.m_ratings + "\n" + "bondType = " + contract.m_bondType + "\n" + "couponType = " + contract.m_couponType + "\n" + "convertible = " + contract.m_convertible + "\n" + "callable = " + contract.m_callable + "\n" + "putable = " + contract.m_putable + "\n" + "descAppend = " + contract.m_descAppend + "\n" + "exchange = " + contract.m_exchange + "\n" + "currency = " + contract.m_currency + "\n" + "marketName = " + contractDetails.m_marketName + "\n" + "tradingClass = " + contractDetails.m_tradingClass + "\n" + "conid = " + contractDetails.m_conid + "\n" + "minTick = " + contractDetails.m_minTick + "\n" + "orderTypes = " + contractDetails.m_orderTypes + "\n" + "validExchanges = " + contractDetails.m_validExchanges + "\n" + "nextOptionDate = " + contract.m_nextOptionDate + "\n" + "nextOptionType = " + contract.m_nextOptionType + "\n" + "nextOptionPartial = " + contract.m_nextOptionPartial + "\n" + "notes = " + contract.m_notes + "\n" + " ---- Bond Contract Details End ----\n";
            return msg;
        }
		
        static public System.String execDetails(int orderId, Contract contract, Execution execution)
        {
            System.String msg = " ---- Execution Details begin ----\n" + "orderId = " + System.Convert.ToString(orderId) + "\n" + "clientId = " + System.Convert.ToString(execution.m_clientId) + "\n" + "symbol = " + contract.m_symbol + "\n" + "secType = " + contract.m_secType + "\n" + "expiry = " + contract.m_expiry + "\n" + "strike = " + contract.m_strike + "\n" + "right = " + contract.m_right + "\n" + "contractExchange = " + contract.m_exchange + "\n" + "currency = " + contract.m_currency + "\n" + "localSymbol = " + contract.m_localSymbol + "\n" + "execId = " + execution.m_execId + "\n" + "time = " + execution.m_time + "\n" + "acctNumber = " + execution.m_acctNumber + "\n" + "executionExchange = " + execution.m_exchange + "\n" + "side = " + execution.m_side + "\n" + "shares = " + execution.m_shares + "\n" + "price = " + execution.m_price + "\n" + "permId = " + execution.m_permId + "\n" + "liquidation = " + execution.m_liquidation + "\n" + " ---- Execution Details end ----\n";
            return msg;
        }
		
        static public System.String updateMktDepth(int tickerId, int position, int operation, int side, double price, int size)
        {
            return "updateMktDepth: " + tickerId + " " + position + " " + operation + " " + side + " " + price + " " + size;
        }
		
        static public System.String updateMktDepthL2(int tickerId, int position, System.String marketMaker, int operation, int side, double price, int size)
        {
            return "updateMktDepth: " + tickerId + " " + position + " " + marketMaker + " " + operation + " " + side + " " + price + " " + size;
        }
		
        static public System.String updateNewsBulletin(int msgId, int msgType, System.String message, System.String origExchange)
        {
            return "MsgId=" + msgId + " :: MsgType=" + msgType + " :: Origin=" + origExchange + " :: Message=" + message;
        }
		
        static public System.String managedAccounts(System.String accountsList)
        {
            return "Connected : The list of managed accounts are : [" + accountsList + "]";
        }
		
        static public System.String receiveFA(int faDataType, System.String xml)
        {
            return FINANCIAL_ADVISOR + " " + EClientSocket.faMsgTypeName(faDataType) + " " + xml;
        }
		
        static public System.String historicalData(int reqId, System.String date, double open, double high, double low, double close, int volume, int count, double WAP, bool hasGaps)
        {
            return "id=" + reqId + " date = " + date + " open=" + open + " high=" + high + " low=" + low + " close=" + close + " volume=" + volume + " count=" + count + " WAP=" + WAP + " hasGaps=" + hasGaps;
        }
		
        static public System.String scannerParameters(System.String xml)
        {
            return SCANNER_PARAMETERS + "\n" + xml;
        }
		
        static public System.String scannerData(int reqId, int rank, ContractDetails contractDetails, System.String distance, System.String benchmark, System.String projection, System.String legsStr)
        {
            Contract contract = contractDetails.m_summary;
            return "id = " + reqId + " rank=" + rank + " symbol=" + contract.m_symbol + " secType=" + contract.m_secType + " expiry=" + contract.m_expiry + " strike=" + contract.m_strike + " right=" + contract.m_right + " exchange=" + contract.m_exchange + " currency=" + contract.m_currency + " localSymbol=" + contract.m_localSymbol + " marketName=" + contractDetails.m_marketName + " tradingClass=" + contractDetails.m_tradingClass + " distance=" + distance + " benchmark=" + benchmark + " projection=" + projection + " legsStr=" + legsStr;
        }
    }
}