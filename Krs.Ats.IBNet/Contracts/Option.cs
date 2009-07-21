using System;
using System.Text;

namespace Krs.Ats.IBNet.Contracts
{
    /// <summary>
    /// Option Class - uses default constructors for creating an option contract.
    /// </summary>
    /// <seealso cref="Contract"/>
    [Serializable()]
    public class Option : Contract
    {
        /// <summary>
        /// Creates an Option Contract
        /// </summary>
        /// <param name="equitySymbol">Symbol of the equity. See <see cref="Contract.Symbol"/>.</param>
        /// <param name="optionSymbol">Symbol of the option for the underlying equity. See <see cref="Contract.LocalSymbol"/>.</param>
        /// <param name="expiry">Option Expiration String. See <see cref="Contract.Expiry"/>.</param>
        /// <param name="right">Option Right (Put or Call). See <see cref="Contract.Right"/>.</param>
        /// <param name="strike">Option Strike Price. See <see cref="Contract.Strike"/>.</param>
        public Option(string equitySymbol, string optionSymbol, string expiry,
        RightType right, decimal strike)
            : base(0, equitySymbol, SecurityType.Option, expiry, (double)strike,
        right, "100", "SMART", "USD", optionSymbol, "SMART", SecurityIdType.None, string.Empty)
        {
        }

        /// <summary>
        /// Creates an Option Contract
        /// </summary>
        /// <param name="equitySymbol">Symbol of the equity. See <see cref="Contract.Symbol"/>.</param>
        /// <param name="optionSymbol">Symbol of the option for the underlying equity. See <see cref="Contract.LocalSymbol"/>.</param>
        /// <param name="year">Option Expiration Year. See <see cref="Contract.Expiry"/>.</param>
        /// <param name="month">Option Expiration Month. See <see cref="Contract.Expiry"/>.</param>
        /// <param name="right">Option Right (Put or Call). See <see cref="Contract.Right"/>.</param>
        /// <param name="strike">Option Strike Price. See <see cref="Contract.Strike"/>.</param>
        public Option(string equitySymbol, string optionSymbol, int year, int month,
        RightType right, decimal strike)
            : base(0, equitySymbol, SecurityType.Option, "", (double)strike,
        right, "100", "SMART", "USD", optionSymbol, "SMART", SecurityIdType.None, string.Empty)
        {
            StringBuilder ExpirationString = new StringBuilder();
            ExpirationString.AppendFormat("{0:0000}{1:00}", year, month);
            Expiry = ExpirationString.ToString();
        }
    } 
}
