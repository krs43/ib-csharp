using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Order Auction Strategy
    /// </summary>
    public enum AuctionStrategy : int
    {
        Undefined = 0,
        AuctionMatch = 1,
        AuctionImprovement = 2,
        AuctionTransparent = 3
    }
}
