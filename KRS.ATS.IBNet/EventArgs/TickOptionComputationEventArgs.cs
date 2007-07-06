using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Tick Option Computation Event Arguments
    /// </summary>
    public class TickOptionComputationEventArgs : EventArgs
    {
        private readonly int tickerId;
        /// <summary>
        /// The ticker Id that was specified previously in the call to reqMktData().
        /// </summary>
        public int TickerId
        {
            get
            {
                return tickerId;
            }
        }

        private readonly TickType tickType;
        /// <summary>
        /// Specifies the type of option computation.
        /// </summary>
        /// <seealso cref="TickType"/>
        public TickType TickType
        {
            get
            {
                return tickType;
            }
        }

        private readonly double impliedVol;
        /// <summary>
        /// The implied volatility calculated by the TWS option modeler, using the specificed ticktype value.
        /// </summary>
        public double ImpliedVol
        {
            get
            {
                return impliedVol;
            }
        }

        private readonly double delta;
        /// <summary>
        /// The option delta calculated by the TWS option modeler.
        /// </summary>
        public double Delta
        {
            get
            {
                return delta;
            }
        }

        private readonly double modelPrice;
        /// <summary>
        /// The model price.
        /// </summary>
        public double ModelPrice
        {
            get
            {
                return modelPrice;
            }
        }

        private readonly double pvDividend;
        /// <summary>
        /// Present value of dividends expected on the option’s underlier.
        /// </summary>
        public double PVDividend
        {
            get
            {
                return pvDividend;
            }
        }

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="tickerId">The ticker Id that was specified previously in the call to reqMktData().</param>
        /// <param name="tickType">Specifies the type of option computation.</param>
        /// <param name="impliedVol">The implied volatility calculated by the TWS option modeler, using the specificed ticktype value.</param>
        /// <param name="delta">The option delta calculated by the TWS option modeler.</param>
        /// <param name="modelPrice">The model price.</param>
        /// <param name="pvDividend">Present value of dividends expected on the option’s underlier.</param>
        public TickOptionComputationEventArgs(int tickerId, TickType tickType, double impliedVol, double delta, double modelPrice, double pvDividend)
        {
            this.tickerId = tickerId;
            this.pvDividend = pvDividend;
            this.delta = delta;
            this.modelPrice = modelPrice;
            this.impliedVol = impliedVol;
            this.tickType = tickType;
        }
    }
}
