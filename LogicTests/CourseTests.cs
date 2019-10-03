using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TimetableDesigner.Model;

namespace LogicTests
{
    class CourseTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TimeSetGetTest()
        {
            Course course = new Course(0);
            course.SetTimespan(new Time(8, 15), new Time(9, 45));
            Assert.True(course.Start == new Time(8, 15));
            Assert.True(course.End == new Time(9, 45));
            Assert.Catch<ArgumentException>(() => course.SetTimespan(new Time(9,10),new Time(8,55)));
        }

        [Test]
        public void CourseTimeOverlapTests()
        {
            Course course = new Course(0);
            course.SetTimespan(Day.Friday, new Time(9, 30), new Time(10, 50));
            //Not in the sameday
            Assert.False(course.IsOverlappingWithTimePeriod(Day.Monday, new Time(11, 20), new Time(12, 25)));
            //Same day, not overlapping time
            Assert.False(course.IsOverlappingWithTimePeriod(Day.Friday, new Time(11, 20), new Time(12, 25)));
            //Same day, starting in time the first ended
            Assert.False(course.IsOverlappingWithTimePeriod(Day.Friday, new Time(10, 50), new Time(12, 25)));
            //The opposite
            Assert.False(course.IsOverlappingWithTimePeriod(Day.Friday, new Time(8, 20), new Time(9, 30)));
            ///Some truly overlapping timeperiods
            Assert.True(course.IsOverlappingWithTimePeriod(Day.Friday, new Time(9, 20), new Time(10, 40)));
            Assert.True(course.IsOverlappingWithTimePeriod(Day.Friday, new Time(9, 40), new Time(10, 40)));
            Assert.True(course.IsOverlappingWithTimePeriod(Day.Friday, new Time(9, 50), new Time(10, 50)));

        }

        [Test]
        public void CoursesInTheSameTime()
        {
            Course c1 = new Course(0), c2 = new Course(1);
            c1.SetTimespan(Day.Monday, new Time(8, 15), new Time(9, 20));
            c2.SetTimespan(Day.Friday, new Time(9, 30), new Time(10, 10));
            Assert.IsFalse(c1.IsInTheSameTimeWith(c2));
            //in the same time but not the same day
            c2.SetTimespan(Day.Friday, new Time(8, 20), new Time(9, 30));
            Assert.IsFalse(c1.IsInTheSameTimeWith(c2));
            //in the same day not not same time
            c2.SetTimespan(Day.Monday, new Time(9, 30), new Time(10, 10));
            Assert.IsFalse(c1.IsInTheSameTimeWith(c2));
            //Actually in the same time
            c2.SetTimespan(Day.Monday, new Time(8, 20), new Time(9, 30));
            Assert.IsTrue(c1.IsInTheSameTimeWith(c2));
        }
    }
}
