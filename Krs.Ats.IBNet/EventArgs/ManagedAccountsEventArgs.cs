using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Managed Accounts Event Arguments
    /// </summary>
    [Serializable()]
    public class ManagedAccountsEventArgs : EventArgs
    {
        private readonly string accountsList;

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="accountsList">The comma delimited list of FA managed accounts.</param>
        public ManagedAccountsEventArgs(string accountsList)
        {
            this.accountsList = accountsList;
        }

        /// <summary>
        /// The comma delimited list of FA managed accounts.
        /// </summary>
        public string AccountsList
        {
            get { return accountsList; }
        }
    }
}