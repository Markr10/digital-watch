using System;

namespace DigitalWatch.Displays
{
    /// <summary>
    /// A char of a display.
    /// </summary>
    public struct DisplayChar
    {
        /// <summary>
        /// Gets or sets the character that the display should show.
        /// </summary>
        /// <value>The character that the display should show.</value>
        public char Value { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DigitalWatch.Displays.DisplayChar"/> should blink.
        /// </summary>
        /// <value><c>true</c> if it should blink; otherwise, <c>false</c>.</value>
        /// <remarks>Not all displays have support for blinking.</remarks>
        public bool Blink { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalWatch.Displays.DisplayChar"/> struct with
        /// all the fields that are needed to fill the struct.
        /// </summary>
        /// <param name="value">The character that the display should show.</param>
        /// <param name="blink">If set to <c>true</c> it should blink.</param>
        /// <remarks>Not all displays have support for blinking.</remarks>
        public DisplayChar(char value, bool blink)
        {
            Value = value;
            Blink = blink;
        }
    }
}

