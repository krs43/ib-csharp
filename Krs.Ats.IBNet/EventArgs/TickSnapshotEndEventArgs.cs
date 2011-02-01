using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Tick Snapshot End Event Arguments
	/// </summary>
	[Serializable()]
	public class TickSnapshotEndEventArgs : EventArgs
	{
		private int requestId;

		/// <summary>
		/// Full Constructor
		/// </summary>
		/// <param name="requestId">The ticker ID of the request to which this row is responding.</param>
		public TickSnapshotEndEventArgs(int requestId)
		{
			this.requestId = requestId;
		}

		/// <summary>
		/// Uninitialized Constructor for Serialization
		/// </summary>
		public TickSnapshotEndEventArgs()
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