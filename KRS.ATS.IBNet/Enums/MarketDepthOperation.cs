using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Market Depth Operation
    /// </summary>
    [Serializable()] 
    public enum MarketDepthOperation : int
    {
        /// <summary>
        /// Insert  (insert this new order into the row identified by 'position')
        /// </summary>
        Insert = 0,
        /// <summary>
        /// Update (update the existing order in the row identified by 'position')
        /// </summary>
        Update = 1,
        /// <summary>
        /// Delete (delete the existing order at the row identified by 'position')
        /// </summary>
        Delete = 2
    }
}