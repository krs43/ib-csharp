using System;

namespace Krs.Ats.IBNet
{
	///<summary>
	/// Class for storing Algorithm Order Parameters
	///</summary>
	[Serializable]
	public class TagValue
	{

	    #region Private Variables

	    private string tag;
	    private string value;

	    #endregion

	    #region Constructors

	    ///<summary>
	    /// Create a new Tag Value
	    ///</summary>
	    public TagValue()
	    {
	    }
		
	    ///<summary>
	    /// Initialize a Tag Value with values
	    ///</summary>
	    ///<param name="tag">Tag Name</param>
	    ///<param name="value">String Value</param>
	    public TagValue(string tag, string value)
	    {
	        this.tag = tag;
	        this.value = value;
	    }

	    #endregion

	    #region Properties

        /// <summary>
        /// Tag Name
        /// </summary>
	    public string Tag
	    {
            get { return tag; }
            set { tag = value; }
	    }

        /// <summary>
        /// Value of Tag
        /// </summary>
	    public string Value
	    {
            get { return value; }
            set { this.value = value; }
	    }

	    #endregion

	}
}