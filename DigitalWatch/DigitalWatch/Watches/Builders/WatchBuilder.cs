using System;

namespace DigitalWatch.Watches.Builders
{
	public interface WatchBuilder
	{
		void SetDisplay(string displayType);
		void AddComponent(string component);
		Watch CreateWatch();
	}
}

