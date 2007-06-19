/*
* TickType.java
*
*/
using System;

namespace KRS.ATS.IBNet
{
    public class TickType
    {
        // constants - tick types
        public const int BID_SIZE = 0;
        public const int BID = 1;
        public const int ASK = 2;
        public const int ASK_SIZE = 3;
        public const int LAST = 4;
        public const int LAST_SIZE = 5;
        public const int HIGH = 6;
        public const int LOW = 7;
        public const int VOLUME = 8;
        public const int CLOSE = 9;
        public const int BID_OPTION = 10;
        public const int ASK_OPTION = 11;
        public const int LAST_OPTION = 12;
        public const int MODEL_OPTION = 13;
        public const int OPEN = 14;
        public const int LOW_13_WEEK = 15;
        public const int HIGH_13_WEEK = 16;
        public const int LOW_26_WEEK = 17;
        public const int HIGH_26_WEEK = 18;
        public const int LOW_52_WEEK = 19;
        public const int HIGH_52_WEEK = 20;
        public const int AVG_VOLUME = 21;
        public const int OPEN_INTEREST = 22;
        public const int OPTION_HISTORICAL_VOL = 23;
        public const int OPTION_IMPLIED_VOL = 24;
        public const int OPTION_BID_EXCH = 25;
        public const int OPTION_ASK_EXCH = 26;
        public const int OPTION_CALL_OPEN_INTEREST = 27;
        public const int OPTION_PUT_OPEN_INTEREST = 28;
        public const int OPTION_CALL_VOLUME = 29;
        public const int OPTION_PUT_VOLUME = 30;
        public const int INDEX_FUTURE_PREMIUM = 31;
        public const int BID_EXCH = 32;
        public const int ASK_EXCH = 33;
        public const int AUCTION_VOLUME = 34;
        public const int AUCTION_PRICE = 35;
        public const int AUCTION_IMBALANCE = 36;
        public const int MARK_PRICE = 37;
        public const int BID_EFP_COMPUTATION = 38;
        public const int ASK_EFP_COMPUTATION = 39;
        public const int LAST_EFP_COMPUTATION = 40;
        public const int OPEN_EFP_COMPUTATION = 41;
        public const int HIGH_EFP_COMPUTATION = 42;
        public const int LOW_EFP_COMPUTATION = 43;
        public const int CLOSE_EFP_COMPUTATION = 44;
		
        public static System.String getField(int tickType)
        {
            switch (tickType)
            {
				
                case BID_SIZE:  return "bidSize";
				
                case BID:  return "bidPrice";
				
                case ASK:  return "askPrice";
				
                case ASK_SIZE:  return "askSize";
				
                case LAST:  return "lastPrice";
				
                case LAST_SIZE:  return "lastSize";
				
                case HIGH:  return "high";
				
                case LOW:  return "low";
				
                case VOLUME:  return "volume";
				
                case CLOSE:  return "close";
				
                case BID_OPTION:  return "bidOptComp";
				
                case ASK_OPTION:  return "askOptComp";
				
                case LAST_OPTION:  return "lastOptComp";
				
                case MODEL_OPTION:  return "modelOptComp";
				
                case OPEN:  return "open";
				
                case LOW_13_WEEK:  return "13WeekLow";
				
                case HIGH_13_WEEK:  return "13WeekHigh";
				
                case LOW_26_WEEK:  return "26WeekLow";
				
                case HIGH_26_WEEK:  return "26WeekHigh";
				
                case LOW_52_WEEK:  return "52WeekLow";
				
                case HIGH_52_WEEK:  return "52WeekHigh";
				
                case AVG_VOLUME:  return "AvgVolume";
				
                case OPEN_INTEREST:  return "OpenInterest";
				
                case OPTION_HISTORICAL_VOL:  return "OptionHistoricalVolatility";
				
                case OPTION_IMPLIED_VOL:  return "OptionImpliedVolatility";
				
                case OPTION_BID_EXCH:  return "OptionBidExchStr";
				
                case OPTION_ASK_EXCH:  return "OptionAskExchStr";
				
                case OPTION_CALL_OPEN_INTEREST:  return "OptionCallOpenInterest";
				
                case OPTION_PUT_OPEN_INTEREST:  return "OptionPutOpenInterest";
				
                case OPTION_CALL_VOLUME:  return "OptionCallVolume";
				
                case OPTION_PUT_VOLUME:  return "OptionPutVolume";
				
                case INDEX_FUTURE_PREMIUM:  return "IndexFuturePremium";
				
                case BID_EXCH:  return "bidExch";
				
                case ASK_EXCH:  return "askExch";
				
                case AUCTION_VOLUME:  return "auctionVolume";
				
                case AUCTION_PRICE:  return "auctionPrice";
				
                case AUCTION_IMBALANCE:  return "auctionImbalance";
				
                case MARK_PRICE:  return "markPrice";
				
                case BID_EFP_COMPUTATION:  return "bidEFP";
				
                case ASK_EFP_COMPUTATION:  return "askEFP";
				
                case LAST_EFP_COMPUTATION:  return "lastEFP";
				
                case OPEN_EFP_COMPUTATION:  return "openEFP";
				
                case HIGH_EFP_COMPUTATION:  return "highEFP";
				
                case LOW_EFP_COMPUTATION:  return "lowEFP";
				
                case CLOSE_EFP_COMPUTATION:  return "closeEFP";
				
                default:  return "unknown";
				
            }
        }
    }
}