
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::DigitalWatch.Displays.LCDDisplayWidget lcddisplaywidget1;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.lcddisplaywidget1 = new global::DigitalWatch.Displays.LCDDisplayWidget ();
		this.lcddisplaywidget1.Events = ((global::Gdk.EventMask)(256));
		this.lcddisplaywidget1.Name = "lcddisplaywidget1";
		this.Add (this.lcddisplaywidget1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 400;
		this.DefaultHeight = 300;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
	}
}
