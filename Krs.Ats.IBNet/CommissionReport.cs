using System;

/* file created: June, 2013 - Shane Castle - shane.castle@vaultic.com */

namespace Krs.Ats.IBNet
{
    public class CommissionReport
    {
        private CommissionReport report;

        public String ExecId { get; set; }
        public double Commission { get; set; }
        public String Currency { get; set; }
        public double? RealizedPnL { get; set; }
        public double? Yield { get; set; }
        public DateTime? YieldRedemptionDate { get; set; }

        public CommissionReport()
        {
        }
    }
}
    