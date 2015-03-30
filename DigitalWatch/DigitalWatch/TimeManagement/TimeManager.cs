using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

namespace DigitalWatch.Timemanagement
{
	public delegate void TimeReached(Time currentTime);

	/// <summary>
	/// The Time manager is an important component of the application. 
	/// It manages the time for components in the watch application.
	/// 
	/// Components can subscribe for interval triggers and can subscribe for 
	/// alarm triggers.
	/// 
	/// The TimeManager is an Singeton object. You can use the GetInstance function to get an instance of this object.
	/// </summary>
	public sealed class TimeManager
	{
		/// <summary>
		/// 	An instance of the TimeManager. The instance is ThreadSafe
		/// </summary>
		private static Lazy<TimeManager> instance = new Lazy<TimeManager>(System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
		/// <summary>
		/// 	The central timer
		/// </summary>
		private Timer clockTimer;
		/// <summary>
		/// A dictionary which holds all the time object for the differant components.
		/// Each time object is identied by a key. A component kan get his time by supplying his key (also called token)
		/// </summary>
		private Dictionary<object, Time> timeDict;
		/// <summary>
		/// A list with alarmReservations. An alarmReservation maps to a Time object.
		/// </summary>
		private ArrayList alarmReservations;
		/// <summary>
		/// A list with intervalReservations. An interval is triggered a each iteration of the clockTimer.
		/// </summary>
		private ArrayList intervalReservations;

		/// <summary>
		/// Repesesents an IntervalReservation. An IntervalReservation object
		/// is a container which has components needed for the program
		/// </summary>
		private class IntervalReservation
		{
			public object TimeToken { get; set;}
			public TimeReached Listener { get; set; }
			public bool Enabled { get; set; }
		}

		/// <summary>
		/// An AlarmReservation contains the alarm time for a component
		/// </summary>
		private class AlarmReservation : IntervalReservation
		{
			public Time AlarmTime { get; set; }
		}

		public TimeManager()
		{
			clockTimer = new Timer (60 *1000);
			clockTimer.Elapsed += new ElapsedEventHandler (OnTimerElapsed);
			timeDict = new Dictionary<object, Time> ();
			alarmReservations = new ArrayList ();
			intervalReservations = new ArrayList ();
		}

		/// <summary>
		/// 	Starts the clockTimer of this TimeManager
		/// </summary>
		private void Start()
		{
			clockTimer.Enabled = true;
		}

		public static TimeManager GetInstance()
		{
			//Start the clock if this is the first instance
			if (!instance.IsValueCreated)
			{
				instance.Value.Start ();
			}
			return instance.Value;
		}

		/// <summary>
		/// 	Creates a new Time object an return a token to it.
		/// 	You can use this token get the currentTime, change the time, set alarms for this time and subscribe for interval notifications.
		/// 	This Method is threadsafe
		/// </summary>
		/// <returns>The time token.</returns>
		public object GetTimeToken()
		{
			object token = new object ();
			DateTime currentTIme = DateTime.Now;
			Time time = new Time () {
				Hours = currentTIme.Hour,
				Minutes = currentTIme.Minute
			};

			lock(timeDict)
			{
				timeDict.Add (token, time);
			}
			return token;
		}

		/// <summary>
		/// Raises the timer elapsed event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void OnTimerElapsed(object sender, ElapsedEventArgs e)
		{
			IncreaseTime ();
			CheckAlarms ();
			TriggerIntervals ();
		}


		private void IncreaseTime()
		{
			lock(timeDict)
			{
				foreach (object token in timeDict.Keys)
				{
					timeDict [token].Increase ();
				}
			}
		}

		private void CheckAlarms()
		{
			lock (alarmReservations)
			{
				foreach (AlarmReservation res in alarmReservations)
				{
					if (res.AlarmTime.Equals (GetCurrentTime(res.TimeToken)) && res.Enabled)
					{
						res.Listener (res.AlarmTime);
						alarmReservations.Remove (res);
					}
				}

			}
		}

		private void TriggerIntervals()
		{
			lock (intervalReservations)
			{
				foreach (IntervalReservation res in intervalReservations)
				{
					if (res.Enabled)
					{
						res.Listener (GetCurrentTime (res.TimeToken));
					}
				}
			}
		}



		public Time GetCurrentTime(object token)
		{
			Time currentTime;
			lock (timeDict)
			{
				currentTime = timeDict [token];
			}
			return currentTime; 
		}

		public void ChangeTime(object token, Time newTime)
		{
			lock (timeDict)
			{
				timeDict [token] = newTime;
			}
		}

		public object AddAlarm(TimeReached listener, Time time, object timeToken)
		{
			AlarmReservation reservation = new AlarmReservation () {
				AlarmTime = time,
				Enabled = true,
				Listener = listener,
				TimeToken = timeToken
			};

			lock (alarmReservations)
			{
				alarmReservations.Add (reservation);
			}

			return reservation;
		}

		public object AddInterval(TimeReached listener, object timeToken)
		{
			IntervalReservation reservation = new IntervalReservation (){
				Enabled = true,
				Listener = listener,
				TimeToken = timeToken
			};

			lock (intervalReservations)
			{
				intervalReservations.Add (reservation);
			}
			return reservation;
		}

		public void RemoveAlarm(object token)
		{
			lock (alarmReservations)
			{
				alarmReservations.Remove (token);
			}
		}

		public void RemoveInterval(object token)
		{
			lock (intervalReservations)
			{
				intervalReservations.Remove (token);
			}
		}

		public void PauzeInterval(object token)
		{
			lock (intervalReservations)
			{
				foreach (IntervalReservation res in intervalReservations)
				{
					if (res.Equals (token))
					{
						res.Enabled = false;
					}
				}
			}
		}

		public void ResumeInterval(object token)
		{
			lock(intervalReservations)
			{
				foreach (IntervalReservation res in intervalReservations)
				{
					if(res.Equals(token))
					{
						res.Enabled = true;
					}
				}
			}
		}

		public void RemoveTime(object timeToken)
		{
			RemoveAllAlarms (timeToken);
			RemoveAllIntervals (timeToken);

			lock (timeDict)
			{
				timeDict.Remove (timeToken);
			}
		}

		private void RemoveAllAlarms(object timeToken)
		{
			lock (alarmReservations)
			{
				foreach(AlarmReservation res in alarmReservations)
				{
					if(res.TimeToken.Equals(timeToken))
					{
						alarmReservations.Remove(res);
					}
				}
			}
		}

		private void RemoveAllIntervals(object timeToken)
		{
			lock (intervalReservations)
			{
				foreach(IntervalReservation res in intervalReservations)
				{
					if(res.TimeToken.Equals(timeToken))
					{
						intervalReservations.Remove(res);
					}
				}
			}
		}

	}
}

