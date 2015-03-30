using System;

namespace DigitalWatch.Timemanagement
{
	/// <summary>
	/// This object represents time in this application.
	/// A time object has Hours, Minutes and Seconds. You use the
	/// different increase and decrease functions to manipulate the time.
	/// </summary>
	public class Time
	{
		public int Hours { get; set; }
		public int Minutes { get; set; }
		public int Seconds { get; set; }

		/// <summary>
		/// Basic increase function. Increases the Seconds.
		/// Increases Minutes when Seconds &gt; 59
		/// </summary>
		public void Increase()
		{
			if (Seconds > 59)
			{
				Seconds = 0;
				IncreaseMinutes ();
			}
			else
			{
				Seconds += 1;
			}
		}

		/// <summary>
		/// Basic decrease function. Decreases the Seconds.
		/// Decreases Minutes when Seconds &lt; 1
		/// </summary>
		public void Decrease()
		{
			if (Seconds < 1)
			{
				Seconds = 59;
				DecreaseMinutes ();
			}
			else
			{
				Seconds-= 1;
			}
		}

		/// <summary>
		/// Increases the hours.
		/// </summary>
		public void IncreaseHours()
		{
			if (Hours > 23)
			{
				Hours = 0;
			}
			else
			{
				Hours ++;
			}
		}

		/// <summary>
		/// Decreases the hours.
		/// </summary>
		public void DecreaseHours()
		{
			if (Hours < 1)
			{
				Hours = 23;
			}
			else
			{
				Hours--;
			}
		}

		/// <summary>
		/// Increases the minutes.
		/// Incresaes the hours when Minutes &gt; 59
		/// </summary>
		public void IncreaseMinutes()
		{
			if (Minutes > 59)
			{
				Minutes = 0;
				IncreaseHours ();
			}
			else
			{
				Minutes ++;
			}
		}

		/// <summary>
		/// Decreases the minutes.
		/// Decreases the hours when Minutes &lt; 1
		/// </summary>
		public void DecreaseMinutes()
		{
			if (Minutes < 1)
			{
				Minutes = 59;
				DecreaseHours ();
			}
			else
			{
				Minutes--;
			}
		}

		/// <summary>
		/// Subtract this time from another time object
		/// </summary>
		/// <param name="time">The subtracted time as TimeSpan</param>
		public TimeSpan Subtract(Time time)
		{
			int subHours = Hours - time.Hours;
			int subMinutes = Minutes - time.Minutes;
			int subSeconds = Seconds - time.Seconds;
			return new TimeSpan (subHours, subMinutes, subSeconds);
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="DigitalWatch.Timemanagement.Time"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="DigitalWatch.Timemanagement.Time"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
		/// <see cref="DigitalWatch.Timemanagement.Time"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals (object obj)
		{
			if (obj is Time)
			{
				Time timeObj = (Time)obj;
				return timeObj.Hours == Hours && timeObj.Minutes == Minutes;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Makes a full copy of this object
		/// </summary>
		/// <returns>An copy of this object</returns>
		public Time MakeCopy()
		{
			return new Time (){ Hours = this.Hours, Minutes = this.Minutes, Seconds = this.Seconds };
		}

		/// <summary>
		/// Serves as a hash function for a <see cref="DigitalWatch.Timemanagement.Time"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode ()
		{
			return (Hours * 10000) + (Minutes * 100) + Seconds;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="DigitalWatch.Timemanagement.Time"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="DigitalWatch.Timemanagement.Time"/>.</returns>
		public override string ToString ()
		{
			string timeString = string.Empty;
			if (Hours < 10)
			{
				timeString += "0";
			}
			timeString += Hours;
			timeString += ":";
			if (Minutes < 10)
			{
				timeString += "0";
			}
			timeString += Minutes;
			timeString += ":";
			if (Seconds < 10)
			{
				timeString += "0";
			}
			timeString += Seconds;
			return timeString;
		}

	}
}

