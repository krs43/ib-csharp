using System;
using System.ComponentModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Financial Advisor Allocation Method
    /// </summary>
    [Serializable()]
    public enum FinancialAdvisorAllocationMethod
    {
        /// <summary>
        /// Percent Change
        /// </summary>
        [Description("PctChange")] PercentChange,
        /// <summary>
        /// Available Equity
        /// </summary>
        [Description("AvailableEquity")] AvailableEquity,
        /// <summary>
        /// Net Liquidity
        /// </summary>
        [Description("NetLiq")] NetLiquidity,
        /// <summary>
        /// Equal Quantity
        /// </summary>
        [Description("EqualQuantity")] EqualQuantity,
        /// <summary>
        /// No Allocation Method
        /// </summary>
        [Description("")] None
    }
}
