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
                throw new TimetableException("This teacher does not teach this subject", TimetableError.TeacherSubject);
        }

        public IList<TimetableError> ValidateSubject(Course course, Subject subject)
        {
            List<TimetableError> ret = new List<TimetableError>();
            if (course.Teacher != null && !course.Teacher.IsATeachedSubject(subject) && subject != null)
            {
                ret.Add(TimetableError.TeacherSubject);
               
            }
            return ret;

        }

        public void ChangeTime(Course course, Day day, Time from, Time to)
        {
            List<TimetableError> errors = new List<TimetableError>();
            if (course.Teacher != null && course.Teacher.HasCourseAtTimePeriod(day, from, to))
                errors.Add(TimetableError.TeacherTime);
            if (course.Group != null && course.Group.HasCourseAtTimePeriod(day, from, to))
                errors.Add(TimetableError.GroupTime);
            if (errors.Count > 0)
                throw new TimetableException(errors);
            course.SetTimespan(day, from, to);
        }

        public IList<TimetableError> ValidateTime(Course course, Day day, Time from, Time to)
        {
            List<TimetableError> errors = new List<TimetableError>();
            if (course.Teacher != null && course.Teacher.HasCourseAtTimePeriod(day, from, to))
                errors.Add(TimetableError.TeacherTime);
            if (course.Group != null && course.Group.HasCourseAtTimePeriod(day, from, to))
                errors.Add(TimetableError.GroupTime);
            return errors;
        }

        public void ChangeTeacher(Course course, Teacher teacher)
        {
            if (teacher != null && teacher.HasCourseAtTheSameTime(course))
                throw new TimetableException("New teacher has a course at that time", TimetableError.TeacherTime);
            course.Teacher = teacher;
        }

        public IList<TimetableError> ValidateTeacher(Course course, Teacher teacher)
        {
            List<TimetableError> ret = new List<TimetableError>();
            if (teacher.HasCourseAtTheSameTime(course))
                ret.Add(TimetableError.TeacherTime);
            if (!teacher.IsATeachedSubject(course.Subject))
                ret.Add(TimetableError.TeacherSubject);
            return ret;
        }

        public void ChangeGroup(Course course, Group group)
        {
            if (group.HasCourseAtTheSameTime(course))
                throw new TimetableException("New Group has a course overlapping the this one", TimetableError.GroupTime);
            course.Group = group;
            
        }

        public IList<TimetableError> ValidateGroup(Course course, Group group)
        {
            List<TimetableError> ret = new List<TimetableError>();
            if (group.HasCourseAtTheSameTime(course)){
                ret.Add(TimetableError.GroupTime);
            }
            return ret;
        }

        public IList<TimetableError> ValidateAll(Course course, Group group, Teacher teacher, Subject subject, Day day, Time from, Time to)
        {
            List<TimetableError> ret = new List<TimetableError>();
            if (!teacher.IsATeachedSubject(subject))
                ret.Add(TimetableError.TeacherSubject);
            if (teacher.HasCourseAtTimePeriod(day,from,to))
                ret.Add(TimetableError.TeacherTime);
            if (group.HasCourseAtTimePeriod(day,from,to))
                ret.Add(TimetableError.GroupTime);
            return ret;
        }


    }
}
