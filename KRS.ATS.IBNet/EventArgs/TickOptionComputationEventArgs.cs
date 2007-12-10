using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Tick Option Computation Event Arguments
    /// </summary>
    public class TickOptionComputationEventArgs : EventArgs
    {
        private readonly double delta;
        private readonly double impliedVol;
        private readonly double modelPrice;
        private readonly double pvDividend;
        private readonly int tickerId;

        private readonly TickType tickType;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="tickerId">The ticker Id that was specified previously in the call to reqMktData().</param>
        /// <param name="tickType">Specifies the type of option computation.</param>
        /// <param name="impliedVol">The implied volatility calculated by the TWS option modeler, using the specificed ticktype value.</param>
        /// <param name="delta">The option delta calculated by the TWS option modeler.</param>
        /// <param name="modelPrice">The model price.</param>
        /// <param name="pvDividend">Present value of dividends expected on the option’s underlier.</param>
        public TickOptionComputationEventArgs(int tickerId, TickType tickType, double impliedVol, double delta,
                                              double modelPrice, double pvDividend)
        {
            this.tickerId = tickerId;
            this.pvDividend = pvDividend;
            this.delta = delta;
            this.modelPrice = modelPrice;
            this.impliedVol = impliedVol;
            this.tickType = tickType;
        }

        /// <summary>
        /// The ticker Id that was specified previously in the call to reqMktData().
        /// </summary>
        public int TickerId
        {
            get { return tickerId; }
        }

        /// <summary>
        /// Specifies the type of option computation.
        /// </summary>
        /// <seealso cref="TickType"/>
        public TickType TickType
        {
            get { return tickType; }
        }

        /// <summary>
        /// The implied volatility calculated by the TWS option modeler, using the specificed ticktype value.
        /// </summary>
        public double ImpliedVol
        {
            get { return impliedVol; }
        }

        /// <summary>
        /// The option delta calculated by the TWS option modeler.
        /// </summary>
        public double Delta
        {
            get { return delta; }
        }

        /// <summary>
        /// The model price.
        /// </summary>
        public double ModelPrice
        {
            get { return modelPrice; }
        }

        /// <summary>
        /// Present value of dividends expected on the option’s underlier.
        /// </summary>
        public double PVDividend
        {
            get { return pvDividend; }
        }
    }
}