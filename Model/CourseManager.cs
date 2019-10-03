using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableDesigner.Model
{
    public class CourseManager
    {
        public static void ChangeSubjectTo(Course course, Subject subject)
        {
            if (course.Teacher.IsATeachedSubject(subject))
            {
                course.Subject = subject;
            }
            else
                throw new TeacherSubjectException("This teacher does not teach this subject");
        }

        public static void ChangeTimeTo(Course course, Day day, Time from, Time to)
        {
            if (course.Teacher.HasCourseAtTimePeriod(day, from, to))
                throw new GroupTimeException();
            if (course.Group.HasCourseAtTimePeriod(day, from, to))
                throw new GroupTimeException();
            course.SetTimespan(day, from, to);
        }


    }
}
