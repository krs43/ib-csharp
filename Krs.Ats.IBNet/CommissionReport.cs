using System;

/* file created: June, 2013 - Shane Castle - shane.castle@vaultic.com */

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Returns the commission amount for an order execution.
    /// </summary>
    public class CommissionReport
    {
        /// <summary>
        /// Execution Id.
        /// </summary>
        public String ExecId { get; set; }

        /// <summary>
        /// Total commission amount.
        /// </summary>
        public double Commission { get; set; }

        /// <summary>
        /// Currency.
        /// </summary>
        public String Currency { get; set; }

        /// <summary>
        /// Realized Profit and Loss.
        /// </summary>
        public double? RealizedPnL { get; set; }

        /// <summary>
        /// Yield.
        /// </summary>
        public double? Yield { get; set; }

        /// <summary>
        /// Yield Redemption Date.
        /// </summary>
        public DateTime? YieldRedemptionDate { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CommissionReport()
        {
        }
    }
}
    