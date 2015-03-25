using System;
using DigitalWatch.Time;

namespace DigitalWatch.Components
{
	public class Alarm : WatchComponent
	{
		event UpdateScreen OnScreenUpdate;
		bool editorMode;
		bool hoursSelected;
		bool alarmEnabled;
		int hours;
		int minutes;
		object timerToken;

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

