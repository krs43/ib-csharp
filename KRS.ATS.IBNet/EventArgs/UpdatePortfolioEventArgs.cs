using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Update Portfolio Event Arguments
    /// </summary>
    public class UpdatePortfolioEventArgs : EventArgs
    {
        private readonly Contract contract;
        public Contract Contract
        {
            get
            {
                return contract;
            }
        }

        private readonly int position;
        public int Position
        {
            get
            {
                return position;
            }
        }

        private readonly double marketPrice;
        public double MarketPrice
        {
            get
            {
                return marketPrice;
            }
        }

        private readonly double marketValue;
        public double MarketValue
        {
            get
            {
                return marketValue;
            }
        }

        private readonly double averageCost;
        public double AverageCost
        {
            get
            {
                return averageCost;
            }
        }

        private readonly double unrealizedPNL;
        public double UnrealizedPNL
        {
            get
            {
                return unrealizedPNL;
            }
        }

        private readonly double realizedPNL;
        public double RealizedPNL
        {
            get
            {
                return realizedPNL;
            }
        }

        private readonly string accountName;
        public string AccountName
        {
            get
            {
                return accountName;
            }
        }

        public UpdatePortfolioEventArgs(Contract contract, int position, double marketPrice, double marketValue, double averageCost, double unrealizedPNL, double realizedPNL, string accountName)
        {
            this.contract = contract;
            this.accountName = accountName;
            this.realizedPNL = realizedPNL;
            this.unrealizedPNL = unrealizedPNL;
            this.averageCost = averageCost;
            this.marketValue = marketValue;
            this.marketPrice = marketPrice;
            this.position = position;
        }
    }
}
