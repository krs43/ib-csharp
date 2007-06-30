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
        public string Key
        {
            get
            {
                return key;
            }
        }

        private readonly string valueRenamed;
        public string ValueRenamed
        {
            get
            {
                return valueRenamed;
            }
        }

        private readonly string currency;
        public string Currency
        {
            get
            {
                return currency;
            }
        }

        private readonly string accountName;
        public string AccountName
        {
            get
            {
                return accountName;
            }
        }

        public UpdateAccountValueEventArgs(string key, string valueRenamed, string currency, string accountName)
        {
            this.key = key;
            this.accountName = accountName;
            this.currency = currency;
            this.valueRenamed = valueRenamed;
        }
    }
}
