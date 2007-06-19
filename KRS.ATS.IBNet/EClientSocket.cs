/*
* EClientSocket.java
*
*/
using System;
using KRS.ATS.IBNet;

namespace KRS.ATS.IBNet
{
    public class EClientSocket
    {
        virtual public bool Connected
        {
            get
            {
                return m_connected;
            }
			
        }
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'setServerLogLevel'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        virtual public int ServerLogLevel
        {
            set
            {
                lock (this)
                {
                    // not connected?
                    if (!m_connected)
                    {
                        error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                        return ;
                    }
					
                    //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                    int VERSION = 1;
					
                    // send the set server logging level message
                    try
                    {
                        send(SET_SERVER_LOGLEVEL);
                        send(VERSION);
                        send(value);
                    }
                    catch (System.Exception e)
                    {
                        //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                        error(EClientErrors.NO_VALID_ID, EClientErrors.FAIL_SEND_SERVER_LOG_LEVEL, "" + e);
                        close();
                    }
                }
            }
			
        }
		
        // Client version history
        //
        // 	6 = Added parentId to orderStatus
        // 	7 = The new execDetails event returned for an order filled status and reqExecDetails
        //     Also market depth is available.
        // 	8 = Added lastFillPrice to orderStatus() event and permId to execution details
        //  9 = Added 'averageCost', 'unrealizedPNL', and 'unrealizedPNL' to updatePortfolio event
        // 10 = Added 'serverId' to the 'open order' & 'order status' events.
        //      We send back all the API open orders upon connection.
        //      Added new methods reqAllOpenOrders, reqAutoOpenOrders()
        //      Added FA support - reqExecution has filter.
        //                       - reqAccountUpdates takes acct code.
        // 11 = Added permId to openOrder event.
        // 12 = requsting open order attributes ignoreRth, hidden, and discretionary
        // 13 = added goodAfterTime
        // 14 = always send size on bid/ask/last tick
        // 15 = send allocation description string on openOrder
        // 16 = can receive account name in account and portfolio updates, and fa params in openOrder
        // 17 = can receive liquidation field in exec reports, and notAutoAvailable field in mkt data
        // 18 = can receive good till date field in open order messages, and request intraday backfill
        // 19 = can receive rthOnly flag in ORDER_STATUS
        // 20 = expects TWS time string on connection after server version >= 20.
        // 21 = can receive bond contract details.
        // 22 = can receive price magnifier in version 2 contract details message
        // 23 = support for scanner
        // 24 = can receive volatility order parameters in open order messages
        // 25 = can receive HMDS query start and end times
        // 26 = can receive option vols in option market data messages
        // 27 = can receive delta neutral order type and delta neutral aux price in place order version 20: API 8.85
        // 28 = can receive option model computation ticks: API 8.9
        // 29 = can receive trail stop limit price in open order and can place them: API 8.91
        // 30 = can receive extended bond contract def, new ticks, and trade count in bars
        // 31 = can receive EFP extensions to scanner and market data, and combo legs on open orders 
		
        private const int CLIENT_VERSION = 31;
        private const int SERVER_VERSION = 1;
        //UPGRADE_NOTE: Final was removed from the declaration of 'EOL'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        private static readonly sbyte[] EOL = new sbyte[]{0};
        private const System.String BAG_SEC_TYPE = "BAG";
		
        // FA msg data types
        public const int GROUPS = 1;
        public const int PROFILES = 2;
        public const int ALIASES = 3;
		
        public static System.String faMsgTypeName(int faDataType)
        {
            switch (faDataType)
            {
				
                case GROUPS: 
                    return "GROUPS";
				
                case PROFILES: 
                    return "PROFILES";
				
                case ALIASES: 
                    return "ALIASES";
            }
            return null;
        }
		
