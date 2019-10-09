using System;
using System.Collections.Generic;
using System.Text;
using TimetableDesigner.Model;

namespace TimetableDesigner.Persistence
{
    public class JsonSubjectRepo :JsonRepoBase<Subject>, ISubjectRepo
    {
        private List<Subject> subjects;
    }
}
