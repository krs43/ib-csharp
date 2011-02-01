using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Contract Details Event Arguments
    /// </summary>
    [Serializable()]
    public class AccountDownloadEndEventArgs : EventArgs
    {
        private string accountName;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="accountName">Account Name</param>
        public AccountDownloadEndEventArgs(string accountName)
        {
            this.accountName = accountName;
        }

        /// <summary>
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public AccountDownloadEndEventArgs()
        {
            
        }

        /// <summary>
        /// Request Id
        /// </summary>
        public string AccountName
        {
            get { return accountName; }
			set { accountName = value; }
		}
    }
}