using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TimetableDesigner.Model
{
    /// <summary>
    /// The Class for representing a teacher.
    /// </summary>
    [JsonObject(IsReference = true)]
    public class Teacher : EntityBase
    {
        //Providing a constructor, to make new class
        
        [JsonProperty(PropertyName ="Courses")]
        private List<Course> courses = new List<Course>();

        [JsonProperty(PropertyName ="TeachedSubjects")]
        private List<Subject> teachedSubjects = new List<Subject>();

        /// <summary>
        /// This is a List Accesser for the Courses they teach, it cannot be modified by this reference
        /// </summary>
        [JsonIgnore]
        public System.Collections.ObjectModel.ReadOnlyCollection<Course> Courses { //Somehow this Special class has more functions than IReadOnlyList
            get 
            {
                return courses.AsReadOnly(); 
            }
        }

        /// <summary>
        /// Only readonly representation of the teached subjects, it can be modified by the class's methods
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<Subject> TeachedSubjects
        {
            get
            {
                return teachedSubjects.AsReadOnly();
            }
        }

        public void AddCourse(Course course)
        {
            foreach (var item in Courses)
            {
                if (item.IsInTheSameTimeWith(course))
                    throw new TeacherTimeException("There are a Course in the same time");
            }
            courses.Add(course);

        }
        public void RemoveCourse(Course course)
        {
            courses.Remove(course);
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
