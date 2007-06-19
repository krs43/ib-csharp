using System;

namespace KRS.ATS.IBNet
{
    public class ScannerSubscription
    {
        public const int NO_ROW_NUMBER_SPECIFIED = - 1;
		
        private int m_numberOfRows = NO_ROW_NUMBER_SPECIFIED;
        private System.String m_instrument;
        private System.String m_locationCode;
        private System.String m_scanCode;
        //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
        private double m_abovePrice = System.Double.MaxValue;
        //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
        private double m_belowPrice = System.Double.MaxValue;
        private int m_aboveVolume = System.Int32.MaxValue;
        private int m_averageOptionVolumeAbove = System.Int32.MaxValue;
        //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
        private double m_marketCapAbove = System.Double.MaxValue;
        //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
        private double m_marketCapBelow = System.Double.MaxValue;
        private System.String m_moodyRatingAbove;
        private System.String m_moodyRatingBelow;
        private System.String m_spRatingAbove;
        private System.String m_spRatingBelow;
        private System.String m_maturityDateAbove;
        private System.String m_maturityDateBelow;
        //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
        private double m_couponRateAbove = System.Double.MaxValue;
        //UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
        private double m_couponRateBelow = System.Double.MaxValue;
        private System.String m_excludeConvertible;
        private System.String m_scannerSettingPairs;
        private System.String m_stockTypeFilter;
		
        // Get
        public virtual int numberOfRows()
        {
            return m_numberOfRows;
        }
        public virtual System.String instrument()
        {
            return m_instrument;
        }
        public virtual System.String locationCode()
        {
            return m_locationCode;
        }
        public virtual System.String scanCode()
        {
            return m_scanCode;
        }
        public virtual double abovePrice()
        {
            return m_abovePrice;
        }
        public virtual double belowPrice()
        {
            return m_belowPrice;
        }
        public virtual int aboveVolume()
        {
            return m_aboveVolume;
        }
        public virtual int averageOptionVolumeAbove()
        {
            return m_averageOptionVolumeAbove;
        }
        public virtual double marketCapAbove()
        {
            return m_marketCapAbove;
        }
        public virtual double marketCapBelow()
        {
            return m_marketCapBelow;
        }
        public virtual System.String moodyRatingAbove()
        {
            return m_moodyRatingAbove;
        }
        public virtual System.String moodyRatingBelow()
        {
            return m_moodyRatingBelow;
        }
        public virtual System.String spRatingAbove()
        {
            return m_spRatingAbove;
        }
        public virtual System.String spRatingBelow()
        {
            return m_spRatingBelow;
        }
        public virtual System.String maturityDateAbove()
        {
            return m_maturityDateAbove;
        }
        public virtual System.String maturityDateBelow()
        {
            return m_maturityDateBelow;
        }
        public virtual double couponRateAbove()
        {
            return m_couponRateAbove;
        }
        public virtual double couponRateBelow()
        {
            return m_couponRateBelow;
        }
        public virtual System.String excludeConvertible()
        {
            return m_excludeConvertible;
        }
        public virtual System.String scannerSettingPairs()
        {
            return m_scannerSettingPairs;
        }
        public virtual System.String stockTypeFilter()
        {
            return m_stockTypeFilter;
        }
		
        // Set
        public virtual void  numberOfRows(int num)
        {
            m_numberOfRows = num;
        }
        public virtual void  instrument(System.String txt)
        {
            m_instrument = txt;
        }
        public virtual void  locationCode(System.String txt)
        {
            m_locationCode = txt;
        }
        public virtual void  scanCode(System.String txt)
        {
            m_scanCode = txt;
        }
        public virtual void  abovePrice(double price)
        {
            m_abovePrice = price;
        }
        public virtual void  belowPrice(double price)
        {
            m_belowPrice = price;
        }
        public virtual void  aboveVolume(int volume)
        {
            m_aboveVolume = volume;
        }
        public virtual void  averageOptionVolumeAbove(int volume)
        {
            m_averageOptionVolumeAbove = volume;
        }
        public virtual void  marketCapAbove(double cap)
        {
            m_marketCapAbove = cap;
        }
        public virtual void  marketCapBelow(double cap)
        {
            m_marketCapBelow = cap;
        }
        public virtual void  moodyRatingAbove(System.String r)
        {
            m_moodyRatingAbove = r;
        }
        public virtual void  moodyRatingBelow(System.String r)
        {
            m_moodyRatingBelow = r;
        }
        public virtual void  spRatingAbove(System.String r)
        {
            m_spRatingAbove = r;
        }
        public virtual void  spRatingBelow(System.String r)
        {
            m_spRatingBelow = r;
        }
        public virtual void  maturityDateAbove(System.String d)
        {
            m_maturityDateAbove = d;
        }
        public virtual void  maturityDateBelow(System.String d)
        {
            m_maturityDateBelow = d;
        }
        public virtual void  couponRateAbove(double r)
        {
            m_couponRateAbove = r;
        }
        public virtual void  couponRateBelow(double r)
        {
            m_couponRateBelow = r;
        }
        public virtual void  excludeConvertible(System.String c)
        {
            m_excludeConvertible = c;
        }
        public virtual void  scannerSettingPairs(System.String val)
        {
            m_scannerSettingPairs = val;
        }
        public virtual void  stockTypeFilter(System.String val)
        {
            m_stockTypeFilter = val;
        }
    }
}