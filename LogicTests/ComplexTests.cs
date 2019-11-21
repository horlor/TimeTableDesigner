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
            Teacher teacher = new Teacher();
            Subject s1 = new Subject(), s2 = new Subject();
            teacher.AddSubject(s1);
            Course course = new Course();
            CourseManager.Instance.ChangeSubject(course, s1);
            Assert.DoesNotThrow(() => { CourseManager.Instance.ChangeTeacher(course, teacher); });
            Assert.False(teacher.IsATeachedSubject(s2));
            Assert.Catch<TimetableException>(() => { CourseManager.Instance.ChangeSubject(course,s2); });
        }

        [Test]
        public void ValidationTests()
        {
            CourseManager manager = CourseManager.Instance;
            Teacher teacher = new Teacher();
            Subject s1 = new Subject(), s2 = new Subject();
            teacher.AddSubject(s1);
            Course course = new Course();
            manager.ChangeSubject(course, s1);
            Assert.AreEqual(0, manager.ValidateTeacher(course, teacher).Count);
            manager.ChangeSubject(course, s2);
            Assert.AreEqual(1, manager.ValidateTeacher(course, teacher).Count);

            Course c2 = new Course();
            manager.ChangeTime(c2, Day.Friday, new Time(8, 0), new Time(9, 0));
            manager.ChangeTeacher(c2, teacher);
            Assert.AreEqual(TimetableError.TeacherSubject, manager.ValidateTeacher(course, teacher)[0]);
            manager.ChangeTime(course, Day.Friday, new Time(8, 0), new Time(8, 45));
            Assert.AreEqual(2, manager.ValidateTeacher(course, teacher).Count);

            Assert.AreEqual(0, manager.ValidateTime(course, Day.Friday, new Time(10, 0), new Time(11, 0)).Count);
        }
    }
}
