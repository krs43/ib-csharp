/*
* EClientErrors.java
*
*/
using System;

namespace Krs.Ats.IBNet
{
    public class IBClientErrors
    {
        internal const int NO_VALID_ID = - 1;
		
        internal static readonly CodeMsgPair ALREADY_CONNECTED = new CodeMsgPair(501, "Already connected.");
        internal static readonly CodeMsgPair CONNECT_FAIL = new CodeMsgPair(502, "Couldn't connect to TWS.  Confirm that \"Enable ActiveX and Socket Clients\" is enabled on the TWS \"Configure->API\" menu.");
        internal static readonly CodeMsgPair UPDATE_TWS = new CodeMsgPair(503, "The TWS is out of date and must be upgraded.");
        internal static readonly CodeMsgPair NOT_CONNECTED = new CodeMsgPair(504, "Not connected");
        internal static readonly CodeMsgPair UNKNOWN_ID = new CodeMsgPair(505, "Fatal Error: Unknown message id.");
        internal static readonly CodeMsgPair FAIL_SEND_REQMKT = new CodeMsgPair(510, "Request Market Data Sending Error - ");
        internal static readonly CodeMsgPair FAIL_SEND_CANMKT = new CodeMsgPair(511, "Cancel Market Data Sending Error - ");
        internal static readonly CodeMsgPair FAIL_SEND_ORDER = new CodeMsgPair(512, "Order Sending Error - ");
        internal static readonly CodeMsgPair FAIL_SEND_ACCT = new CodeMsgPair(513, "Account Update Request Sending Error -");
        internal static readonly CodeMsgPair FAIL_SEND_EXEC = new CodeMsgPair(514, "Request For Executions Sending Error -");
        internal static readonly CodeMsgPair FAIL_SEND_CORDER = new CodeMsgPair(515, "Cancel Order Sending Error -");
        internal static readonly CodeMsgPair FAIL_SEND_OORDER = new CodeMsgPair(516, "Request Open Order Sending Error -");
        internal static readonly CodeMsgPair UNKNOWN_CONTRACT = new CodeMsgPair(517, "Unknown contract. Verify the contract details supplied.");
        internal static readonly CodeMsgPair FAIL_SEND_REQCONTRACT = new CodeMsgPair(518, "Request Contract Data Sending Error - ");
        internal static readonly CodeMsgPair FAIL_SEND_REQMKTDEPTH = new CodeMsgPair(519, "Request Market Depth Sending Error - ");
        internal static readonly CodeMsgPair FAIL_SEND_CANMKTDEPTH = new CodeMsgPair(520, "Cancel Market Depth Sending Error - ");
        internal static readonly CodeMsgPair FAIL_SEND_SERVER_LOG_LEVEL = new CodeMsgPair(521, "Set Server Log Level Sending Error - ");
        internal static readonly CodeMsgPair FAIL_SEND_FA_REQUEST = new CodeMsgPair(522, "FA Information Request Sending Error - ");
        internal static readonly CodeMsgPair FAIL_SEND_FA_REPLACE = new CodeMsgPair(523, "FA Information Replace Sending Error - ");
        internal static readonly CodeMsgPair FAIL_SEND_REQSCANNER = new CodeMsgPair(524, "Request Scanner Subscription Sending Error - ");
        internal static readonly CodeMsgPair FAIL_SEND_CANSCANNER = new CodeMsgPair(525, "Cancel Scanner Subscription Sending Error - ");
        internal static readonly CodeMsgPair FAIL_SEND_REQSCANNERPARAMETERS = new CodeMsgPair(526, "Request Scanner Parameter Sending Error - ");
        internal static readonly CodeMsgPair FAIL_SEND_REQHISTDATA = new CodeMsgPair(527, "Request Historical Data Sending Error - ");

        public class CodeMsgPair
        {
			
            ///////////////////////////////////////////////////////////////////
            // Public members
            ///////////////////////////////////////////////////////////////////
            internal int m_errorCode;
            internal String m_errorMsg;
			
            ///////////////////////////////////////////////////////////////////
            // Get/Set methods
            ///////////////////////////////////////////////////////////////////
            public virtual int code()
            {
                return m_errorCode;
            }
            public virtual String msg()
            {
                return m_errorMsg;
            }
			
            ///////////////////////////////////////////////////////////////////
            // Constructors
            ///////////////////////////////////////////////////////////////////
            /// <summary> </summary>
            public CodeMsgPair(int i, String errString)
            {
                m_errorCode = i;
                m_errorMsg = errString;
            }
        }
    }
}