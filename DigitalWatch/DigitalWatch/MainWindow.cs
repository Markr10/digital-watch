using System;
using Gtk;
using DigitalWatch.Watches;
using DigitalWatch.Watches.Builders;
using DigitalWatch.Components;
using DigitalWatch.Timemanagement;

/// <summary>
/// Main window.
/// </summary>
public partial class MainWindow: Gtk.Window
{
	/// <summary>
	/// Initializes a new instance of the <see cref="MainWindow"/> class.
	/// Builds the watches.
	/// </summary>
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		// Build a watch with a LCD display
		BasicWatchBuilder builder = new BasicWatchBuilder ();
		object timeToken = TimeManager.GetInstance ().GetTimeToken ();
		TimeComponent timeComp = new  TimeComponent(timeToken);

		Alarm alarmComp = new Alarm (timeToken);
		builder.AddComponent (timeComp);
		builder.AddComponent (alarmComp);

		Timer timerComp = new Timer ();
		builder.AddComponent (timerComp);

		Stopwatch stopWatchComp = new Stopwatch ();
		builder.AddComponent (stopWatchComp);

		builder.SetDisplay (lcddisplaywidget);

		Watch myWatch = builder.CreateWatch ();


		// Build a watch with a segment display
		BasicWatchBuilder segBuilder = new BasicWatchBuilder ();
		object segTimeToken = TimeManager.GetInstance ().GetTimeToken ();
		TimeComponent segTimeComp = new TimeComponent (segTimeToken);

		Alarm segAlarmComp = new Alarm (segTimeToken);
		segBuilder.AddComponent (segTimeComp);
		segBuilder.AddComponent (segAlarmComp);

		segBuilder.SetDisplay (segmentdisplaywidget);

		Watch segWatch = segBuilder.CreateWatch ();

        // Build a watch with a dial display
        BasicWatchBuilder dialBuilder = new BasicWatchBuilder ();
        TimeComponent dialTimeComp = new TimeComponent ();

        dialBuilder.AddComponent (dialTimeComp);

        dialBuilder.SetDisplay (dialdisplaywidget);

        Watch dialWatch = dialBuilder.CreateWatch ();

	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
