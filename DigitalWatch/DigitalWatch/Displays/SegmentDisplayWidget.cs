using System;
using Gtk;
using Gdk;

namespace DigitalWatch.Displays
{
	/// <summary>
	/// 	The segemnt display is a Display which is very similiar to the 
	/// 	LCDDisplayWidget. It uses the DS-Digital font to create the segments look
	/// </summary>

	[System.ComponentModel.ToolboxItem (true)]
	public partial class SegmentDisplayWidget : Gtk.Bin, Display
	{
		public event OnButtonPress OnModeButtonPress;
		public event OnButtonPress OnPrimaryButtonPress;
		public event OnButtonPress OnSecondaryButtonPress;
		public event OnButtonPress OnPrimaryLongButtonPress;

		/// <summary>
		/// The blink color of the segments. Change this constant if you want another
		/// blink color (red, gray, green)
		/// </summary>
		private const string BLINK_COLOR = "gray";
		/// <summary>
		/// Font of the segment display. Change this if you want to change the font and size
		/// of the display.
		/// </summary>
		private const string FONT_DESCRIPTION = "DS-Digital Bold 50";
		/// <summary>
		/// 	Indicates when ever the display needs to blink
		/// </summary>
		private bool isBlinking;
		/// <summary>
		/// 	Indicates when ever the segmends have the blink color
		/// </summary>
		private bool displayLabelHasBlinkColor;
		/// <summary>
		/// The color of the blink.
		/// </summary>
		private Color blinkColor;

		/// <summary>
		/// Initializes a new instance of the <see cref="DigitalWatch.Displays.SegmentDisplayWidget"/> class.
		/// </summary>
		public SegmentDisplayWidget ()
		{
			this.Build ();
			DisplayLabel.ModifyFont(Pango.FontDescription.FromString(FONT_DESCRIPTION));
			Clear ();
			isBlinking = false;
			displayLabelHasBlinkColor = false;
			blinkColor = new Color ();
			Color.Parse(BLINK_COLOR,ref blinkColor);
		}

		/// <summary>
		/// 	Triggered by GLib.Timout
		/// </summary>
		/// <returns><c>true</c> when the timer has to make another cycle or false when the timer can stop</returns>
		private bool OnBlink()
		{
			if (displayLabelHasBlinkColor)
			{
				DisplayLabel.ModifyFg (StateType.Normal);
				displayLabelHasBlinkColor = false;
			}
			else
			{
				DisplayLabel.ModifyFg (StateType.Normal, blinkColor);
				displayLabelHasBlinkColor = true;
			}

			//The timer can be stoped if the blink function has been disabled and
			//the display has the normal color.
			return isBlinking || displayLabelHasBlinkColor;
		}

		protected void OnPrimaryButtonClicked (object sender, EventArgs e)
		{
			if (OnPrimaryButtonPress != null)
			{
				OnPrimaryButtonPress ();
			}
		}

		protected void OnSecondaryButtonClicked (object sender, EventArgs e)
		{
			if (OnSecondaryButtonPress != null)
			{
				OnSecondaryButtonPress ();
			}
		}

		protected void OnModeButtonClicked (object sender, EventArgs e)
		{
			if (OnModeButtonPress != null)
			{
				OnModeButtonPress ();
			}
		}

		protected void OnPrimaryLongButtonClicked (object sender, EventArgs e)
		{
			if (OnPrimaryLongButtonPress != null)
			{
				OnPrimaryLongButtonPress ();
			}
		}
		/// <summary>
		/// Prints the text on the screen.
		/// </summary>
		/// <param name="text">The text to display</param>
		/// <param name="blink">If set to <c>true</c> blink.</param>
		public void Write(string text, bool blink)
		{
			Gtk.Application.Invoke (delegate{DisplayLabel.Text = text;});
			if (blink == true && isBlinking == false)
			{
				isBlinking = true;
				GLib.Timeout.Add (1000, new GLib.TimeoutHandler (OnBlink));
			}
			else if(blink == false)
			{
				isBlinking = false;
			}
		}

		public void Clear()
		{
			Gtk.Application.Invoke (delegate {DisplayLabel.Text = "00:00";});
		}
	}
}

