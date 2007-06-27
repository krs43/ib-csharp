using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Krs.Ats.IBNet
{
    public class IBClient : IDisposable
    {
        #region IB Wrapper to Events
        public event EventHandler<TickPriceEventArgs> TickPrice;
        protected virtual void OnTickPrice(TickPriceEventArgs e)
        {
            if(TickPrice!=null)
                TickPrice(this, e);
        }
        private void tickPrice(int tickerId, int field, double price, int canAutoExecute)
        {
            TickPriceEventArgs e = new TickPriceEventArgs(tickerId, field, price, canAutoExecute);
            OnTickPrice(e);
        }

        public event EventHandler<TickSizeEventArgs> TickSize;
        protected virtual void OnTickSize(TickSizeEventArgs e)
        {
            if(TickSize!=null)
                TickSize(this, e);
        }
        private void tickSize(int tickerId, int field, int size)
        {
            TickSizeEventArgs e = new TickSizeEventArgs(tickerId, field, size);
            OnTickSize(e);
        }

        public event EventHandler<TickOptionComputationEventArgs> TickOptionComputation;
        protected virtual void OnTickOptionComputation(TickOptionComputationEventArgs e)
        {
            if (TickOptionComputation != null)
                TickOptionComputation(this, e);
        }
        private void tickOptionComputation(int tickerId, int field, double impliedVol, double delta, double modelPrice,
                                          double pvDividend)
        {
            TickOptionComputationEventArgs e = new TickOptionComputationEventArgs(tickerId, field, impliedVol, delta, modelPrice, pvDividend);
        }

        public event EventHandler<TickGenericEventArgs> TickGeneric;
        protected virtual void OnTickGeneric(TickGenericEventArgs e)
        {
            if (TickGeneric != null)
                TickGeneric(this, e);
        }
        private void tickGeneric(int tickerId, int tickType, double value_Renamed)
        {
            TickGenericEventArgs e = new TickGenericEventArgs(tickerId, tickType, value_Renamed);
            OnTickGeneric(e);
        }

        public event EventHandler<TickStringEventArgs> TickString;
        protected virtual void OnTickString(TickStringEventArgs e)
        {
            if (TickString != null)
                TickString(this, e);
        }
        private void tickString(int tickerId, int tickType, string value_Renamed)
        {
            TickStringEventArgs e = new TickStringEventArgs(tickerId, tickType, value_Renamed);
            OnTickString(e);
        }

        public event EventHandler<TickEFPEventArgs> TickEFP;
        protected virtual void OnTickEFP(TickEFPEventArgs e)
        {
            if(TickEFP!=null)
                TickEFP(this, e);
        }
        private void tickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints,
                            double impliedFuture, int holdDays, string futureExpiry, double dividendImpact,
                            double dividendsToExpiry)
        {
            TickEFPEventArgs e = new TickEFPEventArgs(tickerId, tickType, basisPoints, formattedBasisPoints, impliedFuture,
                                                      holdDays, futureExpiry, dividendImpact, dividendsToExpiry);
            OnTickEFP(e);
        }

        public event EventHandler<OrderStatusEventArgs> OrderStatus;
        protected virtual void OnOrderStatus(OrderStatusEventArgs e)
        {
            if (OrderStatus != null)
                OrderStatus(this, e);
        }
        private void orderStatus(int orderId, string status, int filled, int remaining, double avgFillPrice, int permId,
                                int parentId, double lastFillPrice, int clientId)
        {
            OrderStatusEventArgs e = new OrderStatusEventArgs(orderId, status, filled, remaining,
                avgFillPrice, permId, parentId, lastFillPrice, clientId);
            OnOrderStatus(e);
        }

        public event EventHandler<OpenOrderEventArgs> OpenOrder;
        protected virtual void OnOpenOrder(OpenOrderEventArgs e)
        {
            if (OpenOrder != null)
                OpenOrder(this, e);
        }
        private void openOrder(int orderId, Contract contract, Order order)
        {
            OpenOrderEventArgs e = new OpenOrderEventArgs(orderId, contract, order);
            OnOpenOrder(e);
        }

        public event EventHandler<UpdateAccountValueEventArgs> UpdateAccountValue;
        protected virtual void OnUpdateAccountValue(UpdateAccountValueEventArgs e)
        {
            if (UpdateAccountValue != null)
                UpdateAccountValue(this, e);
        }
        private void updateAccountValue(string key, string value_Renamed, string currency, string accountName)
        {
            UpdateAccountValueEventArgs e = new UpdateAccountValueEventArgs(key, value_Renamed, currency, accountName);
            OnUpdateAccountValue(e);
        }

        public event EventHandler<UpdatePortfolioEventArgs> UpdatePortfolio;
        protected virtual void OnUpdatePortfolio(UpdatePortfolioEventArgs e)
        {
            if (UpdatePortfolio != null)
                UpdatePortfolio(this, e);
        }
        private void updatePortfolio(Contract contract, int position, double marketPrice, double marketValue,
                                    double averageCost, double unrealizedPNL, double realizedPNL, string accountName)
        {
            UpdatePortfolioEventArgs e = new UpdatePortfolioEventArgs(contract, position, marketPrice, marketValue, averageCost, unrealizedPNL, realizedPNL, accountName);
            OnUpdatePortfolio(e);
        }

        public event EventHandler<UpdateAccountTimeEventArgs> UpdateAccountTime;
        protected virtual void OnUpdateAccountTime(UpdateAccountTimeEventArgs e)
        {
            if(UpdateAccountTime != null)
                UpdateAccountTime(this, e);
        }
        private void updateAccountTime(string timeStamp)
        {
            UpdateAccountTimeEventArgs e = new UpdateAccountTimeEventArgs(timeStamp);
            OnUpdateAccountTime(e);
        }

        public event EventHandler<NextValidIdEventArgs> NextValidId;
        protected virtual void OnNextValidId(NextValidIdEventArgs e)
        {
            if (NextValidId != null)
                NextValidId(this, e);
        }
        private void nextValidId(int orderId)
        {
            NextValidIdEventArgs e = new NextValidIdEventArgs(orderId);
            OnNextValidId(e);
        }

        public event EventHandler<ContractDetailsEventArgs> ContractDetails;
        protected virtual void OnContractDetails(ContractDetailsEventArgs e)
        {
            if (ContractDetails != null)
                ContractDetails(this, e);
        }
        private void contractDetails(ContractDetails contractDetails)
        {
            ContractDetailsEventArgs e = new ContractDetailsEventArgs(contractDetails);
            OnContractDetails(e);
        }

        public event EventHandler<BondContractDetailsEventArgs> BondContractDetails;
        protected virtual void OnBondContractDetails(BondContractDetailsEventArgs e)
        {
            if (BondContractDetails != null)
                BondContractDetails(this, e);
        }
        private void bondContractDetails(ContractDetails contractDetails)
        {
            BondContractDetailsEventArgs e = new BondContractDetailsEventArgs(contractDetails);
            OnBondContractDetails(e);
        }

        public event EventHandler<ExecDetailsEventArgs> ExecDetails;
        protected virtual void OnExecDetails(ExecDetailsEventArgs e)
        {
            if(ExecDetails!= null)
            {
                ExecDetails(this, e);
            }
        }
        private void execDetails(int orderId, Contract contract, Execution execution)
        {
            ExecDetailsEventArgs e = new ExecDetailsEventArgs(orderId, contract, execution);
            OnExecDetails(e);
        }

        public event EventHandler<UpdateMktDepthEventArgs> UpdateMktDepth;
        protected virtual void OnUpdateMktDepth(UpdateMktDepthEventArgs e)
        {
            if (UpdateMktDepth != null)
                UpdateMktDepth(this, e);
        }
        private void updateMktDepth(int tickerId, int position, int operation, int side, double price, int size)
        {
            UpdateMktDepthEventArgs e = new UpdateMktDepthEventArgs(tickerId, position, operation, side, price, size);
            OnUpdateMktDepth(e);
        }

        public event EventHandler<UpdateMktDepthL2EventArgs> UpdateMktDepthL2;
        protected virtual void OnUpdateMktDepthL2(UpdateMktDepthL2EventArgs e)
        {
            if (UpdateMktDepthL2 != null)
                UpdateMktDepthL2(this, e);
        }
        private void updateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side,
                                     double price, int size)
        {
            UpdateMktDepthL2EventArgs e = new UpdateMktDepthL2EventArgs(tickerId, position, marketMaker, operation, side, price, size);
            OnUpdateMktDepthL2(e);
        }

        public event EventHandler<UpdateNewsBulletinEventArgs> UpdateNewsBulletin;
        protected virtual void OnUpdateNewsBulletin(UpdateNewsBulletinEventArgs e)
        {
            if (UpdateNewsBulletin != null)
                UpdateNewsBulletin(this, e);
        }
        private void updateNewsBulletin(int msgId, int msgType, string message, string origExchange)
        {
            UpdateNewsBulletinEventArgs e = new UpdateNewsBulletinEventArgs(msgId, msgType, message, origExchange);
            OnUpdateNewsBulletin(e);
        }

        public event EventHandler<ManagedAccountsEventArgs> ManagedAccounts;
        protected virtual void OnManagedAccounts(ManagedAccountsEventArgs e)
        {
            if (ManagedAccounts != null)
                ManagedAccounts(this, e);
        }
        private void managedAccounts(string accountsList)
        {
            ManagedAccountsEventArgs e = new ManagedAccountsEventArgs(accountsList);
            OnManagedAccounts(e);
        }

        public event EventHandler<ReceiveFAEventArgs> ReceiveFA;
        protected virtual void OnReceiveFA(ReceiveFAEventArgs e)
        {
            if (ReceiveFA != null)
                ReceiveFA(this, e);
        }
        private void receiveFA(int faDataType, string xml)
        {
            ReceiveFAEventArgs e = new ReceiveFAEventArgs(faDataType, xml);
            OnReceiveFA(e);
        }

        public event EventHandler<HistoricalDataEventArgs> HistoricalData;
        protected virtual void OnHistoricalData(HistoricalDataEventArgs e)
        {
            if (HistoricalData != null)
                HistoricalData(this, e);
        }
        private void historicalData(int reqId, string date, double open, double high, double low, double close,
                                   int volume, int count, double WAP, bool hasGaps)
        {
            HistoricalDataEventArgs e = new HistoricalDataEventArgs(reqId, date, open, high, low, close, volume, count, WAP, hasGaps);
            OnHistoricalData(e);
        }

        public event EventHandler<ScannerParametersEventArgs> ScannerParameters;
        protected virtual void OnScannerParameters(ScannerParametersEventArgs e)
        {
            if (ScannerParameters != null)
                ScannerParameters(this, e);
        }
        private void scannerParameters(string xml)
        {
            ScannerParametersEventArgs e = new ScannerParametersEventArgs(xml);
            OnScannerParameters(e);
        }

        public event EventHandler<ScannerDataEventArgs> ScannerData;
        protected virtual void OnScannerData(ScannerDataEventArgs e)
        {
            if (ScannerData != null)
                ScannerData(this, e);
        }
        private void scannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark,
                                string projection, string legsStr)
        {
            ScannerDataEventArgs e = new ScannerDataEventArgs(reqId, rank,contractDetails,distance,benchmark,projection,legsStr);
            OnScannerData(e);
        }

        public event EventHandler<ErrorEventArgs> Error;
        protected virtual void OnError(ErrorEventArgs e)
        {
            if (Error != null)
                Error(this, e);
        }
        private void error(int tickerId, ErrorMessages errorCode, string errorMsg)
        {
            lock (this)
            {
                ErrorEventArgs e = new ErrorEventArgs(tickerId, errorCode, errorMsg);
                OnError(e);
            }
        }

        public void error(ErrorMessages errorCode, ErrorMessages errorString)
        {
            error(errorCode, errorString.ToString());
        }

        public void error(int tickerId, ErrorMessages errorCode, ErrorMessages errorString)
        {
            error(tickerId, errorCode, errorString.ToString());
        }

        public void error(ErrorMessages errorCode, Exception e)
        {
            error(errorCode, e.ToString());
        }

        public void error(int tickerId, ErrorMessages errorCode, Exception e)
        {
            error(tickerId, errorCode, e.ToString());
        }

        public void error(ErrorMessages errorCode)
        {
            error(errorCode, "");
        }

        public void error(string tail)
        {
            error(ErrorMessages.NoValidId, tail);
        }

        public void error(int tickerId, ErrorMessages errorCode)
        {
            error(tickerId, errorCode, "");
        }

        public void error(ErrorMessages errorCode, string tail)
        {
            error((int)ErrorMessages.NoValidId, errorCode, tail);
        } 

        public event EventHandler<ConnectionClosedEventArgs> ConnectionClosed;
        protected virtual void OnConnectionClosed(ConnectionClosedEventArgs e)
        {
            if(ConnectionClosed!=null)
                ConnectionClosed(this, e);
        }
        private void connectionClosed()
        {
            ConnectionClosedEventArgs e = new ConnectionClosedEventArgs();
            OnConnectionClosed(e);
        }
        #endregion

        #region Constructor / Destructor
        public IBClient()
        {
            readThread = new Thread(Run);
        }
        void  IDisposable.Dispose()
        {
 	        throw new Exception("The method or operation is not implemented.");
        }
        #endregion

        #region IBClientSocket
        #region Values
        private const int CLIENT_VERSION = 31;
        private const int SERVER_VERSION = 1;
        #endregion

        #region Properties
        public bool Connected
        {
            get
            {
                return connected;
            }
			
        }
        public int ServerLogLevel
        {
            set
            {
                lock (this)
                {
                    // not connected?
                    if (!connected)
                    {
                        error(ErrorMessages.NotConnected, "");
                        return ;
                    }
					
                    //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                    int VERSION = 1;
					
                    // send the set server logging level message
                    try
                    {
                        send((int)OutgoingMessage.SetServerLogLevel);
                        send(VERSION);
                        send(value);
                    }
                    catch (Exception e)
                    {
                        //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                        error(ErrorMessages.FailSendServerLogLevel, e.ToString());
                        close();
                    }
                }
            }
			
        }
        public int ServerVersion
        {
            get { return serverVersion; }
        }
        public String TwsConnectionTime
        {
            get { return twsTime;}
        }
        #endregion

        #region Private Variables
        private static readonly sbyte[] EOL = new sbyte[]{0};
        private System.Net.Sockets.TcpClient ibSocket; // the ibSocket
        private System.IO.BinaryWriter dos; // the ibSocket output stream
        private bool connected; // true if we are connected
        private int serverVersion = 1;
        private String twsTime;
        #endregion

        #region General Methods
        public void Connect(String host, int port, int clientId)
        {
            lock (this)
            {
                // already connected?
                host = checkConnected(host);
                if (host == null)
                {
                    return ;
                }
                try
                {
                    System.Net.Sockets.TcpClient socket = new System.Net.Sockets.TcpClient(host, port);
                    Connect(socket, clientId);
                }
                catch (Exception)
                {
                    connectionError();
                }
            }
        }
        protected internal virtual void  connectionError()
        {
            error(ErrorMessages.ConnectFail, ErrorMessages.ConnectFail);
       }
        protected internal virtual String checkConnected(String host)
        {
            if (connected)
            {
                error(ErrorMessages.ConnectFail, ErrorMessages.AlreadyConnected);
                return null;
            }
            if (isNull(host))
            {
                host = "127.0.0.1";
            }
            return host;
        }
        //UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'eConnect'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  Connect(System.Net.Sockets.TcpClient socket, int clientId)
        {
            lock (this)
            {
                this.ibSocket = socket;
				
                // create io streams
                //UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
                System.IO.BinaryReader dis = new System.IO.BinaryReader(this.ibSocket.GetStream());
                //UPGRADE_TODO: Class 'java.io.DataOutputStream' was converted to 'System.IO.BinaryWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataOutputStream'"
                dos = new System.IO.BinaryWriter(this.ibSocket.GetStream());
				
                // set client version
                send(CLIENT_VERSION);
				
                // start Reader thread
                this.dis = dis;
				
                // check server version
                serverVersion = ReadInt();
                Console.Out.WriteLine("Server Version:" + serverVersion);
                if (serverVersion >= 20)
                {
                    twsTime = ReadStr();
                    Console.Out.WriteLine("TWS Time at connection:" + twsTime);
                }
                if (serverVersion < SERVER_VERSION)
                {
                    error(ErrorMessages.UpdateTws, ErrorMessages.UpdateTws);
                    return ;
                }
				
                // Send the client id
                if (serverVersion >= 3)
                {
                    send(clientId);
                }
				
                Start();
				
                // set connected flag
                connected = true;
            }
        }
		
        public virtual void  Disconnect()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    return ;
                }
				
                try
                {
                    // stop Reader thread
                    Stop();
					
                    // close ibSocket
                    if (ibSocket != null)
                    {
                        ibSocket.Close();
                    }
                }
                catch (Exception)
                {
                }
				
                connected = false;
            }
        }		
        protected internal virtual void  close()
        {
            Disconnect();
            connectionClosed();
        }
        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Network Commmands
        public void  CancelScannerSubscription(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                if (serverVersion < 24)
                {
                    error(ErrorMessages.UpdateTws, "It does not support API scanner subscription.");
                    return ;
                }
				
                int VERSION = 1;
				
                // send cancel mkt data msg
                try
                {
                    send((int)OutgoingMessage.CancelScannerSubscription);
                    send(VERSION);
                    send(tickerId);
                }
                catch (Exception e)
                {
                    error(tickerId, ErrorMessages.FailSendCancelScanner, e);
                    close();
                }
            }
        }
		
        public void  ReqScannerParameters()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                if (serverVersion < 24)
                {
                    error(ErrorMessages.UpdateTws, "It does not support API scanner subscription.");
                    return ;
                }
				
                int VERSION = 1;
				
                try
                {
                    send((int)OutgoingMessage.RequestScannerParameters);
                    send(VERSION);
                }
                catch (Exception e)
                {
                    error(ErrorMessages.FailSendRequestScannerParameters, e);
                    close();
                }
            }
        }
		
        public void  ReqScannerSubscription(int tickerId, ScannerSubscription subscription)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                if (serverVersion < 24)
                {
                    error(ErrorMessages.UpdateTws, "It does not support API scanner subscription.");
                    return ;
                }
				
                int version = 3;
				
                try
                {
                    send((int)OutgoingMessage.RequestScannerSubscription);
                    send(version);
                    send(tickerId);
                    sendMax(subscription.NumberOfRows);
                    send(subscription.Instrument);
                    send(subscription.LocationCode);
                    send(subscription.ScanCode);
                    sendMax(subscription.AbovePrice);
                    sendMax(subscription.BelowPrice);
                    sendMax(subscription.AboveVolume);
                    sendMax(subscription.MarketCapAbove);
                    sendMax(subscription.MarketCapBelow);
                    send(subscription.MoodyRatingAbove);
                    send(subscription.MoodyRatingBelow);
                    send(subscription.SpRatingAbove);
                    send(subscription.SpRatingBelow);
                    send(subscription.MaturityDateAbove);
                    send(subscription.MaturityDateBelow);
                    sendMax(subscription.CouponRateAbove);
                    sendMax(subscription.CouponRateBelow);
                    send(subscription.ExcludeConvertible);
                    if (serverVersion >= 25)
                    {
                        send(subscription.AverageOptionVolumeAbove);
                        send(subscription.ScannerSettingPairs);
                    }
                    if (serverVersion >= 27)
                    {
                        send(subscription.StockTypeFilter);
                    }
                }
                catch (Exception e)
                {
                    error(tickerId, ErrorMessages.FailSendRequestScanner, e);
                    close();
                }
            }
        }
		
        public void ReqMktData(int tickerId, Contract contract, List<GenericTickType> genericTickList)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                int version = 6;
				
                try
                {
                    // send req mkt data msg
                    send((int)OutgoingMessage.RequestMarketData);
                    send(version);
                    send(tickerId);
					
                    send(contract.Symbol);
                    send(contract.SecType.ToString());
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(((contract.Right == RightType.Undefined) ? "" : contract.Right.ToString()));
                    if (serverVersion >= 15)
                    {
                        send(contract.Multiplier);
                    }
                    send(contract.Exchange);
                    if (serverVersion >= 14)
                    {
                        send(contract.PrimaryExch);
                    }
                    send(contract.Currency);
                    if (serverVersion >= 2)
                    {
                        send(contract.LocalSymbol);
                    }
                    if (serverVersion >= 8 && contract.SecType == SecurityType.Bag)
                    {
                        if (contract.ComboLegs == null)
                        {
                            send(0);
                        }
                        else
                        {
                            send(contract.ComboLegs.Count);
							
                            ComboLeg comboLeg;
                            for (int i = 0; i < contract.ComboLegs.Count; i++)
                            {
                                comboLeg = (ComboLeg) contract.ComboLegs[i];
                                send(comboLeg.ConId);
                                send(comboLeg.Ratio);
                                send(comboLeg.Action.ToString());
                                send(comboLeg.Exchange);
                            }
                        }
                    }
                    if (serverVersion >= 31)
                    {
                        string genList = "";
                        if (genericTickList != null)
                        {
                            if (genericTickList.Count > 0)
                                genList = genericTickList[0].ToString();
                            for (int ix = 1; ix < genericTickList.Count; ix++)
                                genList = genList + "," + genericTickList[ix].ToString();
                        }
                        send(genList);
                    }
                }
                catch (Exception e)
                {
                    error(tickerId, ErrorMessages.FailSendRequestMarket, e);
                    close();
                }
            }
        }
		
        public void  CancelHistoricalData(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                if (serverVersion < 24)
                {
                    error(ErrorMessages.UpdateTws, "It does not support historical data query cancellation.");
                    return ;
                }
				
                int version = 1;
				
                // send cancel mkt data msg
                try
                {
                    send((int)OutgoingMessage.CancelHistoricalData);
                    send(version);
                    send(tickerId);
                }
                catch (Exception e)
                {
                    error(tickerId, ErrorMessages.FailSendCancelScanner, e);
                    close();
                }
            }
        }
		
        public void  ReqHistoricalData(int tickerId, Contract contract, String endDateTime, String durationStr, String barSizeSetting, String whatToShow, int useRTH, int formatDate)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(tickerId, ErrorMessages.NotConnected);
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 4;
				
                try
                {
                    if (serverVersion < 16)
                    {
                        error(ErrorMessages.UpdateTws, "It does not support historical data backfill.");
                        return ;
                    }

                    send((int)OutgoingMessage.RequestHistoricalData);
                    send(VERSION);
                    send(tickerId);
                    send(contract.Symbol);
                    send(contract.SecType.ToString());
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(((contract.Right == RightType.Undefined) ? "" : contract.Right.ToString()));
                    send(contract.Multiplier);
                    send(contract.Exchange);
                    send(contract.PrimaryExch);
                    send(contract.Currency);
                    send(contract.LocalSymbol);
                    if (serverVersion >= 31)
                    {
                        send(contract.IncludeExpired?1:0);
                    }
                    if (serverVersion >= 20)
                    {
                        send(endDateTime);
                        send(barSizeSetting);
                    }
                    send(durationStr);
                    send(useRTH);
                    send(whatToShow);
                    if (serverVersion > 16)
                    {
                        send(formatDate);
                    }
                    if (contract.SecType == SecurityType.Bag)
                    {
                        if (contract.ComboLegs == null)
                        {
                            send(0);
                        }
                        else
                        {
                            send(contract.ComboLegs.Count);
							
                            ComboLeg comboLeg;
                            for (int i = 0; i < contract.ComboLegs.Count; i++)
                            {
                                comboLeg = (ComboLeg) contract.ComboLegs[i];
                                send(comboLeg.ConId);
                                send(comboLeg.Ratio);
                                send(comboLeg.Action.ToString());
                                send(comboLeg.Exchange);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    error(tickerId, ErrorMessages.FailSendRequestHistoricalData, e);
                    close();
                }
            }
        }
		
        public void  ReqContractDetails(Contract contract)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                // This feature is only available for versions of TWS >=4
                if (serverVersion < 4)
                {
                    error(ErrorMessages.UpdateTws, "Does not support Request Contract Details.");
                    return ;
                }
				
                int version = 3;
				
                try
                {
                    // send req mkt data msg
                    send((int)OutgoingMessage.RequestContractData);
                    send(version);
					
                    send(contract.Symbol);
                    send(contract.SecType.ToString());
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(((contract.Right == RightType.Undefined) ? "" : contract.Right.ToString()));
                    if (serverVersion >= 15)
                    {
                        send(contract.Multiplier);
                    }
                    send(contract.Exchange);
                    send(contract.Currency);
                    send(contract.LocalSymbol);
                    if (serverVersion >= 31)
                    {
                        send(contract.IncludeExpired);
                    }
                }
                catch (Exception e)
                {
                    error(ErrorMessages.FailSendRequestContract, e);
                    close();
                }
            }
        }
		
        public void  ReqMktDepth(int tickerId, Contract contract, int numRows)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                // This feature is only available for versions of TWS >=6
                if (serverVersion < 6)
                {
                    error(ErrorMessages.UpdateTws, "It does not support market depth.");
                    return ;
                }
				
                int version = 3;
				
                try
                {
                    // send req mkt data msg
                    send((int)OutgoingMessage.RequestMarketDepth);
                    send(version);
                    send(tickerId);
					
                    send(contract.Symbol);
                    send(contract.SecType.ToString());
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(((contract.Right == RightType.Undefined) ? "" : contract.Right.ToString()));
                    if (serverVersion >= 15)
                    {
                        send(contract.Multiplier);
                    }
                    send(contract.Exchange);
                    send(contract.Currency);
                    send(contract.LocalSymbol);
                    if (serverVersion >= 19)
                    {
                        send(numRows);
                    }
                }
                catch (Exception e)
                {
                    error(tickerId, ErrorMessages.FailSendRequestMarketDepth, e);
                    close();
                }
            }
        }
		
        public void  CancelMktData(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                int version = 1;
				
                // send cancel mkt data msg
                try
                {
                    send((int)OutgoingMessage.CancelMarketData);
                    send(version);
                    send(tickerId);
                }
                catch (Exception e)
                {
                    error(tickerId, ErrorMessages.FailSendCancelMarket, e);
                    close();
                }
            }
        }
		
        public void  CancelMktDepth(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                // This feature is only available for versions of TWS >=6
                if (serverVersion < 6)
                {
                    error(ErrorMessages.UpdateTws, "It does not support canceling market depth.");
                    return ;
                }
				
                int version = 1;
				
                // send cancel mkt data msg
                try
                {
                    send((int)OutgoingMessage.CancelMarketDepth);
                    send(version);
                    send(tickerId);
                }
                catch (Exception e)
                {
                    error(tickerId, ErrorMessages.FailSendCancelMarketDepth, e);
                    close();
                }
            }
        }
		
        public void  ExerciseOptions(int tickerId, Contract contract, int exerciseAction, int exerciseQuantity, String account, int override_Renamed)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(tickerId, ErrorMessages.NotConnected);
                    return ;
                }
				
                int version = 1;
				
                try
                {
                    if (serverVersion < 21)
                    {
                        error(ErrorMessages.UpdateTws, "It does not support options exercise from the API.");
                        return ;
                    }

                    send((int)OutgoingMessage.ExerciseOptions);
                    send(version);
                    send(tickerId);
                    send(contract.Symbol);
                    send(contract.SecType.ToString());
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(((contract.Right == RightType.Undefined) ? "" : contract.Right.ToString()));
                    send(contract.Multiplier);
                    send(contract.Exchange);
                    send(contract.Currency);
                    send(contract.LocalSymbol);
                    send(exerciseAction);
                    send(exerciseQuantity);
                    send(account);
                    send(override_Renamed);
                }
                catch (Exception e)
                {
                    error(tickerId, ErrorMessages.FailSendRequestMarket, e);
                    close();
                }
            }
        }
		
        public void  PlaceOrder(int tickerId, Contract contract, Order order)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(tickerId, ErrorMessages.NotConnected);
                    return ;
                }
				
                int version = 21;
				
                // send place order msg
                try
                {
                    send((int)OutgoingMessage.PlaceOrder);
                    send(version);
                    send(tickerId);
					
                    // send contract fields
                    send(contract.Symbol);
                    send(contract.SecType.ToString());
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(((contract.Right == RightType.Undefined) ? "" : contract.Right.ToString()));
                    if (serverVersion >= 15)
                    {
                        send(contract.Multiplier);
                    }
                    send(contract.Exchange);
                    if (serverVersion >= 14)
                    {
                        send(contract.PrimaryExch);
                    }
                    send(contract.Currency);
                    if (serverVersion >= 2)
                    {
                        send(contract.LocalSymbol);
                    }
					
                    // send main order fields
                    send(order.Action.ToString());
                    send(order.TotalQuantity);
                    send(order.OrderType.ToString());
                    send(order.LmtPrice);
                    send(order.AuxPrice);
					
                    // send extended order fields
                    send(order.Tif.ToString());
                    send(order.OcaGroup);
                    send(order.Account);
                    send(order.OpenClose);
                    send(order.Origin);
                    send(order.OrderRef);
                    send(order.Transmit);
                    if (serverVersion >= 4)
                    {
                        send(order.ParentId);
                    }
					
                    if (serverVersion >= 5)
                    {
                        send(order.BlockOrder);
                        send(order.SweepToFill);
                        send(order.DisplaySize);
                        send(order.TriggerMethod);
                        send(order.IgnoreRth);
                    }
					
                    if (serverVersion >= 7)
                    {
                        send(order.Hidden);
                    }
					
                    // Send combo legs for BAG requests
                    if (serverVersion >= 8 && contract.SecType == SecurityType.Bag)
                    {
                        if (contract.ComboLegs == null)
                        {
                            send(0);
                        }
                        else
                        {
                            send(contract.ComboLegs.Count);
							
                            ComboLeg comboLeg;
                            for (int i = 0; i < contract.ComboLegs.Count; i++)
                            {
                                comboLeg = (ComboLeg) contract.ComboLegs[i];
                                send(comboLeg.ConId);
                                send(comboLeg.Ratio);
                                send(comboLeg.Action.ToString());
                                send(comboLeg.Exchange);
                                send(comboLeg.OpenClose.ToString());
                            }
                        }
                    }
					
                    if (serverVersion >= 9)
                    {
                        send(order.SharesAllocation); // deprecated
                    }
					
                    if (serverVersion >= 10)
                    {
                        send(order.DiscretionaryAmt);
                    }
					
                    if (serverVersion >= 11)
                    {
                        send(order.GoodAfterTime);
                    }
					
                    if (serverVersion >= 12)
                    {
                        send(order.GoodTillDate);
                    }
					
                    if (serverVersion >= 13)
                    {
                        send(order.FaGroup);
                        send(order.FaMethod);
                        send(order.FaPercentage);
                        send(order.FaProfile);
                    }
                    if (serverVersion >= 18)
                    {
                        // institutional short sale slot fields.
                        send(order.ShortSaleSlot); // 0 only for retail, 1 or 2 only for institution.
                        send(order.DesignatedLocation); // only populate when order.shortSaleSlot = 2.
                    }
                    if (serverVersion >= 19)
                    {
                        send(order.OcaType.ToString());
                        send(order.RthOnly);
                        send(order.Rule80A);
                        send(order.SettlingFirm);
                        send(order.AllOrNone);
                        sendMax(order.MinQty);
                        sendMax(order.PercentOffset);
                        send(order.ETradeOnly);
                        send(order.FirmQuoteOnly);
                        sendMax(order.NbboPriceCap);
                        sendMax(order.AuctionStrategy);
                        sendMax(order.StartingPrice);
                        sendMax(order.StockRefPrice);
                        sendMax(order.Delta);
                        // Volatility orders had specific watermark price attribs in server version 26
                        // BUG: VOL is not an order type this will never be true
                        double lower = (serverVersion == 26 && order.OrderType.Equals("VOL"))?Double.MaxValue:order.StockRangeLower;
                        double upper = (serverVersion == 26 && order.OrderType.Equals("VOL"))?Double.MaxValue:order.StockRangeUpper;
                        sendMax(lower);
                        sendMax(upper);
                    }
					
                    if (serverVersion >= 22)
                    {
                        send(order.OverridePercentageConstraints);
                    }
					
                    if (serverVersion >= 26)
                    {
                        // Volatility orders
                        sendMax(order.Volatility);
                        sendMax(order.VolatilityType);
                        if (serverVersion < 28)
                        {
                            // BUG: MKT Order will never be the case
                            send(order.DeltaNeutralOrderType.ToUpper().Equals("MKT".ToUpper()));
                        }
                        else
                        {
                            send(order.DeltaNeutralOrderType);
                            sendMax(order.DeltaNeutralAuxPrice);
                        }
                        send(order.ContinuousUpdate);
                        if (serverVersion == 26)
                        {
                            // Volatility orders had specific watermark price attribs in server version 26
                            // BUG: VOL will never be an order
                            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                            double lower = order.OrderType.Equals("VOL")?order.StockRangeLower:Double.MaxValue;
                            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                            double upper = order.OrderType.Equals("VOL")?order.StockRangeUpper:Double.MaxValue;
                            sendMax(lower);
                            sendMax(upper);
                        }
                        sendMax(order.ReferencePriceType);
                    }
					
                    if (serverVersion >= 30)
                    {
                        // TRAIL_STOP_LIMIT stop price
                        sendMax(order.TrailStopPrice);
                    }
                }
                catch (Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(tickerId, ErrorMessages.FailSendOrder, "" + e);
                    close();
                }
            }
        }
		
        public void  ReqAccountUpdates(bool subscribe, String acctCode)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int version = 2;
				
                // send cancel order msg
                try
                {
                    send((int)OutgoingMessage.RequestAccountData);
                    send(version);
                    send(subscribe);
					
                    // Send the account code. This will only be used for FA clients
                    if (serverVersion >= 9)
                    {
                        send(acctCode);
                    }
                }
                catch (Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(ErrorMessages.FailSendAccountUpdate, e);
                    close();
                }
            }
        }
		
        public void  ReqExecutions(ExecutionFilter filter)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int version = 2;
				
                // send cancel order msg
                try
                {
                    send((int)OutgoingMessage.RequestExecutions);
                    send(version);
					
                    // Send the execution rpt filter data
                    if (serverVersion >= 9)
                    {
                        send(filter.ClientId);
                        send(filter.AcctCode);
						
                        // Note that the valid format for time is "yyyymmdd-hh:mm:ss"
                        send(filter.Time.ToString("yyyymmdd-hh:mm:ss"));
                        send(filter.Symbol);
                        send(filter.SecType.ToString());
                        send(filter.Exchange);
                        send(filter.Side.ToString());
                    }
                }
                catch (Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(ErrorMessages.FailSendExecution, e);
                    close();
                }
            }
        }
		
        public void  CancelOrder(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(tickerId, ErrorMessages.NotConnected);
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int version = 1;
				
                // send cancel order msg
                try
                {
                    send((int)OutgoingMessage.CancelOrder);
                    send(version);
                    send(tickerId);
                }
                catch (Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(tickerId, ErrorMessages.FailSendCancelOrder, e);
                    close();
                }
            }
        }
		
        public void  ReqOpenOrders()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                int version = 1;
				
                // send cancel order msg
                try
                {
                    send((int)OutgoingMessage.RequestOpenOrders);
                    send(version);
                }
                catch (Exception e)
                {
                    error(ErrorMessages.FailSendOpenOrder, e);
                    close();
                }
            }
        }
		
        public void  ReqIds(int numIds)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                int version = 1;
				
                try
                {
                    send((int)OutgoingMessage.RequestIds);
                    send(version);
                    send(numIds);
                }
                catch (Exception e)
                {
                    //BUG: Wrong error message
                    error(ErrorMessages.FailSendCancelOrder, e);
                    close();
                }
            }
        }
		
        public void  ReqNewsBulletins(bool allMsgs)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                int version = 1;
				
                try
                {
                    send((int)OutgoingMessage.RequestNewsBulletins);
                    send(version);
                    send(allMsgs);
                }
                catch (Exception e)
                {
                    //BUG: Wrong error message
                    error(ErrorMessages.FailSendCancelOrder, e);
                    close();
                }
            }
        }
		
        public void  CancelNewsBulletins()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                int version = 1;
				
                // send cancel order msg
                try
                {
                    send((int)OutgoingMessage.CancelNewsBulletins);
                    send(version);
                }
                catch (Exception e)
                {
                    // BUG: This is a failure to cancel an order?
                    error(ErrorMessages.FailSendCancelOrder, e);
                    close();
                }
            }
        }
		
        public void  ReqAutoOpenOrders(bool bAutoBind)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int version = 1;
				
                // send req open orders msg
                try
                {
                    send((int)OutgoingMessage.RequestAutoOpenOrders);
                    send(version);
                    send(bAutoBind);
                }
                catch (Exception e)
                {
                    //BUG: This is an auto opn order not an open order
                    error(ErrorMessages.FailSendOpenOrder, e);
                    close();
                }
            }
        }
		
        public void  ReqAllOpenOrders()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                int version = 1;
				
                // send req all open orders msg
                try
                {
                    send((int)OutgoingMessage.RequestAllOpenOrders);
                    send(version);
                }
                catch (Exception e)
                {
                    //BUG: Wrong error message
                    error(ErrorMessages.FailSendOpenOrder, e);
                    close();
                }
            }
        }
		
        public void  ReqManagedAccts()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                int version = 1;
				
                // send req FA managed accounts msg
                try
                {
                    send((int)OutgoingMessage.RequestManagedAccounts);
                    send(version);
                }
                catch (Exception e)
                {
                    //Bug: Wrong error message
                    error(ErrorMessages.FailSendOpenOrder, e);
                    close();
                }
            }
        }
		
        public void  RequestFA(int faDataType)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                // This feature is only available for versions of TWS >= 13
                if (serverVersion < 13)
                {
                    error(ErrorMessages.UpdateTws, "Does not support request FA.");
                    return ;
                }
				
                int version = 1;
				
                try
                {
                    send((int)OutgoingMessage.RequestFA);
                    send(version);
                    send(faDataType);
                }
                catch (Exception e)
                {
                    error(faDataType, ErrorMessages.FailSendFARequest, e);
                    close();
                }
            }
        }
		
        public void  ReplaceFA(int faDataType, String xml)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessages.NotConnected);
                    return ;
                }
				
                // This feature is only available for versions of TWS >= 13
                if (serverVersion < 13)
                {
                    error(ErrorMessages.UpdateTws, "Does not support Replace FA.");
                    return ;
                }
				
                int version = 1;
				
                try
                {
                    send((int)OutgoingMessage.ReplaceFA);
                    send(version);
                    send(faDataType);
                    send(xml);
                }
                catch (Exception e)
                {
                    error(faDataType, ErrorMessages.FailSendFAReplace, e);
                    close();
                }
            }
        }
        #endregion

        #region Helper Methods

        private static bool is_Renamed(String str)
        {
            // return true if the string is not empty
            return str != null && str.Length > 0;
        }
		
        private static bool isNull(String str)
        {
            // return true if the string is null or empty
            return !is_Renamed(str);
        }

        /// <summary>
        /// Converts an array of sbytes to an array of bytes
        /// </summary>
        /// <param name="sbyteArray">The array of sbytes to be converted</param>
        /// <returns>The new array of bytes</returns>
        public static byte[] ToByteArray(sbyte[] sbyteArray)
        {
            byte[] byteArray = null;

            if (sbyteArray != null)
            {
                byteArray = new byte[sbyteArray.Length];
                for (int index = 0; index < sbyteArray.Length; index++)
                    byteArray[index] = (byte)sbyteArray[index];
            }
            return byteArray;
        }
		
        protected internal virtual void  send(String str)
        {
            // write string to data buffer; writer thread will
            // write it to ibSocket
            if (str != null)
            {
                dos.Write(ToByteArray(str));
            }
            sendEOL();
        }

        /// <summary>
        /// Converts a string to an array of bytes
        /// </summary>
        /// <param name="sourceString">The string to be converted</param>
        /// <returns>The new array of bytes</returns>
        public static byte[] ToByteArray(System.String sourceString)
        {
            return System.Text.UTF8Encoding.UTF8.GetBytes(sourceString);
        }
		
        private void  sendEOL()
        {
            dos.Write(ToByteArray(EOL));
        }
		
        private void  send(int val)
        {
            send(Convert.ToString(val));
        }
		
        private void  send(char val)
        {
            dos.Write((Byte) val);
            sendEOL();
        }
		
        private void  send(double val)
        {
            send(Convert.ToString(val));
        }
		
        private void  send(long val)
        {
            send(Convert.ToString(val));
        }
		
        private void  sendMax(double val)
        {
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            if (val == Double.MaxValue)
            {
                sendEOL();
            }
            else
            {
                send(Convert.ToString(val));
            }
        }
		
        private void  sendMax(int val)
        {
            if (val == Int32.MaxValue)
            {
                sendEOL();
            }
            else
            {
                send(Convert.ToString(val));
            }
        }
		
        private void  send(bool val)
        {
            send(val?1:0);
        }
        #endregion
        #endregion

        #region IBReader
        #region Thread Sync
        private readonly Thread readThread;
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
        private System.IO.BinaryReader dis;
        #endregion

        #region General Code
        public void  Run()
        {
            try
            {
                // loop until thread is terminated
                while (!Stopping && ProcessMsg((IncomingMessage)ReadInt()));
            }
            finally
            {
                SetStopped();
                connectionClosed();
                close();
            }
        }

        internal void Start()
        {
            if(!Stopping)
                readThread.Start();
        }
        #endregion

        #region Process Message
        /// <summary>Overridden in subclass.</summary>
        private bool ProcessMsg(IncomingMessage msgId)
        {
            if (msgId == IncomingMessage.Error)
                return false;
			
            switch (msgId)
            {
				
                case IncomingMessage.TickPrice:  {
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
                    tickPrice(tickerId, tickType, price, canAutoExecute);
						
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
                            tickSize(tickerId, sizeTickType, size);
                        }
                    }
                    break;
                }

            case IncomingMessage.TickSize:
                {
                    int version = ReadInt();
                    int tickerId = ReadInt();
                    int tickType = ReadInt();
                    int size = ReadInt();
						
                    tickSize(tickerId, tickType, size);
                    break;
                }


            case IncomingMessage.TickOptionComputation:
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
                    if (tickType == (int)TickType.ModelOption)
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
                    tickOptionComputation(tickerId, tickType, impliedVol, delta, modelPrice, pvDividend);
                    break;
                }


            case IncomingMessage.TickGeneric:
                {
                    int version = ReadInt();
                    int tickerId = ReadInt();
                    int tickType = ReadInt();
                    double value_Renamed = ReadDouble();
						
                    tickGeneric(tickerId, tickType, value_Renamed);
                    break;
                }


            case IncomingMessage.TickString:
                {
                    int version = ReadInt();
                    int tickerId = ReadInt();
                    int tickType = ReadInt();
                    System.String value_Renamed = ReadStr();
						
                    tickString(tickerId, tickType, value_Renamed);
                    break;
                }


            case IncomingMessage.TickEfp:
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
                    tickEFP(tickerId, tickType, basisPoints, formattedBasisPoints, impliedFuturesPrice, holdDays, futureExpiry, dividendImpact, dividendsToExpiry);
                    break;
                }


            case IncomingMessage.OrderStatus:
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
						
                    orderStatus(id, status, filled, remaining, avgFillPrice, permId, parentId, lastFillPrice, clientId);
                    break;
                }


            case IncomingMessage.AccountValue:
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
                    updateAccountValue(key, val, cur, accountName);
                    break;
                }


            case IncomingMessage.PortfolioValue:
                {
                    int version = ReadInt();
                    Contract contract = new Contract();
                    contract.Symbol = ReadStr();
                    contract.SecType = (SecurityType)Enum.Parse(typeof(SecurityType), ReadStr());
                    contract.Expiry = ReadStr();
                    contract.Strike = ReadDouble();
                    string rstr = ReadStr();
                    contract.Right = (rstr.Length <= 0 ? RightType.Undefined : (RightType)Enum.Parse(typeof(RightType), rstr));
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
						
                    updatePortfolio(contract, position, marketPrice, marketValue, averageCost, unrealizedPNL, realizedPNL, accountName);
						
                    break;
                }


            case IncomingMessage.AccountUpdateTime:
                {
                    int version = ReadInt();
                    System.String timeStamp = ReadStr();
                    updateAccountTime(timeStamp);
                    break;
                }


            case IncomingMessage.ErrorMessage:
                {
                    int version = ReadInt();
                    if (version < 2)
                    {
                        System.String msg = ReadStr();
                        error(msg);
                    }
                    else
                    {
                        int id = ReadInt();
                        int errorCode = ReadInt();
                        System.String errorMsg = ReadStr();
                        error(id, (ErrorMessages)errorCode, errorMsg);
                    }
                    break;
                }


            case IncomingMessage.OpenOrder:
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
                    contract.Right = (rstr.Length <= 0 ? RightType.Undefined : (RightType)Enum.Parse(typeof(RightType), rstr));
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
                    order.OrderType = (OrderType)Enum.Parse(typeof(OrderType), ReadStr());
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
                        order.OcaType = (OcaType)ReadInt();
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
                        if (serverVersion == 26)
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
						
                    openOrder(order.OrderId, contract, order);
                    break;
                }


            case IncomingMessage.NextValidID:
                {
                    int version = ReadInt();
                    int orderId = ReadInt();
                    nextValidId(orderId);
                    break;
                }


            case IncomingMessage.ScannerData:
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
                        contract.Summary.Right = (rstr.Length <= 0 ? RightType.Undefined : (RightType)Enum.Parse(typeof(RightType), rstr));
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
                        scannerData(tickerId, rank, contract, distance, benchmark, projection, legsStr);
                    }
                    break;
                }


            case IncomingMessage.ContractData:
                {
                    int version = ReadInt();
                    ContractDetails contract = new ContractDetails();
                    contract.Summary.Symbol = ReadStr();
                    contract.Summary.SecType = (SecurityType)Enum.Parse(typeof(SecurityType), ReadStr());
                    contract.Summary.Expiry = ReadStr();
                    contract.Summary.Strike = ReadDouble();
                    string rstr = ReadStr();
                    contract.Summary.Right = (rstr.Length <= 0 ? RightType.Undefined : (RightType)Enum.Parse(typeof(RightType), rstr));
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
                    contractDetails(contract);
                    break;
                }

            case IncomingMessage.BondContractData:
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
                    bondContractDetails(contract);
                    break;
                }

            case IncomingMessage.ExecutionData:
                {
                    int version = ReadInt();
                    int orderId = ReadInt();
						
                    Contract contract = new Contract();
                    contract.Symbol = ReadStr();
                    contract.SecType = (SecurityType)Enum.Parse(typeof(SecurityType), ReadStr());
                    contract.Expiry = ReadStr();
                    contract.Strike = ReadDouble();
                    string rstr = ReadStr();
                    contract.Right = (rstr.Length <= 0 ? RightType.Undefined : (RightType)Enum.Parse(typeof(RightType), rstr));
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
						
                    execDetails(orderId, contract, exec);
                    break;
                }

            case IncomingMessage.MarketDepth:
                {
                    int version = ReadInt();
                    int id = ReadInt();
						
                    int position = ReadInt();
                    int operation = ReadInt();
                    int side = ReadInt();
                    double price = ReadDouble();
                    int size = ReadInt();
						
                    updateMktDepth(id, position, operation, side, price, size);
                    break;
                }

            case IncomingMessage.MarketDepthL2:
                {
                    int version = ReadInt();
                    int id = ReadInt();
						
                    int position = ReadInt();
                    System.String marketMaker = ReadStr();
                    int operation = ReadInt();
                    int side = ReadInt();
                    double price = ReadDouble();
                    int size = ReadInt();
						
                    updateMktDepthL2(id, position, marketMaker, operation, side, price, size);
                    break;
                }

            case IncomingMessage.NewsBulletins:
                {
                    int version = ReadInt();
                    int newsMsgId = ReadInt();
                    int newsMsgType = ReadInt();
                    System.String newsMessage = ReadStr();
                    System.String originatingExch = ReadStr();
						
                    updateNewsBulletin(newsMsgId, newsMsgType, newsMessage, originatingExch);
                    break;
                }

            case IncomingMessage.ManagedAccounts:
                {
                    int version = ReadInt();
                    System.String accountsList = ReadStr();
						
                    managedAccounts(accountsList);
                    break;
                }

            case IncomingMessage.ReceiveFA:
                {
                    int version = ReadInt();
                    int faDataType = ReadInt();
                    System.String xml = ReadStr();
						
                    receiveFA(faDataType, xml);
                    break;
                }

            case IncomingMessage.HistoricalData:
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
                        historicalData(reqId, date, open, high, low, close, volume, barCount, WAP, System.Boolean.Parse(hasGaps));
                    }
                    // send end of dataset marker
                    historicalData(reqId, completedIndicator, - 1, - 1, - 1, - 1, - 1, - 1, - 1, false);
                    break;
                }

            case IncomingMessage.ScannerParameters:
                {
                    int version = ReadInt();
                    System.String xml = ReadStr();
                    scannerParameters(xml);
                    break;
                }
				
                default:  {
                    error(ErrorMessages.NoValidId);
                    return false;
                }
				
            }
            return true;
        }
        #endregion

        #region Helper Methods
        protected internal virtual String ReadStr()
        {
            System.Text.StringBuilder buf = new System.Text.StringBuilder();
            while (true)
            {
                sbyte c = (sbyte) dis.ReadByte();
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
        #endregion
}
}
