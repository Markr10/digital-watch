using System;
using DigitalWatch.Components;
using DigitalWatch.Displays;

namespace DigitalWatch.Watches.Builders
{
	public interface WatchBuilder
	{
		void SetDisplay(Display display);
		void AddComponent(WatchComponent component);
		Watch CreateWatch();
	}
}

