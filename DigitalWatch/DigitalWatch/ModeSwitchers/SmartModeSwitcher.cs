using DigitalWatch.Components;

namespace DigitalWatch.Mode
{
    /// <summary>
    /// This is a modified version of the normal ModeSwitcher. The SmartModeSwitcher 
    /// adds support for PausebleWatchComponents.
    /// </summary>
    public class SmartModeSwitcher<Mode> : ModeSwitcher<Mode>
    {
        /// <summary>
        /// Adds a Mode the the list. Call the Start methods of the components if
        /// the components is the first added component (active component) and is 
        /// a pauseble component.
        /// </summary>
        /// <param name="mode">Mode.</param>
        public override void AddMode(Mode mode)
        {
            if (innerList.Count == 0 && mode is PausableWatchComponent)
            {
                ((PausableWatchComponent)mode).Start();
            }
            base.AddMode(mode);
        }

        /// <summary>
        /// Adds multiple modes to the list.
        /// </summary>
        /// <param name="modes">Modes.</param>
        public override void AddModes(Mode[] modes)
        {
            if (innerList.Count == 0 && modes[0] is PausableWatchComponent)
            {
                ((PausableWatchComponent)modes[0]).Start();
            }
            base.AddModes(modes);
        }

        /// <summary>
        /// Switches to the next mode. Calls Pause on the previous mode
        /// and calls Start on the new current mode (if pausebleComponent).
        /// </summary>
        public override void NextMode()
        {
            Mode curMode = GetCurrentMode();
            if (curMode is PausableWatchComponent)
            {
                ((PausableWatchComponent)curMode).Pause();
            }

            base.NextMode();

            curMode = GetCurrentMode();
            if (curMode is PausableWatchComponent)
            {
                ((PausableWatchComponent)curMode).Start();
            }
        }
    }
}

