using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Scanner Parameters Event Arguments
	/// </summary>
	[Serializable()]
	public class ScannerParametersEventArgs : EventArgs
	{
		private string xml;

		/// <summary>
		/// Full Constructor
		/// </summary>
		/// <param name="xml">Document describing available scanner subscription parameters.</param>
		public ScannerParametersEventArgs(string xml)
		{
			this.xml = xml;
		}

		/// <summary>
		/// Uninitialized Constructor for Serialization
		/// </summary>
		public ScannerParametersEventArgs()
		{
			
		}

		/// <summary>
		/// Document describing available scanner subscription parameters.
		/// </summary>
		public string Xml
		{
			get { return xml; }
			set { xml = value; }
		}
	}
}