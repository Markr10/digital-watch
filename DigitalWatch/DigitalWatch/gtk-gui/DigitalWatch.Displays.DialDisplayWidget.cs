
// This file has been generated by the GUI designer. Do not modify.
namespace DigitalWatch.Displays
{
	public partial class DialDisplayWidget
	{
		private global::Gtk.VBox vbox1;
		
		private global::Gtk.DrawingArea drawingarea1;
		
		private global::Gtk.HBox hbox1;
		
		private global::Gtk.Button PrimaryButton;
		
		private global::Gtk.Button PrimaryLongButton;
		
		private global::Gtk.Button SecondaryButton;
		
		private global::Gtk.Button ModeButton;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget DigitalWatch.Displays.DialDisplayWidget
			global::Stetic.BinContainer.Attach (this);
			this.Name = "DigitalWatch.Displays.DialDisplayWidget";
			// Container child DigitalWatch.Displays.DialDisplayWidget.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.drawingarea1 = new global::Gtk.DrawingArea ();
			this.drawingarea1.Name = "drawingarea1";
			this.vbox1.Add (this.drawingarea1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.drawingarea1]));
			w1.Position = 0;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.PrimaryButton = new global::Gtk.Button ();
			this.PrimaryButton.CanFocus = true;
			this.PrimaryButton.Name = "PrimaryButton";
			this.PrimaryButton.UseUnderline = true;
			this.PrimaryButton.Label = global::Mono.Unix.Catalog.GetString ("Primary");
			this.hbox1.Add (this.PrimaryButton);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.PrimaryButton]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.PrimaryLongButton = new global::Gtk.Button ();
			this.PrimaryLongButton.CanFocus = true;
			this.PrimaryLongButton.Name = "PrimaryLongButton";
			this.PrimaryLongButton.UseUnderline = true;
			this.PrimaryLongButton.Label = global::Mono.Unix.Catalog.GetString ("Primary Long");
			this.hbox1.Add (this.PrimaryLongButton);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.PrimaryLongButton]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.SecondaryButton = new global::Gtk.Button ();
			this.SecondaryButton.CanFocus = true;
			this.SecondaryButton.Name = "SecondaryButton";
			this.SecondaryButton.UseUnderline = true;
			this.SecondaryButton.Label = global::Mono.Unix.Catalog.GetString ("Secondary");
			this.hbox1.Add (this.SecondaryButton);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.SecondaryButton]));
			w4.Position = 2;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.ModeButton = new global::Gtk.Button ();
			this.ModeButton.CanFocus = true;
			this.ModeButton.Name = "ModeButton";
			this.ModeButton.UseUnderline = true;
			this.ModeButton.Label = global::Mono.Unix.Catalog.GetString ("Mode");
			this.hbox1.Add (this.ModeButton);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.ModeButton]));
			w5.PackType = ((global::Gtk.PackType)(1));
			w5.Position = 3;
			w5.Expand = false;
			w5.Fill = false;
			this.vbox1.Add (this.hbox1);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox1]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.drawingarea1.ExposeEvent += new global::Gtk.ExposeEventHandler (this.OnExpose);
			this.PrimaryButton.Clicked += new global::System.EventHandler (this.OnPrimaryButtonClicked);
			this.PrimaryLongButton.Clicked += new global::System.EventHandler (this.OnPrimaryLongButtonClicked);
			this.SecondaryButton.Clicked += new global::System.EventHandler (this.OnSecondaryButtonClicked);
			this.ModeButton.Clicked += new global::System.EventHandler (this.OnModeButtonClicked);
		}
	}
}
