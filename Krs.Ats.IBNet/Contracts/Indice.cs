using System;

namespace Krs.Ats.IBNet.Contracts
{
    /// <summary>
    /// Create a contract with the default parameters for an indice
    /// </summary>
    /// <seealso cref="Contract"/>
    [Serializable()]
    public class Index : Contract
    {
        /// <summary>
        /// Create an Indice Contract for a specific exchange
        /// </summary>
        /// <param name="symbol">Symbol for the indice contract. See <see cref="Contract.Symbol"/>.</param>
        /// <param name="exchange">Exchange for the indice contract. See <see cref="Contract.Exchange"/></param>
        public Index(string symbol, string exchange)
            : base(symbol, exchange, SecurityType.Index, "USD")
        {
        }
    }
}
