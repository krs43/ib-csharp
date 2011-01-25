using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Tick Option Computation Event Arguments
	/// </summary>
	[Serializable()]
	public class TickOptionComputationEventArgs : EventArgs
	{
		private readonly double delta;
		private readonly double impliedVol;
		private readonly double optionPrice;
		private readonly double pvDividend;
		private readonly int tickerId;
		private readonly double gamma;
		private readonly double vega;
		private readonly double theta;
		private readonly double underlyingPrice;

		private readonly TickType tickType;

		/// <summary>
		/// Full Constructor
		/// </summary>
		/// <param name="tickerId">The ticker Id that was specified previously in the call to reqMktData().</param>
		/// <param name="tickType">Specifies the type of option computation.</param>
		/// <param name="impliedVol">The implied volatility calculated by the TWS option modeler, using the specificed ticktype value.</param>
		/// <param name="delta">The option delta calculated by the TWS option modeler.</param>
		/// <param name="optionPrice">The model price.</param>
		/// <param name="pvDividend">Present value of dividends expected on the option’s underlier.</param>
		/// <param name="gamma">Gamma</param>
		/// <param name="vega">Vega</param>
		/// <param name="theta">Theta</param>
		/// <param name="undPrice">Underlying Price</param>
		public TickOptionComputationEventArgs(int tickerId, TickType tickType, double impliedVol, double delta, double optionPrice, double pvDividend, double gamma, double vega, double theta, double undPrice)
		{
			this.tickerId = tickerId;
			this.pvDividend = pvDividend;
			this.delta = delta;
			this.optionPrice = optionPrice;
			this.impliedVol = impliedVol;
			this.tickType = tickType;
			this.gamma = gamma;
			this.vega = vega;
			this.theta = theta;
			this.underlyingPrice = undPrice;
		}

		/// <summary>
		/// Uninitialized Constructor for Serialization
		/// </summary>
		public TickOptionComputationEventArgs()
		{
			
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
		/// The Option price.
		/// </summary>
		public double OptionPrice
		{
			get { return optionPrice; }
		}

		/// <summary>
		/// Present value of dividends expected on the option’s underlier.
		/// </summary>
		public double PVDividend
		{
			get { return pvDividend; }
		}

		/// <summary>
		/// Gamma
		/// </summary>
		public double Gamma
		{
			get { return gamma; }
		}

		/// <summary>
		/// Vega
		/// </summary>
		public double Vega
		{
			get { return vega; }
		}

		/// <summary>
		/// Theta
		/// </summary>
		public double Theta
		{
			get { return theta; }
		}

		/// <summary>
		/// Underlying Price
		/// </summary>
		public double UnderlyingPrice
		{
			get { return underlyingPrice; }
		}
	}
}