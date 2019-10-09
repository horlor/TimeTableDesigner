using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableDesigner.Model
{
   public class Time
    {
        private int hour, min;
        public int Hour
        {
            get { return hour; }
            set
            {
                if (value >= 0 && value < 24)
                    hour = value;
                else
                    throw new ArgumentOutOfRangeException("Hour");
            }
        }
        public int Minute
        {
            get { return min; }
            set
            {
                if (value < 0 || value >= 60)
                    throw new ArgumentOutOfRangeException("Minute");
                min = value;
            }
        }

        public Time(int h, int m)
        {
            Hour = h;
            Minute = m;
        }


        public static bool  operator<(Time lhs, Time rhs)
        {
            if (lhs is null || rhs is null)
                return false;
            return lhs.hour < rhs.hour || lhs.hour ==rhs.hour && lhs.min < rhs.min;
        }

        public static bool operator>(Time lhs, Time rhs)
        {
            return rhs < lhs;
        }

        public static bool operator==(Time lhs, Time rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                    return true;
                return false;
            }
            return lhs.Equals(rhs);
        }

        public static bool operator!=(Time lhs, Time rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            Time time = obj as Time;
            if (time is null)
                return false;
            return hour == time.hour && min == time.min;
        }

        public override int GetHashCode()
        {
            return hour.GetHashCode() * 14453 + min.GetHashCode();
        }
    }


    public enum Day
    {
        Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
    }
}
