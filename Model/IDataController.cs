using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableDesigner.Model
{
    public interface IDataController
    {
        public ICourseRepo CourseRepo { get; }
        public IGroupRepo GroupRepo { get; }
        public ISubjectRepo SubjectRepo { get; }
        public ITeacherRepo TeacherRepo { get; }
    }
}
