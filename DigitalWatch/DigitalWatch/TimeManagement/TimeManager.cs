using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

namespace DigitalWatch.Timemanagement
{
	/// <summary>
	/// Delegate used by the TimeManager to notify components when an interval or alarm is triggered
	/// </summary>
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
		private readonly static Lazy<TimeManager> instance = new Lazy<TimeManager>(System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
		/// <summary>
		/// 	The central timer
		/// </summary>
		private readonly Timer clockTimer;
		/// <summary>
		/// A dictionary which holds all the time object for the differant components.
		/// Each time object is identied by a key. A component kan get his time by supplying his key (also called token)
		/// </summary>
		private readonly Dictionary<object, Time> timeDict;
		/// <summary>
		/// A list with alarmReservations. An alarmReservation maps to a Time object.
		/// </summary>
		private readonly ArrayList alarmReservations;
		/// <summary>
		/// A list with intervalReservations. An interval is triggered a each iteration of the clockTimer.
		/// </summary>
		private readonly ArrayList intervalReservations;

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

		/// <summary>
		/// Initializes a new instance of the <see cref="DigitalWatch.Timemanagement.TimeManager"/> class.
		/// </summary>
		public TimeManager()
		{
			clockTimer = new Timer (1000);
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

		/// <summary>
		/// 	Gets an instance of the TimeManager.
		/// 	The TimeManager in Lazyevaluated an wil start automaticly
		/// 	with ticking.
		/// </summary>
		/// <returns>The instance.</returns>
		public static TimeManager GetInstance()
		{
			if (!instance.IsValueCreated)
			{
				instance.Value.Start ();
			}
			return instance.Value;
		}

		/// <summary>
		/// 	Creates a new Time object an return a token to it.
		/// 	You can use this token get the currentTime, change the time, set 
		/// 	alarms for this time and subscribe for interval notifications.
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

		/// <summary>
		/// 	Iterates over all the time object in the
		/// 	timeDist and calls the Increase function on them
		/// </summary>
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

		/// <summary>
		/// Checks the alarms.
		/// </summary>
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

		/// <summary>
		/// 	Trigger the interval subscribers which made a reservation in the intervalReservations list.
		/// 	This method only triggers a reservation when it's enabled
		/// </summary>
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


		/// <summary>
		/// Gets the current time.
		/// </summary>
		/// <returns>The current time.</returns>
		/// <param name="token">The timeToken which the program has recieved 
		/// 	by calling the GetTimeToken method</param>
		public Time GetCurrentTime(object token)
		{
			Time currentTime;
			lock (timeDict)
			{
				currentTime = timeDict [token];
			}
			return currentTime; 
		}

		/// <summary>
		/// Changes the time of a certen time object
		/// </summary>
		/// <param name="token">The timeToken retrieved by calling GetTimeToken</param>
		/// <param name="newTime">The new time</param>
		public void ChangeTime(object token, Time newTime)
		{
			lock (timeDict)
			{
				timeDict [token] = newTime;
			}
		}

		/// <summary>
		/// 	Adds an alarm reservation on a specific time object
		/// </summary>
		/// <returns>An alarm token. Use this token to cancel the alarm at a later time</returns>
		/// <param name="listener">Listener.</param>
		/// <param name="time">Time.</param>
		/// <param name="timeToken">Time token.</param>
		public object AddAlarm(TimeReached listener, Time time, object timeToken)
		{
			//Create a copy of the time object so it can not be changed externaly
			Time timeCopy = time.MakeCopy ();

			var reservation = new AlarmReservation () {
				AlarmTime = timeCopy,
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

		/// <summary>
		/// 	Adds an interval reservation on a specific time object
		/// </summary>
		/// <returns>A token to enable, disable or remove the interval reservation at a later time</returns>
		/// <param name="listener">A Listener which will be triggered on each iteration</param>
		/// <param name="timeToken">The timeToken obtained from GetTimeToken</param>
		public object AddInterval(TimeReached listener, object timeToken)
		{
			var reservation = new IntervalReservation (){
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

		/// <summary>
		/// Removes the alarm.
		/// </summary>
		/// <param name="token">Token.</param>
		public void RemoveAlarm(object token)
		{
			lock (alarmReservations)
			{
				alarmReservations.Remove (token);
			}
		}

		/// <summary>
		/// Removes the an intervalReservation.
		/// </summary>
		/// <param name="token">Intervaltoken obtained from AddInterval</param>
		public void RemoveInterval(object token)
		{
			lock (intervalReservations)
			{
				intervalReservations.Remove (token);
			}
		}

		/// <summary>
		/// Pauzes an interval.
		/// </summary>
		/// <param name="token">The intervalToken obtained from AddInterval</param>
		public void PauzeInterval(object token)
		{
			EnableInterval (token, false);
		}

		/// <summary>
		/// Resumes an interval.
		/// </summary>
		/// <param name="token">The intervalToken obtained from AddInterval</param>
		public void ResumeInterval(object token)
		{
			EnableInterval (token, true);
		}

		/// <summary>
		/// Enables or disables an interval
		/// </summary>
		/// <param name="token">Token.</param>
		/// <param name="enableInterval">If set to <c>true</c> enable interval.</param>
		private void EnableInterval(object token, bool enableInterval)
		{
			lock(intervalReservations)
			{
				foreach (IntervalReservation res in intervalReservations)
				{
					if(res.Equals(token))
					{
						res.Enabled = enableInterval;
					}
				}
			}
		}

		/// <summary>
		/// Removes a Time object and al intervals and alarms connected to it.
		/// </summary>
		/// <param name="timeToken">The timeToken obtained from GetTimeToken</param>
		public void RemoveTime(object timeToken)
		{
			RemoveAllAlarms (timeToken);
			RemoveAllIntervals (timeToken);

			lock (timeDict)
			{
				timeDict.Remove (timeToken);
			}
		}

		/// <summary>
		/// Removes all alarms connected to a specific timeToken
		/// </summary>
		/// <param name="timeToken">Time token.</param>
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

		/// <summary>
		/// Removes all intervals connected to a specific timeToken
		/// </summary>
		/// <param name="timeToken">Time token.</param>
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

