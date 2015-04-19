using System;
using DigitalWatch.Timemanagement;

namespace DigitalWatch.Components
{
	/// <summary>
	/// 	TimeCompontent witch displays the time (not to confuse with the Time class in the TimeManagement namespace).
	/// </summary>
	public class TimeComponent : PauzableWatchComponent
	{
		public event UpdateScreen OnScreenUpdate;
		/// <summary>
		/// 	The time token used to get and change the time
		/// </summary>
		private object timeToken;
		private TimeManager timeManager;
		/// <summary>
		/// 	Indicates if the Time component is in an editor mode or not
		/// </summary>
        private EditorMode editorMode;

		public TimeComponent ()
		{
			timeManager = TimeManager.GetInstance ();
            editorMode = EditorMode.None;
		}

		public TimeComponent(object timeToken)
		{
			timeManager = TimeManager.GetInstance ();
            editorMode = EditorMode.None;
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
                switch (editorMode)
                {
                    case EditorMode.None:
                        OnScreenUpdate(currentTime.ToDisplayTextParts(BlinkingPart.None), this);
                        break;
                    case EditorMode.Seconds:
                        OnScreenUpdate(currentTime.ToDisplayTextParts(BlinkingPart.Seconds), this);
                        break;
                    case EditorMode.Minutes:
                        OnScreenUpdate(currentTime.ToDisplayTextParts(BlinkingPart.Minutes), this);
                        break;
                    case EditorMode.Hours:
                        OnScreenUpdate(currentTime.ToDisplayTextParts(BlinkingPart.Hours), this);
                        break;
                    default:
                        throw new ArgumentException("Editor mode " + editorMode + " not implemented.");
                }
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
            if(editorMode == EditorMode.None)
            {
                return;
            }

            Timemanagement.Time currentTime = GetCurrentTime();
            switch (editorMode)
            {
                case EditorMode.Seconds:
                    currentTime.Increase();
                    break;
                case EditorMode.Minutes:
                    currentTime.IncreaseMinutes();
                    break;
                case EditorMode.Hours:
                    currentTime.IncreaseHours();
                    break;
                default:
                    throw new ArgumentException("Editor mode " + editorMode + " not implemented.");
            }
            timeManager.ChangeTime (timeToken, currentTime);
            ForceScreenUpdate ();
		}

		/// <summary>
		/// 	Decreases the time when the component is in editor mode.
		/// </summary>
		public void SecondaryButtonPress()
		{
            if(editorMode == EditorMode.None)
            {
                return;
            }

            Timemanagement.Time currentTime = GetCurrentTime();
            switch (editorMode)
            {
                case EditorMode.Seconds:
                    currentTime.Decrease();
                    break;
                case EditorMode.Minutes:
                    currentTime.DecreaseMinutes();
                    break;
                case EditorMode.Hours:
                    currentTime.DecreaseHours();
                    break;
                default:
                    throw new ArgumentException("Editor mode " + editorMode + " not implemented.");
            }
            timeManager.ChangeTime (timeToken, currentTime);
            ForceScreenUpdate ();
		}
		/// <summary>
		/// Switch between the editor modes 
		/// </summary>
        /// <exception cref="T:System.ArgumentException"></exception>
		public void PrimaryButtonLongPress()
		{
            switch (editorMode)
            {
                case EditorMode.None:
                    editorMode = EditorMode.Seconds;
                    break;
                case EditorMode.Seconds:
                    editorMode = EditorMode.Minutes;
                    break;
                case EditorMode.Minutes:
                    editorMode = EditorMode.Hours;
                    break;
                case EditorMode.Hours:
                    editorMode = EditorMode.None;
                    break;
                default:
                    throw new ArgumentException("Editor mode " + editorMode + " not implemented.");
            }
			ForceScreenUpdate ();
		}
	}
}

