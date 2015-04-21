
namespace DigitalWatch.Displays
{
    public delegate void OnButtonPress();

    /// <summary>
    /// Represents a watch display.
    /// The display triggers events when the user interacts with it.
    /// WatchComponens can write to the display. But never inderact directly with it.
    /// 
    /// <para>The Write and Clear methods must be thread safe.</para>
    /// </summary>
    public interface Display
    {
        /// <summary>
        /// Occurs on a mode button press.
        /// </summary>
        event OnButtonPress OnModeButtonPress;
        /// <summary>
        /// Occurs on a primary button press.
        /// </summary>
        event OnButtonPress OnPrimaryButtonPress;
        /// <summary>
        /// Occurs on a secondary button press.
        /// </summary>
        event OnButtonPress OnSecondaryButtonPress;
        /// <summary>
        /// Occurs on a primary long button press.
        /// </summary>
        event OnButtonPress OnPrimaryLongButtonPress;

        /// <summary>
        /// Send the specified text parts to a display to show them on the display.
        /// </summary>
        /// <param name="textParts">Text parts to show.</param>
        /// <remarks>Some displays are designed to show certain text.</remarks>
        void Write(DisplayTextPart[] textParts);

        /// <summary>
        /// Clear the display.
        /// </summary>
        void Clear();
    }
}

