using System;
using DigitalWatch.Displays;
using DigitalWatch.Components;
using DigitalWatch.Mode;

namespace DigitalWatch.Watches
{
	/// <summary>
	/// The BasicWatch is a basic implementation of the Watch interface.
	/// It holds the reference to the display and used a SmartModeSwitcher to cycle between
	/// components.
	/// </summary>
	public class BasicWatch : Watch
	{
		/// <summary>
		/// The mode switcher. This will hold all the components
		/// </summary>
		private readonly ModeSwitcher<WatchComponent> modeSwitcher;
		/// <summary>
		/// The display.
		/// </summary>
		private readonly Display display;

		/// <summary>
		/// Initializes a new instance of the <see cref="DigitalWatch.Watches.BasicWatch"/> class.
		/// </summary>
		/// <param name="watchDisplay">Watch display.</param>
		/// <param name="watchComponents">Watch components.</param>
		public BasicWatch (Display watchDisplay, WatchComponent[] watchComponents)
		{
			modeSwitcher = new SmartModeSwitcher<WatchComponent> ();
			modeSwitcher.AddModes (watchComponents);
			display = watchDisplay;
			InitDisplayListeners ();
			InitComponentListeners (watchComponents);
			modeSwitcher.GetCurrentMode ().ForceScreenUpdate ();
		}

		/// <summary>
		/// Inits the component listeners.
		/// </summary>
		/// <param name="components">Components.</param>
		private void InitComponentListeners(WatchComponent[] components)
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

		/// <summary>
		/// Inits the display listeners.
		/// </summary>
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

