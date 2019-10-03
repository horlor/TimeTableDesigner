﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TimetableDesigner.Model;

namespace LogicTests
{
    class GroupTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void AddCourseTest()
        {
            Group g = new Group();
            Course c1 = new Course(), c2 = new Course();
            g.AddCourse(c1);
            Assert.True(g.Courses.IndexOf(c1)!=-1);
            Assert.True(g.Courses.IndexOf(c2)==-1);
        }

        [Test]
        public void RemoveCourseTest()
        {
            Group g = new Group();
            Course c = new Course();
            g.AddCourse(c);
            Assert.True(g.Courses.IndexOf(c)!=-1);
            g.RemoveCourse(c);
            Assert.True(g.Courses.IndexOf(c)==-1);
        }
    }
}
