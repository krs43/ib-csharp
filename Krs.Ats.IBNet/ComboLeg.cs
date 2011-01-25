using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Used to manage the legs of a combination order.
	/// </summary>
	/// <seealso cref="Contract.ComboLegs"/>
	/// <seealso href="http://www.interactivebrokers.com/php/webhelp/Interoperability/Socket_Client_Java/java_properties.htm#ComboLeg"/>
	[Serializable()]
	public class ComboLeg
	{
		#region Private Variables

		private ActionSide action; // BUY/SELL/SSHORT
		private int conId;
		private String exchange;
		private ComboOpenClose openClose;
		private int ratio;
		private int exemptCode;

		// for stock legs when doing short sale
		private ShortSaleSlot shortSaleSlot; // 1 = clearing broker, 2 = third party
		private String designatedLocation;

		#endregion

		#region Constructors

		/// <summary>
		/// Initialize the ComboLeg
		/// </summary>
		public ComboLeg() : this(0,0,ActionSide.Undefined, null, ComboOpenClose.Unknown, ShortSaleSlot.Unapplicable, null, -1)
		{
		}

		/// <summary>
		/// Initialize the ComboLeg
		/// </summary>
		/// <param name="conId">The unique contract identifier specifying the security. See property <see cref="ComboLeg.ConId"/>.</param>
		/// <param name="ratio">Select the relative number of contracts for the leg you are constructing. See property <see cref="ComboLeg.Ratio"/>.</param>
		/// <param name="action">The side (buy or sell) for the leg you are constructing. See property <see cref="ComboLeg.Action"/></param>
		/// <param name="exchange">The exchange to which the complete combination order will be routed. See property <see cref="ComboLeg.Exchange"/>.</param>
		/// <param name="openClose">Specifies whether the order is an open or close order. Retail customers must use <see cref="ComboOpenClose.Same"/>. See property <see cref="ComboLeg.OpenClose"/></param>
		/// <param name="shortSaleSlot">ShortSaleSlot of Third Party requires DesignatedLocation to be specified. Non-empty DesignatedLocation values for all other cases will cause orders to be rejected. See Property <see cref="ComboLeg.ShortSaleSlot"/></param>
		/// <param name="designatedLocation">Use only when shortSaleSlot value = 2. See Property <see cref="ComboLeg.DesignatedLocation"/></param>
		public ComboLeg(int conId, int ratio, ActionSide action, String exchange, ComboOpenClose openClose, ShortSaleSlot shortSaleSlot, string designatedLocation) : this(conId, ratio, action, exchange, openClose, shortSaleSlot, designatedLocation, -1)
		{
		}

		/// <summary>
		/// Initialize the ComboLeg
		/// </summary>
		/// <param name="conId">The unique contract identifier specifying the security. See property <see cref="ComboLeg.ConId"/>.</param>
		/// <param name="ratio">Select the relative number of contracts for the leg you are constructing. See property <see cref="ComboLeg.Ratio"/>.</param>
		/// <param name="action">The side (buy or sell) for the leg you are constructing. See property <see cref="ComboLeg.Action"/></param>
		/// <param name="exchange">The exchange to which the complete combination order will be routed. See property <see cref="ComboLeg.Exchange"/>.</param>
		/// <param name="openClose">Specifies whether the order is an open or close order. Retail customers must use <see cref="ComboOpenClose.Same"/>. See property <see cref="ComboLeg.OpenClose"/></param>
		/// <param name="shortSaleSlot">ShortSaleSlot of Third Party requires DesignatedLocation to be specified. Non-empty DesignatedLocation values for all other cases will cause orders to be rejected. See Property <see cref="ComboLeg.ShortSaleSlot"/></param>
		/// <param name="designatedLocation">Use only when shortSaleSlot value = 2. See Property <see cref="ComboLeg.DesignatedLocation"/></param>
		/// <param name="exemptCode">Short Sale Exempt Code. See Property <see cref="ComboLeg.ExemptCode"/></param>
		public ComboLeg(int conId, int ratio, ActionSide action, String exchange, ComboOpenClose openClose, ShortSaleSlot shortSaleSlot, string designatedLocation, int exemptCode)
		{
			this.conId = conId;
			this.ratio = ratio;
			this.action = action;
			this.exchange = exchange;
			this.openClose = openClose;
			this.shortSaleSlot = shortSaleSlot;
			this.designatedLocation = designatedLocation;
			this.exemptCode = exemptCode;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The unique contract identifier specifying the security.
		/// </summary>
		public int ConId
		{
			get { return conId; }
			set { conId = value; }
		}

		/// <summary>
		/// Select the relative number of contracts for the leg you are constructing.
		/// To help determine the ratio for a specific combination order, refer to the
		/// Interactive Analytics section of the User's Guide.
		/// </summary>
		public int Ratio
		{
			get { return ratio; }
			set { ratio = value; }
		}

		/// <summary>
		/// The side (buy or sell) for the leg you are constructing.
		/// </summary>
		public ActionSide Action
		{
			get { return action; }
			set { action = value; }
		}

		/// <summary>
		/// The exchange to which the complete combination order will be routed.
		/// </summary>
		public string Exchange
		{
			get { return exchange; }
			set { exchange = value; }
		}

		/// <summary>
		/// Specifies whether the order is an open or close order.
		/// Retail customers must use <see cref="ComboOpenClose.Same"/>.
		/// </summary>
		/// <seealso cref="ComboOpenClose"/>
		public ComboOpenClose OpenClose
		{
			get { return openClose; }
			set { openClose = value; }
		}

		/// <summary>
		/// ShortSaleSlot of Third Party requires DesignatedLocation to be specified. Non-empty DesignatedLocation values for all other cases will cause orders to be rejected.
		/// </summary>
		public ShortSaleSlot ShortSaleSlot
		{
			get { return shortSaleSlot; }
			set { shortSaleSlot = value; }
		}

		/// <summary>
		/// Use only when shortSaleSlot value = 2.
		/// </summary>
		public string DesignatedLocation
		{
			get { return designatedLocation; }
			set { designatedLocation = value; }
		}

		/// <summary>
		/// Short Sale Exempt Code
		/// </summary>
		public int ExemptCode
		{
			get { return exemptCode; }
			set { exemptCode = value; }
		}

		#endregion
	}
}