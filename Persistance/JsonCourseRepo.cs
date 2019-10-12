using System;
using System.Collections.Generic;
using System.Text;
using TimetableDesigner.Model;
using Newtonsoft.Json;
using System.IO;

namespace TimetableDesigner.Persistence
{
    public class JsonCourseRepo : JsonRepoBase<Course>, ICourseRepo
    {

    }
}
