
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.Table table;
	
	private global::DigitalWatch.Displays.LCDDisplayWidget lcddisplaywidget;
	
	private global::DigitalWatch.Displays.SegmentDisplayWidget segmentdisplaywidget;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.table = new global::Gtk.Table (((uint)(2)), ((uint)(2)), false);
		this.table.Name = "table";
		this.table.RowSpacing = ((uint)(6));
		this.table.ColumnSpacing = ((uint)(6));
		// Container child table.Gtk.Table+TableChild
		this.lcddisplaywidget = new global::DigitalWatch.Displays.LCDDisplayWidget ();
		this.lcddisplaywidget.Events = ((global::Gdk.EventMask)(256));
		this.lcddisplaywidget.Name = "lcddisplaywidget";
		this.table.Add (this.lcddisplaywidget);
		global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table [this.lcddisplaywidget]));
		w1.XOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table.Gtk.Table+TableChild
		this.segmentdisplaywidget = new global::DigitalWatch.Displays.SegmentDisplayWidget ();
		this.segmentdisplaywidget.Events = ((global::Gdk.EventMask)(256));
		this.segmentdisplaywidget.Name = "segmentdisplaywidget";
		this.table.Add (this.segmentdisplaywidget);
		global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table [this.segmentdisplaywidget]));
		w2.LeftAttach = ((uint)(1));
		w2.RightAttach = ((uint)(2));
		w2.XOptions = ((global::Gtk.AttachOptions)(4));
		w2.YOptions = ((global::Gtk.AttachOptions)(4));
		this.Add (this.table);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 637;
		this.DefaultHeight = 407;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
	}
}
