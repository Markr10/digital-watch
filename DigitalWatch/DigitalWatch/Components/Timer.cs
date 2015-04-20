using System;
using System.Timers;
using System.Media;

namespace DigitalWatch.Components
{
	/// <summary>
	/// Timer WatchComponent. Implements timer functionality.
	/// </summary>
	public class Timer : WatchComponent
	{
		public event UpdateScreen OnScreenUpdate;
		/// <summary>
		/// Indicates if the Timer is in an editor mode or not
		/// </summary>
        private EditorMode editorMode;
		/// <summary>
		/// The time of this timer
		/// </summary>
		private Timemanagement.Time time;
		/// <summary>
		/// 	The timer used for this timer instance 
		/// </summary>
		private System.Timers.Timer timer;

		/// <summary>
		/// Initializes a new instance of the <see cref="DigitalWatch.Components.Timer"/> class.
		/// </summary>
		public Timer ()
		{
            editorMode = EditorMode.None;
			time = new Timemanagement.Time (){ Hours = 0, Minutes = 0 };
			timer = new System.Timers.Timer (1000);
			timer.Elapsed += new ElapsedEventHandler (OnTimerElapsed);
		}

		/// <summary>
		/// Raises the timer elapsed event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void OnTimerElapsed(object sender, ElapsedEventArgs e)
		{
			lock (time)
			{
				time.Decrease ();
				if (time.Hours == 0 && time.Minutes == 0)
				{
					timer.Enabled = false;
					SystemSounds.Beep.Play ();
				}
			}

			ForceScreenUpdate ();
		}

		/// <summary>
		/// Called when the user pressed the primary button
		/// </summary>
		public void PrimaryButtonPress()
		{
            if (editorMode != EditorMode.None)
			{
				lock (time)
				{
                    switch (editorMode)
                    {
                        case EditorMode.Seconds:
                            time.Increase();
                            break;
                        case EditorMode.Minutes:
                            time.IncreaseMinutes();
                            break;
                        case EditorMode.Hours:
                            time.IncreaseHours();
                            break;
                        default:
                            throw new ArgumentException("Editor mode " + editorMode + " not implemented.");
                    }
				}

			}
			else
			{
				timer.Enabled = !timer.Enabled;
			}
			ForceScreenUpdate ();
		}

		/// <summary>
		/// Called when the user pressed the secondary button
		/// </summary>
		public void SecondaryButtonPress()
		{
            if(editorMode == EditorMode.None)
            {
                return;
            }

			lock (time)
			{
                switch (editorMode)
                {
                    case EditorMode.Seconds:
                        time.Decrease();
                        break;
                    case EditorMode.Minutes:
                        time.DecreaseMinutes();
                        break;
                    case EditorMode.Hours:
                        time.DecreaseHours();
                        break;
                    default:
                        throw new ArgumentException("Editor mode " + editorMode + " not implemented.");
                }
			}
			ForceScreenUpdate ();
		}

		// <summary>
		// Called when the user long presses the primary button
		// </summary>
		public void PrimaryButtonLongPress()
		{
			bool validTime;
			lock (time)
			{
				validTime = time.Hours > 0 || time.Minutes > 0;
			}

            switch (editorMode)
            {
                case EditorMode.None:
                    timer.Enabled = false;
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
                    if (validTime)
                    {
                        timer.Enabled = true;
                    }
                    break;
                default:
                    throw new ArgumentException("Editor mode " + editorMode + " not implemented.");
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
                Displays.DisplayTextPart[] textParts;
				lock (time)
				{
                    switch (editorMode)
                    {
                        case EditorMode.None:
                            OnScreenUpdate(time.ToDisplayTextParts(BlinkingPart.None), this);
                            break;
                        case EditorMode.Seconds:
                            OnScreenUpdate(time.ToDisplayTextParts(BlinkingPart.Seconds), this);
                            break;
                        case EditorMode.Minutes:
                            OnScreenUpdate(time.ToDisplayTextParts(BlinkingPart.Minutes), this);
                            break;
                        case EditorMode.Hours:
                            OnScreenUpdate(time.ToDisplayTextParts(BlinkingPart.Hours), this);
                            break;
                        default:
                            throw new ArgumentException("Editor mode " + editorMode + " not implemented.");
                    }
				}

                OnScreenUpdate(textParts, this);
			}
		}
	}
}

