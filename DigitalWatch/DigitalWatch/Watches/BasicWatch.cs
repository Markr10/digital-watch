using System;
using DigitalWatch.Displays;
using DigitalWatch.Components;
using DigitalWatch.Mode;

namespace DigitalWatch.Watches
{
	public class BasicWatch : Watch
	{
		private readonly ModeSwitcher<WatchComponent> modeSwitcher;
		private readonly Display display;

		public BasicWatch (Display watchDisplay, WatchComponent[] watchComponents)
		{
			modeSwitcher = new ModeSwitcher<WatchComponent> ();
			modeSwitcher.AddModes (watchComponents);
			display = watchDisplay;
			InitDisplayListeners ();
			InitComponetListeners (watchComponents);
			modeSwitcher.GetCurrentMode ().ForceScreenUpdate ();
		}

		private void InitComponetListeners(WatchComponent[] components)
		{
			foreach (WatchComponent comp in components)
			{
				comp.OnScreenUpdate += new UpdateScreen((string text, bool blink, WatchComponent sender) => {
					if(modeSwitcher.IsCurrentMode(sender))
					{
						display.Write(text, blink);
					}
				});
			}
		}

		private void InitDisplayListeners()
		{
			display.OnModeButtonPress += new OnButtonPress (() => {
				modeSwitcher.NextMode ();
				modeSwitcher.GetCurrentMode().ForceScreenUpdate();
			});
			display.OnPrimaryButtonPress += new OnButtonPress (() => modeSwitcher.GetCurrentMode ().PrimaryButtonPress ());
			display.OnPrimaryLongButtonPress += new OnButtonPress(() => modeSwitcher.GetCurrentMode().PrimaryButtonLongPress());
			display.OnSecondaryButtonPress += new OnButtonPress (() => modeSwitcher.GetCurrentMode ().SecondaryButtonPress ());
		}

		public void SwitchMode ()
		{
			modeSwitcher.NextMode ();
		}
	}
}

