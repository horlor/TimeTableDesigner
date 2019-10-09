using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableDesigner.Model
{
    public interface IDataController
    {
        ICourseRepo CourseRepo { get; }
        IGroupRepo GroupRepo { get; }
        ISubjectRepo SubjectRepo { get; }
        ITeacherRepo TeacherRepo { get; }
    }
}
