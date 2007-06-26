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
        private readonly int id;
        public int Id
        {
            get
            {
                return id;
            }
        }
        private readonly int errorCode;
        public int ErrorCode
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

        public ErrorEventArgs(int id, int errorCode, string errorMsg)
        {
            this.id = id;
            this.errorMsg = errorMsg;
            this.errorCode = errorCode;
        }
    }
}
