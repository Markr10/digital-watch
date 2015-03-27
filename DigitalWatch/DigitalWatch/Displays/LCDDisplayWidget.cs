using System;
using System.Timers;
using Gtk;
using Gdk;

namespace DigitalWatch.Displays
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class LCDDisplayWidget : Gtk.Bin, Display
	{
		public event OnButtonPress OnModeButtonPress;
		public event OnButtonPress OnPrimaryButtonPress;
		public event OnButtonPress OnSecondaryButtonPress;
		public event OnButtonPress OnPrimaryLongButtonPress;

		private Timer blinkTimer;
		private bool blink;
		private readonly Color RED_COLOR;
		private readonly Color DEFAULT;

		public LCDDisplayWidget ()
		{
			this.Build ();
			Clear ();
			blinkTimer = new Timer (1000);
			blinkTimer.Enabled = false;
			blink = false;
			blinkTimer.Elapsed += new ElapsedEventHandler (OnBlink);

			Color red = new Color ();
			Color.Parse ("red", ref red);
			RED_COLOR = red;

			Color black = new Color ();
			Color.Parse ("black", ref black);
			DEFAULT = black;
		}

		public void Write(string text, bool blink)
		{
			Gtk.Application.Invoke (delegate{DisplayLabel.Text = text;});

			if (blink == true && !blinkTimer.Enabled)
			{
				blinkTimer.Enabled = true;
			}
			else if(blink == false && blinkTimer.Enabled)
			{
				blinkTimer.Enabled = false;
			}
		}

		private void OnBlink(object sender, ElapsedEventArgs e)
		{
			if (blink == false)
			{
				Gtk.Application.Invoke(delegate {DisplayLabel.ModifyText(StateType.Normal, RED_COLOR);});
				blink = true;
			}
			else
			{
				Gtk.Application.Invoke(delegate {DisplayLabel.ModifyText(StateType.Normal, DEFAULT);});
				blink = false;
			}
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

