using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableDesigner.Model
{
    [JsonObject( IsReference = true)]
    public class Group : EntityBase
    {

        [JsonProperty]
        private List<Course> courses =  new List<Course>();

        public System.Collections.ObjectModel.ReadOnlyCollection<Course> Courses
        {
            get
            {
                return courses.AsReadOnly();
            }
        }

        protected internal void AddCourse(Course course)
        {
            foreach (var item in courses)
            {
                if (item.IsInTheSameTimeWith(course))
                    throw new GroupTimeException("There are a Course in the same time");
            }
            
            courses.Add(course);
        }

        protected internal void RemoveCourse(Course course)
        {
            courses.Remove(course);
        }

        public void RemoveFromAllCourses()
        {
            while (courses.Count > 0)
                courses[0].Group = null;
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


        public bool HasCourseAtTimePeriod(Day day, Time from, Time to)
        {
            foreach (Course c in courses)
            {
                if (c.IsOverlappingWithTimePeriod(day, from, to))
                    return true;
            }
            return false;
        }
    }
}
