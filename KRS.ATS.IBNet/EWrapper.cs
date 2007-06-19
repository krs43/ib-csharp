/*
* EWrapper.java
*
*/
using System;
using KRS.ATS.IBNet;

namespace KRS.ATS.IBNet
{
    public interface EWrapper:AnyWrapper
    {
		
        ///////////////////////////////////////////////////////////////////////
        // Interface methods
        ///////////////////////////////////////////////////////////////////////
        void  tickPrice(int tickerId, int field, double price, int canAutoExecute);
        void  tickSize(int tickerId, int field, int size);
        void  tickOptionComputation(int tickerId, int field, double impliedVol, double delta, double modelPrice, double pvDividend);
        void  tickGeneric(int tickerId, int tickType, double value_Renamed);
        void  tickString(int tickerId, int tickType, System.String value_Renamed);
        void  tickEFP(int tickerId, int tickType, double basisPoints, System.String formattedBasisPoints, double impliedFuture, int holdDays, System.String futureExpiry, double dividendImpact, double dividendsToExpiry);
        void  orderStatus(int orderId, System.String status, int filled, int remaining, double avgFillPrice, int permId, int parentId, double lastFillPrice, int clientId);
        void  openOrder(int orderId, Contract contract, Order order);
        void  updateAccountValue(System.String key, System.String value_Renamed, System.String currency, System.String accountName);
        void  updatePortfolio(Contract contract, int position, double marketPrice, double marketValue, double averageCost, double unrealizedPNL, double realizedPNL, System.String accountName);
        void  updateAccountTime(System.String timeStamp);
        void  nextValidId(int orderId);
        void  contractDetails(ContractDetails contractDetails);
        void  bondContractDetails(ContractDetails contractDetails);
        void  execDetails(int orderId, Contract contract, Execution execution);
        void  updateMktDepth(int tickerId, int position, int operation, int side, double price, int size);
        void  updateMktDepthL2(int tickerId, int position, System.String marketMaker, int operation, int side, double price, int size);
        void  updateNewsBulletin(int msgId, int msgType, System.String message, System.String origExchange);
        void  managedAccounts(System.String accountsList);
        void  receiveFA(int faDataType, System.String xml);
        void  historicalData(int reqId, System.String date, double open, double high, double low, double close, int volume, int count, double WAP, bool hasGaps);
        void  scannerParameters(System.String xml);
        void  scannerData(int reqId, int rank, ContractDetails contractDetails, System.String distance, System.String benchmark, System.String projection, System.String legsStr);
    }
}