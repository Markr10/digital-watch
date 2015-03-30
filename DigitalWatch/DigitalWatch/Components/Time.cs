using System;
using DigitalWatch.Timemanagement;

namespace DigitalWatch.Components
{
	public class Time : PauzableWatchComponent
	{
		public event UpdateScreen OnScreenUpdate;
		private object timeToken;
		private TimeManager timeManager;
		private bool inEditorMode;

		public Time ()
		{
			timeManager = TimeManager.GetInstance ();
			inEditorMode = false;
		}

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

		public object GetTimeToken()
		{
			return timeToken;
		}

		public void Pauze()
		{
			if (timeToken != null)
			{
				timeManager.PauzeInterval (timeToken);
			}
		}

		public void ForceScreenUpdate ()
		{
			OnTimeUpdate (GetCurrentTime());
		}

		private void OnTimeUpdate(Timemanagement.Time currentTime)
		{
			if (OnScreenUpdate != null)
			{
				OnScreenUpdate (currentTime.ToString(), inEditorMode, this);
			}
		}

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

		public void PrimaryButtonLongPress()
		{
			inEditorMode = !inEditorMode;
			ForceScreenUpdate ();
		}
	}
}

