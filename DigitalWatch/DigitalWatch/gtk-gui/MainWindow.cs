
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.Table table1;
	
	private global::DigitalWatch.Displays.LCDDisplayWidget lcddisplaywidget1;
	
	private global::DigitalWatch.Displays.SegmentDisplayWidget segmentdisplaywidget2;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.table1 = new global::Gtk.Table (((uint)(2)), ((uint)(2)), false);
		this.table1.Name = "table1";
		this.table1.RowSpacing = ((uint)(6));
		this.table1.ColumnSpacing = ((uint)(6));
		// Container child table1.Gtk.Table+TableChild
		this.lcddisplaywidget1 = new global::DigitalWatch.Displays.LCDDisplayWidget ();
		this.lcddisplaywidget1.Events = ((global::Gdk.EventMask)(256));
		this.lcddisplaywidget1.Name = "lcddisplaywidget1";
		this.table1.Add (this.lcddisplaywidget1);
		global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1 [this.lcddisplaywidget1]));
		w1.XOptions = ((global::Gtk.AttachOptions)(4));
		// Container child table1.Gtk.Table+TableChild
		this.segmentdisplaywidget2 = new global::DigitalWatch.Displays.SegmentDisplayWidget ();
		this.segmentdisplaywidget2.Events = ((global::Gdk.EventMask)(256));
		this.segmentdisplaywidget2.Name = "segmentdisplaywidget2";
		this.table1.Add (this.segmentdisplaywidget2);
		global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1 [this.segmentdisplaywidget2]));
		w2.LeftAttach = ((uint)(1));
		w2.RightAttach = ((uint)(2));
		w2.XOptions = ((global::Gtk.AttachOptions)(4));
		w2.YOptions = ((global::Gtk.AttachOptions)(4));
		this.Add (this.table1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 653;
		this.DefaultHeight = 300;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
	}
}
