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
                if (teacher==null || teacher.IsATeachedSubject(value))
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

        // This is kinda OOP hack, teacher or group should tell if change is not needed
        //TODO try to find better solution
        //idea: remove and readd at change - too resourcedemanding
        public void SetTimespan(Day day, Time from, Time to)
        {
            if (teacher != null && teacher.HasCourseAtTimePeriod(day, from, to))
                throw new ArgumentException("Teacher has course at the given timeperiod");
            if (group != null && group.HasCourseAtTimePeriod(day, from, to))
                throw new ArgumentException("Given group has a Course at the Given timeperiod");
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
