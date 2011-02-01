using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Update News Bulletin Event Arguments
	/// </summary>
	[Serializable()]
	public class UpdateNewsBulletinEventArgs : EventArgs
	{
		private string message;
		private int msgId;
		private NewsType msgType;
		private string originExchange;

		/// <summary>
		/// Full Constructor
		/// </summary>
		/// <param name="msgId">The bulletin ID, incrementing for each new bulletin.</param>
		/// <param name="msgType">Specifies the type of bulletin.</param>
		/// <param name="message">The bulletin's message text.</param>
		/// <param name="originExchange">The exchange from which this message originated.</param>
		public UpdateNewsBulletinEventArgs(int msgId, NewsType msgType, string message, string originExchange)
		{
			this.msgId = msgId;
			this.originExchange = originExchange;
			this.message = message;
			this.msgType = msgType;
		}

		/// <summary>
		/// Uninitialized Constructor for Serialization
		/// </summary>
		public UpdateNewsBulletinEventArgs()
		{
			
		}

		/// <summary>
		/// The bulletin ID, incrementing for each new bulletin.
		/// </summary>
		public int MsgId
		{
			get { return msgId; }
			set { msgId = value; }
		}

		/// <summary>
		/// Specifies the type of bulletin.
		/// </summary>
		/// <seealso cref="NewsType"/>
		public NewsType MsgType
		{
			get { return msgType; }
			set { msgType = value; }
		}

		/// <summary>
		/// The bulletin's message text.
		/// </summary>
		public string Message
		{
			get { return message; }
			set { message = value; }
		}

		/// <summary>
		/// The exchange from which this message originated.
		/// </summary>
		public string OriginExchange
		{
			get { return originExchange; }
			set { originExchange = value; }
		}
	}
}