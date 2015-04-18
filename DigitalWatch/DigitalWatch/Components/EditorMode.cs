using System;

namespace DigitalWatch.Components
{
	/// <summary>
	/// Possible editor modes of a component.
	/// </summary>
    /// <remarks>A null value should be used when the component is not in an editor mode.</remarks>
	public enum EditorMode
	{
		Seconds,
		Minutes,
		Hours,
	}
}

