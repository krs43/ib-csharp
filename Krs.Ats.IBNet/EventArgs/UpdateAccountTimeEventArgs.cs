using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Update Account Time Event Arguments
	/// </summary>
	[Serializable()]
	public class UpdateAccountTimeEventArgs : EventArgs
	{
		private string timestamp;

		/// <summary>
		/// Full Constructor
		/// </summary>
		/// <param name="timestamp">Current system time on the server side.</param>
		public UpdateAccountTimeEventArgs(string timestamp)
		{
			this.timestamp = timestamp;
		}

		/// <summary>
		/// Uninitialized Constructor for Serialization
		/// </summary>
		public UpdateAccountTimeEventArgs()
		{
			
		}

		/// <summary>
		/// Current system time on the server side.
		/// </summary>
		public string Timestamp
		{
			get { return timestamp; }
			set { timestamp = value; }
		}
	}
}