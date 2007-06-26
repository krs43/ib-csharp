using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    public enum GenericTickType
    {
        OptionVolume = 100,
        OptionOpenInterest = 101,
        HistoricalVolatility = 104,
        OptionImpliedVolatility = 106,
        IndexFuturePremium = 162,
        MiscellaneousStats = 165,
        MarkPrice = 221,
        AuctionPrice = 225
    }
}
