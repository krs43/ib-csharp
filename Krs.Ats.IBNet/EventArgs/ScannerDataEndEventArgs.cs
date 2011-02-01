using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Scanner Data Event Arguments
	/// </summary>
	[Serializable()]
	public class ScannerDataEndEventArgs : EventArgs
	{
		private int requestId;

		/// <summary>
		/// Full Constructor
		/// </summary>
		/// <param name="requestId">The ticker ID of the request to which this row is responding.</param>
		public ScannerDataEndEventArgs(int requestId)
		{
			this.requestId = requestId;
		}

		/// <summary>
		/// Uninitialized Constructor for Serialization
		/// </summary>
		public ScannerDataEndEventArgs()
		{
			
		}

		/// <summary>
		/// The ticker ID of the request to which this row is responding.
		/// </summary>
		public int RequestId
		{
			get { return requestId; }
			set { requestId = value; }
		}
	}
}