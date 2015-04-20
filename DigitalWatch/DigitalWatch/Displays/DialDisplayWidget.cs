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

        private double hoursAngle;
        private double minutesAngle;
        private double secondsAngle;

		public DialDisplayWidget ()
		{
			this.Build ();
            hoursAngle = minutesAngle = secondsAngle = 0.0;
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
            hoursAngle = Double.Parse(textParts[0].Value) * Math.PI / 6;
            minutesAngle = Double.Parse(textParts[2].Value) * Math.PI / 30;
            secondsAngle = Double.Parse(textParts[4].Value) * Math.PI / 30;

            this.QueueDraw();
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
            int radius = (width < height ? width : height) / 2 - 10;
			// Set the size of the line
			const int lineWidth = 6;
			cr.LineWidth = lineWidth;

			//// Draw background
            //cr.SetSourceRGBA (0.729, 0.373, 0.22, 0.9); // orange
			//cr.Paint ();

			// Draw background clock
			cr.Translate (width / 2, height / 2);
			cr.Arc (0, 0, radius, 0, 2 * Math.PI);
			cr.SetSourceRGBA (1.0, 1.0, 1.0, 0.8); // almost white
			cr.FillPreserve ();

			cr.SetSourceRGB (0, 0, 0); // black
			cr.Stroke ();

            // Draw clock ticks
            cr.LineCap = LineCap.Round;
            for (int i = 0; i < 12; i++)
            {
                cr.Save();

                // Inset height for a tick // TODO Check comment
                double inset = 5.5;

                if(i % 3 != 0)
                {
                    // Properties for the smaller ticks
                    inset *= 0.8;
                    cr.LineWidth = lineWidth / 3 * 2;
                }

                // Draw tick
                cr.MoveTo(
                    (radius - inset) * Math.Cos(i * Math.PI / 6),
                    (radius - inset) * Math.Sin(i * Math.PI / 6));
                cr.LineTo (
                    radius * Math.Cos(i * Math.PI / 6),
                    radius * Math.Sin(i * Math.PI / 6));
                cr.Stroke();

                // Restore the stack-pen-size
                cr.Restore();
            }

            // Draw seconds hand
            cr.Save();
            cr.LineWidth = lineWidth / 3;
            cr.SetSourceRGBA(0.7, 0.7, 0.7, 0.8); // gray
            cr.MoveTo(0, 0);
            cr.LineTo(
                Math.Sin(secondsAngle) * (radius * 0.9),
                -Math.Cos(secondsAngle) * (radius * 0.9));
            cr.Stroke();
            // Restore the stack-pen-size
            cr.Restore();

            // Draw minutes hand
            // Save source color 
            cr.Save();
            cr.SetSourceRGBA(0.486, 0.149, 0.471, 0.9); // purple
            cr.MoveTo(0, 0);
            cr.LineTo(Math.Sin(
                minutesAngle + secondsAngle / 60) * (radius * 0.8),
                -Math.Cos(minutesAngle + secondsAngle / 60) * (radius * 0.8));
            cr.Stroke();

            // Draw hours hand
            cr.SetSourceRGBA (0.729, 0.373, 0.22, 0.9); // orange
            cr.MoveTo(0, 0);
            cr.LineTo(
                Math.Sin(hoursAngle + minutesAngle / 12.0) * (radius * 0.5),
                -Math.Cos(hoursAngle + minutesAngle / 12.0) * (radius * 0.5));
            cr.Stroke();
            //Restore source color
            cr.Restore();

            // Draw the middle dot
            cr.Arc(0, 0, lineWidth / 3.0, 0, 2 * Math.PI);
            cr.Fill();

			((IDisposable) cr.GetTarget()).Dispose();                                      
			((IDisposable) cr).Dispose();
		}
	}
}

