﻿using System;
using System.Collections;
using System.Timers;

namespace DigitalWatch.Time
{
	public delegate void TimeReached(DateTime currentTime);

	public static class TimeManager
	{
		private static ArrayList timers = new ArrayList ();

		public static DateTime GetCurrentTime()
		{
			return DateTime.Now;
		}

		public static object NotifyOnce(TimeReached listener, DateTime time)
		{
			TimeSpan diff = new TimeSpan (DateTime.Now, time);
			Timer timer = new Time (diff.Milliseconds);
			timer.Elapsed += new ElapsedEventHandler ((object sender, ElapsedEventArgs e) =>
			{
				CancelTimer(sender);
				listener(GetCurrentTime());

			});
			timer.Enabled = true;
			timers.Add (timer);
			return timer;
		}

		public static object NotifyOnInterval(TimeReached listener, int interval)
		{
			Timer timer = new Timer (interval);
			timer.Elapsed += new ElapsedEventHandler ((object sender, ElapsedEventArgs e) =>
			{
				listener(GetCurrentTime());
			});
			timer.Enabled = true;
			AddTimer(timer);
			return timer;
		}

		public static void CancelTimer(object token)
		{
			Timer timer = token as Timer;
			timer.Dispose ();
			RemoveTimer(timer);
		}

		private static void AddTimer(Timer timer)
		{
			lock (timers)
			{
				timers.Add (timer);
			}
		}

		private static void RemoveTimer(Timer timer)
		{
			lock (timers)
			{
				timers.Remove (timer);
			}
		}
	}
}

