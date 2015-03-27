using System;
using DigitalWatch.Time;

namespace DigitalWatch.Components
{
	public class Time : PauzableWatchComponent
	{
		public event UpdateScreen OnScreenUpdate;
		private object token;

		public Time ()
		{

		}

		public void Start()
		{
			if (token == null)
			{
				token = TimeManager.NotifyOnInterval (new TimeReached (OnTimeUpdate), 1000);
			}
			else
			{
				TimeManager.ResumeTimer (token);
			}

		}

		public void Pauze()
		{
			if (token != null)
			{
				TimeManager.PauzeTimer (token);
			}
		}

		public void ForceScreenUpdate ()
		{
			OnTimeUpdate (TimeManager.GetCurrentTime ());
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

