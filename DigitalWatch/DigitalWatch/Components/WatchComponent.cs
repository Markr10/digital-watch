
namespace DigitalWatch.Components
{
    /// <summary>
    /// Delegate used for updating the screen.
    /// </summary>
    public delegate void UpdateScreen(Displays.DisplayTextPart[] textParts,WatchComponent sender);

    /// <summary>
    /// A WatchComponent implements some kind of watch functionality.
    /// When the user interacts with the watch, the event will be send to the watch component.
    /// The watch component can do something with this user input (writing to the screen, setting an alarm, etc.)
    /// </summary>
    public interface WatchComponent
    {
        /// <summary>
        /// Occurs when the screen should update.
        /// </summary>
        event UpdateScreen OnScreenUpdate;

        /// <summary>
        /// Called when the user pressed the primary button.
        /// </summary>
        void PrimaryButtonPress();

        /// <summary>
        /// Called when the user pressed the secondary button.
        /// </summary>
        void SecondaryButtonPress();

        /// <summary>
        /// Called when the user long presses the primary button.
        /// </summary>
        void PrimaryButtonLongPress();

        /// <summary>
        /// Forces the compontent to write something the screen.
        /// </summary>
        void ForceScreenUpdate();
    }

    /// <summary>
    /// The pauseableWatchComponent is an upgraded version of the normal
    /// WatchComponent. The SmartModeSwitcher will call the Pause function when
    /// the component moves to the background and will call the Start function when 
    /// the components moves to the foreground. 
    /// 
    /// <para>PausableWatchCOmponents can be used to make the program less resourse intensive.</para>
    /// </summary>
    public interface PausableWatchComponent: WatchComponent
    {
        /// <summary>
        /// Pause this instance.
        /// </summary>
        void Pause();

        /// <summary>
        /// Start/resume this instance.
        /// </summary>
        void Start();
    }
}

