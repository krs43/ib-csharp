using System;
using System.Collections.Generic;
using System.Text;

namespace KRS.ATS.IBNet
{
    /// <summary>
    /// Update Account Time Event Arguments
    /// </summary>
    public class UpdateAccountTimeEventArgs : EventArgs
    {
        private readonly string timeStamp;
        public string TimeStamp
        {
            get
            {
                return timeStamp;
            }
        }

        public UpdateAccountTimeEventArgs(string timeStamp)
        {
            this.timeStamp = timeStamp;
        }
    }
}
