using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// outgoing msg id's
    /// </summary>
    public enum OutgoingMessage
    {
         REQ_MKT_DATA = 1,
         CANCEL_MKT_DATA = 2,
         PLACE_ORDER = 3,
         CANCEL_ORDER = 4,
         REQ_OPEN_ORDERS = 5,
         REQ_ACCOUNT_DATA = 6,
         REQ_EXECUTIONS = 7,
         REQ_IDS = 8,
         REQ_CONTRACT_DATA = 9,
         REQ_MKT_DEPTH = 10,
         CANCEL_MKT_DEPTH = 11,
         REQ_NEWS_BULLETINS = 12,
         CANCEL_NEWS_BULLETINS = 13,
         SET_SERVER_LOGLEVEL = 14,
         REQ_AUTO_OPEN_ORDERS = 15,
         REQ_ALL_OPEN_ORDERS = 16,
         REQ_MANAGED_ACCTS = 17,
         REQ_FA = 18,
         REPLACE_FA = 19,
         REQ_HISTORICAL_DATA = 20,
         EXERCISE_OPTIONS = 21,
         REQ_SCANNER_SUBSCRIPTION = 22,
         CANCEL_SCANNER_SUBSCRIPTION = 23,
         REQ_SCANNER_PARAMETERS = 24,
         CANCEL_HISTORICAL_DATA = 25,
    }
}
