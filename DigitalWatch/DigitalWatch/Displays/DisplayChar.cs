using System;

namespace DigitalWatch
{
    /// <summary>
    /// A char of a display.
    /// </summary>
    public struct DisplayChar
    {
        /// <summary>
        /// Gets or sets the character that the display should show.
        /// </summary>
        /// <value>The value.</value>
        public char Value { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DigitalWatch.DisplayChar"/> should blink.
        /// </summary>
        /// <value><c>true</c> if it should blink; otherwise, <c>false</c>.</value>
        /// <remarks>Not all displays have support for blinking.</remarks>
        public bool Blink { get; set; }
    }
}

