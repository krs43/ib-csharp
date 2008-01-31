using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Trigger method for actions.
    /// </summary>
    [Serializable()] 
    public enum TriggerMethod : int
    {
        /// <summary>
        /// 0 - the default value. The "double bid/ask" method will be used for orders for OTC stocks and US options. All other orders will used the "last" method.
        /// </summary>
        Default = 0,
        /// <summary>
        /// 1 - use "double bid/ask" method, where stop orders are triggered based on two consecutive bid or ask prices.
        /// </summary>
        DoubleBidAsk = 1,
        /// <summary>
        /// 2 - "last" method, where stop orders are triggered based on the last price.
        /// </summary>
        Last = 2,
        /// <summary>
        /// 3 double last method.
        /// </summary>
        DoubleLast = 3,
        /// <summary>
        /// 4 bid/ask method.
        /// </summary>
        BidAsk = 4,
        /// <summary>
        /// 7 last or bid/ask method.
        /// </summary>
        LastOrBidAsk = 7,
        /// <summary>
        /// 8 mid-point method.
        /// </summary>
        MidpointMethod = 8
    }
}