        // outgoing msg id's
        private const int REQ_MKT_DATA = 1;
        private const int CANCEL_MKT_DATA = 2;
        private const int PLACE_ORDER = 3;
        private const int CANCEL_ORDER = 4;
        private const int REQ_OPEN_ORDERS = 5;
        private const int REQ_ACCOUNT_DATA = 6;
        private const int REQ_EXECUTIONS = 7;
        private const int REQ_IDS = 8;
        private const int REQ_CONTRACT_DATA = 9;
        private const int REQ_MKT_DEPTH = 10;
        private const int CANCEL_MKT_DEPTH = 11;
        private const int REQ_NEWS_BULLETINS = 12;
        private const int CANCEL_NEWS_BULLETINS = 13;
        private const int SET_SERVER_LOGLEVEL = 14;
        private const int REQ_AUTO_OPEN_ORDERS = 15;
        private const int REQ_ALL_OPEN_ORDERS = 16;
        private const int REQ_MANAGED_ACCTS = 17;
        private const int REQ_FA = 18;
        private const int REPLACE_FA = 19;
        private const int REQ_HISTORICAL_DATA = 20;
        private const int EXERCISE_OPTIONS = 21;
        private const int REQ_SCANNER_SUBSCRIPTION = 22;
        private const int CANCEL_SCANNER_SUBSCRIPTION = 23;
        private const int REQ_SCANNER_PARAMETERS = 24;
        private const int CANCEL_HISTORICAL_DATA = 25;
		
        private AnyWrapper m_anyWrapper; // msg handler
        private System.Net.Sockets.TcpClient m_socket; // the socket
        //UPGRADE_TODO: Class 'java.io.DataOutputStream' was converted to 'System.IO.BinaryWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataOutputStream'"
        private System.IO.BinaryWriter m_dos; // the socket output stream
        private bool m_connected; // true if we are connected
        private EReader m_reader; // thread which reads msgs from socket
        private int m_serverVersion = 1;
        private System.String m_TwsTime;
		
