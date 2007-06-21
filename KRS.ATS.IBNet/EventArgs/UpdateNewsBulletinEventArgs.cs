using System;
using System.Collections.Generic;
using System.Text;

namespace KRS.ATS.IBNet
{
    /// <summary>
    /// Update News Bulletin Event Arguments
    /// </summary>
    public class UpdateNewsBulletinEventArgs : EventArgs
    {
        private readonly int msgId;
        public int MsgId
        {
            get
            {
                return msgId;
            }
        }
        private readonly int msgType;
        public int MsgType
        {
            get
            {
                return msgType;
            }
        }
        private readonly string message;
        public string Message
        {
            get
            {
                return message;
            }
        }
        private readonly string origExchange;
        public string OrigExchange
        {
            get
            {
                return origExchange;
            }
        }

        public UpdateNewsBulletinEventArgs(int msgId, int msgType, string message, string origExchange)
        {
            this.msgId = msgId;
            this.origExchange = origExchange;
            this.message = message;
            this.msgType = msgType;
        }
    }
}
