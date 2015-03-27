using System;
using DigitalWatch.Components;

namespace DigitalWatch.Mode
{
	public class SmartModeSwitcher<M> : ModeSwitcher<M>
	{
		public SmartModeSwitcher ()
		{
		}

		public override void AddMode (M mode)
		{
			base.AddMode (mode);
		}

		public override void NextMode ()
		{
			M curMode = GetCurrentMode ();
			if(curMode is PauzableWatchComponent)
			{
				((PauzableWatchComponent)curMode).Pauze ();
			}

			base.NextMode ();

			curMode = GetCurrentMode ();
			if(curMode is PauzableWatchComponent)
			{
				((PauzableWatchComponent)curMode).Start ();
			}
		}

		private void StartFirstModeIfNeeded()
		{

		}
	}
}

