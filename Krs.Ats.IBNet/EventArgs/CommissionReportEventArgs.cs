using System;

/* file created: June, 2013 - Shane Castle - shane.castle@vaultic.com */

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Commission Report Event Arguments
    /// </summary>
    [Serializable()]
    public class CommissionReportEventArgs : EventArgs
    {
        public CommissionReportEventArgs(CommissionReport report)
        {
            this.CommissionReport = report;
        }

        /// <summary>
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public CommissionReportEventArgs() {}

        /// <summary>
        /// Contains the commission report details.
        /// </summary>
        /// <seealso cref="ComissionReport"/>
        public CommissionReport CommissionReport { get; set; }
    }
}