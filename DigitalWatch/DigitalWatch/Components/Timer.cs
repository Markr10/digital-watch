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
		/// Indicates if the Timer is in editor mode or not
		/// </summary>
		private bool editorMode;
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
			editorMode = false;
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
			if (editorMode)
			{
				lock (time)
				{
					time.IncreaseMinutes ();
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
			if (editorMode)
			{
				lock (time)
				{
					time.DecreaseMinutes ();
				}
				ForceScreenUpdate ();
			}
		}

		/// <summary>
		/// Called when the user long presses the primary button
		/// </summary>
		public void PrimaryButtonLongPress()
		{
			bool valitTime;
			lock (time)
			{
				valitTime = time.Hours > 0 || time.Minutes > 0;
			}


			if (editorMode && valitTime)
			{
				timer.Enabled = true;
			}
			else if (!editorMode)
			{
				timer.Enabled = false;
			}
			editorMode = !editorMode;
			ForceScreenUpdate ();
		}

		/// <summary>
		/// Forces the compontent to write something the screen
		/// </summary>
		public void ForceScreenUpdate ()
		{
			if (OnScreenUpdate != null)
			{
				string text;
				lock (time)
				{
					text = time.ToString ();
				}

				OnScreenUpdate (text, editorMode, this);
			}
		}


	}
}

