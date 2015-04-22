using System;
using Gdk;
using Gtk;

namespace DigitalWatch.Displays
{
    /// <summary>
    /// The segment display is a Display which is very similiar to the 
    /// LCDDisplayWidget. It uses the DS-Digital font to create the segments look.
    /// </summary>

    [System.ComponentModel.ToolboxItem(true)]
    public partial class SegmentDisplayWidget : Bin, Display
    {
        #region Display implementation variables

        public event OnButtonPress OnModeButtonPress;
        public event OnButtonPress OnPrimaryButtonPress;
        public event OnButtonPress OnSecondaryButtonPress;
        public event OnButtonPress OnPrimaryLongButtonPress;

        #endregion

        /// <summary>
        /// The blink color of the segments. Change this constant if you want another
        /// blink color (red, gray, green).
        /// </summary>
        private const string BLINK_COLOR = "gray";
        /// <summary>
        /// Font of the segment display. Change this if you want to change the font and size
        /// of the display.
        /// </summary>
        private const string FONT_DESCRIPTION = "DS-Digital Bold 50";
        /// <summary>
        /// Indicates when ever the display needs to blink.
        /// </summary>
        private bool isBlinking;
        /// <summary>
        /// Indicates when ever the segmends have the blink color.
        /// </summary>
        private bool displayLabelHasBlinkColor;
        /// <summary>
        /// Indicates if the timer is running.
        /// </summary>
        private bool timerIsRunning;
        /// <summary>
        /// The color of the blink.
        /// </summary>
        private Color blinkColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalWatch.Displays.SegmentDisplayWidget"/> class.
        /// </summary>
        public SegmentDisplayWidget()
        {
            Build();
            DisplayLabel.ModifyFont(Pango.FontDescription.FromString(FONT_DESCRIPTION));
            isBlinking = false;
            displayLabelHasBlinkColor = false;
            timerIsRunning = false;
            blinkColor = new Color();
            Color.Parse(BLINK_COLOR, ref blinkColor);
            Clear();
        }

        #region Display implementation

        /// <summary>
        /// Shows the specified text parts on the display.
        /// </summary>
        /// <param name="textParts">Text parts to show.</param>
        public void Write(DisplayTextPart[] textParts)
        {
            // Text to display
            string text = string.Empty;
            foreach (DisplayTextPart textPart in textParts)
            {
                text += textPart.Value;
            }
            Application.Invoke(delegate
                {
                    DisplayLabel.Text = text;
                });

            foreach (DisplayTextPart textPart in textParts)
            {
                if (textPart.Blink && isBlinking == false)
                {
                    isBlinking = true;
                    if (!timerIsRunning)
                    {
                        GLib.Timeout.Add(1000, new GLib.TimeoutHandler(OnBlink));
                        timerIsRunning = true;
                    }
                    return;
                }
            }
            // At this point no character of the display should blink.
            isBlinking = false;
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

        /// <summary>
        /// Triggered by GLib.Timout.
        /// </summary>
        /// <returns><c>true</c> when the timer has to make another cycle or false when the timer can stop.</returns>
        private bool OnBlink()
        {
            if (displayLabelHasBlinkColor)
            {
                DisplayLabel.ModifyFg(StateType.Normal);
                displayLabelHasBlinkColor = false;
            }
            else
            {
                DisplayLabel.ModifyFg(StateType.Normal, blinkColor);
                displayLabelHasBlinkColor = true;
            }

            //The timer can be stoped if the blink function has been disabled and
            //the display has the normal color.
            timerIsRunning = isBlinking || displayLabelHasBlinkColor;
            return timerIsRunning;
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

