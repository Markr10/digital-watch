using System;
using DigitalWatch.Time;

namespace DigitalWatch.Components
{
	public class Alarm : WatchComponent
	{
		public event UpdateScreen OnScreenUpdate;
		private bool editorMode;
		private bool hoursSelected;
		private bool alarmEnabled;
		private int hours;
		private int minutes;
		private object timerToken;

		public Alarm ()
		{
			editorMode = false;
			hoursSelected = false;
			hours = minutes = 0;
			timerToken = null;
		}

		public void PrimaryButtonPress()
		{
			if (editorMode)
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
			else
			{
				if (timerToken != null)
				{
					DisableAlarm ();
				}
				else
				{
					SetAlarm ();
				}
			}
			UpdateScreen ();
		}

		public void SecondaryButtonPress()
		{
			if (editorMode)
			{
				hoursSelected = !hoursSelected;
			}
		}

		public void PrimaryButtonLongPress()
		{
			if (editorMode == false)
			{
				editorMode = true;
			} 
			else
			{
				editorMode = false;
				SetAlarm ();
			}
			UpdateScreen ();
		}

		public void ForceScreenUpdate ()
		{

		}

		private void SetAlarm()
		{
			DisableAlarm ();
			DateTime time = TimeManager.GetCurrentTime ();
			time.AddHours (hours - time.Hour);
			time.AddMinutes (minutes - time.Minute);
			timerToken = TimeManager.NotifyOnce (new TimeReached (OnTimeManagerNotify), time);
		}

		private void DisableAlarm()
		{
			if (timerToken != null)
			{
				TimeManager.CancelTimer (timerToken);
				timerToken = null;
			}
		}

		public void OnTimeManagerNotify(DateTime currentTime)
		{
			//Beep Beep	
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

		private void UpdateScreen()
		{
			if (OnScreenUpdate != null)
			{
				string writeString = hours + ":" + minutes;
				OnScreenUpdate (writeString, editorMode, this);
			}
		}
	}
}

