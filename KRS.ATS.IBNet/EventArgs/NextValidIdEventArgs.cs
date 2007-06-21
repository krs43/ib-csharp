using System;
using System.Collections.Generic;
using System.Text;

namespace KRS.ATS.IBNet
{
    public class NextValidIdEventArgs
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
