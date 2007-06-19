/*
* EClientErrors.java
*
*/
using System;

namespace KRS.ATS.IBNet
{
    public class EClientErrors
    {
        internal const int NO_VALID_ID = - 1;
		
        //UPGRADE_NOTE: Final was removed from the declaration of 'ALREADY_CONNECTED '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair ALREADY_CONNECTED = new CodeMsgPair(501, "Already connected.");
        //UPGRADE_NOTE: Final was removed from the declaration of 'CONNECT_FAIL '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair CONNECT_FAIL = new CodeMsgPair(502, "Couldn't connect to TWS.  Confirm that \"Enable ActiveX and Socket Clients\" is enabled on the TWS \"Configure->API\" menu.");
        //UPGRADE_NOTE: Final was removed from the declaration of 'UPDATE_TWS '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair UPDATE_TWS = new CodeMsgPair(503, "The TWS is out of date and must be upgraded.");
        //UPGRADE_NOTE: Final was removed from the declaration of 'NOT_CONNECTED '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair NOT_CONNECTED = new CodeMsgPair(504, "Not connected");
        //UPGRADE_NOTE: Final was removed from the declaration of 'UNKNOWN_ID '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair UNKNOWN_ID = new CodeMsgPair(505, "Fatal Error: Unknown message id.");
        //UPGRADE_NOTE: Final was removed from the declaration of 'FAIL_SEND_REQMKT '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair FAIL_SEND_REQMKT = new CodeMsgPair(510, "Request Market Data Sending Error - ");
        //UPGRADE_NOTE: Final was removed from the declaration of 'FAIL_SEND_CANMKT '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair FAIL_SEND_CANMKT = new CodeMsgPair(511, "Cancel Market Data Sending Error - ");
        //UPGRADE_NOTE: Final was removed from the declaration of 'FAIL_SEND_ORDER '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair FAIL_SEND_ORDER = new CodeMsgPair(512, "Order Sending Error - ");
        //UPGRADE_NOTE: Final was removed from the declaration of 'FAIL_SEND_ACCT '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair FAIL_SEND_ACCT = new CodeMsgPair(513, "Account Update Request Sending Error -");
        //UPGRADE_NOTE: Final was removed from the declaration of 'FAIL_SEND_EXEC '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair FAIL_SEND_EXEC = new CodeMsgPair(514, "Request For Executions Sending Error -");
        //UPGRADE_NOTE: Final was removed from the declaration of 'FAIL_SEND_CORDER '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair FAIL_SEND_CORDER = new CodeMsgPair(515, "Cancel Order Sending Error -");
        //UPGRADE_NOTE: Final was removed from the declaration of 'FAIL_SEND_OORDER '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair FAIL_SEND_OORDER = new CodeMsgPair(516, "Request Open Order Sending Error -");
        //UPGRADE_NOTE: Final was removed from the declaration of 'UNKNOWN_CONTRACT '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair UNKNOWN_CONTRACT = new CodeMsgPair(517, "Unknown contract. Verify the contract details supplied.");
        //UPGRADE_NOTE: Final was removed from the declaration of 'FAIL_SEND_REQCONTRACT '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair FAIL_SEND_REQCONTRACT = new CodeMsgPair(518, "Request Contract Data Sending Error - ");
        //UPGRADE_NOTE: Final was removed from the declaration of 'FAIL_SEND_REQMKTDEPTH '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair FAIL_SEND_REQMKTDEPTH = new CodeMsgPair(519, "Request Market Depth Sending Error - ");
        //UPGRADE_NOTE: Final was removed from the declaration of 'FAIL_SEND_CANMKTDEPTH '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair FAIL_SEND_CANMKTDEPTH = new CodeMsgPair(520, "Cancel Market Depth Sending Error - ");
        //UPGRADE_NOTE: Final was removed from the declaration of 'FAIL_SEND_SERVER_LOG_LEVEL '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair FAIL_SEND_SERVER_LOG_LEVEL = new CodeMsgPair(521, "Set Server Log Level Sending Error - ");
        //UPGRADE_NOTE: Final was removed from the declaration of 'FAIL_SEND_FA_REQUEST '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair FAIL_SEND_FA_REQUEST = new CodeMsgPair(522, "FA Information Request Sending Error - ");
        //UPGRADE_NOTE: Final was removed from the declaration of 'FAIL_SEND_FA_REPLACE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair FAIL_SEND_FA_REPLACE = new CodeMsgPair(523, "FA Information Replace Sending Error - ");
        //UPGRADE_NOTE: Final was removed from the declaration of 'FAIL_SEND_REQSCANNER '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair FAIL_SEND_REQSCANNER = new CodeMsgPair(524, "Request Scanner Subscription Sending Error - ");
        //UPGRADE_NOTE: Final was removed from the declaration of 'FAIL_SEND_CANSCANNER '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair FAIL_SEND_CANSCANNER = new CodeMsgPair(525, "Cancel Scanner Subscription Sending Error - ");
        //UPGRADE_NOTE: Final was removed from the declaration of 'FAIL_SEND_REQSCANNERPARAMETERS '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair FAIL_SEND_REQSCANNERPARAMETERS = new CodeMsgPair(526, "Request Scanner Parameter Sending Error - ");
        //UPGRADE_NOTE: Final was removed from the declaration of 'FAIL_SEND_REQHISTDATA '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        internal static readonly CodeMsgPair FAIL_SEND_REQHISTDATA = new CodeMsgPair(527, "Request Historical Data Sending Error - ");
		
        public EClientErrors()
        {
        }
		
        public class CodeMsgPair
        {
			
            ///////////////////////////////////////////////////////////////////
            // Public members
            ///////////////////////////////////////////////////////////////////
            internal int m_errorCode;
            internal System.String m_errorMsg;
			
            ///////////////////////////////////////////////////////////////////
            // Get/Set methods
            ///////////////////////////////////////////////////////////////////
            public virtual int code()
            {
                return m_errorCode;
            }
            public virtual System.String msg()
            {
                return m_errorMsg;
            }
			
            ///////////////////////////////////////////////////////////////////
            // Constructors
            ///////////////////////////////////////////////////////////////////
            /// <summary> </summary>
            public CodeMsgPair(int i, System.String errString)
            {
                m_errorCode = i;
                m_errorMsg = errString;
            }
        }
    }
}