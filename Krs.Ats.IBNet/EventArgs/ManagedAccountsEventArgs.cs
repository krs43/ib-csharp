using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Managed Accounts Event Arguments
    /// </summary>
    [Serializable()]
    public class ManagedAccountsEventArgs : EventArgs
    {
        private string accountsList;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="accountsList">The comma delimited list of FA managed accounts.</param>
        public ManagedAccountsEventArgs(string accountsList)
        {
            this.accountsList = accountsList;
        }

        /// <summary>
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public ManagedAccountsEventArgs()
        {
            
        }

        /// <summary>
        /// The comma delimited list of FA managed accounts.
        /// </summary>
        public string AccountsList
        {
            get { return accountsList; }
			set { accountsList = value; }
		}
    }
}