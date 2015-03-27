using System;

namespace DigitalWatch.Components
{
	public class Timer : WatchComponent
	{
		public event UpdateScreen OnScreenUpdate;
		private bool editorMode;
		private bool hoursSelected;
		private int hours;
		private int minutes;

		public Timer ()
		{
			editorMode = false;
			hoursSelected = false;
			hours = minutes = 0;
		}

		public void Start()
		{

		}

		public void Stop()
		{


		}

		public void PrimaryButtonPress()
		{
			if (hoursSelected)
			{
				IncrementHours ();
			} 
			else
			{
				IncrementMinutes ();
			}
		}

		public void SecondaryButtonPress()
		{
			hoursSelected = !hoursSelected;
		}

		public void PrimaryButtonLongPress()
		{
			editorMode = true;
		}

		public void ForceScreenUpdate ()
		{

		}

		private void IncrementHours()
		{
			if (hours < 24)
			{
				hours++;
			} 
			else
			{
				hours = 0;
			}
		}

		private void IncrementMinutes()
		{
			if (minutes < 60)
			{
				minutes++;
			} 
			else
			{
				minutes = 0;

			}
		}
	}
}

