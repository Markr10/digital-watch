using System;
using Gtk;
using DigitalWatch.Watches;
using DigitalWatch.Watches.Builders;
using DigitalWatch.Displays;
using DigitalWatch.Components;
using DigitalWatch.Timemanagement;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		BasicWatchBuilder builder = new BasicWatchBuilder ();
		object timeToken = TimeManager.GetInstance ().GetTimeToken ();
		DigitalWatch.Components.Time timeComp = new  DigitalWatch.Components.Time(timeToken);

		Alarm alarmComp = new Alarm (timeToken);
		builder.AddComponent (timeComp);
		builder.AddComponent (alarmComp);

		Timer timerComp = new Timer ();
		builder.AddComponent (timerComp);

		Stopwatch stopWatchComp = new Stopwatch ();
		builder.AddComponent (stopWatchComp);

		builder.SetDisplay (lcddisplaywidget1);

		Watch myWatch = builder.CreateWatch ();



		BasicWatchBuilder builder2 = new BasicWatchBuilder ();
		object timeToken2 = TimeManager.GetInstance ().GetTimeToken ();
		DigitalWatch.Components.Time timeComp2 = new DigitalWatch.Components.Time (timeToken2);

		Alarm alarmComp2 = new Alarm (timeToken2);
		builder2.AddComponent (timeComp2);
		builder2.AddComponent (alarmComp2);

		builder2.SetDisplay (segmentdisplaywidget2);

		Watch myWatch2 = builder2.CreateWatch ();

	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