        public virtual int serverVersion()
        {
            return m_serverVersion;
        }
        public virtual System.String TwsConnectionTime()
        {
            return m_TwsTime;
        }
        public virtual AnyWrapper wrapper()
        {
            return m_anyWrapper;
        }
        public virtual EReader reader()
        {
            return m_reader;
        }
		
		
        public EClientSocket(AnyWrapper anyWrapper)
        {
            m_anyWrapper = anyWrapper;
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'eConnect'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  eConnect(System.String host, int port, int clientId)
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
                    eConnect(socket, clientId);
                }
                catch (System.Exception e)
                {
                    connectionError();
                }
            }
        }
		
        protected internal virtual void  connectionError()
        {
            m_anyWrapper.error(EClientErrors.NO_VALID_ID, EClientErrors.CONNECT_FAIL.code(), EClientErrors.CONNECT_FAIL.msg());
            m_reader = null;
        }
		
        protected internal virtual System.String checkConnected(System.String host)
        {
            if (m_connected)
            {
                m_anyWrapper.error(EClientErrors.NO_VALID_ID, EClientErrors.ALREADY_CONNECTED.code(), EClientErrors.ALREADY_CONNECTED.msg());
                return null;
            }
            if (isNull(host))
            {
                host = "127.0.0.1";
            }
            return host;
        }
		
        //UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
        public virtual EReader createReader(EClientSocket socket, System.IO.BinaryReader dis)
        {
            return new EReader(socket, dis);
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'eConnect'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  eConnect(System.Net.Sockets.TcpClient socket, int clientId)
        {
            lock (this)
            {
                m_socket = socket;
				
                // create io streams
                //UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
                System.IO.BinaryReader dis = new System.IO.BinaryReader(m_socket.GetStream());
                //UPGRADE_TODO: Class 'java.io.DataOutputStream' was converted to 'System.IO.BinaryWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataOutputStream'"
                m_dos = new System.IO.BinaryWriter(m_socket.GetStream());
				
                // set client version
                send(CLIENT_VERSION);
				
                // start reader thread
                m_reader = createReader(this, dis);
				
                // check server version
                m_serverVersion = m_reader.readInt();
                System.Console.Out.WriteLine("Server Version:" + m_serverVersion);
                if (m_serverVersion >= 20)
                {
                    m_TwsTime = m_reader.readStr();
                    System.Console.Out.WriteLine("TWS Time at connection:" + m_TwsTime);
                }
                if (m_serverVersion < SERVER_VERSION)
                {
                    m_anyWrapper.error(EClientErrors.NO_VALID_ID, EClientErrors.UPDATE_TWS.code(), EClientErrors.UPDATE_TWS.msg());
                    return ;
                }
				
                // Send the client id
                if (m_serverVersion >= 3)
                {
                    send(clientId);
                }
				
                m_reader.Start();
				
                // set connected flag
                m_connected = true;
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'eDisconnect'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  eDisconnect()
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    return ;
                }
				
                try
                {
                    // stop reader thread
                    if (m_reader != null)
                    {
                        m_reader.Interrupt();
                    }
					
                    // close socket
                    if (m_socket != null)
                    {
                        m_socket.Close();
                    }
                }
                catch (System.Exception e)
                {
                }
				
                m_connected = false;
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'cancelScannerSubscription'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  cancelScannerSubscription(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                if (m_serverVersion < 24)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.UPDATE_TWS, "  It does not support API scanner subscription.");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 1;
				
                // send cancel mkt data msg
                try
                {
                    send(CANCEL_SCANNER_SUBSCRIPTION);
                    send(VERSION);
                    send(tickerId);
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(tickerId, EClientErrors.FAIL_SEND_CANSCANNER, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'reqScannerParameters'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  reqScannerParameters()
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                if (m_serverVersion < 24)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.UPDATE_TWS, "  It does not support API scanner subscription.");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 1;
				
                try
                {
                    send(REQ_SCANNER_PARAMETERS);
                    send(VERSION);
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(EClientErrors.NO_VALID_ID, EClientErrors.FAIL_SEND_REQSCANNERPARAMETERS, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'reqScannerSubscription'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  reqScannerSubscription(int tickerId, ScannerSubscription subscription)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                if (m_serverVersion < 24)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.UPDATE_TWS, "  It does not support API scanner subscription.");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 3;
				
                try
                {
                    send(REQ_SCANNER_SUBSCRIPTION);
                    send(VERSION);
                    send(tickerId);
                    sendMax(subscription.numberOfRows());
                    send(subscription.instrument());
                    send(subscription.locationCode());
                    send(subscription.scanCode());
                    sendMax(subscription.abovePrice());
                    sendMax(subscription.belowPrice());
                    sendMax(subscription.aboveVolume());
                    sendMax(subscription.marketCapAbove());
                    sendMax(subscription.marketCapBelow());
                    send(subscription.moodyRatingAbove());
                    send(subscription.moodyRatingBelow());
                    send(subscription.spRatingAbove());
                    send(subscription.spRatingBelow());
                    send(subscription.maturityDateAbove());
                    send(subscription.maturityDateBelow());
                    sendMax(subscription.couponRateAbove());
                    sendMax(subscription.couponRateBelow());
                    send(subscription.excludeConvertible());
                    if (m_serverVersion >= 25)
                    {
                        send(subscription.averageOptionVolumeAbove());
                        send(subscription.scannerSettingPairs());
                    }
                    if (m_serverVersion >= 27)
                    {
                        send(subscription.stockTypeFilter());
                    }
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(tickerId, EClientErrors.FAIL_SEND_REQSCANNER, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'reqMktData'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  reqMktData(int tickerId, Contract contract, System.String genericTickList)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 6;
				
                try
                {
                    // send req mkt data msg
                    send(REQ_MKT_DATA);
                    send(VERSION);
                    send(tickerId);
					
                    send(contract.m_symbol);
                    send(contract.m_secType);
                    send(contract.m_expiry);
                    send(contract.m_strike);
                    send(contract.m_right);
                    if (m_serverVersion >= 15)
                    {
                        send(contract.m_multiplier);
                    }
                    send(contract.m_exchange);
                    if (m_serverVersion >= 14)
                    {
                        send(contract.m_primaryExch);
                    }
                    send(contract.m_currency);
                    if (m_serverVersion >= 2)
                    {
                        send(contract.m_localSymbol);
                    }
                    if (m_serverVersion >= 8 && BAG_SEC_TYPE.ToUpper().Equals(contract.m_secType.ToUpper()))
                    {
                        if (contract.m_comboLegs == null)
                        {
                            send(0);
                        }
                        else
                        {
                            send(contract.m_comboLegs.Count);
							
                            ComboLeg comboLeg;
                            for (int i = 0; i < contract.m_comboLegs.Count; i++)
                            {
                                comboLeg = (ComboLeg) contract.m_comboLegs[i];
                                send(comboLeg.m_conId);
                                send(comboLeg.m_ratio);
                                send(comboLeg.m_action);
                                send(comboLeg.m_exchange);
                            }
                        }
                    }
                    if (m_serverVersion >= 31)
                    {
                        send(genericTickList);
                    }
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(tickerId, EClientErrors.FAIL_SEND_REQMKT, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'cancelHistoricalData'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  cancelHistoricalData(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                if (m_serverVersion < 24)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.UPDATE_TWS, "  It does not support historical data query cancellation.");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 1;
				
                // send cancel mkt data msg
                try
                {
                    send(CANCEL_HISTORICAL_DATA);
                    send(VERSION);
                    send(tickerId);
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(tickerId, EClientErrors.FAIL_SEND_CANSCANNER, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'reqHistoricalData'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  reqHistoricalData(int tickerId, Contract contract, System.String endDateTime, System.String durationStr, System.String barSizeSetting, System.String whatToShow, int useRTH, int formatDate)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(tickerId, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 4;
				
                try
                {
                    if (m_serverVersion < 16)
                    {
                        error(EClientErrors.NO_VALID_ID, EClientErrors.UPDATE_TWS, "  It does not support historical data backfill.");
                        return ;
                    }
					
                    send(REQ_HISTORICAL_DATA);
                    send(VERSION);
                    send(tickerId);
                    send(contract.m_symbol);
                    send(contract.m_secType);
                    send(contract.m_expiry);
                    send(contract.m_strike);
                    send(contract.m_right);
                    send(contract.m_multiplier);
                    send(contract.m_exchange);
                    send(contract.m_primaryExch);
                    send(contract.m_currency);
                    send(contract.m_localSymbol);
                    if (m_serverVersion >= 31)
                    {
                        send(contract.m_includeExpired?1:0);
                    }
                    if (m_serverVersion >= 20)
                    {
                        send(endDateTime);
                        send(barSizeSetting);
                    }
                    send(durationStr);
                    send(useRTH);
                    send(whatToShow);
                    if (m_serverVersion > 16)
                    {
                        send(formatDate);
                    }
                    if (BAG_SEC_TYPE.ToUpper().Equals(contract.m_secType.ToUpper()))
                    {
                        if (contract.m_comboLegs == null)
                        {
                            send(0);
                        }
                        else
                        {
                            send(contract.m_comboLegs.Count);
							
                            ComboLeg comboLeg;
                            for (int i = 0; i < contract.m_comboLegs.Count; i++)
                            {
                                comboLeg = (ComboLeg) contract.m_comboLegs[i];
                                send(comboLeg.m_conId);
                                send(comboLeg.m_ratio);
                                send(comboLeg.m_action);
                                send(comboLeg.m_exchange);
                            }
                        }
                    }
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(tickerId, EClientErrors.FAIL_SEND_REQHISTDATA, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'reqContractDetails'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  reqContractDetails(Contract contract)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                // This feature is only available for versions of TWS >=4
                if (m_serverVersion < 4)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.UPDATE_TWS.code(), EClientErrors.UPDATE_TWS.msg());
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 3;
				
                try
                {
                    // send req mkt data msg
                    send(REQ_CONTRACT_DATA);
                    send(VERSION);
					
                    send(contract.m_symbol);
                    send(contract.m_secType);
                    send(contract.m_expiry);
                    send(contract.m_strike);
                    send(contract.m_right);
                    if (m_serverVersion >= 15)
                    {
                        send(contract.m_multiplier);
                    }
                    send(contract.m_exchange);
                    send(contract.m_currency);
                    send(contract.m_localSymbol);
                    if (m_serverVersion >= 31)
                    {
                        send(contract.m_includeExpired);
                    }
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(EClientErrors.NO_VALID_ID, EClientErrors.FAIL_SEND_REQCONTRACT, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'reqMktDepth'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  reqMktDepth(int tickerId, Contract contract, int numRows)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                // This feature is only available for versions of TWS >=6
                if (m_serverVersion < 6)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.UPDATE_TWS.code(), EClientErrors.UPDATE_TWS.msg());
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 3;
				
                try
                {
                    // send req mkt data msg
                    send(REQ_MKT_DEPTH);
                    send(VERSION);
                    send(tickerId);
					
                    send(contract.m_symbol);
                    send(contract.m_secType);
                    send(contract.m_expiry);
                    send(contract.m_strike);
                    send(contract.m_right);
                    if (m_serverVersion >= 15)
                    {
                        send(contract.m_multiplier);
                    }
                    send(contract.m_exchange);
                    send(contract.m_currency);
                    send(contract.m_localSymbol);
                    if (m_serverVersion >= 19)
                    {
                        send(numRows);
                    }
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(tickerId, EClientErrors.FAIL_SEND_REQMKTDEPTH, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'cancelMktData'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  cancelMktData(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 1;
				
                // send cancel mkt data msg
                try
                {
                    send(CANCEL_MKT_DATA);
                    send(VERSION);
                    send(tickerId);
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(tickerId, EClientErrors.FAIL_SEND_CANMKT, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'cancelMktDepth'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  cancelMktDepth(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                // This feature is only available for versions of TWS >=6
                if (m_serverVersion < 6)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.UPDATE_TWS.code(), EClientErrors.UPDATE_TWS.msg());
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 1;
				
                // send cancel mkt data msg
                try
                {
                    send(CANCEL_MKT_DEPTH);
                    send(VERSION);
                    send(tickerId);
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(tickerId, EClientErrors.FAIL_SEND_CANMKTDEPTH, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'exerciseOptions'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  exerciseOptions(int tickerId, Contract contract, int exerciseAction, int exerciseQuantity, System.String account, int override_Renamed)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(tickerId, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 1;
				
                try
                {
                    if (m_serverVersion < 21)
                    {
                        error(EClientErrors.NO_VALID_ID, EClientErrors.UPDATE_TWS, "  It does not support options exercise from the API.");
                        return ;
                    }
					
                    send(EXERCISE_OPTIONS);
                    send(VERSION);
                    send(tickerId);
                    send(contract.m_symbol);
                    send(contract.m_secType);
                    send(contract.m_expiry);
                    send(contract.m_strike);
                    send(contract.m_right);
                    send(contract.m_multiplier);
                    send(contract.m_exchange);
                    send(contract.m_currency);
                    send(contract.m_localSymbol);
                    send(exerciseAction);
                    send(exerciseQuantity);
                    send(account);
                    send(override_Renamed);
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(tickerId, EClientErrors.FAIL_SEND_REQMKT, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'placeOrder'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  placeOrder(int id, Contract contract, Order order)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 21;
				
                // send place order msg
                try
                {
                    send(PLACE_ORDER);
                    send(VERSION);
                    send(id);
					
                    // send contract fields
                    send(contract.m_symbol);
                    send(contract.m_secType);
                    send(contract.m_expiry);
                    send(contract.m_strike);
                    send(contract.m_right);
                    if (m_serverVersion >= 15)
                    {
                        send(contract.m_multiplier);
                    }
                    send(contract.m_exchange);
                    if (m_serverVersion >= 14)
                    {
                        send(contract.m_primaryExch);
                    }
                    send(contract.m_currency);
                    if (m_serverVersion >= 2)
                    {
                        send(contract.m_localSymbol);
                    }
					
                    // send main order fields
                    send(order.m_action);
                    send(order.m_totalQuantity);
                    send(order.m_orderType);
                    send(order.m_lmtPrice);
                    send(order.m_auxPrice);
					
                    // send extended order fields
                    send(order.m_tif);
                    send(order.m_ocaGroup);
                    send(order.m_account);
                    send(order.m_openClose);
                    send(order.m_origin);
                    send(order.m_orderRef);
                    send(order.m_transmit);
                    if (m_serverVersion >= 4)
                    {
                        send(order.m_parentId);
                    }
					
                    if (m_serverVersion >= 5)
                    {
                        send(order.m_blockOrder);
                        send(order.m_sweepToFill);
                        send(order.m_displaySize);
                        send(order.m_triggerMethod);
                        send(order.m_ignoreRth);
                    }
					
                    if (m_serverVersion >= 7)
                    {
                        send(order.m_hidden);
                    }
					
                    // Send combo legs for BAG requests
                    if (m_serverVersion >= 8 && BAG_SEC_TYPE.ToUpper().Equals(contract.m_secType.ToUpper()))
                    {
                        if (contract.m_comboLegs == null)
                        {
                            send(0);
                        }
                        else
                        {
                            send(contract.m_comboLegs.Count);
							
                            ComboLeg comboLeg;
                            for (int i = 0; i < contract.m_comboLegs.Count; i++)
                            {
                                comboLeg = (ComboLeg) contract.m_comboLegs[i];
                                send(comboLeg.m_conId);
                                send(comboLeg.m_ratio);
                                send(comboLeg.m_action);
                                send(comboLeg.m_exchange);
                                send(comboLeg.m_openClose);
                            }
                        }
                    }
					
                    if (m_serverVersion >= 9)
                    {
                        send(order.m_sharesAllocation); // deprecated
                    }
					
                    if (m_serverVersion >= 10)
                    {
                        send(order.m_discretionaryAmt);
                    }
					
                    if (m_serverVersion >= 11)
                    {
                        send(order.m_goodAfterTime);
                    }
					
                    if (m_serverVersion >= 12)
                    {
                        send(order.m_goodTillDate);
                    }
					
                    if (m_serverVersion >= 13)
                    {
                        send(order.m_faGroup);
                        send(order.m_faMethod);
                        send(order.m_faPercentage);
                        send(order.m_faProfile);
                    }
                    if (m_serverVersion >= 18)
                    {
                        // institutional short sale slot fields.
                        send(order.m_shortSaleSlot); // 0 only for retail, 1 or 2 only for institution.
                        send(order.m_designatedLocation); // only populate when order.m_shortSaleSlot = 2.
                    }
                    if (m_serverVersion >= 19)
                    {
                        send(order.m_ocaType);
                        send(order.m_rthOnly);
                        send(order.m_rule80A);
                        send(order.m_settlingFirm);
                        send(order.m_allOrNone);
                        sendMax(order.m_minQty);
                        sendMax(order.m_percentOffset);
                        send(order.m_eTradeOnly);
                        send(order.m_firmQuoteOnly);
                        sendMax(order.m_nbboPriceCap);
                        sendMax(order.m_auctionStrategy);
                        sendMax(order.m_startingPrice);
                        sendMax(order.m_stockRefPrice);
                        sendMax(order.m_delta);
                        // Volatility orders had specific watermark price attribs in server version 26
                        //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                        double lower = (m_serverVersion == 26 && order.m_orderType.Equals("VOL"))?System.Double.MaxValue:order.m_stockRangeLower;
                        //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                        double upper = (m_serverVersion == 26 && order.m_orderType.Equals("VOL"))?System.Double.MaxValue:order.m_stockRangeUpper;
                        sendMax(lower);
                        sendMax(upper);
                    }
					
                    if (m_serverVersion >= 22)
                    {
                        send(order.m_overridePercentageConstraints);
                    }
					
                    if (m_serverVersion >= 26)
                    {
                        // Volatility orders
                        sendMax(order.m_volatility);
                        sendMax(order.m_volatilityType);
                        if (m_serverVersion < 28)
                        {
                            send(order.m_deltaNeutralOrderType.ToUpper().Equals("MKT".ToUpper()));
                        }
                        else
                        {
                            send(order.m_deltaNeutralOrderType);
                            sendMax(order.m_deltaNeutralAuxPrice);
                        }
                        send(order.m_continuousUpdate);
                        if (m_serverVersion == 26)
                        {
                            // Volatility orders had specific watermark price attribs in server version 26
                            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                            double lower = order.m_orderType.Equals("VOL")?order.m_stockRangeLower:System.Double.MaxValue;
                            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                            double upper = order.m_orderType.Equals("VOL")?order.m_stockRangeUpper:System.Double.MaxValue;
                            sendMax(lower);
                            sendMax(upper);
                        }
                        sendMax(order.m_referencePriceType);
                    }
					
                    if (m_serverVersion >= 30)
                    {
                        // TRAIL_STOP_LIMIT stop price
                        sendMax(order.m_trailStopPrice);
                    }
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(id, EClientErrors.FAIL_SEND_ORDER, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'reqAccountUpdates'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  reqAccountUpdates(bool subscribe, System.String acctCode)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 2;
				
                // send cancel order msg
                try
                {
                    send(REQ_ACCOUNT_DATA);
                    send(VERSION);
                    send(subscribe);
					
                    // Send the account code. This will only be used for FA clients
                    if (m_serverVersion >= 9)
                    {
                        send(acctCode);
                    }
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(EClientErrors.NO_VALID_ID, EClientErrors.FAIL_SEND_ACCT, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'reqExecutions'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  reqExecutions(ExecutionFilter filter)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 2;
				
                // send cancel order msg
                try
                {
                    send(REQ_EXECUTIONS);
                    send(VERSION);
					
                    // Send the execution rpt filter data
                    if (m_serverVersion >= 9)
                    {
                        send(filter.m_clientId);
                        send(filter.m_acctCode);
						
                        // Note that the valid format for m_time is "yyyymmdd-hh:mm:ss"
                        send(filter.m_time);
                        send(filter.m_symbol);
                        send(filter.m_secType);
                        send(filter.m_exchange);
                        send(filter.m_side);
                    }
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(EClientErrors.NO_VALID_ID, EClientErrors.FAIL_SEND_EXEC, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'cancelOrder'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  cancelOrder(int id)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 1;
				
                // send cancel order msg
                try
                {
                    send(CANCEL_ORDER);
                    send(VERSION);
                    send(id);
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(id, EClientErrors.FAIL_SEND_CORDER, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'reqOpenOrders'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  reqOpenOrders()
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 1;
				
                // send cancel order msg
                try
                {
                    send(REQ_OPEN_ORDERS);
                    send(VERSION);
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(EClientErrors.NO_VALID_ID, EClientErrors.FAIL_SEND_OORDER, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'reqIds'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  reqIds(int numIds)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 1;
				
                try
                {
                    send(REQ_IDS);
                    send(VERSION);
                    send(numIds);
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(EClientErrors.NO_VALID_ID, EClientErrors.FAIL_SEND_CORDER, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'reqNewsBulletins'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  reqNewsBulletins(bool allMsgs)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 1;
				
                try
                {
                    send(REQ_NEWS_BULLETINS);
                    send(VERSION);
                    send(allMsgs);
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(EClientErrors.NO_VALID_ID, EClientErrors.FAIL_SEND_CORDER, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'cancelNewsBulletins'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  cancelNewsBulletins()
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 1;
				
                // send cancel order msg
                try
                {
                    send(CANCEL_NEWS_BULLETINS);
                    send(VERSION);
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(EClientErrors.NO_VALID_ID, EClientErrors.FAIL_SEND_CORDER, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'reqAutoOpenOrders'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  reqAutoOpenOrders(bool bAutoBind)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 1;
				
                // send req open orders msg
                try
                {
                    send(REQ_AUTO_OPEN_ORDERS);
                    send(VERSION);
                    send(bAutoBind);
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(EClientErrors.NO_VALID_ID, EClientErrors.FAIL_SEND_OORDER, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'reqAllOpenOrders'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  reqAllOpenOrders()
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 1;
				
                // send req all open orders msg
                try
                {
                    send(REQ_ALL_OPEN_ORDERS);
                    send(VERSION);
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(EClientErrors.NO_VALID_ID, EClientErrors.FAIL_SEND_OORDER, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'reqManagedAccts'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  reqManagedAccts()
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 1;
				
                // send req FA managed accounts msg
                try
                {
                    send(REQ_MANAGED_ACCTS);
                    send(VERSION);
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(EClientErrors.NO_VALID_ID, EClientErrors.FAIL_SEND_OORDER, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'requestFA'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  requestFA(int faDataType)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                // This feature is only available for versions of TWS >= 13
                if (m_serverVersion < 13)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.UPDATE_TWS.code(), EClientErrors.UPDATE_TWS.msg());
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 1;
				
                try
                {
                    send(REQ_FA);
                    send(VERSION);
                    send(faDataType);
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(faDataType, EClientErrors.FAIL_SEND_FA_REQUEST, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'replaceFA'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        public virtual void  replaceFA(int faDataType, System.String xml)
        {
            lock (this)
            {
                // not connected?
                if (!m_connected)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.NOT_CONNECTED, "");
                    return ;
                }
				
                // This feature is only available for versions of TWS >= 13
                if (m_serverVersion < 13)
                {
                    error(EClientErrors.NO_VALID_ID, EClientErrors.UPDATE_TWS.code(), EClientErrors.UPDATE_TWS.msg());
                    return ;
                }
				
                //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
                int VERSION = 1;
				
                try
                {
                    send(REPLACE_FA);
                    send(VERSION);
                    send(faDataType);
                    send(xml);
                }
                catch (System.Exception e)
                {
                    //UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
                    error(faDataType, EClientErrors.FAIL_SEND_FA_REPLACE, "" + e);
                    close();
                }
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'error'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        protected internal virtual void  error(System.String err)
        {
            lock (this)
            {
                m_anyWrapper.error(err);
            }
        }
		
        //UPGRADE_NOTE: Synchronized keyword was removed from method 'error'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
        protected internal virtual void  error(int id, int errorCode, System.String errorMsg)
        {
            lock (this)
            {
                m_anyWrapper.error(id, errorCode, errorMsg);
            }
        }
		
        protected internal virtual void  close()
        {
            eDisconnect();
            m_anyWrapper.connectionClosed();
        }
		
        private static bool is_Renamed(System.String str)
        {
            // return true if the string is not empty
            return str != null && str.Length > 0;
        }
		
        private static bool isNull(System.String str)
        {
            // return true if the string is null or empty
            return !is_Renamed(str);
        }
		
        private void  error(int id, EClientErrors.CodeMsgPair pair, System.String tail)
        {
            error(id, pair.code(), pair.msg() + tail);
        }
		
        protected internal virtual void  send(System.String str)
        {
            // write string to data buffer; writer thread will
            // write it to socket
            if (str != null)
            {
                m_dos.Write(SupportClass.ToByteArray(str));
            }
            sendEOL();
        }
		
        private void  sendEOL()
        {
            m_dos.Write(SupportClass.ToByteArray(EOL));
        }
		
        protected internal virtual void  send(int val)
        {
            send(System.Convert.ToString(val));
        }
		
        protected internal virtual void  send(char val)
        {
            m_dos.Write((System.Byte) val);
            sendEOL();
        }
		
        protected internal virtual void  send(double val)
        {
            send(System.Convert.ToString(val));
        }
		
        protected internal virtual void  send(long val)
        {
            send(System.Convert.ToString(val));
        }
		
        private void  sendMax(double val)
        {
            //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
            if (val == System.Double.MaxValue)
            {
                sendEOL();
            }
            else
            {
                send(System.Convert.ToString(val));
            }
        }
		
        private void  sendMax(int val)
        {
            if (val == System.Int32.MaxValue)
            {
                sendEOL();
            }
            else
            {
                send(System.Convert.ToString(val));
            }
        }
		
        protected internal virtual void  send(bool val)
        {
            send(val?1:0);
        }
    }
}