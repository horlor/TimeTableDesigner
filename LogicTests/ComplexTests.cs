﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TimetableDesigner.Model;

namespace LogicTests
{
    class ComplexTests
    {
        [Test]
        public void TeacherAndSubjectCheckInCourses()
        {
            Teacher teacher = new Teacher();
            Subject s1 = new Subject(), s2 = new Subject();
            teacher.AddSubject(s1);
            Course course = new Course();
            CourseManager.Instance.ChangeSubject(course, s1);
            Assert.DoesNotThrow(() => { CourseManager.Instance.ChangeTeacher(course, teacher); });
            Assert.False(teacher.IsATeachedSubject(s2));
            Assert.Catch<TimetableException>(() => { CourseManager.Instance.ChangeSubject(course,s2); });
        }
    }
}
