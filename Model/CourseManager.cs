﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableDesigner.Model
{
    public class CourseManager
    {
        //TODO watching for null params
        public static void ChangeSubject(Course course, Subject subject)
        {
            if (course.Teacher == null || course.Teacher.IsATeachedSubject(subject))
            {
                course.Subject = subject;
            }
            else
                throw new TeacherSubjectException("This teacher does not teach this subject");
        }

        public static void ChangeTime(Course course, Day day, Time from, Time to)
        {
            if (course.Teacher != null && course.Teacher.HasCourseAtTimePeriod(day, from, to))
                throw new GroupTimeException();
            if (course.Group != null && course.Group.HasCourseAtTimePeriod(day, from, to))
                throw new GroupTimeException();
            course.SetTimespan(day, from, to);
        }

        public static void ChangeTeacher(Course course, Teacher teacher)
        {
            if (teacher.HasCourseAtTheSameTime(course))
                throw new TeacherTimeException("New teacher has a course at that time");
            course.Teacher = teacher;
        }

        public static void ChangeGroup(Course course, Group group)
        {
            if (group.HasCourseAtTheSameTime(course))
                throw new GroupTimeException("New Group has a course overlapping the new");
            course.Group = group;
            
        }


    }
}
