using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TimetableDesigner.Model
{

    [JsonObject(IsReference = true)]
    public class Teacher : EntityBase
    {
        
        [JsonProperty(PropertyName ="Courses")]
        private List<Course> courses = new List<Course>();

        [JsonProperty(PropertyName ="TeachedSubjects")]
        private List<Subject> teachedSubjects = new List<Subject>();

        [JsonIgnore]
        public System.Collections.ObjectModel.ReadOnlyCollection<Course> Courses { //Somehow this Special class has more functions than IReadOnlyList
            get 
            {
                return courses.AsReadOnly(); 
            }
        }


        [JsonIgnore]
        public System.Collections.ObjectModel.ReadOnlyCollection<Subject> TeachedSubjects
        {
            get
            {
                return teachedSubjects.AsReadOnly();
            }
        }

        protected internal void AddCourse(Course course)
        {
            foreach (var item in Courses)
            {
                if (item.IsInTheSameTimeWith(course))
                    throw new TimetableException("There is a Course in the same time");
            }
            courses.Add(course);

        }
        protected internal void RemoveCourse(Course course)
        {
                courses.Remove(course);
        }

        public void RemoveFromAllCourses()
        {
            while(courses.Count > 0)
            {
                courses[0].Teacher = null;
            }
        }

        public void AddSubject(Subject subject)
        {
            if (teachedSubjects.IndexOf(subject) == -1)
                teachedSubjects.Add(subject);
        }

        public void RemoveSubject(Subject subject)
        {
            teachedSubjects.Remove(subject);
        }

        public bool IsATeachedSubject(Subject subject)
        {
            return teachedSubjects.IndexOf(subject)!=-1;
        }

        public bool HasCourseAtTheSameTime(Course course)
        {
            foreach (Course item in courses)
            {
                //That we shouldn't compare it with itself
                if (!ReferenceEquals(item, course) && item.IsInTheSameTimeWith(course))
                    return true;
            }
            return false;
        }

        public bool HasCourseAtTimePeriod(Day day, Time from, Time to)
        {
            foreach(Course c in courses)
            {
                if (c.IsOverlappingWithTimePeriod(day, from, to))
                    return true;
            }
            return false;
        }

        public override string ToString()
        {
            return this.Name;
        }

    }
}
