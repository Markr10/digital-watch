using System;
using System.Timers;

namespace DigitalWatch.Components
{
	public class Stopwatch : WatchComponent
	{
		public event UpdateScreen OnScreenUpdate;
		private System.Timers.Timer timer;
		private Timemanagement.Time stopwatchTime;

		public Stopwatch ()
		{
			stopwatchTime= new Timemanagement.Time (){ Hours = 0, Minutes = 0 };
			timer = new System.Timers.Timer (1000);
			timer.Elapsed += new ElapsedEventHandler (OnTimerElapsed);
		}

		protected virtual void OnTimerElapsed(object sender, ElapsedEventArgs e)
		{
			lock (stopwatchTime)
			{
				stopwatchTime.Increase ();
			}

			ForceScreenUpdate ();
		}

		public void PrimaryButtonPress()
		{
			timer.Enabled = !timer.Enabled;
			ForceScreenUpdate ();
		}

		public void SecondaryButtonPress()
		{
			lock (stopwatchTime)
			{
				stopwatchTime.Hours = 0;
				stopwatchTime.Minutes = 0;
				stopwatchTime.Seconds = 0;
			}
			ForceScreenUpdate();
		}

		public void PrimaryButtonLongPress()
		{
			//No implementation for PrimaryLongButtonPress
		}

		public void ForceScreenUpdate ()
		{
			if (OnScreenUpdate != null)
			{
				string text;
				lock (stopwatchTime)
				{
					text = stopwatchTime.ToString ();
				}
				OnScreenUpdate (text, false, this);
			}
		}

	}
}

