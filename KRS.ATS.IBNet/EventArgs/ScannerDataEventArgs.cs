using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Scanner Data Event Arguments
    /// </summary>
    public class ScannerDataEventArgs : EventArgs
    {
        private readonly int reqId;
        /// <summary>
        /// The ticker ID of the request to which this row is responding.
        /// </summary>
        public int ReqId
        {
            get
            {
                return reqId;
            }
        }

        private readonly int rank;
        /// <summary>
        /// The ranking within the response of this bar.
        /// </summary>
        public int Rank
        {
            get
            {
                return rank;
            }
        }

        private readonly ContractDetails contractDetails;
        /// <summary>
        /// This structure contains a full description of the contract that was executed.
        /// </summary>
        public ContractDetails ContractDetails
        {
            get
            {
                return contractDetails;
            }
        }

        private readonly string distance;
        /// <summary>
        /// Meaning varies based on query.
        /// </summary>
        public string Distance
        {
            get
            {
                return distance;
            }
        }

        private readonly string benchmark;
        /// <summary>
        /// Meaning varies based on query.
        /// </summary>
        public string Benchmark
        {
            get
            {
                return benchmark;
            }
        }

        private readonly string projection;
        /// <summary>
        /// Meaning varies based on query.
        /// </summary>
        public string Projection
        {
            get
            {
                return projection;
            }
        }

        private readonly string legsStr;
        /// <summary>
        /// Describes combo legs when scan is returning EFP.
        /// </summary>
        public string LegsStr
        {
            get
            {
                return legsStr;
            }
        }

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="reqId">The ticker ID of the request to which this row is responding.</param>
        /// <param name="rank">The ranking within the response of this bar.</param>
        /// <param name="contractDetails">This structure contains a full description of the contract that was executed.</param>
        /// <param name="distance">Meaning varies based on query.</param>
        /// <param name="benchmark">Meaning varies based on query.</param>
        /// <param name="projection">Meaning varies based on query.</param>
        /// <param name="legsStr">Describes combo legs when scan is returning EFP.</param>
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
