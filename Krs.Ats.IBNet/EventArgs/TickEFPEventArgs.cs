using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Tick EFP Event Arguments
	/// </summary>
	[Serializable()]
	public class TickEfpEventArgs : EventArgs
	{
		private double basisPoints;
		private double dividendImpact;
		private double dividendsToExpiry;
		private string formattedBasisPoints;
		private string futureExpiry;
		private int holdDays;
		private double impliedFuture;
		private int tickerId;

		private TickType tickType;

		/// <summary>
		/// Full Constructor
		/// </summary>
		/// <param name="tickerId">The ticker Id that was specified previously in the call to reqMktData().</param>
		/// <param name="tickType">Specifies the type of price.</param>
		/// <param name="basisPoints">Annualized basis points, which is representative of the
		/// financing rate that can be directly compared to broker rates.</param>
		/// <param name="formattedBasisPoints">Annualized basis points as a formatted string that depicts them in percentage form.</param>
		/// <param name="impliedFuture">Implied futures price.</param>
		/// <param name="holdDays">Number of “hold days” until the expiry of the EFP.</param>
		/// <param name="futureExpiry">Expiration date of the single stock future.</param>
		/// <param name="dividendImpact">The “dividend impact” upon the annualized basis points interest rate.</param>
		/// <param name="dividendsToExpiry">The dividends expected until the expiration of the single stock future.</param>
		public TickEfpEventArgs(int tickerId, TickType tickType, double basisPoints, string formattedBasisPoints,
								double impliedFuture, int holdDays, string futureExpiry, double dividendImpact,
								double dividendsToExpiry)
		{
			this.tickerId = tickerId;
			this.dividendsToExpiry = dividendsToExpiry;
			this.dividendImpact = dividendImpact;
			this.futureExpiry = futureExpiry;
			this.holdDays = holdDays;
			this.impliedFuture = impliedFuture;
			this.formattedBasisPoints = formattedBasisPoints;
			this.basisPoints = basisPoints;
			this.tickType = tickType;
		}

		/// <summary>
		/// Uninitialized Constructor for Serialization
		/// </summary>
		public TickEfpEventArgs()
		{
			
		}

		/// <summary>
		/// The ticker Id that was specified previously in the call to reqMktData().
		/// </summary>
		public int TickerId
		{
			get { return tickerId; }
			set { tickerId = value; }
		}

		/// <summary>
		/// Specifies the type of price.
		/// </summary>
		/// <seealso cref="TickType"/>
		public TickType TickType
		{
			get { return tickType; }
			set { tickType = value; }
		}

		/// <summary>
		/// Annualized basis points, which is representative of the
		/// financing rate that can be directly compared to broker rates.
		/// </summary>
		public double BasisPoints
		{
			get { return basisPoints; }
			set { basisPoints = value; }
		}

		/// <summary>
		/// Annualized basis points as a formatted string that depicts them in percentage form.
		/// </summary>
		public string FormattedBasisPoints
		{
			get { return formattedBasisPoints; }
			set { formattedBasisPoints = value; }
		}

		/// <summary>
		/// Implied futures price.
		/// </summary>
		public double ImpliedFuture
		{
			get { return impliedFuture; }
			set { impliedFuture = value; }
		}

		/// <summary>
		/// Number of “hold days” until the expiry of the EFP.
		/// </summary>
		public int HoldDays
		{
			get { return holdDays; }
			set { holdDays = value; }
		}

		/// <summary>
		/// Expiration date of the single stock future.
		/// </summary>
		public string FutureExpiry
		{
			get { return futureExpiry; }
			set { futureExpiry = value; }
		}

		/// <summary>
		/// The “dividend impact” upon the annualized basis points interest rate.
		/// </summary>
		public double DividendImpact
		{
			get { return dividendImpact; }
			set { dividendImpact = value; }
		}

		/// <summary>
		/// The dividends expected until the expiration of the single stock future.
		/// </summary>
		public double DividendsToExpiry
		{
			get { return dividendsToExpiry; }
			set { dividendsToExpiry = value; }
		}
	}
}