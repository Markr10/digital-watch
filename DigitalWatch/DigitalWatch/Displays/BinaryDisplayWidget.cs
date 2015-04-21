using System;

namespace DigitalWatch.Displays
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class BinaryDisplayWidget : Gtk.Bin, Display
	{

		public BinaryDisplayWidget ()
		{
			this.Build ();
			Clear ();
		}

		#region Display implementation

		public event OnButtonPress OnModeButtonPress;

		public event OnButtonPress OnPrimaryButtonPress;

		public event OnButtonPress OnSecondaryButtonPress;

		public event OnButtonPress OnPrimaryLongButtonPress;

        /// <summary>
        /// Shows specified text parts on the display as binary values.
        /// </summary>
        /// <param name="textParts">Text parts that can be displayed.</param>
        public void Write(DisplayTextPart[] textParts)
		{
			Gtk.Application.Invoke (delegate 
			{
                    DisplayHoursLabel.Text = ConvertTimeElementToBinValue(textParts[0].Value);
                    DisplayMinutesLabel.Text= ConvertTimeElementToBinValue(textParts[2].Value);
			});
		}

        /// <summary>
        /// Converts the time element in (string format) to a binary value with leading zeros.
        /// </summary>
        /// <returns>The time element as a binary value with leading zeros.</returns>
        /// <param name="timeString">Time element as a string.</param>
        private static string ConvertTimeElementToBinValueWithLeadingZeros(string timeString)
        {
            string returnString = ConvertTimeElementToBinValue(timeString);
            returnString = AddLeadingZeros(returnString, (Math.Ceiling(Math.Log(Double.Parse(timeString)) / Math.Log(2.0))));

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

		public void Clear ()
		{
			Gtk.Application.Invoke (delegate 
			{
				DisplayHoursLabel.Text = "0000";
				DisplayMinutesLabel.Text= "000000";
			});
		}

		#endregion

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



	}
}

