using System;

namespace DigitalWatch.Displays
{
	/// <summary>
	/// 	The LCDDisplayWidget is a simple Display for the watch
	/// </summary>

	[System.ComponentModel.ToolboxItem (true)]
	public partial class LCDDisplayWidget : Gtk.Bin, Display
	{
		public event OnButtonPress OnModeButtonPress;
		public event OnButtonPress OnPrimaryButtonPress;
		public event OnButtonPress OnSecondaryButtonPress;
		public event OnButtonPress OnPrimaryLongButtonPress;

		public LCDDisplayWidget ()
		{
			this.Build ();
			DisplayLabel.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM Bold 40"));
			Clear ();

		}

		/// <summary>
		/// Prints the text on the screen.
		/// </summary>
		/// <param name="text">The text to display</param>
		/// <param name="blinkState">Depending on the value, a part of the display will possible blink.</param>
		public void Write(string text, BlinkState blinkState)
		{
			if (blinkState != 0)
			{
				text += " +";
			}

			Gtk.Application.Invoke (delegate{DisplayLabel.Text = text;});
		}


		public void Clear()
		{
			Gtk.Application.Invoke (delegate {DisplayLabel.Text = "00:00";});
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
	}
}

