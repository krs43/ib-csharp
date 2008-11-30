using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Contract Details Event Arguments
    /// </summary>
    [Serializable()]
    public class AccountDownloadEndEventArgs : EventArgs
    {
        private readonly string accountName;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="accountName">Account Name</param>
        public AccountDownloadEndEventArgs(string accountName)
        {
            this.accountName = accountName;
        }

        /// <summary>
        /// Request Id
        /// </summary>
        public string AccountName
        {
            get { return accountName; }
        }
    }
}