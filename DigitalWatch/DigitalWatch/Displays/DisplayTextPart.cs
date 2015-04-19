using System;

namespace DigitalWatch.Displays
{
    /// <summary>
    /// A part of the text for a display.
    /// </summary>
    public struct DisplayTextPart
    {
        /// <summary>
        /// A part of the text that the display should show.
        /// </summary>
        /// <value>A part of the text that the display should show.</value>
        public readonly string Value;
        /// <summary>
        /// A value indicating whether this <see cref="DigitalWatch.Displays.DisplayTextPart"/> should blink.
        /// </summary>
        /// <value><c>true</c> if it should blink; otherwise, <c>false</c>.</value>
        /// <remarks>Not all displays have support for blinking.</remarks>
        public readonly bool Blink;

        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalWatch.Displays.DisplayTextPart"/> struct with
        /// all the fields that are needed to fill the struct.
        /// </summary>
        /// <param name="value">A part of the text that the display should show.</param>
        /// <param name="blink">If set to <c>true</c> it should blink.</param>
        /// <remarks>Not all displays have support for blinking.</remarks>
        public DisplayTextPart(string value, bool blink)
        {
            Value = value;
            Blink = blink;
        }
    }
}

