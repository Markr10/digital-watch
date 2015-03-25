using System;
using System.Collections;

namespace DigitalWatch.Mode
{
	public class ModeSwitcher<M>
	{
		private ArrayList innerList;
		private int currentMode;


		public ModeSwitcher ()
		{
			innerList = new ArrayList ();
			currentMode = 0;
		}

		public void AddMode(M mode)
		{
			innerList.Add (mode);
		}

		public void AddModes(M[] modes)
		{
			foreach (M mode in modes) 
			{
				innerList.Add (mode);
			}
		}

		public void NextMode()
		{
			if (currentMode > innerList.Count) 
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
				return innerList [currentMode];
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

