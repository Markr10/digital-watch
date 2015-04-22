using System;
using Gtk;

namespace DigitalWatch.Displays
{
    /// <summary>
    /// The LCDDisplayWidget is a simple Display for the watch.
    /// </summary>
    [System.ComponentModel.ToolboxItem(true)]
    public partial class LCDDisplayWidget : Bin, Display
    {
        #region Display implementation variables

        /// <summary>
        /// Occurs on a mode button press.
        /// </summary>
        public event OnButtonPress OnModeButtonPress;
        /// <summary>
        /// Occurs on a primary button press.
        /// </summary>
        public event OnButtonPress OnPrimaryButtonPress;
        /// <summary>
        /// Occurs on a secondary button press.
        /// </summary>
        public event OnButtonPress OnSecondaryButtonPress;
        /// <summary>
        /// Occurs on a primary long button press.
        /// </summary>
        public event OnButtonPress OnPrimaryLongButtonPress;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalWatch.Displays.LCDDisplayWidget"/> class.
        /// </summary>
        public LCDDisplayWidget()
        {
            Build();
            DisplayLabel.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM Bold 40"));
            Clear();

        }

        #region Display implementation

        /// <summary>
        /// Shows the specified text parts on the display.
        /// </summary>
        /// <param name="textParts">Text parts to show.</param>
        /// <remarks>This display type only shows that there is something blinking.</remarks>
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

            Application.Invoke(delegate
                {
                    DisplayLabel.Text = text;
                });
        }

        /// <summary>
        /// Sets the display to the default value.
        /// </summary>
        public void Clear()
        {
            Application.Invoke(delegate
                {
                    DisplayLabel.Text = "00:00";
                });
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
    }
}

