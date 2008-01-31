using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Scanner Data Event Arguments
    /// </summary>
    public class ScannerDataEventArgs : EventArgs
    {
        private readonly string benchmark;
        private readonly ContractDetails contractDetails;
        private readonly string distance;
        private readonly string legsStr;
        private readonly string projection;
        private readonly int rank;
        private readonly int reqId;

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
        public ScannerDataEventArgs(int reqId, int rank, ContractDetails contractDetails, string distance,
                                    string benchmark, string projection, string legsStr)
        {
            this.reqId = reqId;
            this.legsStr = legsStr;
            this.projection = projection;
            this.benchmark = benchmark;
            this.distance = distance;
            this.contractDetails = contractDetails;
            this.rank = rank;
        }

        /// <summary>
        /// The ticker ID of the request to which this row is responding.
        /// </summary>
        public int ReqId
        {
            get { return reqId; }
        }

        /// <summary>
        /// The ranking within the response of this bar.
        /// </summary>
        public int Rank
        {
            get { return rank; }
        }

        /// <summary>
        /// This structure contains a full description of the contract that was executed.
        /// </summary>
        public ContractDetails ContractDetails
        {
            get { return contractDetails; }
        }

        /// <summary>
        /// Meaning varies based on query.
        /// </summary>
        public string Distance
        {
            get { return distance; }
        }

        /// <summary>
        /// Meaning varies based on query.
        /// </summary>
        public string Benchmark
        {
            get { return benchmark; }
        }

        /// <summary>
        /// Meaning varies based on query.
        /// </summary>
        public string Projection
        {
            get { return projection; }
        }

        /// <summary>
        /// Describes combo legs when scan is returning EFP.
        /// </summary>
        public string LegsStr
        {
            get { return legsStr; }
        }
    }
}