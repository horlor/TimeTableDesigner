using System;
using System.Collections.Generic;
using System.Text;
using TimetableDesigner.Model;

namespace TimetableDesigner.Persistence
{
    public class MockCourseRepo : MockRepoBase<Course>, ICourseRepo
    {
        public override Course New()
        {
            Course course = new Course(next_id++);
            ts.Add(course);
            return course;
        }
    }
}
