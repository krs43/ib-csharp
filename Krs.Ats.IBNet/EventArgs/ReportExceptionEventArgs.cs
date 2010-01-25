using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Update News Bulletin Event Arguments
	/// </summary>
	[Serializable()]
	public class ReportExceptionEventArgs : EventArgs
	{
	    private Exception error;
	    
	    /// <summary>
	    /// Full constructor.
	    /// </summary>
	    /// <param name="error">The exception that was thrown.</param>
	    public ReportExceptionEventArgs(Exception error)
	    {
				this.error = error;
	    }
	
	    /// <summary>
	    /// The exception that was thrown.
	    /// </summary>
		public Exception Error {
			get { return error; }
		}
	
	}
}
