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
        /// <summary>
        /// Retuned by the executions event, contains the commission report.
        /// </summary>
        /// <param name="report">The commission report.</param>
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
        public CommissionReport CommissionReport { get; set; }
    }
}