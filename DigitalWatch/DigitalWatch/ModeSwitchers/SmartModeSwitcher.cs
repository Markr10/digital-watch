using DigitalWatch.Components;

namespace DigitalWatch.Mode
{
	/// <summary>
	/// 	This is a modified version of the normal ModeSwitcher. The SmartModeSwitcher 
	/// 	adds support for PauzebleWatchComponents
	/// </summary>
	public class SmartModeSwitcher<M> : ModeSwitcher<M>
	{
		/// <summary>
		/// Adds a Mode the the list. Call the Start methods of the componten if
		/// the componten is the first added component (acive component) and is 
		/// a pauzeblecomponent.
		/// </summary>
		/// <param name="mode">Mode.</param>
		public override void AddMode (M mode)
		{
			base.AddMode (mode);
			if (innerList.Count == 0 && mode is PauzableWatchComponent)
			{
				((PauzableWatchComponent)mode).Start ();
			}
		}

		/// <summary>
		/// Switches to the next mode. Calls Pauze on the previous mode
		/// and calls Start on the new currentmode (if pauzebleComponent)
		/// </summary>
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

	}
}

