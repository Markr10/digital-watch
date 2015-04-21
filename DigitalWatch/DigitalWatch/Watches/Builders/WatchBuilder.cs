using System;
using DigitalWatch.Components;
using DigitalWatch.Displays;

namespace DigitalWatch.Watches.Builders
{
    /// <summary>
    /// A WatchBuilder in a classe wich is able to build watch objects
    /// </summary>
    public interface WatchBuilder
    {
        /// <summary>
        /// Sets the display for the watch
        /// </summary>
        /// <param name="display">Display.</param>
        void SetDisplay(Display display);

        /// <summary>
        /// Adds a componet to a watch
        /// </summary>
        /// <param name="component">Component.</param>
        void AddComponent(WatchComponent component);

        /// <summary>
        /// Creates the watch.
        /// </summary>
        /// <returns>The watch.</returns>
        Watch CreateWatch();
    }
}

