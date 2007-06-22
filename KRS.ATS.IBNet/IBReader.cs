/*
* EReader.java
*
*/
namespace KRS.ATS.IBNet
{
    public class IBReader
    {
        #region Thread Sync
        /// <summary>
        /// Lock covering stopping and stopped
        /// </summary>
        readonly object stopLock = new object();
        /// <summary>
        /// Whether or not the worker thread has been asked to stop
        /// </summary>
        bool stopping = false;
        /// <summary>
        /// Whether or not the worker thread has stopped
        /// </summary>
        bool stopped = false;

        /// <summary>
        /// Returns whether the worker thread has been asked to stop.
        /// This continues to return true even after the thread has stopped.
        /// </summary>
        public bool Stopping
        {
            get
            {
                lock (stopLock)
                {
                    return stopping;
                }
            }
        }

        /// <summary>
        /// Returns whether the worker thread has stopped.
        /// </summary>
        public bool Stopped
        {
            get
            {
                lock (stopLock)
                {
                    return stopped;
                }
            }
        }

        /// <summary>
        /// Tells the worker thread to stop, typically after completing its 
        /// current work item. (The thread is *not* guaranteed to have stopped
        /// by the time this method returns.)
        /// </summary>
        public void Stop()
        {
            lock (stopLock)
            {
                stopping = true;
            }
        }

        /// <summary>
        /// Called by the worker thread to indicate when it has stopped.
        /// </summary>
        void SetStopped()
        {
            lock (stopLock)
            {
                stopped = true;
            }
        }
        #endregion

        #region Private Variables / Properties
        private readonly IBClientSocket m_parent;
        private readonly System.IO.BinaryReader m_dis;
        protected internal virtual IBClientSocket parent
        {
            get {return m_parent;}
        }
        private IBWrapper Wrapper
        {
            get {return parent.wrapper;}
        }
        #endregion

        #region General Code
        public IBReader(IBClientSocket parent, System.IO.BinaryReader dis)
        {
            m_parent = parent;
            m_dis = dis;
        }
        public void  Run()
        {
            try
            {
                // loop until thread is terminated
                while (!Stopping && processMsg((IncomingMessage)readInt()))
#pragma warning disable 642
                    ;
#pragma warning restore 642
            }
            finally
            {
                SetStopped();
                parent.wrapper.connectionClosed();
                m_parent.close();
            }
        }
        #endregion

