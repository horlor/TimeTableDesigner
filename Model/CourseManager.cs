using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableDesigner.Model
{
    public class CourseManager
    {
        private static CourseManager instance = null;

        private CourseManager() {}

        public static CourseManager Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new CourseManager();
                }
                return instance;
            }
        }


        
        public void ChangeSubject(Course course, Subject subject)
        {
            if (course.Teacher == null || course.Teacher.IsATeachedSubject(subject) || subject ==null)
            {
                course.Subject = subject;
            }
            else
                throw new TimetableException("This teacher does not teach this subject", TimetableError.TeacherTime);
        }

        public void ChangeTime(Course course, Day day, Time from, Time to)
        {
            List<TimetableError> errors = new List<TimetableError>();
            if (course.Teacher != null && course.Teacher.HasCourseAtTimePeriod(day, from, to))
                errors.Add(TimetableError.TeacherTime);
            if (course.Group != null && course.Group.HasCourseAtTimePeriod(day, from, to))
                errors.Add(TimetableError.GroupTime);
            if(errors.Count > 0)
                throw new TimeoutException()
            course.SetTimespan(day, from, to);
        }

        public void ChangeTeacher(Course course, Teacher teacher)
        {
            if (teacher != null && teacher.HasCourseAtTheSameTime(course))
                throw new TimetableException("New teacher has a course at that time", TimetableError.TeacherTime);
            course.Teacher = teacher;
        }

        public void ChangeGroup(Course course, Group group)
        {
            if (group.HasCourseAtTheSameTime(course))
                throw new TimetableException("New Group has a course overlapping the this one", TimetableError.GroupTime);
            course.Group = group;
            
        }


    }
}
