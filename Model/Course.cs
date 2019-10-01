using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableDesigner.Model
{
    public class Course : EntityBase
    {
        public Course(int id) : base(id) { }

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

        //What to do if this is changed during run? x
        private Subject subject;
        public Subject Subject
        {
            get
            {
                return subject;
            }
            set
            {
                if (teacher.IsATeachedSubject(value))
                {
                    subject = value;
                }
                else
                    throw new ArgumentException("Current teacher can't teach this subject");

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
            set
            {
                if (end != null && value < end)
                    start = value;
                else
                    throw new ArgumentException();
            }
        }

        public Time End
        {
            get
            {
                return end;
            }
            set
            {
                if ( value > start)
                    end = value;
                else
                    throw new ArgumentException();
            }
        }

        public void SetTimespan(Time from, Time to)
        {
            if (from != null && to != null && from < to)
            {
                start = from;
                end = to;
            }
            else
                throw new ArgumentException("Starttime is later than endtime");
        }

        public void SetTimespan(Day day, Time from, Time to)
        {
            Day = day;
            SetTimespan(from, to);
        }


        public bool IsInTheSameTimeWith(Course c)
        {
            return Day == c.Day && (
                Start < c.Start && c.Start < End ||
                //this has started before c has, but this has not endded by the time c has started
                c.Start < Start && Start < c.End);
                //and the same in reverse roles
        }

    }

    public enum Day
    {
        Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
    }
}
