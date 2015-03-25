using System;
using DigitalWatch.Time;

namespace DigitalWatch.Components
{
	public class Time : WatchComponent
	{
		public event UpdateScreen OnScreenUpdate;

		public Time ()
		{

		}

		public void Start()
		{
			TimeManager.NotifyOnInterval(new TimeReached(OnTimeUpdate), 1000);
		}

		private void OnTimeUpdate(DateTime currentTime)
		{
			if (OnScreenUpdate != null)
			{
				string text = currentTime.Hour + ":" + currentTime.Minute;
				OnScreenUpdate (text, false, this);
			}
		}

		public void PrimaryButtonPress()
		{

		}

		public void SecondaryButtonPress()
		{

		}

		public void PrimaryButtonLongPress()
		{

		}
	}
}

