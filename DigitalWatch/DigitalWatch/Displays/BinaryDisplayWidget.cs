using System;
using Gtk;

namespace DigitalWatch.Displays
{
    /// <summary>
    /// The DialDisplayWidget is a binary Display for the watch.
    /// </summary>
    [System.ComponentModel.ToolboxItem(true)]
    public partial class BinaryDisplayWidget : Bin, Display
    {
        #region Display implementation variables

        public event OnButtonPress OnModeButtonPress;
        public event OnButtonPress OnPrimaryButtonPress;
        public event OnButtonPress OnSecondaryButtonPress;
        public event OnButtonPress OnPrimaryLongButtonPress;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalWatch.Displays.BinaryDisplayWidget"/> class.
        /// </summary>
        public BinaryDisplayWidget()
        {
            Build();
            Clear();
        }

        #region Display implementation

        /// <summary>
        /// Shows specified text parts on the display as binary values.
        /// </summary>
        /// <param name="textParts">Text parts that can be displayed.</param>
        public void Write(DisplayTextPart[] textParts)
        {
            Gtk.Application.Invoke(delegate
                {
                    DisplayHoursLabel.Text = ConvertTimeElementToBinValueWithLeadingZeros(textParts[0].Value, 5);
                    DisplayMinutesLabel.Text = ConvertTimeElementToBinValueWithLeadingZeros(textParts[2].Value, 6);
                });
        }

        /// <summary>
        /// Sets the display to the default value.
        /// </summary>
        public void Clear()
        {
            Application.Invoke(delegate
                {
                    DisplayHoursLabel.Text = "00000";
                    DisplayMinutesLabel.Text = "000000";
                });
        }

        /// <summary>
        /// Converts the time element in (string format) to a binary value with leading zeros.
        /// </summary>
        /// <returns>The time element as a binary value with leading zeros.</returns>
        /// <param name="timeString">Time element as a string.</param>
        /// <param name="maxLength">Max length of the text.</param>
        private static string ConvertTimeElementToBinValueWithLeadingZeros(string timeString, int maxLength)
        {
            // Convert string
            string returnString = ConvertTimeElementToBinValue(timeString);
            // Add leading zeros
            returnString = AddLeadingZeros(returnString, maxLength);

            return returnString;
        }

        /// <summary>
        /// Converts the time element in (string format) to a binary value.
        /// </summary>
        /// <returns>The time element as a binary value.</returns>
        /// <param name="timeString">Time element as a string.</param>
        private static string ConvertTimeElementToBinValue(string timeString)
        {
            int timeElement = Int32.Parse(timeString);
            return Convert.ToString(timeElement, 2);
        }

        /// <summary>
        /// Adds leading zeros to a text.
        /// </summary>
        /// <returns>The text with the leading zeros.</returns>
        /// <param name="text">Text without leading zeros.</param>
        /// <param name="maxLength">Max length of the text.</param>
        private static string AddLeadingZeros(string text, int maxLength)
        {
            String returnString = String.Empty;
            for (int i = text.Length; i < maxLength; i++)
            {
                returnString += "0";
            }
            returnString += text;

            return returnString;
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
    }
}

