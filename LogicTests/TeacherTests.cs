using NUnit.Framework;
using TimetableDesigner.Model;
using TimetableDesigner.Persistence;

namespace LogicTests
{
    public class TeacherTests
    {

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void AddSubjectTest()
        {
            Teacher teacher = new Teacher();
            Subject s1 = new Subject(), s2 = new Subject();
            Assert.False(teacher.IsATeachedSubject(s1));
            teacher.AddSubject(s1);
            Assert.True(teacher.IsATeachedSubject(s1));
            Assert.False(teacher.IsATeachedSubject(s2));
            //List should not been able to modified using the Property
            //It could led to unconsistence -> Refractor to giving back only readonly list -> DONE
        }

        [Test]
        public void RemoveSubjectTest()
        {
            Teacher teacher = new Teacher();
            Subject s = new Subject();
            teacher.AddSubject(s);
            Assert.True(teacher.IsATeachedSubject(s));
            teacher.RemoveSubject(s);
            Assert.False(teacher.IsATeachedSubject(s));
        }
    }
}