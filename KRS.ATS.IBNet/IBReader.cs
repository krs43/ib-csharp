using System.Threading;
using System;
using System.Globalization;

namespace Krs.Ats.IBNet
{
    public class IBReader
    {
        #region Thread Sync
        private Thread mThread;
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
            mThread = new Thread(Run);
        }
        public void  Run()
        {
            try
            {
                // loop until thread is terminated
                while (!Stopping && ProcessMsg((IncomingMessage)ReadInt()))
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

        internal void Start()
        {
            if(!Stopping)
                mThread.Start();
        }
        #endregion

        #region Process Message
        /// <summary>Overridden in subclass. </summary>
        protected internal virtual bool ProcessMsg(IncomingMessage msgId)
        {
            if (msgId == IncomingMessage.ERROR)
                return false;
			
            switch (msgId)
            {
				
                case IncomingMessage.TICK_PRICE:  {
                    int version = ReadInt();
                    int tickerId = ReadInt();
                    int tickType = ReadInt();
                    double price = ReadDouble();
                    int size = 0;
                    if (version >= 2)
                    {
                        size = ReadInt();
                    }
                    int canAutoExecute = 0;
                    if (version >= 3)
                    {
                        canAutoExecute = ReadInt();
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
                    int version = ReadInt();
                    int tickerId = ReadInt();
                    int tickType = ReadInt();
                    int size = ReadInt();
						
                    Wrapper.tickSize(tickerId, tickType, size);
                    break;
                }


            case IncomingMessage.TICK_OPTION_COMPUTATION:
                {
                    int version = ReadInt();
                    int tickerId = ReadInt();
                    int tickType = ReadInt();
                    double impliedVol = ReadDouble();
                    if (impliedVol < 0)
                    {
                        // -1 is the "not yet computed" indicator
                        //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                        impliedVol = System.Double.MaxValue;
                    }
                    double delta = ReadDouble();
                    if (System.Math.Abs(delta) > 1)
                    {
                        // -2 is the "not yet computed" indicator
                        //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                        delta = System.Double.MaxValue;
                    }
                    double modelPrice, pvDividend;
                    if (tickType == (int)TickType.MODEL_OPTION)
                    {
                        // introduced in version == 5
                        modelPrice = ReadDouble();
                        pvDividend = ReadDouble();
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
                    int version = ReadInt();
                    int tickerId = ReadInt();
                    int tickType = ReadInt();
                    double value_Renamed = ReadDouble();
						
                    Wrapper.tickGeneric(tickerId, tickType, value_Renamed);
                    break;
                }


            case IncomingMessage.TICK_STRING:
                {
                    int version = ReadInt();
                    int tickerId = ReadInt();
                    int tickType = ReadInt();
                    System.String value_Renamed = ReadStr();
						
                    Wrapper.tickString(tickerId, tickType, value_Renamed);
                    break;
                }


            case IncomingMessage.TICK_EFP:
                {
                    int version = ReadInt();
                    int tickerId = ReadInt();
                    int tickType = ReadInt();
                    double basisPoints = ReadDouble();
                    System.String formattedBasisPoints = ReadStr();
                    double impliedFuturesPrice = ReadDouble();
                    int holdDays = ReadInt();
                    System.String futureExpiry = ReadStr();
                    double dividendImpact = ReadDouble();
                    double dividendsToExpiry = ReadDouble();
                    Wrapper.tickEFP(tickerId, tickType, basisPoints, formattedBasisPoints, impliedFuturesPrice, holdDays, futureExpiry, dividendImpact, dividendsToExpiry);
                    break;
                }


            case IncomingMessage.ORDER_STATUS:
                {
                    int version = ReadInt();
                    int id = ReadInt();
                    System.String status = ReadStr();
                    int filled = ReadInt();
                    int remaining = ReadInt();
                    double avgFillPrice = ReadDouble();
						
                    int permId = 0;
                    if (version >= 2)
                    {
                        permId = ReadInt();
                    }
						
                    int parentId = 0;
                    if (version >= 3)
                    {
                        parentId = ReadInt();
                    }
						
                    double lastFillPrice = 0;
                    if (version >= 4)
                    {
                        lastFillPrice = ReadDouble();
                    }
						
                    int clientId = 0;
                    if (version >= 5)
                    {
                        clientId = ReadInt();
                    }
						
                    Wrapper.orderStatus(id, status, filled, remaining, avgFillPrice, permId, parentId, lastFillPrice, clientId);
                    break;
                }


            case IncomingMessage.ACCT_VALUE:
                {
                    int version = ReadInt();
                    System.String key = ReadStr();
                    System.String val = ReadStr();
                    System.String cur = ReadStr();
                    System.String accountName = null;
                    if (version >= 2)
                    {
                        accountName = ReadStr();
                    }
                    Wrapper.updateAccountValue(key, val, cur, accountName);
                    break;
                }


            case IncomingMessage.PORTFOLIO_VALUE:
                {
                    int version = ReadInt();
                    Contract contract = new Contract();
                    contract.Symbol = ReadStr();
                    contract.SecType = (SecurityType)Enum.Parse(typeof(SecurityType), ReadStr());
                    contract.Expiry = ReadStr();
                    contract.Strike = ReadDouble();
                    string rstr = ReadStr();
                    contract.Right = (rstr.Length <= 0 ? RightType.UNDEFINED : (RightType)Enum.Parse(typeof(RightType), rstr));
                    contract.Currency = ReadStr();
                    if (version >= 2)
                    {
                        contract.LocalSymbol = ReadStr();
                    }
						
                    int position = ReadInt();
                    double marketPrice = ReadDouble();
                    double marketValue = ReadDouble();
                    double averageCost = 0.0;
                    double unrealizedPNL = 0.0;
                    double realizedPNL = 0.0;
                    if (version >= 3)
                    {
                        averageCost = ReadDouble();
                        unrealizedPNL = ReadDouble();
                        realizedPNL = ReadDouble();
                    }
						
                    System.String accountName = null;
                    if (version >= 4)
                    {
                        accountName = ReadStr();
                    }
						
                    Wrapper.updatePortfolio(contract, position, marketPrice, marketValue, averageCost, unrealizedPNL, realizedPNL, accountName);
						
                    break;
                }


            case IncomingMessage.ACCT_UPDATE_TIME:
                {
                    int version = ReadInt();
                    System.String timeStamp = ReadStr();
                    Wrapper.updateAccountTime(timeStamp);
                    break;
                }


            case IncomingMessage.ERR_MSG:
                {
                    int version = ReadInt();
                    if (version < 2)
                    {
                        System.String msg = ReadStr();
                        m_parent.error(-1,-1,msg);
                    }
                    else
                    {
                        int id = ReadInt();
                        int errorCode = ReadInt();
                        System.String errorMsg = ReadStr();
                        m_parent.error(id, errorCode, errorMsg);
                    }
                    break;
                }


            case IncomingMessage.OPEN_ORDER:
                {
                    // read version
                    int version = ReadInt();
						
                    // read order id
                    Order order = new Order();
                    order.OrderId = ReadInt();
						
                    // read contract fields
                    Contract contract = new Contract();
                    contract.Symbol = ReadStr();
                    contract.SecType = (SecurityType)Enum.Parse(typeof(SecurityType), ReadStr());
                    contract.Expiry = ReadStr();
                    contract.Strike = ReadDouble();
                    string rstr = ReadStr();
                    contract.Right = (rstr.Length <= 0 ? RightType.UNDEFINED : (RightType)Enum.Parse(typeof(RightType), rstr));
                    contract.Exchange = ReadStr();
                    contract.Currency = ReadStr();
                    if (version >= 2)
                    {
                        contract.LocalSymbol = ReadStr();
                    }
						
                    // read order fields
                    // BUG: Parse may fail as the string may be empty...
                    order.Action = (ActionSide)Enum.Parse(typeof(ActionSide), ReadStr());
                    order.TotalQuantity = ReadInt();
                    // BUG: Parse may fail as the string may be empty...
                    order.OrderType = (OrderType)Enum.Parse(typeof(ActionSide), ReadStr());
                    order.LmtPrice = ReadDouble();
                    order.AuxPrice = ReadDouble();
                    // BUG: Parse may fail as the string may be empty...
                    order.Tif = (TimeInForce)Enum.Parse(typeof(TimeInForce), ReadStr());
                    order.OcaGroup = ReadStr();
                    order.Account = ReadStr();
                    order.OpenClose = ReadStr();
                    order.Origin = ReadInt();
                    order.OrderRef = ReadStr();
						
                    if (version >= 3)
                    {
                        order.ClientId = ReadInt();
                    }
						
                    if (version >= 4)
                    {
                        order.PermId = ReadInt();
                        order.IgnoreRth = ReadInt() == 1;
                        order.Hidden = ReadInt() == 1;
                        order.DiscretionaryAmt = ReadDouble();
                    }
						
                    if (version >= 5)
                    {
                        order.GoodAfterTime = ReadStr();
                    }
						
                    if (version >= 6)
                    {
                        order.SharesAllocation = ReadStr();
                    }
						
                    if (version >= 7)
                    {
                        order.FaGroup = ReadStr();
                        order.FaMethod = ReadStr();
                        order.FaPercentage = ReadStr();
                        order.FaProfile = ReadStr();
                    }
						
                    if (version >= 8)
                    {
                        order.GoodTillDate = ReadStr();
                    }
						
                    if (version >= 9)
                    {
                        order.Rule80A = ReadStr();
                        order.PercentOffset = ReadDouble();
                        order.SettlingFirm = ReadStr();
                        order.ShortSaleSlot = ReadInt();
                        order.DesignatedLocation = ReadStr();
                        order.AuctionStrategy = ReadInt();
                        order.StartingPrice = ReadDouble();
                        order.StockRefPrice = ReadDouble();
                        order.Delta = ReadDouble();
                        order.StockRangeLower = ReadDouble();
                        order.StockRangeUpper = ReadDouble();
                        order.DisplaySize = ReadInt();
                        order.RthOnly = ReadBoolFromInt();
                        order.BlockOrder = ReadBoolFromInt();
                        order.SweepToFill = ReadBoolFromInt();
                        order.AllOrNone = ReadBoolFromInt();
                        order.MinQty = ReadInt();
                        order.OcaType = (OCAType)ReadInt();
                        order.ETradeOnly = ReadBoolFromInt();
                        order.FirmQuoteOnly = ReadBoolFromInt();
                        order.NbboPriceCap = ReadDouble();
                    }
						
                    if (version >= 10)
                    {
                        order.ParentId = ReadInt();
                        order.TriggerMethod = ReadInt();
                    }
						
                    if (version >= 11)
                    {
                        order.Volatility = ReadDouble();
                        order.VolatilityType = ReadInt();
                        if (version == 11)
                        {
                            int receivedInt = ReadInt();
                            order.DeltaNeutralOrderType = ((receivedInt == 0)?"NONE":"MKT");
                        }
                        else
                        {
                            // version 12 and up
                            order.DeltaNeutralOrderType = ReadStr();
                            order.DeltaNeutralAuxPrice = ReadDouble();
                        }
                        order.ContinuousUpdate = ReadInt();
                        if (m_parent.serverVersion == 26)
                        {
                            order.StockRangeLower = ReadDouble();
                            order.StockRangeUpper = ReadDouble();
                        }
                        order.ReferencePriceType = ReadInt();
                    }
						
                    if (version >= 13)
                    {
                        order.TrailStopPrice = ReadDouble();
                    }
						
                    if (version >= 14)
                    {
                        order.BasisPoints = ReadDouble();
                        order.BasisPointsType = ReadInt();
                        contract.ComboLegsDescrip = ReadStr();
                    }
						
                    Wrapper.openOrder(order.OrderId, contract, order);
                    break;
                }


            case IncomingMessage.NEXT_VALID_ID:
                {
                    int version = ReadInt();
                    int orderId = ReadInt();
                    Wrapper.nextValidId(orderId);
                    break;
                }


            case IncomingMessage.SCANNER_DATA:
                {
                    ContractDetails contract = new ContractDetails();
                    int version = ReadInt();
                    int tickerId = ReadInt();
                    int numberOfElements = ReadInt();
                    for (int ctr = 0; ctr < numberOfElements; ctr++)
                    {
                        int rank = ReadInt();
                        contract.Summary.Symbol = ReadStr();
                        contract.Summary.SecType = (SecurityType)Enum.Parse(typeof(SecurityType), ReadStr());
                        contract.Summary.Expiry = ReadStr();
                        contract.Summary.Strike = ReadDouble();
                        string rstr = ReadStr();
                        contract.Summary.Right = (rstr.Length <= 0 ? RightType.UNDEFINED : (RightType)Enum.Parse(typeof(RightType), rstr));
                        contract.Summary.Exchange = ReadStr();
                        contract.Summary.Currency = ReadStr();
                        contract.Summary.LocalSymbol = ReadStr();
                        contract.MarketName = ReadStr();
                        contract.TradingClass = ReadStr();
                        System.String distance = ReadStr();
                        System.String benchmark = ReadStr();
                        System.String projection = ReadStr();
                        System.String legsStr = null;
                        if (version >= 2)
                        {
                            legsStr = ReadStr();
                        }
                        Wrapper.scannerData(tickerId, rank, contract, distance, benchmark, projection, legsStr);
                    }
                    break;
                }


            case IncomingMessage.CONTRACT_DATA:
                {
                    int version = ReadInt();
                    ContractDetails contract = new ContractDetails();
                    contract.Summary.Symbol = ReadStr();
                    contract.Summary.SecType = (SecurityType)Enum.Parse(typeof(SecurityType), ReadStr());
                    contract.Summary.Expiry = ReadStr();
                    contract.Summary.Strike = ReadDouble();
                    string rstr = ReadStr();
                    contract.Summary.Right = (rstr.Length <= 0 ? RightType.UNDEFINED : (RightType)Enum.Parse(typeof(RightType), rstr));
                    contract.Summary.Exchange = ReadStr();
                    contract.Summary.Currency = ReadStr();
                    contract.Summary.LocalSymbol = ReadStr();
                    contract.MarketName = ReadStr();
                    contract.TradingClass = ReadStr();
                    contract.Conid = ReadInt();
                    contract.MinTick = ReadDouble();
                    contract.Multiplier = ReadStr();
                    contract.OrderTypes = ReadStr();
                    contract.ValidExchanges = ReadStr();
                    if (version >= 2)
                    {
                        contract.PriceMagnifier = ReadInt();
                    }
                    Wrapper.contractDetails(contract);
                    break;
                }

            case IncomingMessage.BOND_CONTRACT_DATA:
                {
                    int version = ReadInt();
                    ContractDetails contract = new ContractDetails();
						
                    contract.Summary.Symbol = ReadStr();
                    contract.Summary.SecType = (SecurityType)Enum.Parse(typeof(SecurityType), ReadStr());
                    contract.Summary.Cusip = ReadStr();
                    contract.Summary.Coupon = ReadDouble();
                    contract.Summary.Maturity = ReadStr();
                    contract.Summary.IssueDate = ReadStr();
                    contract.Summary.Ratings = ReadStr();
                    contract.Summary.BondType = ReadStr();
                    contract.Summary.CouponType = ReadStr();
                    contract.Summary.Convertible = ReadBoolFromInt();
                    contract.Summary.Callable = ReadBoolFromInt();
                    contract.Summary.Putable = ReadBoolFromInt();
                    contract.Summary.DescAppend = ReadStr();
                    contract.Summary.Exchange = ReadStr();
                    contract.Summary.Currency = ReadStr();
                    contract.MarketName = ReadStr();
                    contract.TradingClass = ReadStr();
                    contract.Conid = ReadInt();
                    contract.MinTick = ReadDouble();
                    contract.OrderTypes = ReadStr();
                    contract.ValidExchanges = ReadStr();
                    if (version >= 2)
                    {
                        contract.Summary.NextOptionDate = ReadStr();
                        contract.Summary.NextOptionType = ReadStr();
                        contract.Summary.NextOptionPartial = ReadBoolFromInt();
                        contract.Summary.Notes = ReadStr();
                    }
                    Wrapper.bondContractDetails(contract);
                    break;
                }

            case IncomingMessage.EXECUTION_DATA:
                {
                    int version = ReadInt();
                    int orderId = ReadInt();
						
                    Contract contract = new Contract();
                    contract.Symbol = ReadStr();
                    contract.SecType = (SecurityType)Enum.Parse(typeof(SecurityType), ReadStr());
                    contract.Expiry = ReadStr();
                    contract.Strike = ReadDouble();
                    string rstr = ReadStr();
                    contract.Right = (rstr.Length <= 0 ? RightType.UNDEFINED : (RightType)Enum.Parse(typeof(RightType), rstr));
                    contract.Exchange = ReadStr();
                    contract.Currency = ReadStr();
                    contract.LocalSymbol = ReadStr();
						
                    Execution exec = new Execution();
                    exec.OrderId = orderId;
                    exec.ExecId = ReadStr();
                    exec.Time = ReadStr();
                    exec.AcctNumber = ReadStr();
                    exec.Exchange = ReadStr();
                    exec.Side = (ExecutionSide)Enum.Parse(typeof (ExecutionSide), ReadStr());
                    exec.Shares = ReadInt();
                    exec.Price = ReadDouble();
                    if (version >= 2)
                    {
                        exec.PermId = ReadInt();
                    }
                    if (version >= 3)
                    {
                        exec.ClientId = ReadInt();
                    }
                    if (version >= 4)
                    {
                        exec.Liquidation = ReadInt();
                    }
						
                    Wrapper.execDetails(orderId, contract, exec);
                    break;
                }

            case IncomingMessage.MARKET_DEPTH:
                {
                    int version = ReadInt();
                    int id = ReadInt();
						
                    int position = ReadInt();
                    int operation = ReadInt();
                    int side = ReadInt();
                    double price = ReadDouble();
                    int size = ReadInt();
						
                    Wrapper.updateMktDepth(id, position, operation, side, price, size);
                    break;
                }

            case IncomingMessage.MARKET_DEPTH_L2:
                {
                    int version = ReadInt();
                    int id = ReadInt();
						
                    int position = ReadInt();
                    System.String marketMaker = ReadStr();
                    int operation = ReadInt();
                    int side = ReadInt();
                    double price = ReadDouble();
                    int size = ReadInt();
						
                    Wrapper.updateMktDepthL2(id, position, marketMaker, operation, side, price, size);
                    break;
                }

            case IncomingMessage.NEWS_BULLETINS:
                {
                    int version = ReadInt();
                    int newsMsgId = ReadInt();
                    int newsMsgType = ReadInt();
                    System.String newsMessage = ReadStr();
                    System.String originatingExch = ReadStr();
						
                    Wrapper.updateNewsBulletin(newsMsgId, newsMsgType, newsMessage, originatingExch);
                    break;
                }

            case IncomingMessage.MANAGED_ACCTS:
                {
                    int version = ReadInt();
                    System.String accountsList = ReadStr();
						
                    Wrapper.managedAccounts(accountsList);
                    break;
                }

            case IncomingMessage.RECEIVE_FA:
                {
                    int version = ReadInt();
                    int faDataType = ReadInt();
                    System.String xml = ReadStr();
						
                    Wrapper.receiveFA(faDataType, xml);
                    break;
                }

            case IncomingMessage.HISTORICAL_DATA:
                {
                    int version = ReadInt();
                    int reqId = ReadInt();
                    System.String startDateStr;
                    System.String endDateStr;
                    System.String completedIndicator = "finished";
                    if (version >= 2)
                    {
                        startDateStr = ReadStr();
                        endDateStr = ReadStr();
                        completedIndicator += ("-" + startDateStr + "-" + endDateStr);
                    }
                    int itemCount = ReadInt();
                    for (int ctr = 0; ctr < itemCount; ctr++)
                    {
                        System.String date = ReadStr();
                        double open = ReadDouble();
                        double high = ReadDouble();
                        double low = ReadDouble();
                        double close = ReadDouble();
                        int volume = ReadInt();
                        double WAP = ReadDouble();
                        System.String hasGaps = ReadStr();
                        int barCount = - 1;
                        if (version >= 3)
                        {
                            barCount = ReadInt();
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
                    int version = ReadInt();
                    System.String xml = ReadStr();
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
        protected internal virtual System.String ReadStr()
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
		
		
        internal virtual bool ReadBoolFromInt()
        {
            System.String str = ReadStr();
            return str == null?false:(System.Int32.Parse(str, CultureInfo.InvariantCulture) != 0);
        }
		
        protected internal virtual int ReadInt()
        {
            System.String str = ReadStr();
            return str == null ? 0 : System.Int32.Parse(str, CultureInfo.InvariantCulture);
        }
		
        protected internal virtual long ReadLong()
        {
            System.String str = ReadStr();
            return str == null ? 0L : System.Int64.Parse(str, CultureInfo.InvariantCulture);
        }
		
        protected internal virtual double ReadDouble()
        {
            System.String str = ReadStr();
            return str == null?0:System.Double.Parse(str, CultureInfo.InvariantCulture);
        }
        #endregion
    }
}