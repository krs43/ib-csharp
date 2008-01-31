using System;

namespace Krs.Ats.IBNet.Contracts
{
    /// <summary>
    /// Equity Class - uses default constructors for creating an equity contract.
    /// </summary>
    /// <seealso cref="Contract"/>
    [Serializable()]
    public class Equity : Contract
    {
        /// <summary>
        /// Create an Equity Contract for Smart Exchanges
        /// </summary>
        /// <param name="symbol">Symbol of the equity contract. See <see cref="Contract.Symbol"/>.</param>
        public Equity(string symbol)
            : base(symbol, "Smart", SecurityType.Stock, "USD")
        {
        }

        /// <summary>
        /// Create an Equity Contract for a specific exchange
        /// </summary>
        /// <param name="symbol">Symbol for the equity contract. See <see cref="Contract.Symbol"/>.</param>
        /// <param name="exchange">Exchange for the equity contract. See <see cref="Contract.Exchange"/></param>
        public Equity(string symbol, string exchange)
            : base(symbol, exchange, SecurityType.Stock, "USD")
        {
        }
    }
}
