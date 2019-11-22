using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.Data;
using TimetableDesigner.Model;

namespace TimetableDesigner.Graphics.ViewModel
{
    public class CourseEditViewModel: ViewModelBase
    {
        public Course Model { get; }

        public CourseEditViewModel(Course course)
        {
            Model = course;
            Teacher = DataManager.Instance.FindTeacherByModel(Model.Teacher);
            Subject = DataManager.Instance.FindSubjectByModel(Model.Subject);
        }

        private SubjectViewModel subject;
        public SubjectViewModel Subject
        {
            get
            {
                return subject;
            }
            set
            {
                if (subject != value)
                {
                    subject = value;
                    OnPropertyChanged();
                }
            }
        }

        private TeacherViewModel teacher;
        public TeacherViewModel Teacher
        {
            get
            {
                return teacher;
            }
            set
            {
                if (teacher != value)
                {
                    teacher = value;
                    OnPropertyChanged();
                }
            }
        }

        private Day day;
        public Day Day { 
            get
            {
                return day;
            }
            set
            {
                if(day != value)
                {
                    day = value;
                    OnPropertyChanged();
                }
            }
        }

        private Time start, end;

        public TimeSpan Start
        {
            get;
        }

        public TimeSpan End;

        private void ValidateChanges()
        {
            var errors = CourseManager.Instance.ValidateAll(Model, Model.Group, Teacher.Model, Subject.Model, Day, start, end);
            string errortext = "";
            foreach (var item in errors)
            {
                switch (item)
                {
                    case TimetableError.TeacherSubject: errortext += "The selected Teacher does not teach the given subject.\n"; break;
                    case TimetableError.TeacherTime: errortext += "The selected Teacher has another course in the given time.\n"; break;
                }
            }
            ValidationError = errortext;
        }

        private string validationerror = "";
        public string ValidationError
        {
            get { return validationerror; }
            private set
            {
                if (validationerror != value)
                {
                    validationerror = value;
                    OnPropertyChanged();
                }

            }
        }
    }
}
