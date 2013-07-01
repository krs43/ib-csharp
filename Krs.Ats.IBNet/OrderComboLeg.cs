using System;
using System.Collections.Generic;
using System.Text;

/* file created June, 2013 - Shane Castle - shane.castle@vaultic.com */

namespace Krs.Ats.IBNet
{
    public class OrderComboLeg
    {
        public double Price { get; set; } // price per leg

        public OrderComboLeg()
        {
            Price = Double.MaxValue;
        }

        public OrderComboLeg(double price)
        {
            Price = price;
        }
    }
}
