using System;

namespace DigitalWatch.Displays
{
	public delegate void OnButtonPress();

	public interface Display
	{
		event OnButtonPress OnModeButtonPress;
		event OnButtonPress OnPrimaryButtonPress;
		event OnButtonPress OnSecondaryButtonPress;
		event OnButtonPress OnPrimaryLongButtonPress;
		void Write(string text, bool blink);
		void Clear();
	}
}

