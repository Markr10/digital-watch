using System;
using Gtk;
using DigitalWatch.Watches;
using DigitalWatch.Watches.Builders;
using DigitalWatch.Displays;
using DigitalWatch.Components;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		BasicWatchBuilder builder = new BasicWatchBuilder ();
		Time timeComp = new Time ();
		timeComp.Start ();
		Alarm alarmComp = new Alarm (timeComp.GetTimeToken());
		builder.AddComponent (timeComp);
		builder.AddComponent (alarmComp);

		Timer timerComp = new Timer ();
		builder.AddComponent (timerComp);

		Stopwatch stopWatchComp = new Stopwatch ();
		builder.AddComponent (stopWatchComp);

		builder.SetDisplay (lcddisplaywidget1);

		Watch myWatch = builder.CreateWatch ();

		BasicWatchBuilder builder2 = new BasicWatchBuilder ();
		Time timeComp2 = new Time ();
		timeComp2.Start ();
		Alarm alarmComp2 = new Alarm (timeComp2.GetTimeToken());
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
