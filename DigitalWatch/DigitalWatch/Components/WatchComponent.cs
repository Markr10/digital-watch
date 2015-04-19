
namespace DigitalWatch.Components
{
	/// <summary>
	/// 	Delegate used for updating the screen.
	/// </summary>
    public delegate void UpdateScreen(Displays.DisplayTextPart[] textParts, WatchComponent sender);

	/// <summary>
	/// A WatchComponent implements some kind of watch functionality.
	/// When the user interacts with the watch, the event will be send to the watchcomponent.
	/// The watchcomponent can do comthing with this user input (writing to the screen, setting an alarma, ...)
	/// </summary>
	public interface WatchComponent
	{
		event UpdateScreen OnScreenUpdate;
		/// <summary>
		/// 	Called when the user pressed the primary button
		/// </summary>
		void PrimaryButtonPress();
		/// <summary>
		/// 	Called when the user pressed the secondary button
		/// </summary>
		void SecondaryButtonPress();
		/// <summary>
		/// 	Called when the user long presses the primary button
		/// </summary>
		void PrimaryButtonLongPress();
		/// <summary>
		/// Forces the compontent to write something the screen
		/// </summary>
		void ForceScreenUpdate ();
	}

	/// <summary>
	/// 	The pauzeableWatchCompont is an upgraded version of the normal
	/// 	WatchComponent. The SmartModeSwitcher will call the Pauze function when
	/// 	the compont moves to the background and will call the Start function when 
	/// 	the componten moves to the foreground. 
	/// 	<para>
	/// 	PauzableWatchCOmponents can be used to make the program less resourse intensive
	/// </summary>
	public interface PauzableWatchComponent: WatchComponent
	{
		/// <summary>
		/// Pauze this instance.
		/// </summary>
		void Pauze();
		/// <summary>
		/// Start/resume this instance.
		/// </summary>
		void Start();
	}
}

