using System;
using System.Collections.Generic;
using System.Media;
using DigitalWatch.Timemanagement;

namespace DigitalWatch.Components
{
	/// <summary>
	/// Alarm component for the watch
	/// </summary>
	public class Alarm : WatchComponent
	{
		public event UpdateScreen OnScreenUpdate;
		/// <summary>
		/// 	Indicates if the alarm is in editorMode
		/// </summary>
		private bool editorMode;
		/// <summary>
		/// 	Indicats if the alarm is enabled or not
		/// </summary>
		private bool alarmEnabled;
		/// <summary>
		/// 	Time object witch stores the alarm time
		/// </summary>
		private Timemanagement.Time currentTime;
		/// <summary>
		/// 	The timetoken used for scheduling alarms
		/// </summary>
		private object timeToken;
		/// <summary>
		/// 	The alarmToken used to cancel a running alarm
		/// </summary>
		private object alarmToken;
		/// <summary>
		/// The time manager used by this component
		/// </summary>
		private TimeManager timeManager;

		public Alarm (object timeToken)
		{
			editorMode = false;
			alarmEnabled = false;
			this.timeToken = timeToken;
			timeManager = TimeManager.GetInstance ();
			currentTime = timeManager.GetCurrentTime (timeToken).MakeCopy();
		}

		/// <summary>
		/// 	Called when the user pressed the primary button.
		/// 	Increases the minutes when the component is in editorMode
		/// 	and enables or disables the alarm if the watch is not in editorMode
		/// </summary>
		public void PrimaryButtonPress()
		{
			if (editorMode)
			{
				currentTime.IncreaseMinutes ();
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
			ForceScreenUpdate ();
		}

		/// <summary>
		/// Determines whether this instance has valit alarm time.
		/// </summary>
		/// <returns><c>true</c> if this instance has valit alarm time; otherwise, <c>false</c>.</returns>
		private bool HasValitAlarmTime()
		{
			return currentTime.Minutes > 0 || currentTime.Hours > 0;
		}

		/// <summary>
		/// 	Decreases the minutes when the component is in editor mode
		/// </summary>
		public void SecondaryButtonPress()
		{
			if (editorMode)
			{
				currentTime.DecreaseMinutes ();
				ForceScreenUpdate ();
			}
		}

		/// <summary>
		/// 	Toggles the editorMode
		/// </summary>
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
			ForceScreenUpdate ();
		}

		/// <summary>
		/// Forces the compontent to write something the screen
		/// </summary>
		public void ForceScreenUpdate ()
		{
			if (OnScreenUpdate != null)
			{
                List<Displays.DisplayTextPart> listWithTextParts;
                if (editorMode)
                {
                    listWithTextParts = currentTime.ToListWithDisplayTextParts(BlinkingPart.Seconds);
                }
                else
                {
                    listWithTextParts = currentTime.ToListWithDisplayTextParts(BlinkingPart.None);
                }

                string alarmText = alarmEnabled ? " E" : " D";
                listWithTextParts.Add(new Displays.DisplayTextPart(alarmText, false));

                OnScreenUpdate (listWithTextParts.ToArray(), this);
			}
		}

		/// <summary>
		/// Sets the alarm. Disables the current alarm if ti has been set
		/// </summary>
		private void SetAlarm()
		{
			DisableAlarm ();
			alarmToken = timeManager.AddAlarm (new TimeReached (OnTimeManagerNotify), currentTime, timeToken);
			alarmEnabled = true;
		}

		/// <summary>
		/// Disables the alarm.
		/// </summary>
		private void DisableAlarm()
		{
			if (alarmEnabled)
			{
				timeManager.RemoveAlarm (alarmToken);
				alarmEnabled = false;
			}
		}

		/// <summary>
		/// Raises the time manager notify event.
		/// Sound the alarm
		/// </summary>
		/// <param name="currentTime">Current time.</param>
		public void OnTimeManagerNotify(Timemanagement.Time currentTime)
		{
			SystemSounds.Beep.Play ();
		}


	}
}

