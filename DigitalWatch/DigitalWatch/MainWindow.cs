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
		builder.AddComponent (timeComp);

		builder.SetDisplay (lcddisplaywidget1);

		Watch myWatch = builder.CreateWatch ();

	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
