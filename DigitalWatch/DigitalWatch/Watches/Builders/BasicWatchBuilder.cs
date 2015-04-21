using System.Collections.Generic;
using DigitalWatch.Components;
using DigitalWatch.Displays;
using DigitalWatch.Watches;

namespace DigitalWatch.Watches.Builders
{
    /// <summary>
    /// The BasicWatchBuilder is a WatchBuilder which can build BasicWatches.
    /// </summary>
    public class BasicWatchBuilder : WatchBuilder
    {
        private readonly List<WatchComponent> componentList;
        private Display watchDisplay;

        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalWatch.Watches.Builders.BasicWatchBuilder"/> class.
        /// </summary>
        public BasicWatchBuilder()
        {
            componentList = new List<WatchComponent>();
        }

        /// <summary>
        /// Sets the display for the watch.
        /// </summary>
        /// <param name="display">Display.</param>
        public void SetDisplay(Display display)
        {
            watchDisplay = display;
        }

        /// <summary>
        /// Adds a component to a watch.
        /// </summary>
        /// <param name="component">Component.</param>
        public void AddComponent(WatchComponent component)
        {
            componentList.Add(component);
        }

        /// <summary>
        /// Creates the watch.
        /// </summary>
        /// <returns>The watch.</returns>
        public Watch CreateWatch()
        {
            return new BasicWatch(watchDisplay, componentList.ToArray());
        }
    }
}

