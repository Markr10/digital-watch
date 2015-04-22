using System;
using DigitalWatch.Timemanagement;

namespace DigitalWatch.Components
{
    /// <summary>
    /// TimeCompontent witch displays the time (not to confuse with the Time class in the TimeManagement namespace).
    /// </summary>
    public class TimeComponent : PausableWatchComponent
    {
        /// <summary>
        /// Occurs when the screen should update.
        /// </summary>
        public event UpdateScreen OnScreenUpdate;

        /// <summary>
        /// The time token used to get and change the time.
        /// </summary>
        private object timeToken;
        /// <summary>
        /// The time manager.
        /// </summary>
        private TimeManager timeManager;
        /// <summary>
        /// Indicates if the Time componend is in editorMode or not.
        /// </summary>
        private bool inEditorMode;

        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalWatch.Components.TimeComponent"/> class.
        /// </summary>
        public TimeComponent()
        {
            timeManager = TimeManager.GetInstance();
            inEditorMode = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalWatch.Components.TimeComponent"/> class.
        /// </summary>
        /// <param name="timeToken">Time token.</param>
        public TimeComponent(object timeToken)
        {
            timeManager = TimeManager.GetInstance();
            inEditorMode = false;
            this.timeToken = timeToken;
            timeManager.AddInterval(new TimeReached(OnTimeUpdate), timeToken);
        }

        /// <summary>
        /// Start/resume this instance. 
        /// The Time component will automaticly acquire a timeToken if the token was not supplied
        /// in the contructor.
        /// </summary>
        public void Start()
        {
            if (timeToken == null)
            {
                timeToken = timeManager.GetTimeToken();
                timeManager.AddInterval(new TimeReached(OnTimeUpdate), timeToken);

            }
            else
            {
                timeManager.ResumeInterval(timeToken);
            }
            ForceScreenUpdate();
        }

        /// <summary>
        /// Pause this instance.
        /// </summary>
        public void Pause()
        {
            if (timeToken != null)
            {
                timeManager.PauseInterval(timeToken);
            }
        }

        /// <summary>
        /// Forces the compontent to write something the screen.
        /// </summary>
        public void ForceScreenUpdate()
        {
            OnTimeUpdate(GetCurrentTime());
        }

        /// <summary>
        /// Raises the time update event.
        /// </summary>
        /// <param name="currentTime">Current time.</param>
        private void OnTimeUpdate(Time currentTime)
        {
            if (OnScreenUpdate != null)
            {
                if (inEditorMode)
                {
                    OnScreenUpdate(currentTime.ToDisplayTextParts(BlinkingPart.Seconds), this);
                }
                else
                {
                    OnScreenUpdate(currentTime.ToDisplayTextParts(BlinkingPart.None), this);
                }
            }
        }

        /// <summary>
        /// Gets the current time.
        /// </summary>
        /// <returns>The current time.</returns>
        private Time GetCurrentTime()
        {
            if (timeToken != null)
            {
                return timeManager.GetCurrentTime(timeToken);
            }
            else
            {
                throw new Exception("No timeToken. Get one by calling the start function.");
            }
        }

        /// <summary>
        /// Increases the minutes when the Time component is in editor mode.
        /// </summary>
        public void PrimaryButtonPress()
        {
            if (inEditorMode)
            {
                Time currentTime = GetCurrentTime();
                currentTime.IncreaseMinutes();
                timeManager.ChangeTime(timeToken, currentTime);
                ForceScreenUpdate();
            }
        }

        /// <summary>
        /// Decreases the time when the component is in editor mode.
        /// </summary>
        public void SecondaryButtonPress()
        {
            if (inEditorMode)
            {
                Time currentTime = GetCurrentTime();
                currentTime.DecreaseMinutes();
                timeManager.ChangeTime(timeToken, currentTime);
                ForceScreenUpdate();
            }
        }

        /// <summary>
        /// Toggles the editor mode.
        /// </summary>
        public void PrimaryButtonLongPress()
        {
            inEditorMode = !inEditorMode;
            ForceScreenUpdate();
        }
    }
}

