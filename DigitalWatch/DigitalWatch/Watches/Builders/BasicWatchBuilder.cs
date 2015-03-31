using System;
using System.Collections.Generic;
using DigitalWatch.Watches;
using DigitalWatch.Displays;
using DigitalWatch.Components;

namespace DigitalWatch.Watches.Builders
{
	/// <summary>
	/// 	The BasicWatchBuilder is a WatchBuilder witch can build BasicWatches
	/// </summary>
	public class BasicWatchBuilder : WatchBuilder
	{
		private readonly List<WatchComponent> componentList;
		private Display watchDisplay;

		/// <summary>
		/// Initializes a new instance of the <see cref="DigitalWatch.Watches.Builders.BasicWatchBuilder"/> class.
		/// </summary>
		public BasicWatchBuilder ()
		{
			componentList = new List<WatchComponent> ();
		}

		public void SetDisplay(Display display)
		{
			watchDisplay = display;
		}
	
		public void AddComponent(WatchComponent component)
		{
			componentList.Add (component);
		}

		public Watch CreateWatch()
		{
			return new BasicWatch (watchDisplay, componentList.ToArray ());
		}
	}
}

