using System;
using System.Collections.Generic;
using System.Text;
using TimetableDesigner.Model;
using Newtonsoft.Json;
using System.IO;

namespace TimetableDesigner.Persistence
{
    public class JsonController : IDataController
    {
        private string path;
        public string Path
        {
            get { return path; }
            set {
                //Validition later if necessary
                path = value; 
            }
        }

        private Data data = new Data();

        public ICourseRepo CourseRepo
        {
            get
            {
                return data.courseRepo;
            }
        }
        public IGroupRepo GroupRepo
        {
            get
            {
                return data.groupRepo;
            }
        }
        public ISubjectRepo SubjectRepo
        {
            get
            {
                return data.subjectRepo;
            }
        }
        public ITeacherRepo TeacherRepo
        {
            get
            {
                return data.teacherRepo;
            }
        }
        /*
        public JsonCourseRepo CourseRepo { get; set; } = new JsonCourseRepo();
        public JsonGroupRepo GroupRepo { get; set; } = new JsonGroupRepo();
        public JsonSubjectRepo SubjectRepo { get; set; } = new JsonSubjectRepo();

        public JsonTeacherRepo TeacherRepo { get; set; } = new JsonTeacherRepo();

    */
        public void Save()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            serializer.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (StreamWriter streamWriter = new StreamWriter(path))
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
            {
                serializer.Serialize(jsonWriter, data);
            }

        }

        public void Load()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            serializer.Formatting = Formatting.Indented;
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (StreamReader streamReader = new StreamReader(path))
            using (JsonReader jsonReader = new JsonTextReader(streamReader))
            {
                data = serializer.Deserialize<Data>(jsonReader);
            }

        }
        private class Data
        {
            public JsonSubjectRepo subjectRepo = new JsonSubjectRepo();
            public JsonGroupRepo groupRepo = new JsonGroupRepo();
            public JsonTeacherRepo teacherRepo = new JsonTeacherRepo();
            public JsonCourseRepo courseRepo = new JsonCourseRepo();
        }
    }
}
