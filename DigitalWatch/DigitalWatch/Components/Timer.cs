using System;
using DigitalWatch.Timemanagement;
using System.Timers;
using System.Media;

namespace DigitalWatch.Components
{
	public class Timer : WatchComponent
	{
		public event UpdateScreen OnScreenUpdate;
		private bool editorMode;
		protected Timemanagement.Time time;
		private System.Timers.Timer timer;

		public Timer ()
		{
			editorMode = false;
			time = new Timemanagement.Time (){ Hours = 0, Minutes = 0 };
			timer = new System.Timers.Timer (1000);
			timer.Elapsed += new ElapsedEventHandler (OnTimerElapsed);
		}

		protected virtual void OnTimerElapsed(object sender, ElapsedEventArgs e)
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

