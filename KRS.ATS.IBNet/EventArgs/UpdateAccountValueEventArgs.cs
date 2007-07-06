using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Update Account Value Event Arguments
    /// </summary>
    public class UpdateAccountValueEventArgs : EventArgs
    {
        private readonly string key;
        /// <summary>
        /// A string that indicates one type of account value.
        /// </summary>
        public string Key
        {
            get
            {
                return key;
            }
        }

        private readonly string value;
        /// <summary>
        /// The value associated with the key.
        /// </summary>
        public string Value
        {
            get
            {
                return value;
            }
        }

        private readonly string currency;
        /// <summary>
        /// Defines the currency type, in case the value is a currency type.
        /// </summary>
        public string Currency
        {
            get
            {
                return currency;
            }
        }

        private readonly string accountName;
        /// <summary>
        /// States the account the message applies to. Useful for Financial Advisor sub-account messages.
        /// </summary>
        public string AccountName
        {
            get
            {
                return accountName;
            }
        }

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="key">A string that indicates one type of account value.</param>
        /// <param name="value">The value associated with the key.</param>
        /// <param name="currency">Defines the currency type, in case the value is a currency type.</param>
        /// <param name="accountName">States the account the message applies to. Useful for Financial Advisor sub-account messages.</param>
        public UpdateAccountValueEventArgs(string key, string value, string currency, string accountName)
        {
            this.key = key;
            this.accountName = accountName;
            this.currency = currency;
            this.value = value;
        }
    }
}
