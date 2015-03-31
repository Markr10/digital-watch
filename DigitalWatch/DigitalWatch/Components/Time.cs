using System;
using DigitalWatch.Timemanagement;

namespace DigitalWatch.Components
{
	/// <summary>
	/// 	TimeCompontent witch displays the time (not to confuse with the Time class in the TimeManagement namespace).
	/// </summary>
	public class Time : PauzableWatchComponent
	{
		public event UpdateScreen OnScreenUpdate;
		/// <summary>
		/// 	The time token used to get and change the time
		/// </summary>
		private object timeToken;
		private TimeManager timeManager;
		/// <summary>
		/// 	Indicates if the Time componend is in editorMode or not
		/// </summary>
		private bool inEditorMode;

		public Time ()
		{
			timeManager = TimeManager.GetInstance ();
			inEditorMode = false;
		}

		public Time(object timeToken)
		{
			timeManager = TimeManager.GetInstance ();
			inEditorMode = false;
			this.timeToken = timeToken;
			timeManager.AddInterval (new TimeReached (OnTimeUpdate), timeToken);
		}

		/// <summary>
		/// Start/resume this instance. 
		/// The Time component will automaticly acuire a timeToken if the token was not supplyed
		/// in the contructor.
		/// </summary>
		public void Start()
		{
			if (timeToken == null)
			{
				timeToken = timeManager.GetTimeToken ();
				timeManager.AddInterval (new TimeReached (OnTimeUpdate), timeToken);

			}
			else
			{
				timeManager.ResumeInterval (timeToken);
			}
			ForceScreenUpdate ();
		}

		/// <summary>
		/// Pauze this instance.
		/// </summary>
		public void Pauze()
		{
			if (timeToken != null)
			{
				timeManager.PauzeInterval (timeToken);
			}
		}

		/// <summary>
		/// Forces the compontent to write something the screen
		/// </summary>
		public void ForceScreenUpdate ()
		{
			OnTimeUpdate (GetCurrentTime());
		}

		/// <summary>
		/// Raises the time update event.
		/// </summary>
		/// <param name="currentTime">Current time.</param>
		private void OnTimeUpdate(Timemanagement.Time currentTime)
		{
			if (OnScreenUpdate != null)
			{
				OnScreenUpdate (currentTime.ToString(), inEditorMode, this);
			}
		}

		/// <summary>
		/// Gets the current time.
		/// </summary>
		/// <returns>The current time.</returns>
		private Timemanagement.Time GetCurrentTime()
		{
			if (timeToken != null)
			{
				return timeManager.GetCurrentTime (timeToken);
			}
			else
			{
				throw new Exception ("No timeToken. Get one by calling the start function");
			}
		}

		/// <summary>
		/// 	Wil increase the minutes when the Time component is in editor mode
		/// </summary>
		public void PrimaryButtonPress()
		{
			if (inEditorMode)
			{
				Timemanagement.Time currentTime = GetCurrentTime ();
				currentTime.IncreaseMinutes ();
				timeManager.ChangeTime (timeToken, currentTime);
				ForceScreenUpdate ();
			}
		}

		/// <summary>
		/// 	Decreases the time when the component is in editor mode.
		/// </summary>
		public void SecondaryButtonPress()
		{
			if (inEditorMode)
			{
				Timemanagement.Time currentTime = GetCurrentTime ();
				currentTime.DecreaseMinutes ();
				timeManager.ChangeTime (timeToken, currentTime);
				ForceScreenUpdate ();
			}
		}
		/// <summary>
		/// 	Toggles the editor mode 
		/// </summary>
		public void PrimaryButtonLongPress()
		{
			inEditorMode = !inEditorMode;
			ForceScreenUpdate ();
		}
	}
}

