using System;

namespace DigitalWatch.Timemanagement
{
	public class Time
	{
		public int Hours { get; set; }
		public int Minutes { get; set; }

		public void Increase()
		{
			if (Minutes > 59)
			{
				Minutes = 0;
				if (Hours > 24)
				{
					Hours = 0;
				}
				else
				{
					Hours += 1;
				}
			}
			else
			{
				Minutes += 1;
			}
		}

		public void Decrease()
		{
			if (Minutes < 1)
			{
				Minutes = 59;
				if (Hours < 1)
				{
					Hours = 23;
				}
				else
				{
					Hours-= 1;
				}
			}
			else
			{
				Minutes-= 1;
			}
		}

		public TimeSpan Subtract(Time time)
		{
			int subHours = Hours - time.Hours;
			int subMinutes = Minutes - time.Minutes;
			return new TimeSpan (subHours, subMinutes, 0);
		}

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

		public Time MakeCopy()
		{
			return new Time (){ Hours = this.Hours, Minutes = this.Minutes };
		}

		public override int GetHashCode ()
		{
			return (Hours * 100) + Minutes;
		}

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
			return timeString;
		}

	}
}

