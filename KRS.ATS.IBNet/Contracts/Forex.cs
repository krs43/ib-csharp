using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet.Contracts
{
    /// <summary>
    /// Forex Currency Contract
    /// for use on the IdealPro or Ideal exchanges
    /// </summary>
    public class Forex : Contract
    {
        /// <summary>
        /// Creates a Forex Contract for use on the IdealPro or Ideal exchanges
        /// </summary>
        /// <param name="Currency">Foreign Currency to Exchange</param>
        /// <param name="BaseCurrency">Base Currency</param>
        /// <param name="Exchange">IDEALPRO or IDEAL</param>
        public Forex(string Currency, string BaseCurrency, string Exchange)
            : base(Currency, Exchange, IBNet.SecurityType.Cash, BaseCurrency)
        {
        }

        /// <summary>
        /// Creates a Forex Contract for use on the IdealPro Exchange
        /// </summary>
        /// <param name="Currency">Foreign Currency to Exchange</param>
        /// <param name="BaseCurrency">Base Currency</param>
        public Forex(string Currency, string BaseCurrency)
            : base(Currency, "IDEALPRO", IBNet.SecurityType.Cash, BaseCurrency)
        {
            
        }
    }
}
