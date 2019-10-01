using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableDesigner.Model
{
    public class Group : EntityBase
    {
       
        public Group(int id) : base(id)
        {
        }
        private List<Course> courses =  new List<Course>();

        public IReadOnlyList<Course> Courses
        {
            get
            {
                return courses.AsReadOnly();
            }
        }

        public void AddCourse(Course course)
        {
            foreach (var item in courses)
            {
                if (item.IsInTheSameTimeWith(course))
                    throw new ArgumentException("There are a Course in the same time");
            }
            courses.Add(course);
        }

        public void RemoveCourse(Course course)
        {
            courses.Remove(course);
        }
        public bool HasCourseAtTheSameTime(Course course)
        {
            foreach(Course item in courses)
            {
                //That we shouldn't compare it with itself
                if (!ReferenceEquals(item, course) && item.IsInTheSameTimeWith(course))
                    return true;
            }
            return false;
        }

        public bool CourseChanged(Course course)
        {
            return HasCourseAtTheSameTime(course);
        }
    }
}
