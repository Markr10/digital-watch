using System;
using System.Collections.Generic;
using DigitalWatch.Watches;
using DigitalWatch.Displays;
using DigitalWatch.Components;

namespace DigitalWatch.Watches.Builders
{
	public class BasicWatchBuilder : WatchBuilder
	{
		private List<WatchComponent> componentList;
		private Display watchDisplay;

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

