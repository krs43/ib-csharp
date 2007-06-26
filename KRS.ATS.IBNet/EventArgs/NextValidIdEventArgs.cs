using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    public class NextValidIdEventArgs : EventArgs
    {
        private readonly int orderId;

        public int OrderId
        {
            get
            {
                return orderId;
            }
        }

        public NextValidIdEventArgs(int orderId)
        {
            this.orderId = orderId;
        }
    }
}
