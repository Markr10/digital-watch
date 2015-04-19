using System;
using System.Collections;

namespace DigitalWatch.Mode
{
	/// <summary>
	/// 	The ModeSwitcher is an class witch kan cycle between mode (WatchComponents)
	/// </summary>
	public class ModeSwitcher<M>
	{
		/// <summary>
		/// The inner list witch hold all the Modes
		/// </summary>
		protected readonly ArrayList innerList;
		/// <summary>
		/// Index of the currentMode
		/// </summary>
		protected int currentMode;

		/// <summary>
		/// Initializes a new instance of the <see cref="DigitalWatch.Mode.ModeSwitcher/> class.
		/// </summary>
		public ModeSwitcher ()
		{
			innerList = new ArrayList ();
			currentMode = 0;
		}

		/// <summary>
		/// Adds a Mode the the list
		/// </summary>
		/// <param name="mode">Mode.</param>
		public virtual void AddMode(M mode)
		{
			innerList.Add (mode);
		}

		/// <summary>
		/// Adds multiple modes to the list
		/// </summary>
		/// <param name="modes">Modes.</param>
		public virtual void AddModes(M[] modes)
		{
			foreach (M mode in modes) 
			{
				innerList.Add (mode);
			}
		}

		/// <summary>
		/// 	Switches to the next mode
		/// </summary>
		public virtual void NextMode()
		{
            currentMode = (currentMode + 1) % innerList.Count;
		}

		/// <summary>
		/// Gets the current mode
		/// </summary>
		/// <returns>The current mode.</returns>
		public M GetCurrentMode()
		{
			if (innerList.Count > 0) 
			{
				return (M)innerList [currentMode];
			} 
			else 
			{
				throw new IndexOutOfRangeException ("This mode switcher has no Modes. " +
					"First add some modes before calling this function");
			}
		}

		/// <summary>
		/// Determines whether the suplied mode equals the current mode
		/// </summary>
		/// <returns><c>true</c> if the suplied mode equals the currentmode or <c>false</c> if 
		/// the suplied mode does not equal the currentmode</returns>
		/// <param name="mode">Mode.</param>
		public bool IsCurrentMode(M mode)
		{
			return mode.Equals(GetCurrentMode());
		}
	}
}

