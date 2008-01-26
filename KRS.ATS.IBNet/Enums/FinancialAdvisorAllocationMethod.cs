using System;
using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    public enum FinancialAdvisorAllocationMethod
    {
        [Description("PctChange")] PercentChange,
        [Description("AvailableEquity")] AvailableEquity,
        [Description("NetLiq")] NetLiquidity,
        [Description("EqualQuantity")] EqualQuantity,
        [Description("")] None
    }
}