        #region Process Message
        /// <summary>Overridden in subclass. </summary>
        protected internal virtual bool processMsg(IncomingMessage msgId)
        {
            if (msgId == IncomingMessage.ERROR)
                return false;
			
            switch (msgId)
            {
				
                case IncomingMessage.TICK_PRICE:  {
                    int version = readInt();
                    int tickerId = readInt();
                    int tickType = readInt();
                    double price = readDouble();
                    int size = 0;
                    if (version >= 2)
                    {
                        size = readInt();
                    }
                    int canAutoExecute = 0;
                    if (version >= 3)
                    {
                        canAutoExecute = readInt();
                    }
                    Wrapper.tickPrice(tickerId, tickType, price, canAutoExecute);
						
                    if (version >= 2)
                    {
                        int sizeTickType = - 1; // not a tick
                        switch (tickType)
                        {
								
                            case 1:  // BID
                                sizeTickType = 0; // BID_SIZE
                                break;
								
                            case 2:  // ASK
                                sizeTickType = 3; // ASK_SIZE
                                break;
								
                            case 4:  // LAST
                                sizeTickType = 5; // LAST_SIZE
                                break;
                        }
                        if (sizeTickType != - 1)
                        {
                            Wrapper.tickSize(tickerId, sizeTickType, size);
                        }
                    }
                    break;
                }

            case IncomingMessage.TICK_SIZE:
                {
                    int version = readInt();
                    int tickerId = readInt();
                    int tickType = readInt();
                    int size = readInt();
						
                    Wrapper.tickSize(tickerId, tickType, size);
                    break;
                }


            case IncomingMessage.TICK_OPTION_COMPUTATION:
                {
                    int version = readInt();
                    int tickerId = readInt();
                    int tickType = readInt();
                    double impliedVol = readDouble();
                    if (impliedVol < 0)
                    {
                        // -1 is the "not yet computed" indicator
                        //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                        impliedVol = System.Double.MaxValue;
                    }
                    double delta = readDouble();
                    if (System.Math.Abs(delta) > 1)
                    {
                        // -2 is the "not yet computed" indicator
                        //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                        delta = System.Double.MaxValue;
                    }
                    double modelPrice, pvDividend;
                    if (tickType == TickType.MODEL_OPTION)
                    {
                        // introduced in version == 5
                        modelPrice = readDouble();
                        pvDividend = readDouble();
                    }
                    else
                    {
                        //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                        modelPrice = pvDividend = System.Double.MaxValue;
                    }
                    Wrapper.tickOptionComputation(tickerId, tickType, impliedVol, delta, modelPrice, pvDividend);
                    break;
                }


            case IncomingMessage.TICK_GENERIC:
                {
                    int version = readInt();
                    int tickerId = readInt();
                    int tickType = readInt();
                    double value_Renamed = readDouble();
						
                    Wrapper.tickGeneric(tickerId, tickType, value_Renamed);
                    break;
                }


            case IncomingMessage.TICK_STRING:
                {
                    int version = readInt();
                    int tickerId = readInt();
                    int tickType = readInt();
                    System.String value_Renamed = readStr();
						
                    Wrapper.tickString(tickerId, tickType, value_Renamed);
                    break;
                }


            case IncomingMessage.TICK_EFP:
                {
                    int version = readInt();
                    int tickerId = readInt();
                    int tickType = readInt();
                    double basisPoints = readDouble();
                    System.String formattedBasisPoints = readStr();
                    double impliedFuturesPrice = readDouble();
                    int holdDays = readInt();
                    System.String futureExpiry = readStr();
                    double dividendImpact = readDouble();
                    double dividendsToExpiry = readDouble();
                    Wrapper.tickEFP(tickerId, tickType, basisPoints, formattedBasisPoints, impliedFuturesPrice, holdDays, futureExpiry, dividendImpact, dividendsToExpiry);
                    break;
                }


            case IncomingMessage.ORDER_STATUS:
                {
                    int version = readInt();
                    int id = readInt();
                    System.String status = readStr();
                    int filled = readInt();
                    int remaining = readInt();
                    double avgFillPrice = readDouble();
						
                    int permId = 0;
                    if (version >= 2)
                    {
                        permId = readInt();
                    }
						
                    int parentId = 0;
                    if (version >= 3)
                    {
                        parentId = readInt();
                    }
						
                    double lastFillPrice = 0;
                    if (version >= 4)
                    {
                        lastFillPrice = readDouble();
                    }
						
                    int clientId = 0;
                    if (version >= 5)
                    {
                        clientId = readInt();
                    }
						
                    Wrapper.orderStatus(id, status, filled, remaining, avgFillPrice, permId, parentId, lastFillPrice, clientId);
                    break;
                }


            case IncomingMessage.ACCT_VALUE:
                {
                    int version = readInt();
                    System.String key = readStr();
                    System.String val = readStr();
                    System.String cur = readStr();
                    System.String accountName = null;
                    if (version >= 2)
                    {
                        accountName = readStr();
                    }
                    Wrapper.updateAccountValue(key, val, cur, accountName);
                    break;
                }


            case IncomingMessage.PORTFOLIO_VALUE:
                {
                    int version = readInt();
                    Contract contract = new Contract();
                    contract.m_symbol = readStr();
                    contract.m_secType = readStr();
                    contract.m_expiry = readStr();
                    contract.m_strike = readDouble();
                    contract.m_right = readStr();
                    contract.m_currency = readStr();
                    if (version >= 2)
                    {
                        contract.m_localSymbol = readStr();
                    }
						
                    int position = readInt();
                    double marketPrice = readDouble();
                    double marketValue = readDouble();
                    double averageCost = 0.0;
                    double unrealizedPNL = 0.0;
                    double realizedPNL = 0.0;
                    if (version >= 3)
                    {
                        averageCost = readDouble();
                        unrealizedPNL = readDouble();
                        realizedPNL = readDouble();
                    }
						
                    System.String accountName = null;
                    if (version >= 4)
                    {
                        accountName = readStr();
                    }
						
                    Wrapper.updatePortfolio(contract, position, marketPrice, marketValue, averageCost, unrealizedPNL, realizedPNL, accountName);
						
                    break;
                }


            case IncomingMessage.ACCT_UPDATE_TIME:
                {
                    int version = readInt();
                    System.String timeStamp = readStr();
                    Wrapper.updateAccountTime(timeStamp);
                    break;
                }


            case IncomingMessage.ERR_MSG:
                {
                    int version = readInt();
                    if (version < 2)
                    {
                        System.String msg = readStr();
                        m_parent.error(-1,-1,msg);
                    }
                    else
                    {
                        int id = readInt();
                        int errorCode = readInt();
                        System.String errorMsg = readStr();
                        m_parent.error(id, errorCode, errorMsg);
                    }
                    break;
                }


            case IncomingMessage.OPEN_ORDER:
                {
                    // read version
                    int version = readInt();
						
                    // read order id
                    Order order = new Order();
                    order.m_orderId = readInt();
						
                    // read contract fields
                    Contract contract = new Contract();
                    contract.m_symbol = readStr();
                    contract.m_secType = readStr();
                    contract.m_expiry = readStr();
                    contract.m_strike = readDouble();
                    contract.m_right = readStr();
                    contract.m_exchange = readStr();
                    contract.m_currency = readStr();
                    if (version >= 2)
                    {
                        contract.m_localSymbol = readStr();
                    }
						
                    // read order fields
                    order.m_action = readStr();
                    order.m_totalQuantity = readInt();
                    order.m_orderType = readStr();
                    order.m_lmtPrice = readDouble();
                    order.m_auxPrice = readDouble();
                    order.m_tif = readStr();
                    order.m_ocaGroup = readStr();
                    order.m_account = readStr();
                    order.m_openClose = readStr();
                    order.m_origin = readInt();
                    order.m_orderRef = readStr();
						
                    if (version >= 3)
                    {
                        order.m_clientId = readInt();
                    }
						
                    if (version >= 4)
                    {
                        order.m_permId = readInt();
                        order.m_ignoreRth = readInt() == 1;
                        order.m_hidden = readInt() == 1;
                        order.m_discretionaryAmt = readDouble();
                    }
						
                    if (version >= 5)
                    {
                        order.m_goodAfterTime = readStr();
                    }
						
                    if (version >= 6)
                    {
                        order.m_sharesAllocation = readStr();
                    }
						
                    if (version >= 7)
                    {
                        order.m_faGroup = readStr();
                        order.m_faMethod = readStr();
                        order.m_faPercentage = readStr();
                        order.m_faProfile = readStr();
                    }
						
                    if (version >= 8)
                    {
                        order.m_goodTillDate = readStr();
                    }
						
                    if (version >= 9)
                    {
                        order.m_rule80A = readStr();
                        order.m_percentOffset = readDouble();
                        order.m_settlingFirm = readStr();
                        order.m_shortSaleSlot = readInt();
                        order.m_designatedLocation = readStr();
                        order.m_auctionStrategy = readInt();
                        order.m_startingPrice = readDouble();
                        order.m_stockRefPrice = readDouble();
                        order.m_delta = readDouble();
                        order.m_stockRangeLower = readDouble();
                        order.m_stockRangeUpper = readDouble();
                        order.m_displaySize = readInt();
                        order.m_rthOnly = readBoolFromInt();
                        order.m_blockOrder = readBoolFromInt();
                        order.m_sweepToFill = readBoolFromInt();
                        order.m_allOrNone = readBoolFromInt();
                        order.m_minQty = readInt();
                        order.m_ocaType = readInt();
                        order.m_eTradeOnly = readBoolFromInt();
                        order.m_firmQuoteOnly = readBoolFromInt();
                        order.m_nbboPriceCap = readDouble();
                    }
						
                    if (version >= 10)
                    {
                        order.m_parentId = readInt();
                        order.m_triggerMethod = readInt();
                    }
						
                    if (version >= 11)
                    {
                        order.m_volatility = readDouble();
                        order.m_volatilityType = readInt();
                        if (version == 11)
                        {
                            int receivedInt = readInt();
                            order.m_deltaNeutralOrderType = ((receivedInt == 0)?"NONE":"MKT");
                        }
                        else
                        {
                            // version 12 and up
                            order.m_deltaNeutralOrderType = readStr();
                            order.m_deltaNeutralAuxPrice = readDouble();
                        }
                        order.m_continuousUpdate = readInt();
                        if (m_parent.serverVersion == 26)
                        {
                            order.m_stockRangeLower = readDouble();
                            order.m_stockRangeUpper = readDouble();
                        }
                        order.m_referencePriceType = readInt();
                    }
						
                    if (version >= 13)
                    {
                        order.m_trailStopPrice = readDouble();
                    }
						
                    if (version >= 14)
                    {
                        order.m_basisPoints = readDouble();
                        order.m_basisPointsType = readInt();
                        contract.m_comboLegsDescrip = readStr();
                    }
						
                    Wrapper.openOrder(order.m_orderId, contract, order);
                    break;
                }


            case IncomingMessage.NEXT_VALID_ID:
                {
                    int version = readInt();
                    int orderId = readInt();
                    Wrapper.nextValidId(orderId);
                    break;
                }


            case IncomingMessage.SCANNER_DATA:
                {
                    ContractDetails contract = new ContractDetails();
                    int version = readInt();
                    int tickerId = readInt();
                    int numberOfElements = readInt();
                    for (int ctr = 0; ctr < numberOfElements; ctr++)
                    {
                        int rank = readInt();
                        contract.m_summary.m_symbol = readStr();
                        contract.m_summary.m_secType = readStr();
                        contract.m_summary.m_expiry = readStr();
                        contract.m_summary.m_strike = readDouble();
                        contract.m_summary.m_right = readStr();
                        contract.m_summary.m_exchange = readStr();
                        contract.m_summary.m_currency = readStr();
                        contract.m_summary.m_localSymbol = readStr();
                        contract.m_marketName = readStr();
                        contract.m_tradingClass = readStr();
                        System.String distance = readStr();
                        System.String benchmark = readStr();
                        System.String projection = readStr();
                        System.String legsStr = null;
                        if (version >= 2)
                        {
                            legsStr = readStr();
                        }
                        Wrapper.scannerData(tickerId, rank, contract, distance, benchmark, projection, legsStr);
                    }
                    break;
                }


            case IncomingMessage.CONTRACT_DATA:
                {
                    int version = readInt();
                    ContractDetails contract = new ContractDetails();
                    contract.m_summary.m_symbol = readStr();
                    contract.m_summary.m_secType = readStr();
                    contract.m_summary.m_expiry = readStr();
                    contract.m_summary.m_strike = readDouble();
                    contract.m_summary.m_right = readStr();
                    contract.m_summary.m_exchange = readStr();
                    contract.m_summary.m_currency = readStr();
                    contract.m_summary.m_localSymbol = readStr();
                    contract.m_marketName = readStr();
                    contract.m_tradingClass = readStr();
                    contract.m_conid = readInt();
                    contract.m_minTick = readDouble();
                    contract.m_multiplier = readStr();
                    contract.m_orderTypes = readStr();
                    contract.m_validExchanges = readStr();
                    if (version >= 2)
                    {
                        contract.m_priceMagnifier = readInt();
                    }
                    Wrapper.contractDetails(contract);
                    break;
                }

            case IncomingMessage.BOND_CONTRACT_DATA:
                {
                    int version = readInt();
                    ContractDetails contract = new ContractDetails();
						
                    contract.m_summary.m_symbol = readStr();
                    contract.m_summary.m_secType = readStr();
                    contract.m_summary.m_cusip = readStr();
                    contract.m_summary.m_coupon = readDouble();
                    contract.m_summary.m_maturity = readStr();
                    contract.m_summary.m_issueDate = readStr();
                    contract.m_summary.m_ratings = readStr();
                    contract.m_summary.m_bondType = readStr();
                    contract.m_summary.m_couponType = readStr();
                    contract.m_summary.m_convertible = readBoolFromInt();
                    contract.m_summary.m_callable = readBoolFromInt();
                    contract.m_summary.m_putable = readBoolFromInt();
                    contract.m_summary.m_descAppend = readStr();
                    contract.m_summary.m_exchange = readStr();
                    contract.m_summary.m_currency = readStr();
                    contract.m_marketName = readStr();
                    contract.m_tradingClass = readStr();
                    contract.m_conid = readInt();
                    contract.m_minTick = readDouble();
                    contract.m_orderTypes = readStr();
                    contract.m_validExchanges = readStr();
                    if (version >= 2)
                    {
                        contract.m_summary.m_nextOptionDate = readStr();
                        contract.m_summary.m_nextOptionType = readStr();
                        contract.m_summary.m_nextOptionPartial = readBoolFromInt();
                        contract.m_summary.m_notes = readStr();
                    }
                    Wrapper.bondContractDetails(contract);
                    break;
                }

            case IncomingMessage.EXECUTION_DATA:
                {
                    int version = readInt();
                    int orderId = readInt();
						
                    Contract contract = new Contract();
                    contract.m_symbol = readStr();
                    contract.m_secType = readStr();
                    contract.m_expiry = readStr();
                    contract.m_strike = readDouble();
                    contract.m_right = readStr();
                    contract.m_exchange = readStr();
                    contract.m_currency = readStr();
                    contract.m_localSymbol = readStr();
						
                    Execution exec = new Execution();
                    exec.m_orderId = orderId;
                    exec.m_execId = readStr();
                    exec.m_time = readStr();
                    exec.m_acctNumber = readStr();
                    exec.m_exchange = readStr();
                    exec.m_side = readStr();
                    exec.m_shares = readInt();
                    exec.m_price = readDouble();
                    if (version >= 2)
                    {
                        exec.m_permId = readInt();
                    }
                    if (version >= 3)
                    {
                        exec.m_clientId = readInt();
                    }
                    if (version >= 4)
                    {
                        exec.m_liquidation = readInt();
                    }
						
                    Wrapper.execDetails(orderId, contract, exec);
                    break;
                }

            case IncomingMessage.MARKET_DEPTH:
                {
                    int version = readInt();
                    int id = readInt();
						
                    int position = readInt();
                    int operation = readInt();
                    int side = readInt();
                    double price = readDouble();
                    int size = readInt();
						
                    Wrapper.updateMktDepth(id, position, operation, side, price, size);
                    break;
                }

            case IncomingMessage.MARKET_DEPTH_L2:
                {
                    int version = readInt();
                    int id = readInt();
						
                    int position = readInt();
                    System.String marketMaker = readStr();
                    int operation = readInt();
                    int side = readInt();
                    double price = readDouble();
                    int size = readInt();
						
                    Wrapper.updateMktDepthL2(id, position, marketMaker, operation, side, price, size);
                    break;
                }

            case IncomingMessage.NEWS_BULLETINS:
                {
                    int version = readInt();
                    int newsMsgId = readInt();
                    int newsMsgType = readInt();
                    System.String newsMessage = readStr();
                    System.String originatingExch = readStr();
						
                    Wrapper.updateNewsBulletin(newsMsgId, newsMsgType, newsMessage, originatingExch);
                    break;
                }

            case IncomingMessage.MANAGED_ACCTS:
                {
                    int version = readInt();
                    System.String accountsList = readStr();
						
                    Wrapper.managedAccounts(accountsList);
                    break;
                }

            case IncomingMessage.RECEIVE_FA:
                {
                    int version = readInt();
                    int faDataType = readInt();
                    System.String xml = readStr();
						
                    Wrapper.receiveFA(faDataType, xml);
                    break;
                }

            case IncomingMessage.HISTORICAL_DATA:
                {
                    int version = readInt();
                    int reqId = readInt();
                    System.String startDateStr;
                    System.String endDateStr;
                    System.String completedIndicator = "finished";
                    if (version >= 2)
                    {
                        startDateStr = readStr();
                        endDateStr = readStr();
                        completedIndicator += ("-" + startDateStr + "-" + endDateStr);
                    }
                    int itemCount = readInt();
                    for (int ctr = 0; ctr < itemCount; ctr++)
                    {
                        System.String date = readStr();
                        double open = readDouble();
                        double high = readDouble();
                        double low = readDouble();
                        double close = readDouble();
                        int volume = readInt();
                        double WAP = readDouble();
                        System.String hasGaps = readStr();
                        int barCount = - 1;
                        if (version >= 3)
                        {
                            barCount = readInt();
                        }
                        //UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.lang.Boolean.valueOf' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
                        Wrapper.historicalData(reqId, date, open, high, low, close, volume, barCount, WAP, System.Boolean.Parse(hasGaps));
                    }
                    // send end of dataset marker
                    Wrapper.historicalData(reqId, completedIndicator, - 1, - 1, - 1, - 1, - 1, - 1, - 1, false);
                    break;
                }

            case IncomingMessage.SCANNER_PARAMETERS:
                {
                    int version = readInt();
                    System.String xml = readStr();
                    Wrapper.scannerParameters(xml);
                    break;
                }
				
                default:  {
                    m_parent.error(IBClientErrors.NO_VALID_ID, IBClientErrors.UNKNOWN_ID.code(), IBClientErrors.UNKNOWN_ID.msg());
                    return false;
                }
				
            }
            return true;
        }
        #endregion

        #region Helper Methods
        protected internal virtual System.String readStr()
        {
            System.Text.StringBuilder buf = new System.Text.StringBuilder();
            while (true)
            {
                sbyte c = (sbyte) m_dis.ReadByte();
                if (c == 0)
                {
                    break;
                }
                buf.Append((char) c);
            }
			
            System.String str = buf.ToString();
            return str.Length == 0?null:str;
        }
		
		
        internal virtual bool readBoolFromInt()
        {
            System.String str = readStr();
            return str == null?false:(System.Int32.Parse(str) != 0);
        }
		
        protected internal virtual int readInt()
        {
            System.String str = readStr();
            return str == null?0:System.Int32.Parse(str);
        }
		
        protected internal virtual long readLong()
        {
            System.String str = readStr();
            return str == null?0L:System.Int64.Parse(str);
        }
		
        protected internal virtual double readDouble()
        {
            System.String str = readStr();
            return str == null?0:System.Double.Parse(str);
        }
        #endregion
    }
}