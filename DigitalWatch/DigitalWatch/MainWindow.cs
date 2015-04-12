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

		builder.SetDisplay (lcddisplaywidget);

		Watch myWatch = builder.CreateWatch ();



		BasicWatchBuilder segBuilder = new BasicWatchBuilder ();
		object segTimeToken = TimeManager.GetInstance ().GetTimeToken ();
		TimeComponent segTimeComp = new TimeComponent (segTimeToken);

		Alarm segAlarmComp = new Alarm (segTimeToken);
		segBuilder.AddComponent (segTimeComp);
		segBuilder.AddComponent (segAlarmComp);

		segBuilder.SetDisplay (segmentdisplaywidget);

		Watch segWatch = segBuilder.CreateWatch ();

		BasicWatchBuilder binBuilder = new BasicWatchBuilder ();

		TimeComponent binTimeComp = new TimeComponent ();
		binTimeComp.Start ();
		binTimeComp.PrimaryButtonLongPress ();
		binBuilder.AddComponent (binTimeComp);

		binBuilder.SetDisplay (Binarydisplaywidget);

		Watch binWatch = binBuilder.CreateWatch ();

	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
