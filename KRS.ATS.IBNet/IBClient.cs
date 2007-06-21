using System;
using System.Collections.Generic;
using System.Text;

namespace KRS.ATS.IBNet
{
    public class IBClient : EWrapper
    {
        #region IB Wrapper to Events

        public delegate void TickPriceEventHandler(object sender, TickPriceEventArgs e);
        public event TickPriceEventHandler TickPrice;
        protected virtual void OnTickPrice(TickPriceEventArgs e)
        {
            throw new NotImplementedException();
        }
        void EWrapper.tickPrice(int tickerId, int field, double price, int canAutoExecute)
        {
            throw new NotImplementedException();
        }

        public delegate void TickSizeEventHandler(object sender, TickSizeEventArgs e);
        public event TickSizeEventHandler TickSize;
        protected virtual void OnTickSize(TickSizeEventArgs e)
        {
            throw new NotImplementedException();
        }
        void EWrapper.tickSize(int tickerId, int field, int size)
        {
            throw new NotImplementedException();
        }

        public delegate void TickOptionComputationEventHandler(object sender, TickOptionComputationEventArgs e);
        public event TickOptionComputationEventHandler TickOptionComputation;
        protected virtual void OnTickOptionComputation(TickOptionComputationEventArgs e)
        {
            
        }
        void EWrapper.tickOptionComputation(int tickerId, int field, double impliedVol, double delta, double modelPrice,
                                          double pvDividend)
        {
            throw new NotImplementedException();
        }

        public delegate void TickGenericEventHandler(object sender, TickGenericEventArgs e);
        public event TickGenericEventHandler TickGeneric;
        protected virtual void OnTickGeneric(TickGenericEventArgs e)
        {
            
        }
        void EWrapper.tickGeneric(int tickerId, int tickType, double value_Renamed)
        {
            throw new NotImplementedException();
        }

        public delegate void TickStringEventHandler(object sender, TickStringEventArgs e);
        public event TickStringEventHandler TickString;
        protected virtual void OnTickString(TickStringEventArgs e)
        {
            
        }
        void EWrapper.tickString(int tickerId, int tickType, string value_Renamed)
        {
            throw new NotImplementedException();
        }

        public delegate void TickEFPEventHandler(object sender, TickEFPEventArgs e);
        public event TickEFPEventHandler TickEFP;
        protected virtual void OnTickEFP(TickEFPEventArgs e)
        {
            
        }
        void EWrapper.tickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints,
                            double impliedFuture, int holdDays, string futureExpiry, double dividendImpact,
                            double dividendsToExpiry)
        {
            throw new NotImplementedException();
        }

        public delegate void OrderStatusEventHandler(object sender, OrderStatusEventArgs e);
        public event OrderStatusEventHandler OrderStatus;
        protected virtual void OnOrderStatus(OrderStatusEventArgs e)
        {
            
        }
        void EWrapper.orderStatus(int orderId, string status, int filled, int remaining, double avgFillPrice, int permId,
                                int parentId, double lastFillPrice, int clientId)
        {
            throw new NotImplementedException();
        }

        public delegate void OpenOrderEventHandler(object sender, OpenOrderEventArgs e);
        public event OpenOrderEventHandler OpenOrder;
        protected virtual void OnOpenOrder(OpenOrderEventArgs e)
        {
            
        }
        void EWrapper.openOrder(int orderId, Contract contract, Order order)
        {
            throw new NotImplementedException();
        }

        public delegate void UpdateAccountValueEventHandler(object sender, UpdateAccountValueEventArgs e);
        public event UpdateAccountValueEventHandler UpdateAccountValue;
        protected virtual void OnUpdateAccountValue(UpdateAccountValueEventArgs e)
        {
            
        }
        void EWrapper.updateAccountValue(string key, string value_Renamed, string currency, string accountName)
        {
            throw new NotImplementedException();
        }

        public delegate void UpdatePortfolioEventHandler(object sender, UpdatePortfolioEventArgs e);
        public event UpdatePortfolioEventHandler UpdatePortfolio;
        protected virtual void OnUpdatePortfolio(UpdateAccountValueEventArgs e)
        {
            
        }
        void EWrapper.updatePortfolio(Contract contract, int position, double marketPrice, double marketValue,
                                    double averageCost, double unrealizedPNL, double realizedPNL, string accountName)
        {
            throw new NotImplementedException();
        }

        public delegate void UpdateAccountTimeEventHandler(object sender, UpdateAccountTimeEventArgs e);
        public event UpdateAccountTimeEventHandler UpdateAccountTime;
        protected virtual void OnUpdateAccountTime(UpdateAccountTimeEventArgs e)
        {
            
        }
        void EWrapper.updateAccountTime(string timeStamp)
        {
            throw new NotImplementedException();
        }

        public delegate void NextValidIdEventHandler(object sender, NextValidIdEventArgs e);
        public event NextValidIdEventHandler NextValidId;
        protected virtual void OnNextValidId(NextValidIdEventArgs e)
        {
        }
        void EWrapper.nextValidId(int orderId)
        {
            throw new NotImplementedException();
        }

