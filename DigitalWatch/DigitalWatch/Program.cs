using Gtk;

namespace DigitalWatch
{
    class MainClass
    {
        public static void Main()
        {
            Application.Init();
            MainWindow win = new MainWindow();
            win.Show();
            Application.Run();
        }
    }
}
