using System.Timers;
using DigitalWatch.Timemanagement;

namespace DigitalWatch.Components
{
    /// <summary>
    /// Stopwatch component for the watch.
    /// </summary>
    public class Stopwatch : WatchComponent
    {
        /// <summary>
        /// Occurs when the screen should update.
        /// </summary>
        public event UpdateScreen OnScreenUpdate;

        /// <summary>
        /// The timer used for this timer instance.
        /// </summary>
        private System.Timers.Timer timer;
        /// <summary>
        /// The time of this stopwatch.
        /// </summary>
        private readonly Time stopwatchTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalWatch.Components.Stopwatch"/> class.
        /// </summary>
        public Stopwatch()
        {
            stopwatchTime = new Timemanagement.Time(){ Hours = 0, Minutes = 0 };
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(OnTimerElapsed);
        }

        /// <summary>
        /// Raises the timer elapsed event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Elapsed event arguments.</param>
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            lock (stopwatchTime)
            {
                stopwatchTime.Increase();
            }

            ForceScreenUpdate();
        }

        /// <summary>
        /// Enables or disables the timer (toggle).
        /// </summary>
        public void PrimaryButtonPress()
        {
            timer.Enabled = !timer.Enabled;
            ForceScreenUpdate();
        }

        /// <summary>
        /// Resets the stopwatch to zero.
        /// </summary>
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

        /// <summary>
        /// Does nothing. There is no implementation for this method.
        /// </summary>
        public void PrimaryButtonLongPress()
        {
            // No implementation for PrimaryLongButtonPress
        }

        /// <summary>
        /// Forces the compontent to write something the screen.
        /// </summary>
        public void ForceScreenUpdate()
        {
            if (OnScreenUpdate != null)
            {
                Displays.DisplayTextPart[] textParts;
                lock (stopwatchTime)
                {
                    textParts = stopwatchTime.ToDisplayTextParts(BlinkingPart.None);
                }
                OnScreenUpdate(textParts, this);
            }
        }
    }
}

