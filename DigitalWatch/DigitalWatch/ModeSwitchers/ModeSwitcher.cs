using System;
using System.Collections;

namespace DigitalWatch.Mode
{
	public class ModeSwitcher<M>
	{
		protected ArrayList innerList;
		protected int currentMode;


		public ModeSwitcher ()
		{
			innerList = new ArrayList ();
			currentMode = 0;
		}

		public virtual void AddMode(M mode)
		{
			innerList.Add (mode);
		}

		public virtual void AddModes(M[] modes)
		{
			foreach (M mode in modes) 
			{
				innerList.Add (mode);
			}
		}

		public virtual void NextMode()
		{
			if (currentMode >= (innerList.Count -1)) 
			{
				currentMode = 0;
			} 
			else 
			{
				currentMode++;
			}
		}

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

		public bool IsCurrentMode(M mode)
		{
			return mode.Equals(GetCurrentMode());
		}
	}
}

