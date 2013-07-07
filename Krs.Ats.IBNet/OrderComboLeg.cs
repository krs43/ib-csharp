using System;
using System.Collections.Generic;
using System.Text;

/* file created June, 2013 - Shane Castle - shane.castle@vaultic.com */

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Order Combo Leg.
    /// </summary>
    public class OrderComboLeg
    {
        /// <summary>
        /// The price per leg.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Default constructor. Defaults price to double.MaxValue.
        /// </summary>
        public OrderComboLeg()
        {
            Price = Double.MaxValue;
        }

        /// <summary>
        /// Constructor. Accepts price.
        /// </summary>
        /// <param name="price">The Order ComboLeg price.</param>
        public OrderComboLeg(double price)
        {
            Price = price;
        }
    }
}
