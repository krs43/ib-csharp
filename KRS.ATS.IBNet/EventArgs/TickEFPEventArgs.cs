using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Tick EFP Event Arguments
    /// </summary>
    public class TickEfpEventArgs : EventArgs
    {
        private readonly int tickerId;
        /// <summary>
        /// The ticker Id that was specified previously in the call to reqMktData().
        /// </summary>
        public int TickerId
        {
            get
            {
                return tickerId;
            }
        }

        private readonly TickType tickType;
        /// <summary>
        /// Specifies the type of price.
        /// </summary>
        /// <seealso cref="TickType"/>
        public TickType TickType
        {
            get
            {
                return tickType;
            }
        }

        private readonly double basisPoints;
        /// <summary>
        /// Annualized basis points, which is representative of the
        /// financing rate that can be directly compared to broker rates.
        /// </summary>
        public double BasisPoints
        {
            get
            {
                return basisPoints;
            }
        }

        private readonly string formattedBasisPoints;
        /// <summary>
        /// Annualized basis points as a formatted string that depicts them in percentage form.
        /// </summary>
        public string FormattedBasisPoints
        {
            get
            {
                return formattedBasisPoints;
            }
        }

        private readonly double impliedFuture;
        /// <summary>
        /// Implied futures price.
        /// </summary>
        public double ImpliedFuture
        {
            get
            {
                return impliedFuture;
            }
        }

        private readonly int holdDays;
        /// <summary>
        /// Number of “hold days” until the expiry of the EFP.
        /// </summary>
        public double HoldDays
        {
            get
            {
                return holdDays;
            }
        }

        private readonly string futureExpiry;
        /// <summary>
        /// Expiration date of the single stock future.
        /// </summary>
        public string FutureExpiry
        {
            get
            {
                return futureExpiry;
            }
        }

        private readonly double dividendImpact;
        /// <summary>
        /// The “dividend impact” upon the annualized basis points interest rate.
        /// </summary>
        public double DividendImpact
        {
            get
            {
                return dividendImpact;
            }
        }
        private readonly double dividendsToExpiry;
        /// <summary>
        /// The dividends expected until the expiration of the single stock future.
        /// </summary>
        public double DividendsToExpiry
        {
            get
            {
                return dividendsToExpiry;
            }
        }

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="tickerId">The ticker Id that was specified previously in the call to reqMktData().</param>
        /// <param name="tickType">Specifies the type of price.</param>
        /// <param name="basisPoints">Annualized basis points, which is representative of the
        /// financing rate that can be directly compared to broker rates.</param>
        /// <param name="formattedBasisPoints">Annualized basis points as a formatted string that depicts them in percentage form.</param>
        /// <param name="impliedFuture">Implied futures price.</param>
        /// <param name="holdDays">Number of “hold days” until the expiry of the EFP.</param>
        /// <param name="futureExpiry">Expiration date of the single stock future.</param>
        /// <param name="dividendImpact">The “dividend impact” upon the annualized basis points interest rate.</param>
        /// <param name="dividendsToExpiry">The dividends expected until the expiration of the single stock future.</param>
        public TickEfpEventArgs(int tickerId, TickType tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureExpiry, double dividendImpact, double dividendsToExpiry)
        {
            this.tickerId = tickerId;
            this.dividendsToExpiry = dividendsToExpiry;
            this.dividendImpact = dividendImpact;
            this.futureExpiry = futureExpiry;
            this.holdDays = holdDays;
            this.impliedFuture = impliedFuture;
            this.formattedBasisPoints = formattedBasisPoints;
            this.basisPoints = basisPoints;
            this.tickType = tickType;
        }
    }
}
