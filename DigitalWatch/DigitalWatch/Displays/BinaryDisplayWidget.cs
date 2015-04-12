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


		public void Write (string text, bool blink)
		{
			Gtk.Application.Invoke (delegate 
			{
				string[] binValues = ConvertTimeStringToBinValues (text);
				DisplayHoursLabel.Text = binValues[0];
				DisplayMinutesLabel.Text= binValues[1];
			});
		}

		private static string[] ConvertTimeStringToBinValues(string timeString)
		{
			string[] splitResult = timeString.Split (':');
			string[] binValues = new string[2];
			int hours = Int32.Parse(splitResult [0]);
			int minutes = Int32.Parse (splitResult [1]);
			binValues [0] = Convert.ToString (hours, 2);
			binValues [1] = Convert.ToString (minutes, 2);
			return binValues;
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

