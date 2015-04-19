
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
        /// Send the specified chars to a display to show them on the display.
        /// </summary>
        /// <param name="chars">Chars to display.</param>
        /// <remarks>Some displays are designed to show certain characters.</remarks>
        void Write(DisplayChar[] chars);
		/// <summary>
		/// Clear the display
		/// </summary>
		void Clear();
	}
}

