using System;

namespace Krs.Ats.IBNet
{
    public enum TickType : int
    {
        // constants - tick types
        BID_SIZE = 0,
        BID = 1,
        ASK = 2,
        ASK_SIZE = 3,
        LAST = 4,
        LAST_SIZE = 5,
        HIGH = 6,
        LOW = 7,
        VOLUME = 8,
        CLOSE = 9,
        BID_OPTION = 10,
        ASK_OPTION = 11,
        LAST_OPTION = 12,
        MODEL_OPTION = 13,
        OPEN = 14,
        LOW_13_WEEK = 15,
        HIGH_13_WEEK = 16,
        LOW_26_WEEK = 17,
        HIGH_26_WEEK = 18,
        LOW_52_WEEK = 19,
        HIGH_52_WEEK = 20,
        AVG_VOLUME = 21,
        OPEN_INTEREST = 22,
        OPTION_HISTORICAL_VOL = 23,
        OPTION_IMPLIED_VOL = 24,
        OPTION_BID_EXCH = 25,
        OPTION_ASK_EXCH = 26,
        OPTION_CALL_OPEN_INTEREST = 27,
        OPTION_PUT_OPEN_INTEREST = 28,
        OPTION_CALL_VOLUME = 29,
        OPTION_PUT_VOLUME = 30,
        INDEX_FUTURE_PREMIUM = 31,
        BID_EXCH = 32,
        ASK_EXCH = 33,
        AUCTION_VOLUME = 34,
        AUCTION_PRICE = 35,
        AUCTION_IMBALANCE = 36,
        MARK_PRICE = 37,
        BID_EFP_COMPUTATION = 38,
        ASK_EFP_COMPUTATION = 39,
        LAST_EFP_COMPUTATION = 40,
        OPEN_EFP_COMPUTATION = 41,
        HIGH_EFP_COMPUTATION = 42,
        LOW_EFP_COMPUTATION = 43,
        CLOSE_EFP_COMPUTATION = 44
    }
}