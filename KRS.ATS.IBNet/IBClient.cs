using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    public class IBClient : IBWrapper
    {
        #region Private Variables
        private IBClientSocket socket;
        #endregion

        #region IB Wrapper to Events
        public event EventHandler<TickPriceEventArgs> TickPrice;
        protected virtual void OnTickPrice(TickPriceEventArgs e)
        {
            if(TickPrice!=null)
                TickPrice(this, e);
        }
        void IBWrapper.tickPrice(int tickerId, int field, double price, int canAutoExecute)
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
        void IBWrapper.tickSize(int tickerId, int field, int size)
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
        void IBWrapper.tickOptionComputation(int tickerId, int field, double impliedVol, double delta, double modelPrice,
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
        void IBWrapper.tickGeneric(int tickerId, int tickType, double value_Renamed)
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
        void IBWrapper.tickString(int tickerId, int tickType, string value_Renamed)
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
        void IBWrapper.tickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints,
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
        void IBWrapper.orderStatus(int orderId, string status, int filled, int remaining, double avgFillPrice, int permId,
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
        void IBWrapper.openOrder(int orderId, Contract contract, Order order)
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
        void IBWrapper.updateAccountValue(string key, string value_Renamed, string currency, string accountName)
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
        void IBWrapper.updatePortfolio(Contract contract, int position, double marketPrice, double marketValue,
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
        void IBWrapper.updateAccountTime(string timeStamp)
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
        void IBWrapper.nextValidId(int orderId)
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
        void IBWrapper.contractDetails(ContractDetails contractDetails)
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
        void IBWrapper.bondContractDetails(ContractDetails contractDetails)
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
        void IBWrapper.execDetails(int orderId, Contract contract, Execution execution)
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
        void IBWrapper.updateMktDepth(int tickerId, int position, int operation, int side, double price, int size)
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
        void IBWrapper.updateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side,
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
        void IBWrapper.updateNewsBulletin(int msgId, int msgType, string message, string origExchange)
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
        void IBWrapper.managedAccounts(string accountsList)
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
        void IBWrapper.receiveFA(int faDataType, string xml)
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
        void IBWrapper.historicalData(int reqId, string date, double open, double high, double low, double close,
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
        void IBWrapper.scannerParameters(string xml)
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
        void IBWrapper.scannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark,
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
        void IBWrapper.error(int id, int errorCode, string errorMsg)
        {
            ErrorEventArgs e = new ErrorEventArgs(id, errorCode, errorMsg);
            OnError(e);
        }

        public event EventHandler<ConnectionClosedEventArgs> ConnectionClosed;
        protected virtual void OnConnectionClosed(ConnectionClosedEventArgs e)
        {
            if(ConnectionClosed!=null)
                ConnectionClosed(this, e);
        }
        void IBWrapper.connectionClosed()
        {
            ConnectionClosedEventArgs e = new ConnectionClosedEventArgs();
            OnConnectionClosed(e);
        }
        #endregion

        #region Network Commands
        public void CancelScannerSubscription(int tickerId)
        {
            socket.cancelScannerSubscription(tickerId);
        }
        public void ReqScannerParameters()
        {
            socket.reqScannerParameters();
        }
        public void ReqScannerSubscription(int tickerId, ScannerSubscription subscription)
        {
            socket.reqScannerSubscription(tickerId, subscription);
        }
        public void ReqMktData(int tickerId, Contract contract, List<GenericTickType> genericTickList)
        {
            socket.reqMktData(tickerId, contract, genericTickList);
        }
        public void CancelHistoricalData(int tickerId)
        {
            socket.cancelHistoricalData(tickerId);
        }
        public void ReqHistoricalData(int tickerId, Contract contract, String endDateTime, String durationStr, String barSizeSetting, String whatToShow, int useRTH, int formatDate)
        {
            socket.reqHistoricalData(tickerId, contract, endDateTime, durationStr, barSizeSetting, whatToShow, useRTH, formatDate);
        }
        public void ReqContractDetails(Contract contract)
        {
            socket.reqContractDetails(contract);
        }
        public void ReqMktDepth(int tickerId, Contract contract, int numRows)
        {
            socket.reqMktDepth(tickerId, contract, numRows);
        }
        public void CancelMktData(int tickerId)
        {
            socket.cancelMktData(tickerId);
        }
        public void CancelMktDepth(int tickerId)
        {
            socket.cancelMktDepth(tickerId);
        }
        public void ExerciseOptions(int tickerId, Contract contract, int exerciseAction, int exerciseQuantity, String account, int override_Renamed)
        {
            socket.exerciseOptions(tickerId, contract, exerciseAction, exerciseQuantity, account, override_Renamed);
        }
        public void PlaceOrder(int id, Contract contract, Order order)
        {
            socket.placeOrder(id, contract, order);
        }
        public void ReqAccountUpdates(bool subscribe, String acctCode)
        {
            socket.reqAccountUpdates(subscribe, acctCode);
        }
        public void ReqExecutions(ExecutionFilter filter)
        {
            socket.reqExecutions(filter);
        }
        public void CancelOrder(int id)
        {
            socket.cancelOrder(id);
        }
        public void ReqOpenOrders()
        {
            socket.reqOpenOrders();
        }
        public void ReqIds(int numIds)
        {
            socket.reqIds(numIds);
        }
        public void ReqNewsBulletins(bool allMsgs)
        {
            socket.reqNewsBulletins(allMsgs);
        }
        public void CancelNewsBulletins()
        {
            socket.cancelNewsBulletins();
        }
        public void ReqAutoOpenOrders(bool bAutoBind)
        {
            socket.reqAutoOpenOrders(bAutoBind);
        }
        public void ReqAllOpenOrders()
        {
            socket.reqAllOpenOrders();
        }
        public void ReqManagedAccts()
        {
            socket.reqManagedAccts();
        }
        public void RequestFA(int faDataType)
        {
            socket.requestFA(faDataType);
        }
        public void ReplaceFA(int faDataType, String xml)
        {
            socket.replaceFA(faDataType, xml);
        }
        #endregion

        #region IBClientSocket
        public IBClient()
        {
            socket = new IBClientSocket(this);
        }
        public virtual void  Disconnect()
        {
            socket.Disconnect();
        }
        public void Connect(String host, int port, int clientId)
        {
            socket.Connect(host, port, clientId);
        }
        #endregion
    }
}
