using System;
using Gtk;
using DigitalWatch.Watches;
using DigitalWatch.Watches.Builders;
using DigitalWatch.Components;
using DigitalWatch.Timemanagement;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
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

		builder.SetDisplay (lcddisplaywidget1);

		Watch myWatch = builder.CreateWatch ();



		BasicWatchBuilder segBuilder = new BasicWatchBuilder ();
		object segTimeToken = TimeManager.GetInstance ().GetTimeToken ();
		TimeComponent segTimeComp = new TimeComponent (segTimeToken);

		Alarm segAlarmComp = new Alarm (segTimeToken);
		segBuilder.AddComponent (segTimeComp);
		segBuilder.AddComponent (segAlarmComp);

		segBuilder.SetDisplay (segmentdisplaywidget2);

		Watch segWatch = segBuilder.CreateWatch ();

	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
