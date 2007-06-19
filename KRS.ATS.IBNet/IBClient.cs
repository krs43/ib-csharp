using System;
using System.Collections.Generic;
using System.Text;

namespace KRS.ATS.IBNet
{
    class IBClient : EWrapper
    {
        void EWrapper.tickPrice(int tickerId, int field, double price, int canAutoExecute)
        {
            throw new NotImplementedException();
        }

        void EWrapper.tickSize(int tickerId, int field, int size)
        {
            throw new NotImplementedException();
        }

        void EWrapper.tickOptionComputation(int tickerId, int field, double impliedVol, double delta, double modelPrice,
                                          double pvDividend)
        {
            throw new NotImplementedException();
        }

        void EWrapper.tickGeneric(int tickerId, int tickType, double value_Renamed)
        {
            throw new NotImplementedException();
        }

        void EWrapper.tickString(int tickerId, int tickType, string value_Renamed)
        {
            throw new NotImplementedException();
        }

        void EWrapper.tickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints,
                            double impliedFuture, int holdDays, string futureExpiry, double dividendImpact,
                            double dividendsToExpiry)
        {
            throw new NotImplementedException();
        }

        void EWrapper.orderStatus(int orderId, string status, int filled, int remaining, double avgFillPrice, int permId,
                                int parentId, double lastFillPrice, int clientId)
        {
            throw new NotImplementedException();
        }

        void EWrapper.openOrder(int orderId, Contract contract, Order order)
        {
            throw new NotImplementedException();
        }

        void EWrapper.updateAccountValue(string key, string value_Renamed, string currency, string accountName)
        {
            throw new NotImplementedException();
        }

        void EWrapper.updatePortfolio(Contract contract, int position, double marketPrice, double marketValue,
                                    double averageCost, double unrealizedPNL, double realizedPNL, string accountName)
        {
            throw new NotImplementedException();
        }

        void EWrapper.updateAccountTime(string timeStamp)
        {
            throw new NotImplementedException();
        }

        void EWrapper.nextValidId(int orderId)
        {
            throw new NotImplementedException();
        }

        void EWrapper.contractDetails(ContractDetails contractDetails)
        {
            throw new NotImplementedException();
        }

        void EWrapper.bondContractDetails(ContractDetails contractDetails)
        {
            throw new NotImplementedException();
        }

        void EWrapper.execDetails(int orderId, Contract contract, Execution execution)
        {
            throw new NotImplementedException();
        }

        void EWrapper.updateMktDepth(int tickerId, int position, int operation, int side, double price, int size)
        {
            throw new NotImplementedException();
        }

        void EWrapper.updateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side,
                                     double price, int size)
        {
            throw new NotImplementedException();
        }

        void EWrapper.updateNewsBulletin(int msgId, int msgType, string message, string origExchange)
        {
            throw new NotImplementedException();
        }

        void EWrapper.managedAccounts(string accountsList)
        {
            throw new NotImplementedException();
        }

        void EWrapper.receiveFA(int faDataType, string xml)
        {
            throw new NotImplementedException();
        }

        void EWrapper.historicalData(int reqId, string date, double open, double high, double low, double close,
                                   int volume, int count, double WAP, bool hasGaps)
        {
            throw new NotImplementedException();
        }

        void EWrapper.scannerParameters(string xml)
        {
            throw new NotImplementedException();
        }

        void EWrapper.scannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark,
                                string projection, string legsStr)
        {
            throw new NotImplementedException();
        }

        void AnyWrapper.error(Exception e)
        {
            throw new NotImplementedException();
        }

        void AnyWrapper.error(string str)
        {
            throw new NotImplementedException();
        }

        void AnyWrapper.error(int id, int errorCode, string errorMsg)
        {
            throw new NotImplementedException();
        }

        void AnyWrapper.connectionClosed()
        {
            throw new NotImplementedException();
        }
    }
}