        public delegate void ContractDetailsEventHandler(object sender, ContractDetailsEventArgs e);
        public event ContractDetailsEventHandler ContractDetails;
        protected virtual void OnContractDetails(ContractDetailsEventArgs e)
        {
            
        }
        void EWrapper.contractDetails(ContractDetails contractDetails)
        {
            throw new NotImplementedException();
        }

        public delegate void BondContractDetailsEventHandler(object sender, BondContractDetailsEventArgs e);
        public event BondContractDetailsEventHandler BondContractDetails;
        protected virtual void OnBondContractDetails(BondContractDetailsEventArgs e)
        {
            
        }
        void EWrapper.bondContractDetails(ContractDetails contractDetails)
        {
            throw new NotImplementedException();
        }

        public delegate void ExecDetailsEventHandler(object sender, ExecDetailsEventArgs e);
        public event ExecDetailsEventHandler ExecDetails;
        protected virtual void OnExecDetails(ExecDetailsEventArgs e)
        {
        }
        void EWrapper.execDetails(int orderId, Contract contract, Execution execution)
        {
            throw new NotImplementedException();
        }

        public delegate void UpdateMktDepthEventHandler(object sender, UpdateMktDepthEventArgs e);
        public event UpdateMktDepthEventHandler UpdateMktDepth;
        protected virtual void OnUpdateMktDepth(UpdateMktDepthEventArgs e)
        {
            
        }
        void EWrapper.updateMktDepth(int tickerId, int position, int operation, int side, double price, int size)
        {
            throw new NotImplementedException();
        }

        public delegate void UpdateMktDepthL2EventHandler(object sender, UpdateMktDepthL2EventArgs e);
        public event UpdateMktDepthL2EventHandler UpdateMktDepthL2;
        protected virtual void OnUpdateMktDepthL2(UpdateMktDepthL2EventArgs e)
        {
            
        }
        void EWrapper.updateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side,
                                     double price, int size)
        {
            throw new NotImplementedException();
        }

        public delegate void UpdateNewsBulletinEventHandler(object sender, UpdateNewsBulletinEventArgs e);
        public event UpdateNewsBulletinEventHandler UpdateNewsBulletin;
        protected virtual void OnUpdateNewsBulletin(UpdateNewsBulletinEventArgs e)
        {
            
        }
        void EWrapper.updateNewsBulletin(int msgId, int msgType, string message, string origExchange)
        {
            throw new NotImplementedException();
        }

        public delegate void ManagedAccountsEventHandler(object sender, ManagedAccountsEventArgs e);
        public event ManagedAccountsEventHandler ManagedAccounts;
        protected virtual void OnManagedAccounts(ManagedAccountsEventArgs e)
        {
            
        }
        void EWrapper.managedAccounts(string accountsList)
        {
            throw new NotImplementedException();
        }

        public delegate void ReceiveFAEventHandler(object sender, ReceiveFAEventArgs e);
        public event ReceiveFAEventHandler ReceiveFA;
        protected virtual void OnReceiveFA(ReceiveFAEventArgs e)
        {
            
        }
        void EWrapper.receiveFA(int faDataType, string xml)
        {
            throw new NotImplementedException();
        }

        public delegate void HistoricalDataEventHandler(object sender, HistoricalDataEventArgs e);
        public event HistoricalDataEventHandler HistoricalData;
        protected virtual void OnHistoricalData(HistoricalDataEventArgs e)
        {
            
        }
        void EWrapper.historicalData(int reqId, string date, double open, double high, double low, double close,
                                   int volume, int count, double WAP, bool hasGaps)
        {
            throw new NotImplementedException();
        }

        public delegate void ScannerParametersEventHandler(object sender, ScannerParametersEventArgs e);
        public event ScannerParametersEventHandler ScannerParameters;
        protected virtual void OnScannerParameters(ScannerParametersEventArgs e)
        {
            
        }
        void EWrapper.scannerParameters(string xml)
        {
            throw new NotImplementedException();
        }

        public delegate void ScannerDataEventHandler(object sender, ScannerDataEventArgs e);
        public event ScannerDataEventHandler ScannerData;
        protected virtual void OnScannerData(ScannerDataEventArgs e)
        {
        }
        void EWrapper.scannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark,
                                string projection, string legsStr)
        {
            throw new NotImplementedException();
        }

        public delegate void ErrorEventHandler(object sender, ErrorEventArgs e);
        public event ErrorEventHandler Error;
        protected virtual void OnError(ErrorEventArgs e)
        {
            
        }
        void AnyWrapper.error(int id, int errorCode, string errorMsg)
        {
            throw new NotImplementedException();
        }

        public delegate void ConnectionClosedEventHandler(object sender, ConnectionClosedEventArgs e);
        public event ConnectionClosedEventHandler ConnectionClosed;
        protected virtual void OnConnectionClosed(ConnectionClosedEventArgs e)
        {
            
        }
        void AnyWrapper.connectionClosed()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
