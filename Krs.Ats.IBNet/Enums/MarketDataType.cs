namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Market Data Type.
    /// </summary>
    public enum MarketDataType : int
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Real-time streaming data.
        /// </summary>
        RealTime = 1,

        /// <summary>
        /// Last data recorded. Frozen data will automatically switch to real-time market data during trading hours.
        /// </summary>
        Frozen = 2
    }
}