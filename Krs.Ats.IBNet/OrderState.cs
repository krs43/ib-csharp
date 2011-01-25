using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// The openOrder() callback with the new OrderState() object will now be invoked
	/// each time TWS receives commission information for a trade.
	/// </summary>
	[Serializable()]
	public class OrderState
	{
		#region Private Variables

		private OrderStatus status;

		private String initMargin;
		private String maintMargin;
		private String equityWithLoan;

		private double commission;
		private double minCommission;
		private double maxCommission;
		private String commissionCurrency;

		private String warningText;

		#endregion

		#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		public OrderState():this(OrderStatus.None, null, null, null, 0.0, 0.0, 0.0, null, null)
		{
		}
		
		/// <summary>
		/// Fully Specified Constructor
		/// </summary>
		/// <param name="status">Order Status</param>
		/// <param name="initMargin">Initial margin requirement for the order.</param>
		/// <param name="maintMargin">Maintenance margin requirement for the order.</param>
		/// <param name="equityWithLoan"></param>
		/// <param name="commission"></param>
		/// <param name="minCommission"></param>
		/// <param name="maxCommission"></param>
		/// <param name="commissionCurrency"></param>
		/// <param name="warningText"></param>
		public OrderState(OrderStatus status, String initMargin, String maintMargin, String equityWithLoan, double commission, double minCommission, double maxCommission, String commissionCurrency, String warningText)
		{
			this.status = status;
			this.initMargin = initMargin;
			this.maintMargin = maintMargin;
			this.equityWithLoan = equityWithLoan;
			this.commission = commission;
			this.minCommission = minCommission;
			this.maxCommission = maxCommission;
			this.commissionCurrency = commissionCurrency;
			this.warningText = warningText;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Order Status
		/// </summary>
		public OrderStatus Status
		{
			get { return status; }
			set { status = value; }
		}

		/// <summary>
		/// Initial margin requirement for the order.
		/// </summary>
		public string InitMargin
		{
			get { return initMargin; }
			set { initMargin = value; }
		}

		/// <summary>
		/// Maintenance margin requirement for the order.
		/// </summary>
		public string MaintMargin
		{
			get { return maintMargin; }
			set { maintMargin = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public double Commission
		{
			get { return commission; }
			set { commission = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public double MinCommission
		{
			get { return minCommission; }
			set { minCommission = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public double MaxCommission
		{
			get { return maxCommission; }
			set { maxCommission = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string CommissionCurrency
		{
			get { return commissionCurrency; }
			set { commissionCurrency = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string WarningText
		{
			get { return warningText; }
			set { warningText = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string EquityWithLoan
		{
			get { return equityWithLoan; }
			set { equityWithLoan = value; }
		}

		#endregion
	}
}