using System;
using Gtk;
using Gdk;

namespace DigitalWatch.Displays
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class SegmentDisplayWidget : Gtk.Bin, Display
	{
		public event OnButtonPress OnModeButtonPress;
		public event OnButtonPress OnPrimaryButtonPress;
		public event OnButtonPress OnSecondaryButtonPress;
		public event OnButtonPress OnPrimaryLongButtonPress;

		private bool isBlinking;
		private bool displayLabelHasBlinkColor;
		private Color blinkColor;

		public SegmentDisplayWidget ()
		{
			this.Build ();
			DisplayLabel.ModifyFont(Pango.FontDescription.FromString("DS-Digital Bold 50"));
			Clear ();
			isBlinking = false;
			displayLabelHasBlinkColor = false;
			blinkColor = new Color ();
			Color.Parse("gray",ref blinkColor);
		}

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

