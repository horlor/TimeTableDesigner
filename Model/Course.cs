using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableDesigner.Model
{
   // TODO Validáció egy külön osztályba 
    // A megjelenítésen kezdeni ügyeskedni - egyelőre logika nélkül mondjuk csak egy órarend
    //Legyen UWP
    public class Course : EntityBase
    {

        private Group group;
        public Group Group
        {
            get
            {
                return group;
            }
            set
            {
                if (group != null)
                {
                    group.RemoveCourse(this);
                }
                group.AddCourse(this);
                group = value;
            }
        }

        private Teacher teacher;
        public Teacher Teacher
        {
            get
            {
                return teacher;
            }
            set
            {
                if (teacher != null)
                {
                    teacher.RemoveCourse(this);
                }
                value.AddCourse(this);
                teacher = value;
            }
        }

        private Subject subject;
        public Subject Subject
        {
            get
            {
                return subject;
            }
            set
            {
                subject = value;
            }
        }

        public Day Day { get; set; }

        private Time start, end;

        public Time Start
        {
            get
            {
                return start;
            }
        }

        public Time End
        {
            get
            {
                return end;
            }
        }

        public void SetTimespan(Time from, Time to)
        {
            SetTimespan(Day, from, to);
        }


        public void SetTimespan(Day day, Time from, Time to)
        {
            if (from != null && to != null && from < to)
            {
                start = from;
                end = to;
                Day = day;
            }
            else
                throw new ArgumentException("Starttime is later than endtime");
        }


        public bool IsInTheSameTimeWith(Course c)
        {
            return IsOverlappingWithTimePeriod(c.Day, c.start, c.end);
        }

        public bool IsOverlappingWithTimePeriod(Day day, Time from, Time to)
        {
            //checking for same day
            return Day == day && (
                //Period has started after this, but not after this has ended
                start < from && from < end ||
                //Period started before this, but it had not ended before this started
                from < start && start < to);
        }
    }

    public enum Day
    {
        Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
    }
}
