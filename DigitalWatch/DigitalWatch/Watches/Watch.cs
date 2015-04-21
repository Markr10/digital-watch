using System;

namespace DigitalWatch.Watches
{
    /// <summary>
    /// The Watch represents a Watch object wich functions as 
    /// the bridge between de display and components.
    /// </summary>
    public interface Watch
    {
        /// <summary>
        /// Switchs the mode (active component).
        /// </summary>
        void SwitchMode();
    }
}

