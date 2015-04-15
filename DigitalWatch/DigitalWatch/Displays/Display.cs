
namespace DigitalWatch.Displays
{
	public delegate void OnButtonPress();

	/// <summary>
	/// 	Represents a watch display.
	/// 	The display triggers events when the user interacts with it.
	/// 	WatchComponens can write to the display. But never inderact directly with it.
	/// 	<para>
	/// 	The Write and Clear methods must be thread safe
	/// </summary>
	public interface Display
	{
		/// <summary>
		/// Occurs when on mode button press.
		/// </summary>
		event OnButtonPress OnModeButtonPress;
		/// <summary>
		/// Occurs when on primary button press.
		/// </summary>
		event OnButtonPress OnPrimaryButtonPress;
		/// <summary>
		/// Occurs when on secondary button press.
		/// </summary>
		event OnButtonPress OnSecondaryButtonPress;
		/// <summary>
		/// Occurs when on primary long button press.
		/// </summary>
		event OnButtonPress OnPrimaryLongButtonPress;
		/// <summary>
		/// 	Print text on the screen. 
		/// </summary>
		/// <param name="text">The text to display</param>
		/// <param name="blinkState">Depending on the value, a part of the display will possible blink.</param>
		void Write(string text, BlinkState blinkState);
		/// <summary>
		/// Clear the display
		/// </summary>
		void Clear();
	}
}

