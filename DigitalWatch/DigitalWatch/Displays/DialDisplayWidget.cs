using System;
using Cairo;
using Gtk;

namespace DigitalWatch.Displays
{
    /// <summary>
    /// The DialDisplayWidget is a dial Display for the watch.
    /// </summary>
    [System.ComponentModel.ToolboxItem(true)]
    public partial class DialDisplayWidget : Bin, Display
    {
        #region Display implementation variables

        public event OnButtonPress OnModeButtonPress;
        public event OnButtonPress OnPrimaryButtonPress;
        public event OnButtonPress OnSecondaryButtonPress;
        public event OnButtonPress OnPrimaryLongButtonPress;

        #endregion

        /// <summary>
        /// The angle of the hours hand.
        /// </summary>
        private double hoursAngle;
        /// <summary>
        /// The angle of the minutes hand.
        /// </summary>
        private double minutesAngle;
        /// <summary>
        /// The angle of the seconds hand.
        /// </summary>
        private double secondsAngle;

        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalWatch.Displays.DialDisplayWidget"/> class.
        /// </summary>
        public DialDisplayWidget()
        {
            Build();
            hoursAngle = minutesAngle = secondsAngle = 0.0; // See also Clear method.
                                                            // Not used because the clock is otherwise drawn twice.
        }

        #region Display implementation

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

        /// <summary>
        /// Sets the display to the default value and redraws it.
        /// </summary>
        public void Clear()
        {
            hoursAngle = minutesAngle = secondsAngle = 0.0; // see also constructor
            this.QueueDraw();
        }

        #endregion

        /// <summary>
        /// Raises the primary button clicked event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void OnPrimaryButtonClicked(object sender, EventArgs e)
        {
            // Do something when there is an event handler.
            if (OnPrimaryButtonPress != null)
            {
                OnPrimaryButtonPress();
            }
        }

        /// <summary>
        /// Raises the secondary button clicked event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void OnSecondaryButtonClicked(object sender, EventArgs e)
        {
            // Do something when there is an event handler.
            if (OnSecondaryButtonPress != null)
            {
                OnSecondaryButtonPress();
            }
        }

        /// <summary>
        /// Raises the mode button clicked event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void OnModeButtonClicked(object sender, EventArgs e)
        {
            // Do something when there is an event handler.
            if (OnModeButtonPress != null)
            {
                OnModeButtonPress();
            }
        }

        /// <summary>
        /// Raises the primary long button clicked event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void OnPrimaryLongButtonClicked(object sender, EventArgs e)
        {
            // Do something when there is an event handler.
            if (OnPrimaryLongButtonPress != null)
            {
                OnPrimaryLongButtonPress();
            }
        }

        /// <summary>
        /// Raises the expose event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Expose event arguments.</param>
        protected void OnExpose(object sender, ExposeEventArgs args)
        {
            // Get de drawing context
            DrawingArea area = (DrawingArea)sender;
            Context cr = Gdk.CairoHelper.Create(area.GdkWindow);

            // Set the size of the context
            int width = area.Allocation.Width;
            int height = area.Allocation.Height;
            int radius = (width < height ? width : height) / 2 - 10;
            Color backgroundColorClock = new Color(1.0, 1.0, 1.0, 0.8); // almost white
            // Set the size of the line
            // Basic size of a line
            const int lineWidth = 6;
            cr.LineWidth = lineWidth;

            // Draw background clock
            cr.Translate(width / 2, height / 2);
            cr.Arc(0, 0, radius, 0, 2 * Math.PI);
            cr.SetSourceColor(backgroundColorClock);
            cr.FillPreserve();

            cr.SetSourceRGB(0, 0, 0); // black
            cr.Stroke();

            // Draw clock ticks
            cr.LineCap = LineCap.Round;
            for (int i = 0; i < 12; i++)
            {
                cr.Save();

                // Inset height for a tick
                double inset = 5.5;

                if (i % 3 != 0)
                {
                    // Properties for the smaller ticks
                    inset *= 0.8;
                    cr.LineWidth = lineWidth / 3 * 2;
                }

                // Draw tick
                cr.MoveTo(
                    (radius - inset) * Math.Cos(i * Math.PI / 6),
                    (radius - inset) * Math.Sin(i * Math.PI / 6));
                cr.LineTo(
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
            cr.SetSourceRGBA(0.486, 0.149, 0.471, 0.9); // purple
            cr.MoveTo(0, 0);
            cr.LineTo(Math.Sin(
                    minutesAngle + secondsAngle / 60) * (radius * 0.8),
                -Math.Cos(minutesAngle + secondsAngle / 60) * (radius * 0.8));
            cr.Stroke();

            // Draw hours hand
            cr.SetSourceRGBA(0.729, 0.373, 0.22, 0.9); // orange
            cr.MoveTo(0, 0);
            cr.LineTo(
                Math.Sin(hoursAngle + minutesAngle / 12.0) * (radius * 0.5),
                -Math.Cos(hoursAngle + minutesAngle / 12.0) * (radius * 0.5));
            cr.Stroke();

            // Draw the middle dot
            cr.SetSourceColor(backgroundColorClock);
            cr.Arc(0, 0, lineWidth / 3.0, 0, 2 * Math.PI);
            cr.Fill();

            ((IDisposable)cr.GetTarget()).Dispose();
            ((IDisposable)cr).Dispose();
        }
    }
}

