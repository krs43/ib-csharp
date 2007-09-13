using System;
using System.Collections.Generic;
using System.Text;

using Krs.Ats.IBNet;

namespace Krs.Ats.IBNet.Contracts
{
    /// <summary>
    /// Future Class - uses default constructors for creating an future contract.
    /// </summary>
    /// <seealso cref="Contract"/>
    public class Future : Contract
    {
        /// <summary>
        /// Create an Future Contract for a specific exchange
        /// </summary>
        /// <param name="symbol">Symbol for the future contract. See <see cref="Contract.Symbol"/>.</param>
        /// <param name="exchange">Exchange for the future contract. See <see cref="Contract.Exchange"/>.</param>
        /// <param name="expiry">Expiry for a future contract. See <see cref="Contract.Expiry"/>.</param>
        public Future(string symbol, string exchange, string expiry)
            : base(symbol, exchange, SecurityType.Future, "USD", expiry)
        {
        }
    }
}
