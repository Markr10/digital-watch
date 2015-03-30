using System;
using System.Media;
using DigitalWatch.Timemanagement;

namespace DigitalWatch.Components
{
	public class Alarm : WatchComponent
	{
		public event UpdateScreen OnScreenUpdate;
		private bool editorMode;
		private bool alarmEnabled;
		private Timemanagement.Time currentTime;
		private object timeToken;
		private object alarmToken;
		private TimeManager timeManager;

		public Alarm (object timeToken)
		{
			editorMode = false;
			alarmEnabled = false;
			this.timeToken = timeToken;
			timeManager = TimeManager.GetInstance ();
			currentTime = timeManager.GetCurrentTime (timeToken).MakeCopy();
		}

		public void PrimaryButtonPress()
		{
			if (editorMode)
			{
				currentTime.Increase ();
			} 
			else
			{
				if (alarmEnabled == true)
				{
					DisableAlarm ();
				}
				else if(HasValitAlarmTime())
				{
					SetAlarm ();
				}
			}
			UpdateScreen ();
		}

		private bool HasValitAlarmTime()
		{
			return currentTime.Minutes > 0 || currentTime.Hours > 0;
		}

		public void SecondaryButtonPress()
		{
			if (editorMode)
			{
				currentTime.Decrease ();
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
			UpdateScreen ();
		}

		private void SetAlarm()
		{
			DisableAlarm ();
			alarmToken = timeManager.AddAlarm (new TimeReached (OnTimeManagerNotify), currentTime, timeToken);
			alarmEnabled = true;
		}

		private void DisableAlarm()
		{
			if (alarmEnabled)
			{
				timeManager.RemoveAlarm (alarmToken);
				alarmEnabled = false;
			}
		}

		public void OnTimeManagerNotify(Timemanagement.Time currentTime)
		{
			SystemSounds.Beep.Play ();
		}

		private void UpdateScreen()
		{
			if (OnScreenUpdate != null)
			{
				string writeString = currentTime.ToString ();
				if (alarmEnabled)
				{
					writeString += " E";
				}
				else
				{
					writeString += " D";
				}

				OnScreenUpdate (writeString, editorMode, this);
			}
		}
	}
}

