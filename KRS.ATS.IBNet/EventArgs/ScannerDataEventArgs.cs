using System;
using System.Collections.Generic;
using System.Text;

namespace KRS.ATS.IBNet
{
    /// <summary>
    /// Scanner Data Event Arguments
    /// </summary>
    public class ScannerDataEventArgs : EventArgs
    {
        private readonly int reqId;
        public int ReqId
        {
            get
            {
                return reqId;
            }
        }

        private readonly int rank;
        public int Rank
        {
            get
            {
                return rank;
            }
        }

        private readonly ContractDetails contractDetails;
        public ContractDetails ContractDetails
        {
            get
            {
                return contractDetails;
            }
        }

        private readonly string distance;
        public string Distance
        {
            get
            {
                return distance;
            }
        }

        private readonly string benchmark;
        public string Benchmark
        {
            get
            {
                return benchmark;
            }
        }
        private readonly string projection;
        public string Projection
        {
            get
            {
                return projection;
            }
        }
        private readonly string legsStr;
        public string LegsStr
        {
            get
            {
                return legsStr;
            }
        }

        public ScannerDataEventArgs(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark, string projection, string legsStr)
        {
            this.reqId = reqId;
            this.legsStr = legsStr;
            this.projection = projection;
            this.benchmark = benchmark;
            this.distance = distance;
            this.contractDetails = contractDetails;
            this.rank = rank;
        }
    }
}
