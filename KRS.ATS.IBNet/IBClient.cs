using System;
using System.Collections.Generic;
using System.Text;

namespace KRS.ATS.IBNet
{
    public class IBClient : IBWrapper
    {
#region Private Variables
        private IBClientSocket socket;
        private IBReader reader;
#endregion

        #region IB Wrapper to Events

        public delegate void TickPriceEventHandler(object sender, TickPriceEventArgs e);
        public event TickPriceEventHandler TickPrice;
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

        public delegate void TickSizeEventHandler(object sender, TickSizeEventArgs e);
        public event TickSizeEventHandler TickSize;
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

        public delegate void TickOptionComputationEventHandler(object sender, TickOptionComputationEventArgs e);
        public event TickOptionComputationEventHandler TickOptionComputation;
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

        public delegate void TickGenericEventHandler(object sender, TickGenericEventArgs e);
        public event TickGenericEventHandler TickGeneric;
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

        public delegate void TickStringEventHandler(object sender, TickStringEventArgs e);
        public event TickStringEventHandler TickString;
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

        public delegate void TickEFPEventHandler(object sender, TickEFPEventArgs e);
        public event TickEFPEventHandler TickEFP;
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

        public delegate void OrderStatusEventHandler(object sender, OrderStatusEventArgs e);
        public event OrderStatusEventHandler OrderStatus;
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

        public delegate void OpenOrderEventHandler(object sender, OpenOrderEventArgs e);
        public event OpenOrderEventHandler OpenOrder;
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

        public delegate void UpdateAccountValueEventHandler(object sender, UpdateAccountValueEventArgs e);
        public event UpdateAccountValueEventHandler UpdateAccountValue;
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

        public delegate void UpdatePortfolioEventHandler(object sender, UpdatePortfolioEventArgs e);
        public event UpdatePortfolioEventHandler UpdatePortfolio;
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

        public delegate void UpdateAccountTimeEventHandler(object sender, UpdateAccountTimeEventArgs e);
        public event UpdateAccountTimeEventHandler UpdateAccountTime;
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

        public delegate void NextValidIdEventHandler(object sender, NextValidIdEventArgs e);
        public event NextValidIdEventHandler NextValidId;
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

        public delegate void ContractDetailsEventHandler(object sender, ContractDetailsEventArgs e);
        public event ContractDetailsEventHandler ContractDetails;
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

        public delegate void BondContractDetailsEventHandler(object sender, BondContractDetailsEventArgs e);
        public event BondContractDetailsEventHandler BondContractDetails;
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

        public delegate void ExecDetailsEventHandler(object sender, ExecDetailsEventArgs e);
        public event ExecDetailsEventHandler ExecDetails;
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

        public delegate void UpdateMktDepthEventHandler(object sender, UpdateMktDepthEventArgs e);
        public event UpdateMktDepthEventHandler UpdateMktDepth;
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

        public delegate void UpdateMktDepthL2EventHandler(object sender, UpdateMktDepthL2EventArgs e);
        public event UpdateMktDepthL2EventHandler UpdateMktDepthL2;
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

        public delegate void UpdateNewsBulletinEventHandler(object sender, UpdateNewsBulletinEventArgs e);
        public event UpdateNewsBulletinEventHandler UpdateNewsBulletin;
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

        public delegate void ManagedAccountsEventHandler(object sender, ManagedAccountsEventArgs e);
        public event ManagedAccountsEventHandler ManagedAccounts;
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

        public delegate void ReceiveFAEventHandler(object sender, ReceiveFAEventArgs e);
        public event ReceiveFAEventHandler ReceiveFA;
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

        public delegate void HistoricalDataEventHandler(object sender, HistoricalDataEventArgs e);
        public event HistoricalDataEventHandler HistoricalData;
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

        public delegate void ScannerParametersEventHandler(object sender, ScannerParametersEventArgs e);
        public event ScannerParametersEventHandler ScannerParameters;
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

        public delegate void ScannerDataEventHandler(object sender, ScannerDataEventArgs e);
        public event ScannerDataEventHandler ScannerData;
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

        public delegate void ErrorEventHandler(object sender, ErrorEventArgs e);
        public event ErrorEventHandler Error;
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

        public delegate void ConnectionClosedEventHandler(object sender, ConnectionClosedEventArgs e);
        public event ConnectionClosedEventHandler ConnectionClosed;
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
    }
}
