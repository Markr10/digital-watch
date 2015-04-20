using System;
using Gtk;
using Cairo;

namespace DigitalWatch.Displays
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class DialDisplayWidget : Gtk.Bin, Display
	{

		public event OnButtonPress OnModeButtonPress;
		public event OnButtonPress OnPrimaryButtonPress;
		public event OnButtonPress OnSecondaryButtonPress;
		public event OnButtonPress OnPrimaryLongButtonPress;

		public DialDisplayWidget ()
		{
			this.Build ();
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
        /// Converts the specified text parts to a dial display.
        /// </summary>
        /// <param name="textParts">Text parts to show.</param>
        public void Write(DisplayTextPart[] textParts)
		{

		}

		public void Clear()
		{
			//Gtk.Application.Invoke (delegate {DisplayLabel.Text = "00:00";});
		}

		protected void OnExpose (object sender, Gtk.ExposeEventArgs args)
		{
			// Get de drawing context
			DrawingArea area = (DrawingArea) sender;
			Cairo.Context cr =  Gdk.CairoHelper.Create(area.GdkWindow);

			// Set the size of the context
			int width = area.Allocation.Width;
			int height = area.Allocation.Height;
			// Set the size of the line
			const int lineWidth = 6;
			cr.LineWidth = lineWidth;

			//// Draw background
            //cr.SetSourceRGBA (0.729, 0.373, 0.22, 0.9); // orange
			//cr.Paint ();

			// Draw background clock
			cr.Translate (width / 2, height / 2);
			cr.Arc (0, 0, (width < height ? width : height) / 2 - 10, 0, 2 * Math.PI);
			cr.SetSourceRGBA (1.0, 1.0, 1.0, 0.8); // almost white
			cr.FillPreserve ();

			cr.SetSourceRGB (0, 0, 0); // black
			cr.Stroke ();


			((IDisposable) cr.GetTarget()).Dispose();                                      
			((IDisposable) cr).Dispose();
		}
	}
}

