﻿using DigitalWatch.Components;
using DigitalWatch.Timemanagement;
using DigitalWatch.Watches;
using DigitalWatch.Watches.Builders;
using Gtk;

namespace DigitalWatch
{
    /// <summary>
    /// Main window.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// Builds the watches.
        /// </summary>
        public MainWindow() : base(WindowType.Toplevel)
        {
            Build();

            // Build a watch with a LCD display
            BasicWatchBuilder builder = new BasicWatchBuilder();
            object timeToken = TimeManager.GetInstance().GetTimeToken();
            TimeComponent timeComp = new  TimeComponent(timeToken);

            Alarm alarmComp = new Alarm(timeToken);
            builder.AddComponent(timeComp);
            builder.AddComponent(alarmComp);

            Timer timerComp = new Timer();
            builder.AddComponent(timerComp);

            Stopwatch stopWatchComp = new Stopwatch();
            builder.AddComponent(stopWatchComp);

            builder.SetDisplay(lcddisplaywidget);

            Watch myWatch = builder.CreateWatch();


            // Build a watch with a segment display
            BasicWatchBuilder segBuilder = new BasicWatchBuilder();
            object segTimeToken = TimeManager.GetInstance().GetTimeToken();
            TimeComponent segTimeComp = new TimeComponent(segTimeToken);

            Alarm segAlarmComp = new Alarm(segTimeToken);
            segBuilder.AddComponent(segTimeComp);
            segBuilder.AddComponent(segAlarmComp);

            segBuilder.SetDisplay(segmentdisplaywidget);

            Watch segWatch = segBuilder.CreateWatch();


            // Build a watch with a dial display
            BasicWatchBuilder dialBuilder = new BasicWatchBuilder();
            TimeComponent dialTimeComp = new TimeComponent();

            dialBuilder.AddComponent(dialTimeComp);

            dialBuilder.SetDisplay(dialdisplaywidget);

            Watch dialWatch = dialBuilder.CreateWatch();


            // Build a watch with a binary display
            BasicWatchBuilder binBuilder = new BasicWatchBuilder();

            TimeComponent binTimeComp = new TimeComponent();
            binTimeComp.Start();
            binTimeComp.PrimaryButtonLongPress();
            binBuilder.AddComponent(binTimeComp);

            binBuilder.SetDisplay(binarydisplaywidget);

            Watch binWatch = binBuilder.CreateWatch();
        }

        /// <summary>
        /// Raises the delete event event.
        /// The application calls this method when it is being closed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="a">Delete event arguments.</param>
        protected void OnDeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
            a.RetVal = true;
        }
    }
}
