using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Error Event Arguments
    /// </summary>
    public class ErrorEventArgs : EventArgs
    {
        private readonly int tickerId;
        public int TickerId
        {
            get
            {
                return tickerId;
            }
        }
        private readonly ErrorMessage errorCode;
        public ErrorMessage ErrorCode
        {
            get
            {
                return errorCode;
            }
        }
        private readonly string errorMsg;
        public string ErrorMsg
        {
            get
            {
                return errorMsg;
            }
        }

        public ErrorEventArgs(int tickerId, ErrorMessage errorCode, string errorMsg)
        {
            this.tickerId = tickerId;
            this.errorMsg = errorMsg;
            this.errorCode = errorCode;
        }
    }
}
