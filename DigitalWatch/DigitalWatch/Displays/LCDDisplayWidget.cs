using System;

namespace DigitalWatch.Displays
{
    /// <summary>
    /// 	The LCDDisplayWidget is a simple Display for the watch
    /// </summary>

    [System.ComponentModel.ToolboxItem(true)]
    public partial class LCDDisplayWidget : Gtk.Bin, Display
    {
        public event OnButtonPress OnModeButtonPress;
        public event OnButtonPress OnPrimaryButtonPress;
        public event OnButtonPress OnSecondaryButtonPress;
        public event OnButtonPress OnPrimaryLongButtonPress;

        public LCDDisplayWidget()
        {
            this.Build();
            DisplayLabel.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM Bold 40"));
            Clear();

        }

        protected void OnPrimaryButtonClicked(object sender, EventArgs e)
        {
            if (OnPrimaryButtonPress != null)
            {
                OnPrimaryButtonPress();
            }
        }

        protected void OnSecondaryButtonClicked(object sender, EventArgs e)
        {
            if (OnSecondaryButtonPress != null)
            {
                OnSecondaryButtonPress();
            }
        }

        protected void OnModeButtonClicked(object sender, EventArgs e)
        {
            if (OnModeButtonPress != null)
            {
                OnModeButtonPress();
            }
        }

        protected void OnPrimaryLongButtonClicked(object sender, EventArgs e)
        {
            if (OnPrimaryLongButtonPress != null)
            {
                OnPrimaryLongButtonPress();
            }
        }

        /// <summary>
        /// Shows the specified text parts on the display.
        /// </summary>
        /// <param name="textParts">Text parts to show.</param>
        /// <remarks>This display type only shows that there is something blinking</remarks>
        public void Write(DisplayTextPart[] textParts)
        {
            // Text to display
            string text = string.Empty;
            bool blinkState = false;
            foreach (DisplayTextPart textPart in textParts)
            {
                text += textPart.Value;
                if (!blinkState && textPart.Blink)
                {
                    blinkState = true;
                }
            }

            if (blinkState)
            {
                text += " +";
            }

            Gtk.Application.Invoke(delegate
                {
                    DisplayLabel.Text = text;
                });
        }

        public void Clear()
        {
            Gtk.Application.Invoke(delegate
                {
                    DisplayLabel.Text = "00:00";
                });
        }
    }
}

