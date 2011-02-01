using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Update Portfolio Event Arguments
	/// </summary>
	[Serializable()]
	public class UpdatePortfolioEventArgs : EventArgs
	{
		private string accountName;
		private decimal averageCost;
		private Contract contract;
		private decimal marketPrice;
		private decimal marketValue;

		private int position;
		private decimal realizedPnl;
		private decimal unrealizedPnl;

		/// <summary>
		/// Full Constructor
		/// </summary>
		/// <param name="contract">This structure contains a description of the contract which is being traded.
		/// The exchange field in a contract is not set for portfolio update.</param>
		/// <param name="position">This integer indicates the position on the contract.
		/// If the position is 0, it means the position has just cleared.</param>
		/// <param name="marketPrice">Unit price of the instrument.</param>
		/// <param name="marketValue">The total market value of the instrument.</param>
		/// <param name="averageCost">The average cost per share is calculated by dividing your cost
		/// (execution price + commission) by the quantity of your position.</param>
		/// <param name="unrealizedPnl">The difference between the current market value of your open positions and the average cost, or Value - Average Cost.</param>
		/// <param name="realizedPnl">Shows your profit on closed positions, which is the difference between your entry execution cost
		/// (execution price + commissions to open the position) and exit execution cost ((execution price + commissions to close the position)</param>
		/// <param name="accountName">The name of the account the message applies to.  Useful for Financial Advisor sub-account messages.</param>
		public UpdatePortfolioEventArgs(Contract contract, int position, decimal marketPrice, decimal marketValue,
										decimal averageCost, decimal unrealizedPnl, decimal realizedPnl, string accountName)
		{
			this.contract = contract;
			this.accountName = accountName;
			this.realizedPnl = realizedPnl;
			this.unrealizedPnl = unrealizedPnl;
			this.averageCost = averageCost;
			this.marketValue = marketValue;
			this.marketPrice = marketPrice;
			this.position = position;
		}

		/// <summary>
		/// Uninitialized Constructor for Serialization
		/// </summary>
		public UpdatePortfolioEventArgs()
		{
			
		}

		/// <summary>
		/// This structure contains a description of the contract which is being traded.
		/// The exchange field in a contract is not set for portfolio update.
		/// </summary>
		public Contract Contract
		{
			get { return contract; }
			set { contract = value; }
		}

		/// <summary>
		/// This integer indicates the position on the contract.
		/// If the position is 0, it means the position has just cleared.
		/// </summary>
		public int Position
		{
			get { return position; }
			set { position = value; }
		}

		/// <summary>
		/// Unit price of the instrument.
		/// </summary>
		public decimal MarketPrice
		{
			get { return marketPrice; }
			set { marketPrice = value; }
		}

		/// <summary>
		/// The total market value of the instrument.
		/// </summary>
		public decimal MarketValue
		{
			get { return marketValue; }
			set { marketValue = value; }
		}

		/// <summary>
		/// The average cost per share is calculated by dividing your cost
		/// (execution price + commission) by the quantity of your position.
		/// </summary>
		public decimal AverageCost
		{
			get { return averageCost; }
			set { averageCost = value; }
		}

		/// <summary>
		/// The difference between the current market value of your open positions and the average cost, or Value - Average Cost.
		/// </summary>
		public decimal UnrealizedPnl
		{
			get { return unrealizedPnl; }
			set { unrealizedPnl = value; }
		}

		/// <summary>
		/// Shows your profit on closed positions, which is the difference between your entry execution cost
		/// (execution price + commissions to open the position) and exit execution cost ((execution price + commissions to close the position)
		/// </summary>
		public decimal RealizedPnl
		{
			get { return realizedPnl; }
			set { realizedPnl = value; }
		}

		/// <summary>
		/// The name of the account the message applies to.  Useful for Financial Advisor sub-account messages.
		/// </summary>
		public string AccountName
		{
			get { return accountName; }
			set { accountName = value; }
		}
	}
}