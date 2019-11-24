using System;
using System.Collections.Generic;
using System.Text;
using TimetableDesigner.Model;
using Newtonsoft.Json;
using System.IO;
using Windows.Storage.Streams;

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

        public void Save()
        {
            this.Save(this.Path);

        }
        public void Save(Stream stream)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            serializer.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (StreamWriter streamWriter = new StreamWriter(stream))
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
            {
                serializer.Serialize(jsonWriter, data);
            }
        }


        public void Save(string path)
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

        public void Load(Stream stream)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            serializer.Formatting = Formatting.Indented;
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (StreamReader streamReader = new StreamReader(stream))
            using (JsonReader jsonReader = new JsonTextReader(streamReader))
            {
                data = serializer.Deserialize<Data>(jsonReader);
            }
        }

        public void Load(string path)
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

        public void Load()
        {
            this.Load(this.path);
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
