using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Interactive Brokers Client
    /// Handles all communications to and from the TWS.
    /// </summary>
    public class IBClient : IDisposable
    {
        #region Tracer
        private GeneralTracer ibTrace = new GeneralTracer("ibInfo", "Interactive Brokers Parameter Info");
        private GeneralTracer ibTickTrace = new GeneralTracer("ibTicks", "Interactive Brokers Tick Info");
        #endregion

        #region IB Wrapper to Events

        ///<summary>
        /// Raise the event in a threadsafe manner
        ///</summary>
        ///<param name="event"></param>
        ///<param name="sender"></param>
        ///<param name="e"></param>
        ///<typeparam name="T"></typeparam>
        static private void RaiseEvent<T>(EventHandler<T> @event, object sender, T e)
        where T : EventArgs
        {
            EventHandler<T> handler = @event;
            if (handler == null) return;
            handler(sender, e);
        }

        /// <summary>
        /// This event is called when the market data changes. Prices are updated immediately with no delay.
        /// </summary>
        public event EventHandler<TickPriceEventArgs> TickPrice;

        /// <summary>
        /// Called internally when the receive thread receives a tick price event.
        /// </summary>
        /// <param name="e">Tick Price event arguments</param>
        protected virtual void OnTickPrice(TickPriceEventArgs e)
        {
            RaiseEvent(TickPrice, this, e);
        }

        private void tickPrice(int tickerId, TickType tickType, decimal price, bool canAutoExecute)
        {
            //GeneralTracer.WriteLineIf(ibTickTrace.TraceInfo, "IBEvent: TickPrice: tickerId: {0}, tickType: {1}, price: {2}, canAutoExecute: {3}", tickerId, tickType, price, canAutoExecute);
            TickPriceEventArgs e = new TickPriceEventArgs(tickerId, tickType, price, canAutoExecute);
            OnTickPrice(e);
        }

        /// <summary>
        /// This event is called when the market data changes. Sizes are updated immediately with no delay.
        /// </summary>
        public event EventHandler<TickSizeEventArgs> TickSize;

        /// <summary>
        /// Called internally when the receive thread receives a tick size event.
        /// </summary>
        /// <param name="e">Tick Size Event Arguments</param>
        protected virtual void OnTickSize(TickSizeEventArgs e)
        {
            RaiseEvent(TickSize, this, e);
        }

        private void tickSize(int tickerId, TickType tickType, int size)
        {
            //GeneralTracer.WriteLineIf(ibTickTrace.TraceInfo, "IBEvent: TickSize: tickerId: {0}, tickType: {1}, size: {2}", tickerId, tickType, size);
            TickSizeEventArgs e = new TickSizeEventArgs(tickerId, tickType, size);
            OnTickSize(e);
        }

        /// <summary>
        /// This method is called when the market in an option or its underlier moves.
        /// TWS’s option model volatilities, prices, and deltas, along with the present
        /// value of dividends expected on that option’s underlier are received.
        /// </summary>
        public event EventHandler<TickOptionComputationEventArgs> TickOptionComputation;

        /// <summary>
        /// Called internally when the receive thread receives a tick option computation event.
        /// </summary>
        /// <param name="e">Tick Option Computation Arguments</param>
        protected virtual void OnTickOptionComputation(TickOptionComputationEventArgs e)
        {
            RaiseEvent(TickOptionComputation, this, e);
        }

        private void tickOptionComputation(int tickerId, TickType tickType, double impliedVol, double delta, double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice)
        {
            TickOptionComputationEventArgs e =
                new TickOptionComputationEventArgs(tickerId, tickType, impliedVol, delta, optPrice, pvDividend, gamma, vega, theta, undPrice);
            OnTickOptionComputation(e);
        }

        /// <summary>
        /// This method is called when the market data changes. Values are updated immediately with no delay.
        /// </summary>
        public event EventHandler<TickGenericEventArgs> TickGeneric;

        /// <summary>
        /// Called internally when the receive thread receives a generic tick event.
        /// </summary>
        /// <param name="e">Tick Generic Event Arguments</param>
        protected virtual void OnTickGeneric(TickGenericEventArgs e)
        {
            RaiseEvent(TickGeneric, this, e);
        }

        private void tickGeneric(int tickerId, TickType tickType, double value)
        {
            TickGenericEventArgs e = new TickGenericEventArgs(tickerId, tickType, value);
            OnTickGeneric(e);
        }

        /// <summary>
        /// This method is called when the market data changes. Values are updated immediately with no delay.
        /// </summary>
        public event EventHandler<TickStringEventArgs> TickString;

        /// <summary>
        /// Called internally when the receive thread receives a Tick String  event.
        /// </summary>
        /// <param name="e">Tick String Event Arguments</param>
        protected virtual void OnTickString(TickStringEventArgs e)
        {
            RaiseEvent(TickString, this, e);
        }

        private void tickString(int tickerId, TickType tickType, string value)
        {
            TickStringEventArgs e = new TickStringEventArgs(tickerId, tickType, value);
            OnTickString(e);
        }

        /// <summary>
        /// This method is called when the market data changes. Values are updated immediately with no delay.
        /// </summary>
        public event EventHandler<TickEfpEventArgs> TickEfp;

        /// <summary>
        /// Called internally when the receive thread receives a tick EFP event.
        /// </summary>
        /// <param name="e">Tick Efp Arguments</param>
        protected virtual void OnTickEfp(TickEfpEventArgs e)
        {
            RaiseEvent(TickEfp, this, e);
        }

        private void tickEfp(int tickerId, TickType tickType, double basisPoints, string formattedBasisPoints,
                             double impliedFuture, int holdDays, string futureExpiry, double dividendImpact,
                             double dividendsToExpiry)
        {
            TickEfpEventArgs e =
                new TickEfpEventArgs(tickerId, tickType, basisPoints, formattedBasisPoints, impliedFuture,
                                     holdDays, futureExpiry, dividendImpact, dividendsToExpiry);
            OnTickEfp(e);
        }

        /// <summary>
        /// This methodis called whenever the status of an order changes. It is also fired after reconnecting
        /// to TWS if the client has any open orders.
        /// </summary>
        public event EventHandler<OrderStatusEventArgs> OrderStatus;

        /// <summary>
        /// Called internally when the receive thread receives an order status event.
        /// </summary>
        /// <param name="e">Order Status Event Arguments</param>
        protected virtual void OnOrderStatus(OrderStatusEventArgs e)
        {
            RaiseEvent(OrderStatus, this, e);
        }

        private void orderStatus(int orderId, OrderStatus status, int filled, int remaining, decimal avgFillPrice,
                                 int permId,
                                 int parentId, decimal lastFillPrice, int clientId, string whyHeld)
        {
            OrderStatusEventArgs e = new OrderStatusEventArgs(orderId, status, filled, remaining,
                                                              avgFillPrice, permId, parentId, lastFillPrice, clientId, whyHeld);
            OnOrderStatus(e);
        }

        /// <summary>
        /// This method is called to feed in open orders.
        /// </summary>
        public event EventHandler<OpenOrderEventArgs> OpenOrder;

        /// <summary>
        /// Called internally when the receive thread receives an open order event.
        /// </summary>
        /// <param name="e">Open Order Event Arguments</param>
        protected virtual void OnOpenOrder(OpenOrderEventArgs e)
        {
            RaiseEvent(OpenOrder, this, e);
        }

        private void openOrder(int orderId, Contract contract, Order order, OrderState orderState)
        {
            OpenOrderEventArgs e = new OpenOrderEventArgs(orderId, contract, order, orderState);
            OnOpenOrder(e);
        }

        /// <summary>
        /// This method is called only when reqAccountUpdates() method on the EClientSocket object has been called.
        /// </summary>
        public event EventHandler<UpdateAccountValueEventArgs> UpdateAccountValue;

        /// <summary>
        /// Called internally when the receive thread receives an Update Account Value event.
        /// </summary>
        /// <param name="e">Update Account Value Event Arguments</param>
        protected virtual void OnUpdateAccountValue(UpdateAccountValueEventArgs e)
        {
            RaiseEvent(UpdateAccountValue, this, e);
        }

        private void updateAccountValue(string key, string value, string currency, string accountName)
        {
            UpdateAccountValueEventArgs e = new UpdateAccountValueEventArgs(key, value, currency, accountName);
            OnUpdateAccountValue(e);
        }

        /// <summary>
        /// This method is called only when reqAccountUpdates() method on the EClientSocket object has been called.
        /// </summary>
        public event EventHandler<UpdatePortfolioEventArgs> UpdatePortfolio;

        /// <summary>
        /// Called Internally when the receive thread receives an Update Portfolio event.
        /// </summary>
        /// <param name="e">Update Portfolio Event Arguments</param>
        protected virtual void OnUpdatePortfolio(UpdatePortfolioEventArgs e)
        {
            RaiseEvent(UpdatePortfolio, this, e);
        }

        private void updatePortfolio(Contract contract, int position, decimal marketPrice, decimal marketValue,
                                     decimal averageCost, decimal unrealizedPNL, decimal realizedPNL, string accountName)
        {
            UpdatePortfolioEventArgs e =
                new UpdatePortfolioEventArgs(contract, position, marketPrice, marketValue, averageCost, unrealizedPNL,
                                             realizedPNL, accountName);
            OnUpdatePortfolio(e);
        }

        /// <summary>
        /// This method is called only when reqAccountUpdates() method on the EClientSocket object has been called.
        /// </summary>
        public event EventHandler<UpdateAccountTimeEventArgs> UpdateAccountTime;

        /// <summary>
        /// Called internally when the receive thread receives an Update Account Time event.
        /// </summary>
        /// <param name="e">Update Account Time Event Arguments</param>
        protected virtual void OnUpdateAccountTime(UpdateAccountTimeEventArgs e)
        {
            RaiseEvent(UpdateAccountTime, this, e);
        }

        private void updateAccountTime(string timeStamp)
        {
            UpdateAccountTimeEventArgs e = new UpdateAccountTimeEventArgs(timeStamp);
            OnUpdateAccountTime(e);
        }

        /// <summary>
        /// This method is called after a successful connection to TWS.
        /// </summary>
        public event EventHandler<NextValidIdEventArgs> NextValidId;

        /// <summary>
        /// Called internally when the receive thread receives a Next Valid Id event.
        /// </summary>
        /// <param name="e">Next Valid Id Event Arguments</param>
        protected virtual void OnNextValidId(NextValidIdEventArgs e)
        {
            RaiseEvent(NextValidId, this, e);
        }

        private void nextValidId(int orderId)
        {
            //GeneralTracer.WriteLineIf(ibTickTrace.TraceInfo, "IBEvent: NextValidId: orderId: {0}", orderId);
            NextValidIdEventArgs e = new NextValidIdEventArgs(orderId);
            OnNextValidId(e);
        }

        /// <summary>
        /// This event fires in response to the <see cref="RequestContractDetails"/> method.
        /// </summary>
        public event EventHandler<ContractDetailsEventArgs> ContractDetails;

        /// <summary>
        /// Called internally when the receive thread receives a contract details event.
        /// </summary>
        /// <param name="e">Contract Details Event Arguments</param>
        protected virtual void OnContractDetails(ContractDetailsEventArgs e)
        {
            RaiseEvent(ContractDetails, this, e);
        }

        private void contractDetails(int requestId, ContractDetails contractDetails)
        {
            ContractDetailsEventArgs e = new ContractDetailsEventArgs(requestId, contractDetails);
            OnContractDetails(e);
        }

        /// <summary>
        /// This event fires in response to the <see cref="RequestContractDetails"/> method called on a bond contract.
        /// </summary>
        public event EventHandler<BondContractDetailsEventArgs> BondContractDetails;

        /// <summary>
        /// Called internally when the receive thread receives a Bond Contract Details Event.
        /// </summary>
        /// <param name="e">Bond Contract Details Event Arguments</param>
        protected virtual void OnBondContractDetails(BondContractDetailsEventArgs e)
        {
            RaiseEvent(BondContractDetails, this, e);
        }

        private void bondContractDetails(int requestId, ContractDetails contractDetails)
        {
            BondContractDetailsEventArgs e = new BondContractDetailsEventArgs(requestId, contractDetails);
            OnBondContractDetails(e);
        }

        /// <summary>
        /// Called once all contract details for a given request are received.
        /// This, for example, helps to define the end of an option chain.
        /// </summary>
        public event EventHandler<ContractDetailsEndEventArgs> ContractDetailsEnd;

        /// <summary>
        /// Called internally when the receive thread receives a Contract Details End Event.
        /// </summary>
        /// <param name="e">Contract Details End Event Arguments</param>
        protected virtual void OnContractDetailsEnd(ContractDetailsEndEventArgs e)
        {
            RaiseEvent(ContractDetailsEnd, this, e);
        }

        private void contractDetailsEnd(int requestId)
        {
            ContractDetailsEndEventArgs e = new ContractDetailsEndEventArgs(requestId);
            OnContractDetailsEnd(e);
        }

        /// <summary>
        /// Called once all the open orders for a given request are received.
        /// </summary>
        public event EventHandler<EventArgs> OpenOrderEnd;

        /// <summary>
        /// Called internally when the receive thread receives a Open Orders End Event.
        /// </summary>
        /// <param name="e">Empty Event Arguments</param>
        protected virtual void OnOpenOrderEnd(EventArgs e)
        {
            RaiseEvent(OpenOrderEnd, this, e);
        }

        private void openOrderEnd()
        {
            EventArgs e = new EventArgs();
            OnOpenOrderEnd(e);
        }

        /// <summary>
        /// Called once all Account Details for a given request are received.
        /// </summary>
        public event EventHandler<AccountDownloadEndEventArgs> AccountDownloadEnd;

        /// <summary>
        /// Called internally when the receive thread receives a Account Download End Event.
        /// </summary>
        /// <param name="e">Contract Details End Event Arguments</param>
        protected virtual void OnAccountDownloadEnd(AccountDownloadEndEventArgs e)
        {
            RaiseEvent(AccountDownloadEnd, this, e);
        }

        private void accountDownloadEnd(string accountName)
        {
            AccountDownloadEndEventArgs e = new AccountDownloadEndEventArgs(accountName);
            OnAccountDownloadEnd(e);
        }

        /// <summary>
        /// Called once all contract details for a given request are received.
        /// This, for example, helps to define the end of an option chain.
        /// </summary>
        public event EventHandler<ExecutionDataEndEventArgs> ExecutionDataEnd;

        /// <summary>
        /// Called internally when the receive thread receives a Contract Details End Event.
        /// </summary>
        /// <param name="e">Contract Details End Event Arguments</param>
        protected virtual void OnExecutionDataEnd(ExecutionDataEndEventArgs e)
        {
            RaiseEvent(ExecutionDataEnd, this, e);
        }

        private void executionDataEnd(int requestId)
        {
            ExecutionDataEndEventArgs e = new ExecutionDataEndEventArgs(requestId);
            OnExecutionDataEnd(e);
        }

        /// <summary>
        /// Called once all execution data for a given request are received.
        /// </summary>
        public event EventHandler<DeltaNuetralValidationEventArgs> DeltaNuetralValidation;

        /// <summary>
        /// Called internally when the receive thread receives a Contract Details End Event.
        /// </summary>
        /// <param name="e">Contract Details End Event Arguments</param>
        protected virtual void OnDeltaNuetralValidation(DeltaNuetralValidationEventArgs e)
        {
            RaiseEvent(DeltaNuetralValidation, this, e);
        }

        private void deltaNuetralValidation(int requestId, UnderComp underComp)
        {
            DeltaNuetralValidationEventArgs e = new DeltaNuetralValidationEventArgs(requestId, underComp);
            OnDeltaNuetralValidation(e);
        }

        /// <summary>
        /// This event fires in response to the <see cref="RequestExecutions"/> method or after an order is placed.
        /// </summary>
        public event EventHandler<ExecDetailsEventArgs> ExecDetails;

        /// <summary>
        /// Called internally when the receive thread receives an execution details event.
        /// </summary>
        /// <param name="e">Execution Details Event Arguments</param>
        protected virtual void OnExecDetails(ExecDetailsEventArgs e)
        {
            if (ExecDetails != null)
            {
                ExecDetails(this, e);
            }
        }

        private void execDetails(int reqId, int orderId, Contract contract, Execution execution)
        {
            ExecDetailsEventArgs e = new ExecDetailsEventArgs(reqId, orderId, contract, execution);
            OnExecDetails(e);
        }

        /// <summary>
        /// This method is called when the market depth changes.
        /// </summary>
        public event EventHandler<UpdateMarketDepthEventArgs> UpdateMarketDepth;

        /// <summary>
        /// Called internally when the receive thread receives an update market depth event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnUpdateMarketDepth(UpdateMarketDepthEventArgs e)
        {
            RaiseEvent(UpdateMarketDepth, this, e);
        }

        private void updateMktDepth(int tickerId, int position, MarketDepthOperation operation, MarketDepthSide side,
                                    decimal price, int size)
        {
            UpdateMarketDepthEventArgs e = new UpdateMarketDepthEventArgs(tickerId, position, operation, side, price, size);
            OnUpdateMarketDepth(e);
        }

        /// <summary>
        /// This method is called when the Level II market depth changes.
        /// </summary>
        public event EventHandler<UpdateMarketDepthL2EventArgs> UpdateMarketDepthL2;

        /// <summary>
        /// Called internally when the receive thread receives an update market depth level 2 event.
        /// </summary>
        /// <param name="e">Update Market Depth L2 Event Arguments</param>
        protected virtual void OnUpdateMarketDepthL2(UpdateMarketDepthL2EventArgs e)
        {
            RaiseEvent(UpdateMarketDepthL2, this, e);
        }

        private void updateMktDepthL2(int tickerId, int position, string marketMaker, MarketDepthOperation operation,
                                      MarketDepthSide side,
                                      decimal price, int size)
        {
            UpdateMarketDepthL2EventArgs e =
                new UpdateMarketDepthL2EventArgs(tickerId, position, marketMaker, operation, side, price, size);
            OnUpdateMarketDepthL2(e);
        }
        /// <summary>
        /// This method is triggered for any exceptions caught.
        /// </summary>
        public event EventHandler<ReportExceptionEventArgs> ReportException;

        /// <summary>
        /// Called internally when a exception is being thrown
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnReportException(ReportExceptionEventArgs e) {
            RaiseEvent(ReportException, this, e);
        }

        private void exception(Exception ex)
        {
            ReportExceptionEventArgs e = new ReportExceptionEventArgs(ex);
            OnReportException(e);
        }

        /// <summary>
        /// This method is triggered for each new bulletin if the client has subscribed (i.e. by calling the reqNewsBulletins() method.
        /// </summary>
        public event EventHandler<UpdateNewsBulletinEventArgs> UpdateNewsBulletin;

        /// <summary>
        /// Called internally when the receive thread receives an update news bulletin event.
        /// </summary>
        /// <param name="e">Update News Bulletin Event Arguments</param>
        protected virtual void OnUpdateNewsBulletin(UpdateNewsBulletinEventArgs e)
        {
            RaiseEvent(UpdateNewsBulletin, this, e);
        }

        private void updateNewsBulletin(int msgId, NewsType msgType, string message, string origExchange)
        {
            UpdateNewsBulletinEventArgs e = new UpdateNewsBulletinEventArgs(msgId, msgType, message, origExchange);
            OnUpdateNewsBulletin(e);
        }

        /// <summary>
        /// This method is called when a successful connection is made to a Financial Advisor account.
        /// It is also called when the reqManagedAccts() method is invoked.
        /// </summary>
        public event EventHandler<ManagedAccountsEventArgs> ManagedAccounts;

        /// <summary>
        /// Called internally when the receive thread receives a managed accounts event.
        /// </summary>
        /// <param name="e">Managed Accounts Event Arguments</param>
        protected virtual void OnManagedAccounts(ManagedAccountsEventArgs e)
        {
            RaiseEvent(ManagedAccounts, this, e);
        }

        private void managedAccounts(string accountsList)
        {
            ManagedAccountsEventArgs e = new ManagedAccountsEventArgs(accountsList);
            OnManagedAccounts(e);
        }

        /// <summary>
        /// This method receives previously requested FA configuration information from TWS.
        /// </summary>
        public event EventHandler<ReceiveFAEventArgs> ReceiveFA;

        /// <summary>
        /// Called internally when the receive thread receives a Receive Finanvial Advisor event.
        /// </summary>
        /// <param name="e">Receive FA Event Arguments</param>
        protected virtual void OnReceiveFA(ReceiveFAEventArgs e)
        {
            RaiseEvent(ReceiveFA, this, e);
        }

        private void receiveFA(FADataType faDataType, string xml)
        {
            ReceiveFAEventArgs e = new ReceiveFAEventArgs(faDataType, xml);
            OnReceiveFA(e);
        }

        /// <summary>
        /// This method receives the requested historical data results
        /// </summary>
        public event EventHandler<HistoricalDataEventArgs> HistoricalData;

        /// <summary>
        /// Called internally when the receive thread receives a tick price event.
        /// </summary>
        /// <param name="e">Historical Data Event Arguments</param>
        protected virtual void OnHistoricalData(HistoricalDataEventArgs e)
        {
            RaiseEvent(HistoricalData, this, e);
        }

        private void historicalData(int reqId, DateTime date, decimal open, decimal high, decimal low, decimal close,
                                    int volume, int trades, double WAP, bool hasGaps, int recordNumber, int recordTotal)
        {
            HistoricalDataEventArgs e =
                new HistoricalDataEventArgs(reqId, date, open, high, low, close, volume, trades, WAP, hasGaps, recordNumber, recordTotal);
            OnHistoricalData(e);
        }

        /// <summary>
        /// This method receives an XML document that describes the valid parameters that a scanner subscription can have
        /// </summary>
        public event EventHandler<ScannerParametersEventArgs> ScannerParameters;

        /// <summary>
        /// Called internally when the receive thread receives a scanner parameters event.
        /// </summary>
        /// <param name="e">Scanner Parameters Event Arguments</param>
        protected virtual void OnScannerParameters(ScannerParametersEventArgs e)
        {
            RaiseEvent(ScannerParameters, this, e);
        }

        private void scannerParameters(string xml)
        {
            ScannerParametersEventArgs e = new ScannerParametersEventArgs(xml);
            OnScannerParameters(e);
        }

        /// <summary>
        /// This method receives the requested market scanner data results
        /// </summary>
        public event EventHandler<ScannerDataEventArgs> ScannerData;

        /// <summary>
        /// Called internally when the receive thread receives a tick price event.
        /// </summary>
        /// <param name="e">Scanner Data Event Arguments</param>
        protected virtual void OnScannerData(ScannerDataEventArgs e)
        {
            RaiseEvent(ScannerData, this, e);
        }

        private void scannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark,
                                 string projection, string legsStr)
        {
            ScannerDataEventArgs e =
                new ScannerDataEventArgs(reqId, rank, contractDetails, distance, benchmark, projection, legsStr);
            OnScannerData(e);
        }

        /// <summary>
        /// This method receives the requested market scanner data results
        /// </summary>
        public event EventHandler<ScannerDataEndEventArgs> ScannerDataEnd;

        /// <summary>
        /// Called internally when the receive thread receives a tick price event.
        /// </summary>
        /// <param name="e">Scanner Data Event Arguments</param>
        protected virtual void OnScannerDataEnd(ScannerDataEndEventArgs e)
        {
            RaiseEvent(ScannerDataEnd, this, e);
        }

        private void scannerDataEnd(int reqId)
        {
            ScannerDataEndEventArgs e =
                new ScannerDataEndEventArgs(reqId);
            OnScannerDataEnd(e);
        }


        /// <summary>
        /// This method receives the realtime bars data results.
        /// </summary>
        public event EventHandler<RealTimeBarEventArgs> RealTimeBar;

        /// <summary>
        /// Called internally when the receive thread receives a real time bar event.
        /// </summary>
        /// <param name="e">Real Time Bar Event Arguments</param>
        protected virtual void OnRealTimeBar(RealTimeBarEventArgs e)
        {
            RaiseEvent(RealTimeBar, this, e);
        }

        private void realTimeBar(int reqId, long time, decimal open, decimal high, decimal low, decimal close, long volume, double wap, int count)
        {
            RealTimeBarEventArgs e = new RealTimeBarEventArgs(reqId, time, open, high, low, close, volume, wap, count);
            OnRealTimeBar(e);
        }

        /// <summary>
        /// This method receives the current system time on the server side.
        /// </summary>
        public event EventHandler<CurrentTimeEventArgs> CurrentTime;

        /// <summary>
        /// Called internally when the receive thread receives a current time event.
        /// </summary>
        /// <param name="e">Current Time Event Arguments</param>
        protected virtual void OnCurrentTime(CurrentTimeEventArgs e)
        {
            RaiseEvent(CurrentTime, this, e);
        }

        private void currentTime(DateTime time)
        {
            CurrentTimeEventArgs e = new CurrentTimeEventArgs(time);
            OnCurrentTime(e);

        }

        /// <summary>
        /// Reuters global fundamental market data
        /// </summary>
        public event EventHandler<FundamentalDetailsEventArgs> FundamentalData;

        /// <summary>
        /// Called internally when the receive thread receives a fundamental data event.
        /// </summary>
        /// <param name="e">Fundamental Data Event Arguments</param>
        protected virtual void OnFundamentalData(FundamentalDetailsEventArgs e)
        {
            RaiseEvent(FundamentalData, this, e);
        }

        private void fundamentalData(int requestId, string data)
        {
            FundamentalDetailsEventArgs e = new FundamentalDetailsEventArgs(requestId, data);
            OnFundamentalData(e);
        }

        /// <summary>
        /// Called on a market data type call back.
        /// </summary>
        public event EventHandler<MarketDataTypeEventArgs> MarketDataType;

        /// <summary>
        /// Called internally when the receive thread receives a Market Data Type Event.
        /// </summary>
        protected virtual void OnMarketDataType(MarketDataTypeEventArgs e)
        {
            RaiseEvent(MarketDataType, this, e);
        }

        private void marketDataType(int requestId, MarketDataType dataType)
        {
            MarketDataTypeEventArgs e = new MarketDataTypeEventArgs(requestId, dataType);
            OnMarketDataType(e);
        }

        /// <summary>
        /// This event is fired when there is an error with the communication or when TWS wants to send a message to the client.
        /// </summary>
        public event EventHandler<ErrorEventArgs> Error;

        /// <summary>
        /// Called internally when the receive thread receives an error event.
        /// </summary>
        /// <param name="e">Error Event Arguments</param>
        protected virtual void OnError(ErrorEventArgs e)
        {
            RaiseEvent(Error, this, e);
        }

        private void error(int tickerId, ErrorMessage errorCode, string errorMsg)
        {
            lock (this)
            {
                GeneralTracer.WriteLineIf(ibTrace.TraceError, "IBEvent: Error: tickerId: {0}, errorCode: {1}, errorMsg: {2}", tickerId, errorCode, errorMsg);
                ErrorEventArgs e = new ErrorEventArgs(tickerId, errorCode, errorMsg);
                OnError(e);
            }
        }

        private void error(ErrorMessage errorCode, ErrorMessage errorString)
        {
            error(errorCode, errorString.ToString());
        }

        private void error(ErrorMessage errorCode, Exception e)
        {
            error(errorCode, e.ToString());
        }

        private void error(int tickerId, ErrorMessage errorCode, Exception e)
        {
            error(tickerId, errorCode, e.ToString());
        }

        private void error(ErrorMessage errorCode)
        {
            error(errorCode, "");
        }

        private void error(string tail)
        {
            error(ErrorMessage.NoValidId, tail);
        }

        private void error(int tickerId, ErrorMessage errorCode)
        {
            error(tickerId, errorCode, "");
        }

        private void error(ErrorMessage errorCode, string tail)
        {
            error((int) ErrorMessage.NoValidId, errorCode, tail);
        }

        /// <summary>
        /// This method is called when TWS closes the sockets connection, or when TWS is shut down.
        /// </summary>
        public event EventHandler<ConnectionClosedEventArgs> ConnectionClosed;

        /// <summary>
        /// Called internally when the receive thread receives a connection closed event.
        /// </summary>
        /// <param name="e">Connection Closed Event Arguments</param>
        protected virtual void OnConnectionClosed(ConnectionClosedEventArgs e)
        {
            RaiseEvent(ConnectionClosed, this, e);
        }

        private void connectionClosed()
        {
            ConnectionClosedEventArgs e = new ConnectionClosedEventArgs();
            OnConnectionClosed(e);
        }

        /// <summary>
        /// Called once the tick snap shot is complete.
        /// </summary>
        public event EventHandler<TickSnapshotEndEventArgs> TickSnapshotEnd;

        /// <summary>
        /// Called internally when the receive thread receives a Tick Snapshot End Event.
        /// </summary>
        /// <param name="e">Contract Details End Event Arguments</param>
        protected virtual void OnTickSnapshotEnd(TickSnapshotEndEventArgs e)
        {
            RaiseEvent(TickSnapshotEnd, this, e);
        }

        private void tickSnapshotEnd(int requestId)
        {
            TickSnapshotEndEventArgs e = new TickSnapshotEndEventArgs(requestId);
            OnTickSnapshotEnd(e);
        }

        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public IBClient()
        {
            readThread = new Thread(Run);
            readThread.IsBackground = true;
            readThread.Name = "IBClient Read Thread";
        }

        /// <summary>
        /// Dispose() calls Dispose(true)
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool throwExceptions = false;
        /// <summary>
        /// Used to control the exception handling.
        /// If true, all exceptions are thrown, else only throw non network exceptions.
        /// </summary>
        public bool ThrowExceptions
        {
            get { return throwExceptions; }
            set { throwExceptions = value; }
        }

        /// <summary>
        /// The bulk of the clean-up code is implemented in Dispose(bool)
        /// </summary>
        /// <param name="disposing">Allows the ondispose method to override the dispose action.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                close();
                if (dos != null)
                {
                    dos.Close();
                    dos = null;
                }
                if (dis != null)
                {
                    dis.Close();
                    dis = null;
                }
            }
        }

        #endregion

        #region IBClientSocket

        #region Values

        private const int clientVersion = 53;
        private const int minimumServerVersion = 38;

        #endregion

        #region Properties

        /// <summary>
        /// Returns the status of the connection to TWS.
        /// </summary>
        public bool Connected
        {
            get { return connected; }
        }

        /// <summary>
        /// Returns the version of the TWS instance the API application is connected to
        /// </summary>
        public int ServerVersion
        {
            get { return serverVersion; }
        }

        /// <summary>
        /// Returns the client version of the TWS API
        /// </summary>
        public static int ClientVersion
        {
            get { return clientVersion; }
        }

        /// <summary>
        /// Returns the time the API application made a connection to TWS
        /// </summary>
        public String TwsConnectionTime
        {
            get { return twsTime; }
        }

        #endregion

        #region Private Variables

        private static readonly byte[] EOL = new byte[] {0};
        private bool connected; // true if we are connected
        private BinaryWriter dos; // the ibSocket output stream
        private TcpClient ibSocket; // the ibSocket
        private int serverVersion = 0;
        private String twsTime;

        #endregion

        #region General Methods

        private String checkConnected(String host)
        {
            if (connected)
            {
                error(ErrorMessage.ConnectFail, ErrorMessage.AlreadyConnected);
                return null;
            }
            if (host == null || host.Length < 1)
            {
                host = "127.0.0.1";
            }
            return host;
        }

        private void connect(TcpClient socket, int clientId)
        {
            if (socket == null)
                throw new ArgumentNullException("socket");
            lock (this)
            {
                ibSocket = socket;

                // create io streams
                dis = new BinaryReader(ibSocket.GetStream());
                dos = new BinaryWriter(ibSocket.GetStream());

                // set client version
                send(clientVersion);

                // start Reader thread

                // check server version
                serverVersion = ReadInt();
                GeneralTracer.WriteLineIf(ibTrace.TraceInfo, "IBMethod: Connect: Server Version: {0}", serverVersion);
                if (serverVersion >= 20)
                {
                    twsTime = ReadStr();
                    GeneralTracer.WriteLineIf(ibTrace.TraceInfo, "IBMethod: Connect: TWS Time at connection: {0}", twsTime);
                    //Let's fire the servertime event
                }
                if (serverVersion < minimumServerVersion)
                {
                    error(ErrorMessage.UpdateTws,
                          "Server version " + serverVersion + " is lower than required version " + minimumServerVersion +
                          ".");
                    return;
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

        private void close()
        {
            Disconnect();
            connectionClosed();
        }

        #endregion

        #region Network Commmands

        /// <summary>
        /// This function must be called before any other. There is no feedback for a successful connection, but a subsequent attempt to connect will return the message "Already connected."
        /// </summary>
        /// <param name="host">host name or IP address of the machine where TWS is running. Leave blank to connect to the local host.</param>
        /// <param name="port">must match the port specified in TWS on the Configure>API>Socket Port field.</param>
        /// <param name="clientId">A number used to identify this client connection. All orders placed/modified from this client will be associated with this client identifier.
        /// Each client MUST connect with a unique clientId.</param>
        public void Connect(String host, int port, int clientId)
        {
            if (host == null)
                throw new ArgumentNullException("host");
            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort)
                throw new ArgumentOutOfRangeException("port");
            lock (this)
            {
                // already connected?
                host = checkConnected(host);
                if (host == null)
                {
                    return;
                }
                TcpClient socket = new TcpClient(host, port);
                connect(socket, clientId);
            }
        }

        /// <summary>
        /// Call this method to terminate the connections with TWS. Calling this method does not cancel orders that have already been sent.
        /// </summary>
        public void Disconnect()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    return;
                }

                try
                {
                    // stop Reader thread
                    Stop();
                    readThread.Abort();

                    // close ibSocket
                    if (ibSocket != null)
                    {
                        ibSocket.Close();
                    }
                }
                catch
                {
                }
                connected = false;
            }
        }

        /// <summary>
        /// Call the cancelScannerSubscription() method to stop receiving market scanner results. 
        /// </summary>
        /// <param name="tickerId">the Id that was specified in the call to reqScannerSubscription().</param>
        public void CancelScannerSubscription(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                if (serverVersion < 24)
                {
                    error(ErrorMessage.UpdateTws, "It does not support API scanner subscription.");
                    return;
                }

                int version = 1;

                // send cancel mkt data msg
                try
                {
                    send((int) OutgoingMessage.CancelScannerSubscription);
                    send(version);
                    send(tickerId);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(tickerId, ErrorMessage.FailSendCancelScanner, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call the reqScannerParameters() method to receive an XML document that describes the valid parameters that a scanner subscription can have.
        /// </summary>
        public void RequestScannerParameters()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                if (serverVersion < 24)
                {
                    error(ErrorMessage.UpdateTws, "It does not support API scanner subscription.");
                    return;
                }

                int version = 1;

                try
                {
                    send((int) OutgoingMessage.RequestScannerParameters);
                    send(version);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) | throwExceptions)
                        throw;
                    error(ErrorMessage.FailSendRequestScannerParameters, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call the reqScannerSubscription() method to start receiving market scanner results through the scannerData() EWrapper method. 
        /// </summary>
        /// <param name="tickerId">the Id for the subscription. Must be a unique value. When the subscription  data is received, it will be identified by this Id. This is also used when canceling the scanner.</param>
        /// <param name="subscription">summary of the scanner subscription parameters including filters.</param>
        public void RequestScannerSubscription(int tickerId, ScannerSubscription subscription)
        {
            if (subscription == null)
                throw new ArgumentNullException("subscription");
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                if (serverVersion < 24)
                {
                    error(ErrorMessage.UpdateTws, "It does not support API scanner subscription.");
                    return;
                }

                int version = 3;

                try
                {
                    send((int) OutgoingMessage.RequestScannerSubscription);
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
                    send(subscription.SPRatingAbove);
                    send(subscription.SPRatingBelow);
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
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;
                    error(tickerId, ErrorMessage.FailSendRequestScanner, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to request market data. The market data will be returned by the tickPrice, tickSize, tickOptionComputation(), tickGeneric(), tickString() and tickEFP() methods.
        /// </summary>
        /// <param name="tickerId">the ticker id. Must be a unique value. When the market data returns, it will be identified by this tag. This is also used when canceling the market data.</param>
        /// <param name="contract">this structure contains a description of the contract for which market data is being requested.</param>
        /// <param name="genericTickList">comma delimited list of generic tick types.  Tick types can be found here: (new Generic Tick Types page) </param>
        /// <param name="snapshot">Allows client to request snapshot market data.</param>
        /// <param name="marketDataOff">Market Data Off - used in conjunction with RTVolume Generic tick type causes only volume data to be sent.</param>
        public void RequestMarketData(int tickerId, Contract contract, Collection<GenericTickType> genericTickList, bool snapshot, bool marketDataOff)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                //35 is the minimum version for snapshots
                if (serverVersion < MinServerVersion.ScaleOrders && snapshot)
                {
                    error(tickerId, ErrorMessage.UpdateTws, "It does not support snapshot market data requests.");
                    return;
                }

                //40 is the minimum version for the Underlying Component class
                if (serverVersion < MinServerVersion.UnderComp)
                {
                    if (contract.UnderlyingComponent != null)
                    {
                        error(tickerId, ErrorMessage.UpdateTws, "It does not support delta-neutral orders.");
                        return;
                    }
                }

                //46 is the minimum version for requesting contracts by conid
                if (serverVersion < MinServerVersion.RequestMarketDataConId)
                {
                    if (contract.ContractId > 0)
                    {
                        error(tickerId, ErrorMessage.UpdateTws, "It does not support conId parameter.");
                        return;
                    }
                }

                int version = 9;

                try
                {
                    // send req mkt data msg
                    send((int) OutgoingMessage.RequestMarketData);
                    send(version);
                    send(tickerId);
                    if (serverVersion >= 47)
                        send(contract.ContractId);

                    //Send Contract Fields
                    send(contract.Symbol);
                    send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(((contract.Right == RightType.Undefined)
                              ? ""
                              : EnumDescConverter.GetEnumDescription(contract.Right)));
                    if (serverVersion >= 15)
                    {
                        send(contract.Multiplier);
                    }
                    send(contract.Exchange);
                    if (serverVersion >= 14)
                    {
                        send(contract.PrimaryExchange);
                    }
                    send(contract.Currency);
                    if (serverVersion >= 2)
                    {
                        send(contract.LocalSymbol);
                    }
                    if (serverVersion >= 8 && contract.SecurityType == SecurityType.Bag)
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
                                send(EnumDescConverter.GetEnumDescription(comboLeg.Action));
                                send(comboLeg.Exchange);
                            }
                        }
                    }

                    if (serverVersion >= 40)
                    {
                        if (contract.UnderlyingComponent != null)
                        {
                            UnderlyingComponent underComp = contract.UnderlyingComponent;
                            send(true);
                            send(underComp.ContractId);
                            send(underComp.Delta);
                            send(underComp.Price);
                        }
                        else
                        {
                            send(false);
                        }
                    }

                    if (serverVersion >= 31)
                    {
                        /*
                         * Even though SHORTABLE tick type supported only
                         * starting server version 33 it would be relatively
                         * expensive to expose this restriction here.
                         * 
                         * Therefore we are relying on TWS doing validation.
                         */

                        StringBuilder genList = new StringBuilder();
                        if (genericTickList != null)
                        {
                            for (int counter = 0; counter < genericTickList.Count; counter++)
                                genList.AppendFormat("{0},",
                                                     ((int) genericTickList[counter]).ToString(
                                                         CultureInfo.InvariantCulture));
                        }

                        if (marketDataOff)
                            genList.AppendFormat("mdoff");

                        send(genList.ToString().Trim(','));
                    }
                    //35 is the minum version for SnapShot
                    if (serverVersion >= 35)
                    {
                        send(snapshot);
                    }
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;
                    error(tickerId, ErrorMessage.FailSendRequestMarket, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call the CancelHistoricalData method to stop receiving historical data results.
        /// </summary>
        /// <param name="tickerId">the Id that was specified in the call to <see cref="RequestHistoricalData(int,Contract,DateTime,TimeSpan,BarSize,HistoricalDataType,int)"/>.</param>
        public void CancelHistoricalData(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                if (serverVersion < 24)
                {
                    error(ErrorMessage.UpdateTws, "It does not support historical data query cancellation.");
                    return;
                }

                int version = 1;

                // send cancel mkt data msg
                try
                {
                    send((int) OutgoingMessage.CancelHistoricalData);
                    send(version);
                    send(tickerId);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(tickerId, ErrorMessage.FailSendCancelHistoricalData, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call the CancelRealTimeBars() method to stop receiving real time bar results. 
        /// </summary>
        /// <param name="tickerId">The Id that was specified in the call to <see cref="RequestRealTimeBars"/>.</param>
        public void CancelRealTimeBars(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                //34 is the minimum server version for real time bars
                if (serverVersion < MinServerVersion.RealTimeBars)
                {
                    error(ErrorMessage.UpdateTws, "It does not support realtime bar data query cancellation.");
                    return;
                }

                int version = 1;

                // send cancel mkt data msg
                try
                {
                    send((int)OutgoingMessage.CancelRealTimeBars);
                    send(version);
                    send(tickerId);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) | throwExceptions)
                        throw;

                    error(tickerId, ErrorMessage.FailSendCancelRealTimeBars, e);
                    close();
                }
            }
        }
        
        /// <summary>
        /// Call the reqHistoricalData() method to start receiving historical data results through the historicalData() EWrapper method. 
        /// </summary>
        /// <param name="tickerId">the Id for the request. Must be a unique value. When the data is received, it will be identified by this Id. This is also used when canceling the historical data request.</param>
        /// <param name="contract">this structure contains a description of the contract for which market data is being requested.</param>
        /// <param name="endDateTime">Date is sent after a .ToUniversalTime, so make sure the kind property is set correctly, and assumes GMT timezone. Use the format yyyymmdd hh:mm:ss tmz, where the time zone is allowed (optionally) after a space at the end.</param>
        /// <param name="duration">This is the time span the request will cover, and is specified using the format:
        /// <integer /> <unit />, i.e., 1 D, where valid units are:
        /// S (seconds)
        /// D (days)
        /// W (weeks)
        /// M (months)
        /// Y (years)
        /// If no unit is specified, seconds are used. "years" is currently limited to one.
        /// </param>
        /// <param name="barSizeSetting">
        /// specifies the size of the bars that will be returned (within IB/TWS limits). Valid values include:
        /// <list type="table">
        /// <listheader>
        ///     <term>Bar Size</term>
        ///     <description>Parametric Value</description>
        /// </listheader>
        /// <item>
        ///     <term>1 sec</term>
        ///     <description>1</description>
        /// </item>
        /// <item>
        ///     <term>5 secs</term>
        ///     <description>2</description>
        /// </item>
        /// <item>
        ///     <term>15 secs</term>
        ///     <description>3</description>
        /// </item>
        /// <item>
        ///     <term>30 secs</term>
        ///     <description>4</description>
        /// </item>
        /// <item>
        ///     <term>1 min</term>
        ///     <description>5</description>
        /// </item>
        /// <item>
        ///     <term>2 mins</term>
        ///     <description>6</description>
        /// </item>
        /// <item>
        ///     <term>5 mins</term>
        ///     <description>7</description>
        /// </item>
        /// <item>
        ///     <term>15 mins</term>
        ///     <description>8</description>
        /// </item>
        /// <item>
        ///     <term>30 mins</term>
        ///     <description>9</description>
        /// </item>
        /// <item>
        ///     <term>1 hour</term>
        ///     <description>10</description>
        /// </item>
        /// <item>
        ///     <term>1 day</term>
        ///     <description>11</description>
        /// </item>
        /// <item>
        ///     <term>1 week</term>
        ///     <description></description>
        /// </item>
        /// <item>
        ///     <term>1 month</term>
        ///     <description></description>
        /// </item>
        /// <item>
        ///     <term>3 months</term>
        ///     <description></description>
        /// </item>
        /// <item>
        ///     <term>1 year</term>
        ///     <description></description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="whatToShow">determines the nature of data being extracted. Valid values include:
        /// TRADES
        /// MIDPOINT
        /// BID
        /// ASK
        /// BID/ASK
        /// </param>
        /// <param name="useRth">
        /// determines whether to return all data available during the requested time span, or only data that falls within regular trading hours. Valid values include:
        /// 0 - all data is returned even where the market in question was outside of its regular trading hours.
        /// 1 - only data within the regular trading hours is returned, even if the requested time span falls partially or completely outside of the RTH.
        /// </param>
        public void RequestHistoricalData(int tickerId, Contract contract, DateTime endDateTime, TimeSpan duration,
                                      BarSize barSizeSetting, HistoricalDataType whatToShow, int useRth)
        {
            DateTime beginDateTime = endDateTime.Subtract(duration);

            string dur = ConvertPeriodtoIb(beginDateTime, endDateTime);
            RequestHistoricalData(tickerId, contract, endDateTime, dur, barSizeSetting, whatToShow, useRth);
        }

        /// <summary>
        /// used for reqHistoricalData
        /// </summary>
        protected static string ConvertPeriodtoIb(DateTime StartTime, DateTime EndTime)
        {
            TimeSpan period = EndTime.Subtract(StartTime);
            double secs = period.TotalSeconds;
            long unit;

            if (secs < 1)
                throw new ArgumentOutOfRangeException("Period cannot be less than 1 second.");
            if (secs < 86400)
            {
                unit = (long) Math.Ceiling(secs);
                return string.Concat(unit, " S");
            }
            double days = secs/86400;

            unit = (long) Math.Ceiling(days);
            if (unit <= 34)
                return string.Concat(unit, " D");
            double weeks = days/7;
            unit = (long) Math.Ceiling(weeks);
            if (unit > 52)
                throw new ArgumentOutOfRangeException("Period cannot be bigger than 52 weeks.");
            return string.Concat(unit, " W");
        }

        /// <summary>
        /// Call the reqHistoricalData() method to start receiving historical data results through the historicalData() EWrapper method. 
        /// </summary>
        /// <param name="tickerId">the Id for the request. Must be a unique value. When the data is received, it will be identified by this Id. This is also used when canceling the historical data request.</param>
        /// <param name="contract">this structure contains a description of the contract for which market data is being requested.</param>
        /// <param name="endDateTime">Date is sent after a .ToUniversalTime, so make sure the kind property is set correctly, and assumes GMT timezone. Use the format yyyymmdd hh:mm:ss tmz, where the time zone is allowed (optionally) after a space at the end.</param>
        /// <param name="duration">This is the time span the request will cover, and is specified using the format:
        /// <integer /> <unit />, i.e., 1 D, where valid units are:
        /// S (seconds)
        /// D (days)
        /// W (weeks)
        /// M (months)
        /// Y (years)
        /// If no unit is specified, seconds are used. "years" is currently limited to one.
        /// </param>
        /// <param name="barSizeSetting">
        /// specifies the size of the bars that will be returned (within IB/TWS limits). Valid values include:
        /// <list type="table">
        /// <listheader>
        ///     <term>Bar Size</term>
        ///     <description>Parametric Value</description>
        /// </listheader>
        /// <item>
        ///     <term>1 sec</term>
        ///     <description>1</description>
        /// </item>
        /// <item>
        ///     <term>5 secs</term>
        ///     <description>2</description>
        /// </item>
        /// <item>
        ///     <term>15 secs</term>
        ///     <description>3</description>
        /// </item>
        /// <item>
        ///     <term>30 secs</term>
        ///     <description>4</description>
        /// </item>
        /// <item>
        ///     <term>1 min</term>
        ///     <description>5</description>
        /// </item>
        /// <item>
        ///     <term>2 mins</term>
        ///     <description>6</description>
        /// </item>
        /// <item>
        ///     <term>5 mins</term>
        ///     <description>7</description>
        /// </item>
        /// <item>
        ///     <term>15 mins</term>
        ///     <description>8</description>
        /// </item>
        /// <item>
        ///     <term>30 mins</term>
        ///     <description>9</description>
        /// </item>
        /// <item>
        ///     <term>1 hour</term>
        ///     <description>10</description>
        /// </item>
        /// <item>
        ///     <term>1 day</term>
        ///     <description>11</description>
        /// </item>
        /// <item>
        ///     <term>1 week</term>
        ///     <description></description>
        /// </item>
        /// <item>
        ///     <term>1 month</term>
        ///     <description></description>
        /// </item>
        /// <item>
        ///     <term>3 months</term>
        ///     <description></description>
        /// </item>
        /// <item>
        ///     <term>1 year</term>
        ///     <description></description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="whatToShow">determines the nature of data being extracted. Valid values include:
        /// TRADES
        /// MIDPOINT
        /// BID
        /// ASK
        /// BID/ASK
        /// </param>
        /// <param name="useRth">
        /// determines whether to return all data available during the requested time span, or only data that falls within regular trading hours. Valid values include:
        /// 0 - all data is returned even where the market in question was outside of its regular trading hours.
        /// 1 - only data within the regular trading hours is returned, even if the requested time span falls partially or completely outside of the RTH.
        /// </param>
        public void RequestHistoricalData(int tickerId, Contract contract, DateTime endDateTime, string duration,
                                      BarSize barSizeSetting, HistoricalDataType whatToShow, int useRth)
        {
            if (contract == null)
                throw new ArgumentNullException("contract");
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(tickerId, ErrorMessage.NotConnected);
                    return;
                }

                int version = 4;

                try
                {
                    if (serverVersion < 16)
                    {
                        error(ErrorMessage.UpdateTws, "It does not support historical data backfill.");
                        return;
                    }

                    send((int)OutgoingMessage.RequestHistoricalData);
                    send(version);
                    send(tickerId);

                    //Send Contract Fields
                    send(contract.Symbol);
                    send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(((contract.Right == RightType.Undefined)
                              ? ""
                              : EnumDescConverter.GetEnumDescription(contract.Right)));
                    send(contract.Multiplier);
                    send(contract.Exchange);
                    send(contract.PrimaryExchange);
                    send(contract.Currency);
                    send(contract.LocalSymbol);
                    if (serverVersion >= 31)
                    {
                        send(contract.IncludeExpired ? 1 : 0);
                    }
                    if (serverVersion >= 20)
                    {
                        //yyyymmdd hh:mm:ss tmz
                        send(endDateTime.ToUniversalTime().ToString("yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture) + " GMT");
                        send(EnumDescConverter.GetEnumDescription(barSizeSetting));
                    }
                    send(duration);
                    send(useRth);
                    send(EnumDescConverter.GetEnumDescription(whatToShow));
                    if (serverVersion > 16)
                    {
                        //Send date times as seconds since 1970
                        send(2);
                    }
                    if (contract.SecurityType == SecurityType.Bag)
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
                                comboLeg = (ComboLeg)contract.ComboLegs[i];
                                send(comboLeg.ConId);
                                send(comboLeg.Ratio);
                                send(EnumDescConverter.GetEnumDescription(comboLeg.Action));
                                send(comboLeg.Exchange);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;
                    error(tickerId, ErrorMessage.FailSendRequestHistoricalData, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this function to download all details for a particular underlying. the contract details will be received via the contractDetails() function on the EWrapper.
        /// </summary>
        /// <param name="requestId">Request Id for Contract Details</param>
        /// <param name="contract">summary description of the contract being looked up.</param>
        public void RequestContractDetails(int requestId, Contract contract)
        {
            if (contract == null)
                throw new ArgumentNullException("contract");
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                // This feature is only available for versions of TWS >=4
                if (serverVersion < 4)
                {
                    error(ErrorMessage.UpdateTws, "Does not support Request Contract Details.");
                    return;
                }

                if (serverVersion < MinServerVersion.SecIdType)
                {
                    if (contract.SecIdType != SecurityIdType.None || !string.IsNullOrEmpty(contract.SecId))
                    {
                        error(ErrorMessage.UpdateTws, "It does not support secIdType and secId parameters.");
                        return;
                    }
                }

                const int version = 6;

                try
                {
                    // send req mkt data msg
                    send((int) OutgoingMessage.RequestContractData);
                    send(version);

                    //MIN_SERVER_VER_CONTRACT_DATA_CHAIN = 40
                    if (serverVersion >= 40)
                    {
                        send(requestId);
                    }

                    if(serverVersion >= 37)
                    {
                        send(contract.ContractId);
                    }

                    send(contract.Symbol);
                    send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(EnumDescConverter.GetEnumDescription(contract.Right));
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

                    if (serverVersion >= 45)
                    {
                        send(EnumDescConverter.GetEnumDescription(contract.SecIdType));
                        send(contract.SecId);
                    }
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;
                    error(ErrorMessage.FailSendRequestContract, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call the reqRealTimeBars() method to start receiving real time bar results through the realtimeBar() EWrapper method.
        /// </summary>
        /// <param name="tickerId">The Id for the request. Must be a unique value. When the data is received, it will be identified
        /// by this Id. This is also used when canceling the historical data request.</param>
        /// <param name="contract">This structure contains a description of the contract for which historical data is being requested.</param>
        /// <param name="barSize">Currently only 5 second bars are supported, if any other value is used, an exception will be thrown.</param>
        /// <param name="whatToShow">Determines the nature of the data extracted. Valid values include:
        /// TRADES
        /// BID
        /// ASK
        /// MIDPOINT
        /// </param>
        /// <param name="useRth">useRth – Regular Trading Hours only. Valid values include:
        /// 0 = all data available during the time span requested is returned, including time intervals when the market in question was outside of regular trading hours.
        /// 1 = only data within the regular trading hours for the product requested is returned, even if the time time span falls partially or completely outside.
        /// </param>
        public void RequestRealTimeBars(int tickerId, Contract contract, int barSize, RealTimeBarType whatToShow, bool useRth)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(tickerId, ErrorMessage.NotConnected);
                    return;
                }
                //34 is the minimum version for real time bars
                if (serverVersion < MinServerVersion.RealTimeBars)
                {
                    error(ErrorMessage.UpdateTws, "It does not support real time bars.");
                    return;
                }

                int version = 1;

                try
                {
                    // send req mkt data msg
                    send((int)OutgoingMessage.RequestRealTimeBars);
                    send(version);
                    send(tickerId);

                    //Send Contract Fields
                    send(contract.Symbol);
                    send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(EnumDescConverter.GetEnumDescription(contract.Right));
                    send(contract.Multiplier);
                    send(contract.Exchange);
                    send(contract.PrimaryExchange);
                    send(contract.Currency);
                    send(contract.LocalSymbol);
                    send(barSize);
                    send(EnumDescConverter.GetEnumDescription(whatToShow));
                    send(useRth);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;
                    error(tickerId, ErrorMessage.FailSendRequestRealTimeBars, e);
                    close();
                }
            }
        }
        

        /// <summary>
        /// Call this method to request market depth for a specific contract. The market depth will be returned by the updateMktDepth() and updateMktDepthL2() methods.
        /// </summary>
        /// <param name="tickerId">the ticker Id. Must be a unique value. When the market depth data returns, it will be identified by this tag. This is also used when canceling the market depth.</param>
        /// <param name="contract">this structure contains a description of the contract for which market depth data is being requested.</param>
        /// <param name="numberOfRows">specifies the number of market depth rows to return.</param>
        public void RequestMarketDepth(int tickerId, Contract contract, int numberOfRows)
        {
            if (contract == null)
                throw new ArgumentNullException("contract");
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                // This feature is only available for versions of TWS >=6
                if (serverVersion < 6)
                {
                    error(ErrorMessage.UpdateTws, "It does not support market depth.");
                    return;
                }

                int version = 3;

                try
                {
                    // send req mkt data msg
                    send((int) OutgoingMessage.RequestMarketDepth);
                    send(version);
                    send(tickerId);

                    //Request Contract Fields
                    send(contract.Symbol);
                    send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(((contract.Right == RightType.Undefined)
                              ? ""
                              : EnumDescConverter.GetEnumDescription(contract.Right)));
                    if (serverVersion >= 15)
                    {
                        send(contract.Multiplier);
                    }
                    send(contract.Exchange);
                    send(contract.Currency);
                    send(contract.LocalSymbol);
                    if (serverVersion >= 19)
                    {
                        send(numberOfRows);
                    }
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(tickerId, ErrorMessage.FailSendRequestMarketDepth, e);
                    close();
                }
            }
        }

        /// <summary>
        /// After calling this method, market data for the specified Id will stop flowing.
        /// </summary>
        /// <param name="tickerId">the Id that was specified in the call to reqMktData().</param>
        public void CancelMarketData(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                int version = 1;

                // send cancel mkt data msg
                try
                {
                    send((int) OutgoingMessage.CancelMarketData);
                    send(version);
                    send(tickerId);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(tickerId, ErrorMessage.FailSendCancelMarket, e);
                    close();
                }
            }
        }

        /// <summary>
        /// After calling this method, market depth data for the specified Id will stop flowing.
        /// </summary>
        /// <param name="tickerId">the Id that was specified in the call to reqMktDepth().</param>
        public void CancelMarketDepth(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                // This feature is only available for versions of TWS >=6
                if (serverVersion < 6)
                {
                    error(ErrorMessage.UpdateTws, "It does not support canceling market depth.");
                    return;
                }

                int version = 1;

                // send cancel mkt data msg
                try
                {
                    send((int) OutgoingMessage.CancelMarketDepth);
                    send(version);
                    send(tickerId);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(tickerId, ErrorMessage.FailSendCancelMarketDepth, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call the exerciseOptions() method to exercise options. 
        /// “SMART” is not an allowed exchange in exerciseOptions() calls, and that TWS does a moneyness request for the position in question whenever any API initiated exercise or lapse is attempted.
        /// </summary>
        /// <param name="tickerId">the Id for the exercise request.</param>
        /// <param name="contract">this structure contains a description of the contract to be exercised.  If no multiplier is specified, a default of 100 is assumed.</param>
        /// <param name="exerciseAction">this can have two values:
        /// 1 = specifies exercise
        /// 2 = specifies lapse
        /// </param>
        /// <param name="exerciseQuantity">the number of contracts to be exercised</param>
        /// <param name="account">specifies whether your setting will override the system's natural action. For example, if your action is "exercise" and the option is not in-the-money, by natural action the option would not exercise. If you have override set to "yes" the natural action would be overridden and the out-of-the money option would be exercised. Values are: 
        /// 0 = no
        /// 1 = yes
        /// </param>
        /// <param name="overrideRenamed">
        /// specifies whether your setting will override the system's natural action. For example, if your action is "exercise" and the option is not in-the-money, by natural action the option would not exercise. If you have override set to "yes" the natural action would be overridden and the out-of-the money option would be exercised. Values are: 
        /// 0 = no
        /// 1 = yes
        /// </param>
        public void ExerciseOptions(int tickerId, Contract contract, int exerciseAction, int exerciseQuantity,
                                    String account, int overrideRenamed)
        {
            if (contract == null)
                throw new ArgumentNullException("contract");
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(tickerId, ErrorMessage.NotConnected);
                    return;
                }

                int version = 1;

                try
                {
                    if (serverVersion < 21)
                    {
                        error(ErrorMessage.UpdateTws, "It does not support options exercise from the API.");
                        return;
                    }

                    send((int) OutgoingMessage.ExerciseOptions);
                    send(version);
                    send(tickerId);
                    //Send Contract Fields
                    send(contract.Symbol);
                    send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(((contract.Right == RightType.Undefined)
                              ? ""
                              : EnumDescConverter.GetEnumDescription(contract.Right)));
                    send(contract.Multiplier);
                    send(contract.Exchange);
                    send(contract.Currency);
                    send(contract.LocalSymbol);
                    send(exerciseAction);
                    send(exerciseQuantity);
                    send(account);
                    send(overrideRenamed);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(tickerId, ErrorMessage.FailSendRequestMarket, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to place an order. The order status will be returned by the orderStatus event.
        /// </summary>
        /// <param name="orderId">the order Id. You must specify a unique value. When the order status returns, it will be identified by this tag. This tag is also used when canceling the order.</param>
        /// <param name="contract">this structure contains a description of the contract which is being traded.</param>
        /// <param name="order">this structure contains the details of the order.
        /// Each client MUST connect with a unique clientId.</param>
        public void PlaceOrder(int orderId, Contract contract, Order order)
        {
            if (contract == null)
                throw new ArgumentNullException("contract");
            if (order == null)
                throw new ArgumentNullException("order");
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(orderId, ErrorMessage.NotConnected);
                    return;
                }

                //Scale Orders Minimum Version is 35
                if (serverVersion < MinServerVersion.ScaleOrders)
                {
                    if (order.ScaleInitLevelSize != Int32.MaxValue || order.ScalePriceIncrement != Int32.MaxValue || order.ScalePriceIncrement != decimal.MaxValue)
                    {
                        error(orderId, ErrorMessage.UpdateTws, "It does not support Scale orders.");
                        return;
                    }
                }

                //Minimum Sell Short Combo Leg Order is 35
                if (serverVersion < MinServerVersion.SshortComboLegs)
                {
                    if (!(contract.ComboLegs.Count == 0))
                    {
                        ComboLeg comboLeg;
                        for (int i = 0; i < contract.ComboLegs.Count; ++i)
                        {
                            comboLeg = (ComboLeg)contract.ComboLegs[i];
                            if (comboLeg.ShortSaleSlot != 0 || (!string.IsNullOrEmpty(comboLeg.DesignatedLocation)))
                            {
                                error(orderId, ErrorMessage.UpdateTws, "It does not support SSHORT flag for combo legs.");
                                return;
                            }
                        }
                    }
                }

                if (serverVersion < MinServerVersion.WhatIfOrders)
                {
                    if(order.WhatIf)
                    {
                        error(orderId, ErrorMessage.UpdateTws, "It does not support what if orders.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.FundamentalData)
                {
                    if (contract.UnderlyingComponent != null)
                    {
                        error(orderId, ErrorMessage.UpdateTws, "It does not support delta-neutral orders.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.ScaleOrders2)
                {
                    if (order.ScaleSubsLevelSize != System.Int32.MaxValue)
                    {
                        error(orderId, ErrorMessage.UpdateTws, "It does not support Subsequent Level Size for Scale orders.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.AlgoOrders)
                {
                    if (!string.IsNullOrEmpty(order.AlgoStrategy))
                    {
                        error(orderId, ErrorMessage.UpdateTws, "It does not support algo orders.");
                        return;
                    }
                }


                if (serverVersion < MinServerVersion.NotHeld)
                {
                    if (order.NotHeld)
                    {
                        error(ErrorMessage.UpdateTws, "It does not support notHeld parameter.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.SecIdType)
                {
                    if (contract.SecIdType != SecurityIdType.None || !string.IsNullOrEmpty(contract.SecId))
                    {
                        error(ErrorMessage.UpdateTws, "It does not support secIdType and secId parameters.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.PlaceOrderConId)
                {
                    if (contract.ContractId > 0)
                    {
                        error(ErrorMessage.UpdateTws, "It does not support conId parameter.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.Sshortx)
                {
                    if (order.ExemptCode != -1)
                    {
                        error(ErrorMessage.UpdateTws, "It does not support exemptCode parameter.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.Sshortx)
                {
                    if (contract.ComboLegs.Count > 0)
                    {
                        foreach(var comboLeg in contract.ComboLegs)
                        {
                            if (comboLeg.ExemptCode != -1)
                            {
                                error(ErrorMessage.UpdateTws, "It does not support exemptCode parameter.");
                                return;
                            }
                        }
                    }
                }

                if (serverVersion < MinServerVersion.HedgeOrders)
                {
                    if (!string.IsNullOrEmpty(order.HedgeType))
                    {
                        error(ErrorMessage.UpdateTws, "It does not support hedge orders.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.OptOutSmartRouting)
                {
                    if (order.OptOutSmartRouting)
                    {
                        error(ErrorMessage.UpdateTws, "It does not support optOutSmartRouting parameter.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.DeltaNeutralConId)
                {
                    if (order.DeltaNeutralConId > 0
                            || !string.IsNullOrEmpty(order.DeltaNeutralSettlingFirm)
                            || !string.IsNullOrEmpty(order.DeltaNeutralClearingAccount)
                            || !string.IsNullOrEmpty(order.DeltaNeutralClearingIntent)
                            )
                    {
                        error(ErrorMessage.UpdateTws, "It does not support deltaNeutral parameters: ConId, SettlingFirm, ClearingAccount, ClearingIntent");
                        return;
                    }
                }

                int version = (serverVersion < MinServerVersion.NotHeld) ? 27 : 35;

                // send place order msg
                try
                {
                    send((int) OutgoingMessage.PlaceOrder);
                    send(version);
                    send(orderId);

                    // send contract fields
                    if (serverVersion >= 46)
                        send(contract.ContractId);
                    send(contract.Symbol);
                    send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(((contract.Right == RightType.Undefined)
                              ? ""
                              : EnumDescConverter.GetEnumDescription(contract.Right)));
                    if (serverVersion >= 15)
                    {
                        send(contract.Multiplier);
                    }
                    send(contract.Exchange);
                    if (serverVersion >= 14)
                    {
                        send(contract.PrimaryExchange);
                    }
                    send(contract.Currency);
                    if (serverVersion >= 2)
                    {
                        send(contract.LocalSymbol);
                    }
                    if (serverVersion >= 45)
                    {
                        send(EnumDescConverter.GetEnumDescription(contract.SecIdType));
                        send(contract.SecId);
                    }

                    // send main order fields
                    send(EnumDescConverter.GetEnumDescription(order.Action));
                    send(order.TotalQuantity);
                    send(EnumDescConverter.GetEnumDescription(order.OrderType));
                    send(order.LimitPrice);
                    send(order.AuxPrice);

                    // send extended order fields
                    send(EnumDescConverter.GetEnumDescription(order.Tif));
                    send(order.OcaGroup);
                    send(order.Account);
                    send(order.OpenClose);
                    send((int) order.Origin);
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
                        send((int)order.TriggerMethod);
                        if(serverVersion < 38)
                        {
                            //will never happen
                            send(false);
                        }
                        else
                        {
                            send(order.OutsideRth);
                        }
                    }

                    if (serverVersion >= 7)
                    {
                        send(order.Hidden);
                    }

                    // Send combo legs for BAG requests
                    if (serverVersion >= 8 && contract.SecurityType == SecurityType.Bag)
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
                                send(EnumDescConverter.GetEnumDescription(comboLeg.Action));
                                send(comboLeg.Exchange);
                                send((int)comboLeg.OpenClose);
                                //Min Combo Leg Short Sale Server Version is 35
                                if (serverVersion >= 35)
                                {
                                    send((int)comboLeg.ShortSaleSlot);
                                    send(comboLeg.DesignatedLocation);
                                }
                                if (serverVersion >= 51)
                                    send(comboLeg.ExemptCode);
                            }
                        }
                    }

                    if (serverVersion >= MinServerVersion.SmartComboRoutingParams && contract.SecurityType == SecurityType.Bag)
                    {
                        Collection<TagValue> smartComboRoutingParams = order.SmartComboRoutingParams;
                        int smartComboRoutingParamsCount = smartComboRoutingParams == null ? 0 : smartComboRoutingParams.Count;
                        send(smartComboRoutingParamsCount);
                        if (smartComboRoutingParamsCount > 0)
                        {
                            for (int i = 0; i < smartComboRoutingParamsCount; ++i)
                            {
                                TagValue tagValue = (TagValue)smartComboRoutingParams[i];
                                send(tagValue.Tag);
                                send(tagValue.Value);
                            }
                        }
                    }

                    if (serverVersion >= 9)
                    {
                        send("");
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
                        send(order.FAGroup);
                        send(EnumDescConverter.GetEnumDescription(order.FAMethod));
                        send(order.FAPercentage);
                        send(order.FAProfile);
                    }
                    if (serverVersion >= 18)
                    {
                        // institutional short sale slot fields.
                        send((int)order.ShortSaleSlot); // 0 only for retail, 1 or 2 only for institution.
                        send(order.DesignatedLocation); // only populate when order.shortSaleSlot = 2.
                    }

                    if (serverVersion >= 51)
                        send(order.ExemptCode);

                    if (serverVersion >= 19)
                    {
                        send((int)order.OcaType);
                        if(serverVersion < 38)
                        {
                            //will never happen
                            send(false);
                        }
                        send(EnumDescConverter.GetEnumDescription(order.Rule80A));
                        send(order.SettlingFirm);
                        send(order.AllOrNone);
                        sendMax(order.MinQty);
                        sendMax(order.PercentOffset);
                        send(order.ETradeOnly);
                        send(order.FirmQuoteOnly);
                        sendMax(order.NbboPriceCap);
                        sendMax((int) order.AuctionStrategy);
                        sendMax(order.StartingPrice);
                        sendMax(order.StockRefPrice);
                        sendMax(order.Delta);
                        // Volatility orders had specific watermark price attribs in server version 26
                        double lower = (serverVersion == 26 && order.OrderType.Equals(OrderType.Volatility))
                                           ? Double.MaxValue
                                           : order.StockRangeLower;
                        double upper = (serverVersion == 26 && order.OrderType.Equals(OrderType.Volatility))
                                           ? Double.MaxValue
                                           : order.StockRangeUpper;
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
                        sendMax((int) order.VolatilityType);
                        if (serverVersion < 28)
                        {
                            send(order.DeltaNeutralOrderType.Equals(OrderType.Market));
                        }
                        else
                        {
                            send(EnumDescConverter.GetEnumDescription(order.DeltaNeutralOrderType));
                            sendMax(order.DeltaNeutralAuxPrice);

                            if (serverVersion >= MinServerVersion.DeltaNeutralConId && order.DeltaNeutralOrderType != OrderType.Empty)
                            {
                                send(order.DeltaNeutralConId);
                                send(order.DeltaNeutralSettlingFirm);
                                send(order.DeltaNeutralClearingAccount);
                                send(order.DeltaNeutralClearingIntent);
                            }
                        }
                        send(order.ContinuousUpdate);
                        if (serverVersion == 26)
                        {
                            // Volatility orders had specific watermark price attribs in server version 26
                            double lower = order.OrderType.Equals(OrderType.Volatility)
                                               ? order.StockRangeLower
                                               : Double.MaxValue;
                            double upper = order.OrderType.Equals(OrderType.Volatility)
                                               ? order.StockRangeUpper
                                               : Double.MaxValue;
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

                    //Scale Orders require server version 35 or higher.
                    if (serverVersion >= MinServerVersion.ScaleOrders)
                    {
                        if (serverVersion >= MinServerVersion.ScaleOrders2)
                        {
                            sendMax(order.ScaleInitLevelSize);
                            sendMax(order.ScaleSubsLevelSize);
                        }
                        else
                        {
                            send("");
                            sendMax(order.ScaleInitLevelSize);
                        }
                        sendMax(order.ScalePriceIncrement);
                    }

                    if (serverVersion >= MinServerVersion.HedgeOrders)
                    {
                        send(order.HedgeType);
                        if (!string.IsNullOrEmpty(order.HedgeType))
                        {
                            send(order.HedgeParam);
                        }
                    }

                    if (serverVersion >= MinServerVersion.OptOutSmartRouting)
                    {
                        send(order.OptOutSmartRouting);
                    }

                    if(serverVersion >= MinServerVersion.PtaOrders)
                    {
                        send(order.ClearingAccount);
                        send(order.ClearingIntent);
                    }

                    if(serverVersion >= MinServerVersion.NotHeld)
                        send(order.NotHeld);

                    if (serverVersion >= MinServerVersion.UnderComp)
                    {
                        if (contract.UnderlyingComponent != null)
                        {
                            UnderlyingComponent underComp = contract.UnderlyingComponent;
                            send(true);
                            send(underComp.ContractId);
                            send(underComp.Delta);
                            send(underComp.Price);
                        }
                        else
                        {
                            send(false);
                        }
                    }

                    if (serverVersion >= MinServerVersion.AlgoOrders)
                    {
                        send(order.AlgoStrategy);
                        if (!string.IsNullOrEmpty(order.AlgoStrategy))
                        {
                            if(order.AlgoParams == null)
                            {
                                send(0);
                            }
                            else
                            {
                                send(order.AlgoParams.Count);
                                foreach(TagValue tagValue in order.AlgoParams)
                                {
                                    send(tagValue.Tag);
                                    send(tagValue.Value);
                                }
                            }
                        }
                    }

                    if(serverVersion >= MinServerVersion.WhatIfOrders)
                    {
                        send(order.WhatIf);
                    }
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(orderId, ErrorMessage.FailSendOrder, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this function to start getting account values, portfolio, and last update time information.
        /// </summary>
        /// <param name="subscribe">If set to TRUE, the client will start receiving account and portfolio updates. If set to FALSE, the client will stop receiving this information.</param>
        /// <param name="acctCode">the account code for which to receive account and portfolio updates.</param>
        public void RequestAccountUpdates(bool subscribe, String acctCode)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                int version = 2;

                // send cancel order msg
                try
                {
                    send((int) OutgoingMessage.RequestAccountData);
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
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(ErrorMessage.FailSendAccountUpdate, e);
                    close();
                }
            }
        }

        /// <summary>
        /// When this method is called, the execution reports that meet the filter criteria are downloaded to the client via the execDetails() method.
        /// </summary>
        /// <param name="requestId">Id of the request</param>
        /// <param name="filter">the filter criteria used to determine which execution reports are returned.</param>
        public void RequestExecutions(int requestId, ExecutionFilter filter)
        {
            if (filter == null)
                filter = new ExecutionFilter(0, "", DateTime.MinValue, "", SecurityType.Undefined, "", ActionSide.Undefined);
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                int version = 3;

                // send cancel order msg
                try
                {
                    send((int) OutgoingMessage.RequestExecutions);
                    send(version);

                    if (serverVersion >= 42)
                    {
                        send(requestId);
                    }

                    // Send the execution rpt filter data
                    if (serverVersion >= 9)
                    {
                        send(filter.ClientId);
                        send(filter.AcctCode);

                        // The valid format for time is "yyyymmdd-hh:mm:ss"
                        send(filter.Time.ToUniversalTime().ToString("yyyyMMdd-HH:mm:ss", CultureInfo.InvariantCulture));
                        send(filter.Symbol);
                        send(EnumDescConverter.GetEnumDescription(filter.SecurityType));
                        send(filter.Exchange);
                        send(EnumDescConverter.GetEnumDescription(filter.Side));
                    }
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;
                    error(ErrorMessage.FailSendExecution, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to cancel an order.
        /// </summary>
        /// <param name="orderId">Call this method to cancel an order.</param>
        public void CancelOrder(int orderId)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(orderId, ErrorMessage.NotConnected);
                    return;
                }

                int version = 1;

                // send cancel order msg
                try
                {
                    send((int) OutgoingMessage.CancelOrder);
                    send(version);
                    send(orderId);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;
                    error(orderId, ErrorMessage.FailSendCancelOrder, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to request the open orders that were placed from this client. Each open order will be fed back through the openOrder() and orderStatus() functions on the EWrapper.
        /// 
        /// The client with a clientId of "0" will also receive the TWS-owned open orders. These orders will be associated with the client and a new orderId will be generated. This association will persist over multiple API and TWS sessions.
        /// </summary>
        public void RequestOpenOrders()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                int version = 1;

                // send cancel order msg
                try
                {
                    send((int) OutgoingMessage.RequestOpenOrders);
                    send(version);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(ErrorMessage.FailSendOpenOrder, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Returns one next valid Id...
        /// </summary>
        /// <param name="numberOfIds">Has No Effect</param>
        public void RequestIds(int numberOfIds)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                int version = 1;

                try
                {
                    send((int) OutgoingMessage.RequestIds);
                    send(version);
                    send(numberOfIds);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(ErrorMessage.FailSendCancelOrder, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to start receiving news bulletins. Each bulletin will be returned by the updateNewsBulletin() method.
        /// </summary>
        /// <param name="allMessages">if set to TRUE, returns all the existing bulletins for the current day and any new ones. IF set to FALSE, will only return new bulletins.</param>
        public void RequestNewsBulletins(bool allMessages)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                int version = 1;

                try
                {
                    send((int) OutgoingMessage.RequestNewsBulletins);
                    send(version);
                    send(allMessages);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(ErrorMessage.FailSendCancelOrder, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to stop receiving news bulletins.
        /// </summary>
        public void CancelNewsBulletins()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                int version = 1;

                // send cancel order msg
                try
                {
                    send((int) OutgoingMessage.CancelNewsBulletins);
                    send(version);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(ErrorMessage.FailSendCancelOrder, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to request that newly created TWS orders be implicitly associated with the client. When a new TWS order is created, the order will be associated with the client and fed back through the openOrder() and orderStatus() methods on the EWrapper.
        /// 
        /// TWS orders can only be bound to clients with a clientId of “0”.
        /// </summary>
        /// <param name="autoBind">If set to TRUE, newly created TWS orders will be implicitly associated with the client. If set to FALSE, no association will be made.</param>
        public void RequestAutoOpenOrders(bool autoBind)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                int version = 1;

                // send req open orders msg
                try
                {
                    send((int) OutgoingMessage.RequestAutoOpenOrders);
                    send(version);
                    send(autoBind);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(ErrorMessage.FailSendOpenOrder, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to request the open orders that were placed from all clients and also from TWS. Each open order will be fed back through the openOrder() and orderStatus() functions on the EWrapper.
        /// 
        /// No association is made between the returned orders and the requesting client.
        /// </summary>
        public void RequestAllOpenOrders()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                int version = 1;

                // send req all open orders msg
                try
                {
                    send((int) OutgoingMessage.RequestAllOpenOrders);
                    send(version);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(ErrorMessage.FailSendOpenOrder, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to request the list of managed accounts. The list will be returned by the managedAccounts() function on the EWrapper.
        /// 
        /// This request can only be made when connected to a Financial Advisor (FA) account.
        /// </summary>
        public void RequestManagedAccts()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                int version = 1;

                // send req FA managed accounts msg
                try
                {
                    send((int) OutgoingMessage.RequestManagedAccounts);
                    send(version);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(ErrorMessage.FailSendOpenOrder, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to request FA configuration information from TWS. The data returns in an XML string via the receiveFA() method.
        /// </summary>
        /// <param name="faDataType">
        /// faDataType - specifies the type of Financial Advisor configuration data being requested. Valid values include:
        /// 1 = GROUPS
        /// 2 = PROFILE
        /// 3 =ACCOUNT ALIASES
        /// </param>
        public void RequestFA(FADataType faDataType)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                // This feature is only available for versions of TWS >= 13
                if (serverVersion < 13)
                {
                    error(ErrorMessage.UpdateTws, "Does not support request FA.");
                    return;
                }

                int version = 1;

                try
                {
                    send((int) OutgoingMessage.RequestFA);
                    send(version);
                    send((int) faDataType);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(ErrorMessage.FailSendFARequest, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to request FA configuration information from TWS. The data returns in an XML string via a "receiveFA" ActiveX event.  
        /// </summary>
        /// <param name="faDataType">
        /// specifies the type of Financial Advisor configuration data being requested. Valid values include:
        /// 1 = GROUPS
        /// 2 = PROFILE
        /// 3 = ACCOUNT ALIASES</param>
        /// <param name="xml">the XML string containing the new FA configuration information.</param>
        public void ReplaceFA(FADataType faDataType, String xml)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                // This feature is only available for versions of TWS >= 13
                if (serverVersion < 13)
                {
                    error(ErrorMessage.UpdateTws, "Does not support Replace FA.");
                    return;
                }

                int version = 1;

                try
                {
                    send((int) OutgoingMessage.ReplaceFA);
                    send(version);
                    send((int) faDataType);
                    send(xml);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(ErrorMessage.FailSendFAReplace, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Returns the current system time on the server side.
        /// </summary>
        public void RequestCurrentTime()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                // This feature is only available for versions of TWS >= 33
                if (serverVersion < 33)
                {
                    error(ErrorMessage.UpdateTws, "It does not support current time requests.");
                    return;
                }

                int version = 1;

                try
                {
                    send((int)OutgoingMessage.RequestCurrentTime);
                    send(version);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(ErrorMessage.FailSendRequestCurrentTime, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Request Fundamental Data
        /// </summary>
        /// <param name="requestId">Request Id</param>
        /// <param name="contract">Contract to request fundamental data for</param>
        /// <param name="reportType">Report Type</param>
        public virtual void RequestFundamentalData(int requestId, Contract contract, String reportType)
        {
            lock (this)
            {
                if (!connected)
                {
                    error(requestId, ErrorMessage.NotConnected);
                    return;
                }

                if (serverVersion < MinServerVersion.FundamentalData)
                {
                    error(requestId, ErrorMessage.UpdateTws, "It does not support fundamental data requests.");
                    return;
                }

                int version = 1;

                try
                {
                    // send req fund data msg
                    send((int)OutgoingMessage.RequestFundamentalData);
                    send(version);
                    send(requestId);

                    // send contract fields
                    send(contract.Symbol);
                    send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                    send(contract.Exchange);
                    send(contract.PrimaryExchange);
                    send(contract.Currency);
                    send(contract.LocalSymbol);

                    send(reportType);
                }
                catch (Exception e)
                {
                    error(requestId, ErrorMessage.FailSendRequestFundData, "" + e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to stop receiving Reuters global fundamental data.
        /// </summary>
        /// <param name="requestId">The ID of the data request.</param>
        public virtual void CancelFundamentalData(int requestId)
        {
            lock (this)
            {
                if (!connected)
                {
                    error(requestId, ErrorMessage.NotConnected);
                    return;
                }

                if (serverVersion < MinServerVersion.FundamentalData)
                {
                    error(requestId, ErrorMessage.UpdateTws, "It does not support fundamental data requests.");
                    return;
                }

                int version = 1;

                try
                {
                    // send req mkt data msg
                    send((int)OutgoingMessage.CancelFundamentalData);
                    send(version);
                    send(requestId);
                }
                catch (Exception e)
                {
                    error(requestId, ErrorMessage.FailSendCancelFundData, "" + e);
                    close();
                }
            }
        }
        
        public virtual void CancelCalculateImpliedVolatility(int reqId)
        {
            if (!connected)
            {
                error(ErrorMessage.NotConnected);
                return;
            }

            if (serverVersion < MinServerVersion.CancelCalculateImpliedVolatility)
            {
                error(reqId, ErrorMessage.UpdateTws, "It does not support calculate implied volatility cancellation.");
                return;
            }

            const int version = 1;

            try
            {
                // send cancel calculate implied volatility msg
                send((int)OutgoingMessage.CancelCalcImpliedVolatility);
                send(version);
                send(reqId);
            }
            catch (Exception e)
            {
                error(reqId, ErrorMessage.FailSendCancelCalculateImpliedVolatility, e);
                close();
            }
        }

        /// <summary>
        /// Calculates the Implied Volatility based on the user-supplied option and underlying prices.
        /// The calculated implied volatility is returned by tickOptionComputation( ) in a new tick type, CUST_OPTION_COMPUTATION, which is described below.
        /// </summary>
        /// <param name="requestId">Request Id</param>
        /// <param name="contract">Contract</param>
        /// <param name="optionPrice">Price of the option</param>
        /// <param name="underPrice">Price of teh underlying of the option</param>
        public virtual void RequestCalculateImpliedVolatility(int requestId, Contract contract, double optionPrice, double underPrice)
        {    
            lock(this)
            {
                if (!connected)
                {
                    error(requestId, ErrorMessage.NotConnected);
                    return;
                }

                if (serverVersion < MinServerVersion.RequestCalculateImpliedVolatility)
                {
                    error(ErrorMessage.UpdateTws, "It does not support calculate implied volatility requests.");
                    return;
                }

                const int version = 1;

                try
                {
                    // send calculate implied volatility msg
                    send((int)OutgoingMessage.RequestCalcImpliedVolatility);
                    send(version);
                    send(requestId);

                    // send contract fields
                    send(contract.ContractId);
                    send(contract.Symbol);
                    send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(((contract.Right == RightType.Undefined)
                              ? ""
                              : EnumDescConverter.GetEnumDescription(contract.Right)));
                    send(contract.Multiplier);
                    send(contract.Exchange);
                    send(contract.PrimaryExchange);
                    send(contract.Currency);
                    send(contract.LocalSymbol);

                    send(optionPrice);
                    send(underPrice);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(requestId, ErrorMessage.FailSendReqCalcImpliedVolatility, e);
                    close();
                }
            }
        }

        public virtual void RequestCalculateOptionPrice(int reqId, Contract contract, double volatility,
                                                        double underPrice)
        {
            if (!connected)
            {
                error(ErrorMessage.NotConnected);
                return;
            }

            if (serverVersion < MinServerVersion.RequestCalculateOptionPrice)
            {
                error(reqId, ErrorMessage.UpdateTws, "It does not support calculate option price requests.");
                return;
            }

            const int version = 1;

            try
            {
                // send calculate option price msg
                send((int)OutgoingMessage.RequestCalcOptionPrice);
                send(version);
                send(reqId);

                // send contract fields
                send(contract.ContractId);
                send(contract.Symbol);
                send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                send(contract.Expiry);
                send(contract.Strike);
                send(EnumDescConverter.GetEnumDescription(contract.Right));
                send(contract.Multiplier);
                send(contract.Exchange);
                send(contract.PrimaryExchange);
                send(contract.Currency);
                send(contract.LocalSymbol);

                send(volatility);
                send(underPrice);
            }
            catch (Exception e)
            {
                error(reqId, ErrorMessage.FailSendRequestCalcOptionPrice, e);
                close();
            }
        }

        public virtual void CancelCalculateOptionPrice(int reqId)
        {
            if (!connected)
            {
                error(ErrorMessage.NotConnected);
                return;
            }

            if (serverVersion < MinServerVersion.CancelCalculateOptionPrice)
            {
                error(reqId, ErrorMessage.UpdateTws, "It does not support calculate option price cancellation.");
                return;
            }

            const int version = 1;

            try
            {
                // send cancel calculate option price msg
                send((int)OutgoingMessage.CancelCalcOptionPrice);
                send(version);
                send(reqId);
            }
            catch (Exception e)
            {
                error(reqId, ErrorMessage.FailSendCancelCalculateOptionPrice, e);
                close();
            }
        }

        public virtual void RequestGlobalCancel()
        {
            // not connected?
            if (!connected)
            {
                error(ErrorMessage.NotConnected);
                return;
            }

            if (serverVersion < MinServerVersion.RequestGlobalCancel)
            {
                error(ErrorMessage.UpdateTws, "It does not support globalCancel requests.");
                return;
            }

            const int version = 1;

            // send request global cancel msg
            try
            {
                send((int)OutgoingMessage.RequestGlobalCancel);
                send(version);
            }
            catch (Exception e)
            {
                error(ErrorMessage.FailSendRequestGlobalCancel, e);
                close();
            }
        }

        public virtual void RequestMarketDataType(int marketDataType)
        {
            // not connected?
            if (!connected)
            {
                error(ErrorMessage.NotConnected);
                return;
            }

            if (serverVersion < MinServerVersion.RequestMarketDataType)
            {
                error(ErrorMessage.UpdateTws, "It does not support marketDataType requests.");
                return;
            }

            const int version = 1;

            // send the reqMarketDataType message
            try
            {
                send((int)OutgoingMessage.RequestMarketDataType);
                send(version);
                send(marketDataType);
            }
            catch (Exception e)
            {
                error(ErrorMessage.FailSendRequestMarketDataType, e);
                close();
            }
        }

        /// <summary>
        /// The default level is ERROR. Refer to the API logging page for more details.
        /// </summary>
        /// <param name="serverLogLevel">
        /// logLevel - specifies the level of log entry detail used by the server (TWS) when processing API requests. Valid values include: 
        /// 1 = SYSTEM
        /// 2 = ERROR
        /// 3 = WARNING
        /// 4 = INFORMATION
        /// 5 = DETAIL
        /// </param>
        public void SetServerLogLevel(LogLevel serverLogLevel)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected, "");
                    return;
                }

                int version = 1;

                // send the set server logging level message
                try
                {
                    send((int) OutgoingMessage.SetServerLogLevel);
                    send(version);
                    send((int) serverLogLevel);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                        throw;

                    error(ErrorMessage.FailSendServerLogLevel, e.ToString());
                    close();
                }
            }
        }

        #endregion

        #region Helper Methods

        private void send(String str)
        {
            // write string to data buffer; writer thread will
            // write it to ibSocket
            if (!string.IsNullOrEmpty(str))
            {
                dos.Write(ToByteArray(str));
            }
            sendEOL();
        }

        /// <summary>
        /// Converts a string to an array of bytes
        /// </summary>
        /// <param name="source">The string to be converted</param>
        /// <returns>The new array of bytes</returns>
        private static byte[] ToByteArray(String source)
        {
            return UTF8Encoding.UTF8.GetBytes(source);
        }

        private void sendEOL()
        {
            dos.Write(EOL);
        }

        private void send(int val)
        {
            send(Convert.ToString(val, CultureInfo.InvariantCulture));
        }


        private void send(double val)
        {
            send(Convert.ToString(val, CultureInfo.InvariantCulture));
        }

        private void send(decimal val)
        {
            send(Convert.ToString(val, CultureInfo.InvariantCulture));
        }

        private void sendMax(double val)
        {
            if (val == Double.MaxValue)
            {
                sendEOL();
            }
            else
            {
                send(Convert.ToString(val, CultureInfo.InvariantCulture));
            }
        }

        private void sendMax(int val)
        {
            if (val == Int32.MaxValue)
            {
                sendEOL();
            }
            else
            {
                send(Convert.ToString(val, CultureInfo.InvariantCulture));
            }
        }

        private void sendMax(decimal val)
        {
            if (val == decimal.MaxValue)
            {
                sendEOL();
            }
            else
            {
                send(Convert.ToString(val, CultureInfo.InvariantCulture));
            }
        }

        private void send(bool val)
        {
            send(val ? 1 : 0);
        }

        private void send(bool? val)
        {
            if(val!=null)
            {
                send(val.Value);
            }
            else
            {
                send("");
            }
        }

        #endregion

        #endregion

        #region IBReader

        #region Thread Sync

        private readonly Thread readThread;

        /// <summary>
        /// Thread that is reading and parsing the network stream
        /// </summary>
        public Thread ReadThread
        {
            get
            {
                return readThread;
            }
        }

        /// <summary>
        /// Lock covering stopping and stopped
        /// </summary>
        private readonly object stopLock = new object();

        /// <summary>
        /// Whether or not the worker thread has stopped
        /// </summary>
        private bool stopped;

        /// <summary>
        /// Whether or not the worker thread has been asked to stop
        /// </summary>
        private bool stopping;

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
        private void SetStopped()
        {
            lock (stopLock)
            {
                stopped = true;
            }
        }

        #endregion

        #region Private Variables / Properties

        private BinaryReader dis;

        #endregion

        #region General Code

        /// <summary>
        /// Contains the reader thread.
        /// </summary>
        internal void Run()
        {
            try
            {
                // loop until thread is terminated
                while (!Stopping && ProcessMsg((IncomingMessage) ReadInt())) ;
            }
            catch(IOException)
            {
                
            }
            catch (Exception ex)
            {
                if (throwExceptions)
                    throw;

                exception(ex);
            }
            finally
            {
                SetStopped();
                connectionClosed();
                close();
            }
        }

        /// <summary>
        /// Forks the reading thread
        /// </summary>
        internal void Start()
        {
            if (!Stopping)
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
                case IncomingMessage.TickPrice:
                    {
                        int version = ReadInt();
                        int tickerId = ReadInt();
                        int tickType = ReadInt();
                        decimal price = ReadDecimal();
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
                        tickPrice(tickerId, (TickType) tickType, price, (canAutoExecute != 0));

                        if (version >= 2)
                        {
                            int sizeTickType = - 1; // not a tick
                            switch (tickType)
                            {
                                case 1: // BID
                                    sizeTickType = 0; // BID_SIZE
                                    break;

                                case 2: // ASK
                                    sizeTickType = 3; // ASK_SIZE
                                    break;

                                case 4: // LAST
                                    sizeTickType = 5; // LAST_SIZE
                                    break;
                            }
                            if (sizeTickType != - 1)
                            {
                                tickSize(tickerId, (TickType) sizeTickType, size);
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

                        tickSize(tickerId, (TickType) tickType, size);
                        break;
                    }


                case IncomingMessage.TickOptionComputation:
                    {
                        int version = ReadInt();
                        int tickerId = ReadInt();
                        TickType tickType = (TickType)ReadInt();
                        double impliedVol = ReadDouble();
                        if (impliedVol < 0)
                        {
                            // -1 is the "not yet computed" indicator
                            impliedVol = Double.MaxValue;
                        }
                        double delta = ReadDouble();
                        if (Math.Abs(delta) > 1)
                        {
                            // -2 is the "not yet computed" indicator
                            delta = Double.MaxValue;
                        }

                        double optPrice = Double.MaxValue;
                        double pvDividend = Double.MaxValue;
                        double gamma = Double.MaxValue;
                        double vega = Double.MaxValue;
                        double theta = Double.MaxValue;
                        double undPrice = Double.MaxValue;
                        if (version >= 6 || tickType == TickType.ModelOption)
                        { // introduced in version == 5
                            optPrice = ReadDouble();
                            if (optPrice < 0)
                            { // -1 is the "not yet computed" indicator
                                optPrice = Double.MaxValue;
                            }
                            pvDividend = ReadDouble();
                            if (pvDividend < 0)
                            { // -1 is the "not yet computed" indicator
                                pvDividend = Double.MaxValue;
                            }
                        }
                        if (version >= 6)
                        {
                            gamma = ReadDouble();
                            if (Math.Abs(gamma) > 1)
                            { // -2 is the "not yet computed" indicator
                                gamma = Double.MaxValue;
                            }
                            vega = ReadDouble();
                            if (Math.Abs(vega) > 1)
                            { // -2 is the "not yet computed" indicator
                                vega = Double.MaxValue;
                            }
                            theta = ReadDouble();
                            if (Math.Abs(theta) > 1)
                            { // -2 is the "not yet computed" indicator
                                theta = Double.MaxValue;
                            }
                            undPrice = ReadDouble();
                            if (undPrice < 0)
                            { // -1 is the "not yet computed" indicator
                                undPrice = Double.MaxValue;
                            }
                        }

                        tickOptionComputation(tickerId, (TickType) tickType, impliedVol, delta, optPrice, pvDividend, gamma, vega, theta, undPrice);
                        break;
                    }


                case IncomingMessage.TickGeneric:
                    {
                        int version = ReadInt();
                        int tickerId = ReadInt();
                        int tickType = ReadInt();
                        double value_Renamed = ReadDouble();

                        tickGeneric(tickerId, (TickType) tickType, value_Renamed);
                        break;
                    }


                case IncomingMessage.TickString:
                    {
                        int version = ReadInt();
                        int tickerId = ReadInt();
                        int tickType = ReadInt();
                        String value_Renamed = ReadStr();

                        tickString(tickerId, (TickType) tickType, value_Renamed);
                        break;
                    }


                case IncomingMessage.TickEfp:
                    {
                        int version = ReadInt();
                        int tickerId = ReadInt();
                        int tickType = ReadInt();
                        double basisPoints = ReadDouble();
                        String formattedBasisPoints = ReadStr();
                        double impliedFuturesPrice = ReadDouble();
                        int holdDays = ReadInt();
                        String futureExpiry = ReadStr();
                        double dividendImpact = ReadDouble();
                        double dividendsToExpiry = ReadDouble();
                        tickEfp(tickerId, (TickType) tickType, basisPoints, formattedBasisPoints, impliedFuturesPrice,
                                holdDays, futureExpiry, dividendImpact, dividendsToExpiry);
                        break;
                    }


                case IncomingMessage.OrderStatus:
                    {
                        int version = ReadInt();
                        int id = ReadInt();
                        string orderstat = ReadStr();
                        Krs.Ats.IBNet.OrderStatus status = (string.IsNullOrEmpty(orderstat) ? Krs.Ats.IBNet.OrderStatus.None :
                            (Krs.Ats.IBNet.OrderStatus) EnumDescConverter.GetEnumValue(typeof (Krs.Ats.IBNet.OrderStatus), orderstat));
                        int filled = ReadInt();
                        int remaining = ReadInt();
                        decimal avgFillPrice = ReadDecimal();

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

                        decimal lastFillPrice = 0;
                        if (version >= 4)
                        {
                            lastFillPrice = ReadDecimal();
                        }

                        int clientId = 0;
                        if (version >= 5)
                        {
                            clientId = ReadInt();
                        }

                        string whyHeld = null;
                        if (version >= 6)
                        {
                            whyHeld = ReadStr();
                        }
                        

                        orderStatus(id, status, filled, remaining, avgFillPrice, permId, parentId, lastFillPrice,
                                    clientId, whyHeld);
                        break;
                    }


                case IncomingMessage.AccountValue:
                    {
                        int version = ReadInt();
                        String key = ReadStr();
                        String val = ReadStr();
                        String cur = ReadStr();
                        String accountName = null;
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
                        if(version >= 6)
                        {
                            contract.ContractId = ReadInt();
                        }
                        contract.Symbol = ReadStr();
                        contract.SecurityType =
                            (SecurityType) EnumDescConverter.GetEnumValue(typeof (SecurityType), ReadStr());
                        contract.Expiry = ReadStr();
                        contract.Strike = ReadDouble();
                        string rstr = ReadStr();
                        contract.Right = (rstr == null || rstr.Length <= 0 || rstr.Equals("?") || rstr.Equals("0")
                                              ? RightType.Undefined
                                              : (RightType) EnumDescConverter.GetEnumValue(typeof (RightType), rstr));

                        if (version >= 7)
                        {
                            contract.Multiplier = ReadStr();
                            contract.PrimaryExchange = ReadStr();
                        }

                        contract.Currency = ReadStr();
                        if (version >= 2)
                        {
                            contract.LocalSymbol = ReadStr();
                        }

                        int position = ReadInt();
                        decimal marketPrice = ReadDecimal();
                        decimal marketValue = ReadDecimal();
                        decimal averageCost = 0.0m;
                        decimal unrealizedPNL = 0.0m;
                        decimal realizedPNL = 0.0m;
                        if (version >= 3)
                        {
                            averageCost = ReadDecimal();
                            unrealizedPNL = ReadDecimal();
                            realizedPNL = ReadDecimal();
                        }

                        String accountName = null;
                        if (version >= 4)
                        {
                            accountName = ReadStr();
                        }

                        if (version == 6 && serverVersion == 39)
                        {
                            contract.PrimaryExchange = ReadStr();
                        }

                        updatePortfolio(contract, position, marketPrice, marketValue, averageCost, unrealizedPNL,
                                        realizedPNL, accountName);

                        break;
                    }


                case IncomingMessage.AccountUpdateTime:
                    {
                        int version = ReadInt();
                        String timeStamp = ReadStr();
                        updateAccountTime(timeStamp);
                        break;
                    }


                case IncomingMessage.ErrorMessage:
                    {
                        int version = ReadInt();
                        if (version < 2)
                        {
                            String msg = ReadStr();
                            error(msg);
                        }
                        else
                        {
                            int id = ReadInt();
                            int errorCode = ReadInt();
                            String errorMsg = ReadStr();
                            error(id, (ErrorMessage) errorCode, errorMsg);
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
                        if(version >= 17)
                        {
                            contract.ContractId = ReadInt();
                        }
                        contract.Symbol = ReadStr();
                        contract.SecurityType =
                            (SecurityType) EnumDescConverter.GetEnumValue(typeof (SecurityType), ReadStr());
                        contract.Expiry = ReadStr();
                        contract.Strike = ReadDouble();
                        string rstr = ReadStr();
                        contract.Right = (string.IsNullOrEmpty(rstr) || rstr.Equals("?")
                                              ? RightType.Undefined
                                              : (RightType) EnumDescConverter.GetEnumValue(typeof (RightType), rstr));
                        contract.Exchange = ReadStr();
                        contract.Currency = ReadStr();
                        if (version >= 2)
                        {
                            contract.LocalSymbol = ReadStr();
                        }

                        // read order fields
                        order.Action = (ActionSide) EnumDescConverter.GetEnumValue(typeof (ActionSide), ReadStr());
                        order.TotalQuantity = ReadInt();
                        order.OrderType = (OrderType) EnumDescConverter.GetEnumValue(typeof (OrderType), ReadStr());
                        order.LimitPrice = ReadDecimal();
                        order.AuxPrice = ReadDecimal();
                        order.Tif = (TimeInForce) EnumDescConverter.GetEnumValue(typeof (TimeInForce), ReadStr());
                        order.OcaGroup = ReadStr();
                        order.Account = ReadStr();
                        order.OpenClose = ReadStr();
                        order.Origin = (OrderOrigin) ReadInt();
                        order.OrderRef = ReadStr();

                        if (version >= 3)
                        {
                            order.ClientId = ReadInt();
                        }

                        if (version >= 4)
                        {
                            order.PermId = ReadInt();
                            if(version < 18)
                            {
                                // will never happen
                                /* order.m_ignoreRth = */
                                ReadBoolFromInt();
                            }
                            else
                            {
                                order.OutsideRth = ReadBoolFromInt();
                            }
                            order.Hidden = ReadInt() == 1;
                            order.DiscretionaryAmt = ReadDecimal();
                        }

                        if (version >= 5)
                        {
                            order.GoodAfterTime = ReadStr();
                        }

                        if (version >= 6)
                        {
                            // skip deprecated sharesAllocation field
                            ReadStr();
                        }

                        if (version >= 7)
                        {
                            order.FAGroup = ReadStr();
                            string fam = ReadStr();
                            order.FAMethod = (string.IsNullOrEmpty(fam) ? FinancialAdvisorAllocationMethod.None : (FinancialAdvisorAllocationMethod)EnumDescConverter.GetEnumValue(typeof(FinancialAdvisorAllocationMethod), fam));
                            order.FAPercentage = ReadStr();
                            order.FAProfile = ReadStr();
                        }

                        if (version >= 8)
                        {
                            order.GoodTillDate = ReadStr();
                        }

                        if (version >= 9)
                        {
                            rstr = ReadStr();
                            order.Rule80A = (string.IsNullOrEmpty(rstr) ? AgentDescription.None : (AgentDescription)EnumDescConverter.GetEnumValue(typeof (AgentDescription), rstr));
                            order.PercentOffset = ReadDouble();
                            order.SettlingFirm = ReadStr();
                            order.ShortSaleSlot = (ShortSaleSlot)ReadInt();
                            order.DesignatedLocation = ReadStr();
                            if (serverVersion == 51)
                                ReadInt();  //exempt code
                            else if (version >= 23)
                                order.ExemptCode = ReadInt();
                            order.AuctionStrategy = (AuctionStrategy) ReadInt();
                            order.StartingPrice = ReadDecimal();
                            order.StockRefPrice = ReadDouble();
                            order.Delta = ReadDouble();
                            order.StockRangeLower = ReadDouble();
                            order.StockRangeUpper = ReadDouble();
                            order.DisplaySize = ReadInt();
                            if (version < 18)
                            {
                                // will never happen
                                /* order.m_rthOnly = */
                                ReadBoolFromInt();
                            }
                            order.BlockOrder = ReadBoolFromInt();
                            order.SweepToFill = ReadBoolFromInt();
                            order.AllOrNone = ReadBoolFromInt();
                            order.MinQty = ReadInt();
                            order.OcaType = (OcaType) ReadInt();
                            order.ETradeOnly = ReadBoolFromInt();
                            order.FirmQuoteOnly = ReadBoolFromInt();
                            order.NbboPriceCap = ReadDecimal();
                        }

                        if (version >= 10)
                        {
                            order.ParentId = ReadInt();
                            order.TriggerMethod = (TriggerMethod) ReadInt();
                        }

                        if (version >= 11)
                        {
                            order.Volatility = ReadDouble();
                            rstr = ReadStr();
                            int i;
                            order.VolatilityType = (int.TryParse(rstr, out i) ? (VolatilityType) i : VolatilityType.Undefined);
                            if (version == 11)
                            {
                                int receivedInt = ReadInt();
                                order.DeltaNeutralOrderType = ((receivedInt == 0) ? OrderType.Empty : OrderType.Market);
                            }
                            else
                            {
                                // version 12 and up
                                string dnoa = ReadStr();
                                order.DeltaNeutralOrderType = (string.IsNullOrEmpty(dnoa) ? OrderType.Empty : (OrderType)EnumDescConverter.GetEnumValue(typeof (OrderType), dnoa));
                                order.DeltaNeutralAuxPrice = ReadDouble();

                                if (version >= 27 && order.DeltaNeutralOrderType != OrderType.Empty)
                                {
                                    order.DeltaNeutralConId = ReadInt();
                                    order.DeltaNeutralSettlingFirm = ReadStr();
                                    order.DeltaNeutralClearingAccount = ReadStr();
                                    order.DeltaNeutralClearingIntent = ReadStr();
                                }
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
                            order.TrailStopPrice = ReadDecimal();
                        }

                        if (version >= 14)
                        {
                            order.BasisPoints = ReadDecimal();
                            order.BasisPointsType = ReadInt();
                            contract.ComboLegsDescription = ReadStr();
                        }

                        if (version >= 26)
                        {
                            int smartComboRoutingParamsCount = ReadInt();
                            if (smartComboRoutingParamsCount > 0)
                            {
                                order.SmartComboRoutingParams = new Collection<TagValue>();
                                for (int i = 0; i < smartComboRoutingParamsCount; ++i)
                                {
                                    TagValue tagValue = new TagValue();
                                    tagValue.Tag = ReadStr();
                                    tagValue.Value = ReadStr();
                                    order.SmartComboRoutingParams.Add(tagValue);
                                }
                            }
                        }

                        if (version >= 15)
                        {
                            if (version >= 20)
                            {
                                order.ScaleInitLevelSize = ReadIntMax();
                                order.ScaleSubsLevelSize = ReadIntMax();
                            }
                            else
                            {
                                /* int notSuppScaleNumComponents = */
                                ReadIntMax();
                                order.ScaleInitLevelSize = ReadIntMax();
                            }
                            order.ScalePriceIncrement = ReadDecimalMax();
                        }

                        if (version >= 24)
                        {
                            order.HedgeType = ReadStr();
                            if (!string.IsNullOrEmpty(order.HedgeType))
                            {
                                order.HedgeParam = ReadStr();
                            }
                        }

                        if (version >= 25)
                        {
                            order.OptOutSmartRouting = ReadBoolFromInt();
                        }

                        if (version >= 19)
                        {
                            order.ClearingAccount = ReadStr();
                            order.ClearingIntent = ReadStr();
                        }

                        if (version >= 22)
                            order.NotHeld = ReadBoolFromInt();

                        if (version >= 20)
                        {
                            if (ReadBoolFromInt())
                            {
                                UnderlyingComponent underComp = new UnderlyingComponent();
                                underComp.ContractId = ReadInt();
                                underComp.Delta = ReadDouble();
                                underComp.Price = ReadDecimal();
                                contract.UnderlyingComponent = underComp;
                            }
                        }

                        if (version >= 21)
                        {
                            order.AlgoStrategy = ReadStr();
                            if (!string.IsNullOrEmpty(order.AlgoStrategy))
                            {
                                int algoParamsCount = ReadInt();
                                if (algoParamsCount > 0)
                                {
                                    order.AlgoParams = new Collection<TagValue>();
                                    for (int i = 0; i < algoParamsCount; i++)
                                    {
                                        TagValue tagValue = new TagValue();
                                        tagValue.Tag = ReadStr();
                                        tagValue.Value = ReadStr();
                                        order.AlgoParams.Add(tagValue);
                                    }
                                }
                            }
                        }

                        OrderState orderState = new OrderState();

                        if (version >= 16)
                        {
                            rstr = ReadStr();
                            order.WhatIf = !(string.IsNullOrEmpty(rstr) || rstr == "0");

                            string ost = ReadStr();
                            orderState.Status = (string.IsNullOrEmpty(ost) ? IBNet.OrderStatus.None : (OrderStatus)EnumDescConverter.GetEnumValue(typeof(OrderStatus), ost));
                            orderState.InitMargin = ReadStr();
                            orderState.MaintMargin = ReadStr();
                            orderState.EquityWithLoan = ReadStr();
                            orderState.Commission = ReadDoubleMax();
                            orderState.MinCommission = ReadDoubleMax();
                            orderState.MaxCommission = ReadDoubleMax();
                            orderState.CommissionCurrency = ReadStr();
                            orderState.WarningText = ReadStr();
                        }

                        openOrder(order.OrderId, contract, order, orderState);
                        break;
                    }


                case IncomingMessage.NextValidId:
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
                            if(version >= 3)
                            {
                                contract.Summary.ContractId = ReadInt();
                            }
                            contract.Summary.Symbol = ReadStr();
                            contract.Summary.SecurityType =
                                (SecurityType) EnumDescConverter.GetEnumValue(typeof (SecurityType), ReadStr());
                            contract.Summary.Expiry = ReadStr();
                            contract.Summary.Strike = ReadDouble();
                            string rstr = ReadStr();
                            contract.Summary.Right = (rstr == null || rstr.Length <= 0 || rstr.Equals("?")
                                                          ? RightType.Undefined
                                                          : (RightType)
                                                            EnumDescConverter.GetEnumValue(typeof (RightType), rstr));
                            contract.Summary.Exchange = ReadStr();
                            contract.Summary.Currency = ReadStr();
                            contract.Summary.LocalSymbol = ReadStr();
                            contract.MarketName = ReadStr();
                            contract.TradingClass = ReadStr();
                            String distance = ReadStr();
                            String benchmark = ReadStr();
                            String projection = ReadStr();
                            String legsStr = null;
                            if (version >= 2)
                            {
                                legsStr = ReadStr();
                            }
                            scannerData(tickerId, rank, contract, distance, benchmark, projection, legsStr);
                        }
                        scannerDataEnd(tickerId);
                        break;
                    }


                case IncomingMessage.ContractData:
                    {
                        int version = ReadInt();

                        int reqId = -1;
                        if (version >= 3)
                        {
                            reqId = ReadInt();
                        }

                        ContractDetails contract = new ContractDetails();
                        contract.Summary.Symbol = ReadStr();
                        contract.Summary.SecurityType =
                            (SecurityType) EnumDescConverter.GetEnumValue(typeof (SecurityType), ReadStr());
                        contract.Summary.Expiry = ReadStr();
                        contract.Summary.Strike = ReadDouble();
                        string rstr = ReadStr();
                        contract.Summary.Right = (rstr == null || rstr.Length <= 0 || rstr.Equals("?")
                                                      ? RightType.Undefined
                                                      : (RightType)
                                                        EnumDescConverter.GetEnumValue(typeof (RightType), rstr));
                        contract.Summary.Exchange = ReadStr();
                        contract.Summary.Currency = ReadStr();
                        contract.Summary.LocalSymbol = ReadStr();
                        contract.MarketName = ReadStr();
                        contract.TradingClass = ReadStr();
                        contract.Summary.ContractId = ReadInt();
                        contract.MinTick = ReadDouble();
                        contract.Summary.Multiplier = ReadStr();
                        contract.OrderTypes = ReadStr();
                        contract.ValidExchanges = ReadStr();
                        if (version >= 2)
                        {
                            contract.PriceMagnifier = ReadInt();
                        }
                        if (version >= 4)
                        {
                            contract.UnderConId = ReadInt();
                        }
                        if (version >= 5)
                        {
                            contract.LongName = ReadStr();
                            contract.Summary.PrimaryExchange = ReadStr();
                        }
                        if (version >= 6)
                        {
                            contract.ContractMonth = ReadStr();
                            contract.Industry = ReadStr();
                            contract.Category = ReadStr();
                            contract.Subcategory = ReadStr();
                            contract.TimeZoneId = ReadStr();
                            contract.TradingHours = ReadStr();
                            contract.LiquidHours = ReadStr();
                        }

                        contractDetails(reqId, contract);
                        break;
                    }

                case IncomingMessage.BondContractData:
                    {
                        int version = ReadInt();

                        int reqId = -1;
                        if (version >= 3)
                        {
                            reqId = ReadInt();
                        }

                        ContractDetails contract = new ContractDetails();

                        contract.Summary.Symbol = ReadStr();
                        contract.Summary.SecurityType =
                            (SecurityType) EnumDescConverter.GetEnumValue(typeof (SecurityType), ReadStr());
                        contract.Cusip = ReadStr();
                        contract.Coupon = ReadDouble();
                        contract.Maturity = ReadStr();
                        contract.IssueDate = ReadStr();
                        contract.Ratings = ReadStr();
                        contract.BondType = ReadStr();
                        contract.CouponType = ReadStr();
                        contract.Convertible = ReadBoolFromInt();
                        contract.Callable = ReadBoolFromInt();
                        contract.Putable = ReadBoolFromInt();
                        contract.DescriptionAppend = ReadStr();
                        contract.Summary.Exchange = ReadStr();
                        contract.Summary.Currency = ReadStr();
                        contract.MarketName = ReadStr();
                        contract.TradingClass = ReadStr();
                        contract.Summary.ContractId = ReadInt();
                        contract.MinTick = ReadDouble();
                        contract.OrderTypes = ReadStr();
                        contract.ValidExchanges = ReadStr();
                        if (version >= 2)
                        {
                            contract.NextOptionDate = ReadStr();
                            contract.NextOptionType = ReadStr();
                            contract.NextOptionPartial = ReadBoolFromInt();
                            contract.Notes = ReadStr();
                        }
                        if(version >= 4)
                        {
                            contract.LongName = ReadStr();
                        }
                        bondContractDetails(reqId, contract);
                        break;
                    }

                case IncomingMessage.ExecutionData:
                    {
                        int version = ReadInt();

                        int reqId = -1;
                        if (version >= 7)
                        {
                            reqId = ReadInt();
                        }

                        int orderId = ReadInt();

                        //Handle the 2^31-1 == 0 bug
                        if (orderId == 2147483647)
                            orderId = 0;

                        //Read Contract Fields
                        Contract contract = new Contract();
                        if(version >= 5)
                        {
                            contract.ContractId = ReadInt();
                        }
                        contract.Symbol = ReadStr();
                        contract.SecurityType =
                            (SecurityType) EnumDescConverter.GetEnumValue(typeof (SecurityType), ReadStr());
                        contract.Expiry = ReadStr();
                        contract.Strike = ReadDouble();
                        string rstr = ReadStr();
                        contract.Right = (string.IsNullOrEmpty(rstr) || rstr.Equals("?")
                                              ? RightType.Undefined
                                              : (RightType) EnumDescConverter.GetEnumValue(typeof (RightType), rstr));
                        contract.Exchange = ReadStr();
                        contract.Currency = ReadStr();
                        contract.LocalSymbol = ReadStr();

                        Execution exec = new Execution();
                        exec.OrderId = orderId;
                        exec.ExecutionId = ReadStr();
                        exec.Time = ReadStr();
                        exec.AccountNumber = ReadStr();
                        exec.Exchange = ReadStr();
                        exec.Side = (ExecutionSide) EnumDescConverter.GetEnumValue(typeof (ExecutionSide), ReadStr());
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
                        if (version >= 6)
                        {
                            exec.CumQuantity = ReadInt();
                            exec.AvgPrice = ReadDecimal();
                        }
                        if (version >= 8)
                        {
                            exec.OrderRef = ReadStr();
                        }

                        execDetails(reqId, orderId, contract, exec);
                        break;
                    }

                case IncomingMessage.MarketDepth:
                    {
                        int version = ReadInt();
                        int id = ReadInt();

                        int position = ReadInt();
                        MarketDepthOperation operation = (MarketDepthOperation) ReadInt();
                        MarketDepthSide side = (MarketDepthSide) ReadInt();
                        decimal price = ReadDecimal();
                        int size = ReadInt();

                        updateMktDepth(id, position, operation, side, price, size);
                        break;
                    }

                case IncomingMessage.MarketDepthL2:
                    {
                        int version = ReadInt();
                        int id = ReadInt();

                        int position = ReadInt();
                        String marketMaker = ReadStr();
                        MarketDepthOperation operation = (MarketDepthOperation) ReadInt();
                        MarketDepthSide side = (MarketDepthSide) ReadInt();
                        decimal price = ReadDecimal();
                        int size = ReadInt();

                        updateMktDepthL2(id, position, marketMaker, operation, side, price, size);
                        break;
                    }

                case IncomingMessage.NewsBulletins:
                    {
                        int version = ReadInt();
                        int newsMsgId = ReadInt();
                        NewsType newsMsgType = (NewsType) ReadInt();
                        String newsMessage = ReadStr();
                        String originatingExch = ReadStr();

                        updateNewsBulletin(newsMsgId, newsMsgType, newsMessage, originatingExch);
                        break;
                    }

                case IncomingMessage.ManagedAccounts:
                    {
                        int version = ReadInt();
                        String accountsList = ReadStr();

                        managedAccounts(accountsList);
                        break;
                    }

                case IncomingMessage.ReceiveFA:
                    {
                        int version = ReadInt();
                        FADataType faDataType = (FADataType) ReadInt();
                        String xml = ReadStr();

                        receiveFA(faDataType, xml);
                        break;
                    }

                case IncomingMessage.HistoricalData:
                    {
                        int version = ReadInt();
                        int reqId = ReadInt();
                        if (version >= 2)
                        {
                            //Read Start Date String
                            /*String startDateStr = */ReadStr();
                            /*String endDateStr   = */ReadStr();
                            //completedIndicator += ("-" + startDateStr + "-" + endDateStr);
                        }
                        int itemCount = ReadInt();
                        for (int ctr = 0; ctr < itemCount; ctr++)
                        {
                            //Comes in as seconds
                            //2 - dates are returned as a long integer specifying the number of seconds since 1/1/1970 GMT.
                            String date = ReadStr();
                            long longDate = Int64.Parse(date, CultureInfo.InvariantCulture);
                            //Check if date time string or seconds
                            DateTime timeStamp;
                            if(longDate < 30000000)
                                timeStamp = new DateTime(Int32.Parse(date.Substring(0, 4)), Int32.Parse(date.Substring(4, 2)), Int32.Parse(date.Substring(6, 2)), 0, 0, 0, DateTimeKind.Utc).ToLocalTime();
                            else
                                timeStamp = new DateTime(1970,1,1,0,0,0,DateTimeKind.Utc).AddSeconds(longDate).ToLocalTime();
                            decimal open = ReadDecimal();
                            decimal high = ReadDecimal();
                            decimal low = ReadDecimal();
                            decimal close = ReadDecimal();
                            int volume = ReadInt();
                            double WAP = ReadDouble();
                            String hasGaps = ReadStr();
                            int barCount = - 1;
                            if (version >= 3)
                            {
                                barCount = ReadInt();
                            }
                            historicalData(reqId, timeStamp, open, high, low, close, volume, barCount, WAP,
                                           Boolean.Parse(hasGaps), ctr, itemCount);
                        }
                        break;
                    }

                case IncomingMessage.ScannerParameters:
                    {
                        int version = ReadInt();
                        String xml = ReadStr();
                        scannerParameters(xml);
                        break;
                    }

                case IncomingMessage.CurrentTime:
                    {
                        /*int version =*/
                        ReadInt();
                        long time = ReadLong();
                        DateTime cTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(time);
                        currentTime(cTime);
                        break;
                    }

                case IncomingMessage.RealTimeBars:
                    {
                        /*int version =*/
                        ReadInt();
                        int reqId = ReadInt();
                        long time = ReadLong();
                        decimal open = ReadDecimal();
                        decimal high = ReadDecimal();
                        decimal low = ReadDecimal();
                        decimal close = ReadDecimal();
                        long volume = ReadLong();
                        double wap = ReadDouble();
                        int count = ReadInt();
                        realTimeBar(reqId, time, open, high, low, close, volume, wap, count);
                        break;
                    }

                case IncomingMessage.FundamentalData:
                    {
                        /*int version =*/
                        ReadInt();
                        int reqId = ReadInt();
                        string data = ReadStr();
                        fundamentalData(reqId, data);
                        break;
                    }

                case IncomingMessage.ContractDataEnd:
                    {
                        /*int version =*/
                        ReadInt();
                        int reqId = ReadInt();
                        contractDetailsEnd(reqId);
                        break;
                    }

                case IncomingMessage.OpenOrderEnd:
                    {
                        /*int version =*/ ReadInt();
                        openOrderEnd();
                        break;
                    }

                case IncomingMessage.AccountDownloadEnd:
                    {
                        /*int version =*/ ReadInt();
                        string accountName = ReadStr();
                        accountDownloadEnd(accountName);
                        break;
                    }

                case IncomingMessage.ExecutionDataEnd:
                    {
                        /*int version =*/ ReadInt();
                        int reqId = ReadInt();
                        executionDataEnd(reqId);
                        break;
                    }

                case IncomingMessage.DeltaNuetralValidation:
                    {
                        /*int version =*/ ReadInt();
                        int reqId = ReadInt();

                        UnderComp underComp = new UnderComp();
                        underComp.ConId = ReadInt();
                        underComp.Delta = ReadDouble();
                        underComp.Price = ReadDouble();

                        deltaNuetralValidation(reqId, underComp);
                        break;
                    }
                case IncomingMessage.TickSnapshotEnd:
                    {
                        /*int version =*/ ReadInt();
                        int reqId = ReadInt();

                        tickSnapshotEnd(reqId);
                        break;
                    }
                case IncomingMessage.MarketDataType:
                    {
                        /*int version =*/ ReadInt();
                        int reqId = ReadInt();
                        MarketDataType mdt = (MarketDataType)ReadInt();

                        marketDataType(reqId, mdt);
                        break;
                    }
                default:
                    {
                        error(ErrorMessage.NoValidId);
                        return false;
                    }
            }
            return true;
        }

        #endregion

        #region Helper Methods

        private string ReadStr()
        {
            StringBuilder buf = new StringBuilder();
            while (true)
            {
                sbyte c = (sbyte) dis.ReadByte();
                if (c == 0)
                {
                    break;
                }
                buf.Append((char) c);
            }

            String str = buf.ToString();
            return str.Length == 0 ? null : str;
        }

        private bool ReadBoolFromInt()
        {
            String str = ReadStr();
            return str == null ? false : (Int32.Parse(str, CultureInfo.InvariantCulture) != 0);
        }

        private int ReadInt()
        {
            String str = ReadStr();

            return str == null ? 0 : Int32.Parse(str, CultureInfo.InvariantCulture);
        }

        private int ReadIntMax()
        {
            String str = ReadStr();
            return (string.IsNullOrEmpty(str)) ? Int32.MaxValue : Int32.Parse(str, CultureInfo.InvariantCulture);
        }

        private long ReadLong()
        {
            String str = ReadStr();
            return str == null ? 0L : Int64.Parse(str, CultureInfo.InvariantCulture);
        }

        private double ReadDouble()
        {
            String str = ReadStr();
            return str == null ? 0 : Double.Parse(str, CultureInfo.InvariantCulture);
        }

        private decimal ReadDecimal()
        {
            String str = ReadStr();
            if (string.IsNullOrEmpty(str))
                return 0;
            decimal retVal;
            return decimal.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out retVal) ? retVal : decimal.MaxValue;
        }

        private double ReadDoubleMax()
        {
            String str = ReadStr();
            return (string.IsNullOrEmpty(str)) ? Double.MaxValue : Double.Parse(str, CultureInfo.InvariantCulture);
        }

        private decimal ReadDecimalMax()
        {
            String str = ReadStr();
            decimal retVal;
            return (!string.IsNullOrEmpty(str) && decimal.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out retVal)) ? retVal : decimal.MaxValue;
        }

        #endregion

        #endregion
    }
}
