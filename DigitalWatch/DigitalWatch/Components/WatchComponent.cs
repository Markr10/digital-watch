using System;

namespace DigitalWatch.Components
{
	public delegate void UpdateScreen(string text, bool blink, WatchComponent sender);

	public interface WatchComponent
	{
		event UpdateScreen OnScreenUpdate;
		void PrimaryButtonPress();
		void SecondaryButtonPress();
		void PrimaryButtonLongPress();
		void ForceScreenUpdate ();
	}

	public interface PauzableWatchComponent: WatchComponent
	{
		void Pauze();
		void Start();
	}
}

