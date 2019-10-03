using NUnit.Framework;
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
            Teacher teacher = new Teacher(0);
            Subject s1 = new Subject(1), s2 = new Subject(2);
            teacher.AddSubject(s1);
            Course course = new Course(0);
            course.Subject = s1;
            Assert.DoesNotThrow(() => { course.Teacher = teacher; });
            Assert.False(teacher.IsATeachedSubject(s2));
            Assert.Catch<ArgumentException>(() => { course.Subject = s2; });
        }
    }
}
