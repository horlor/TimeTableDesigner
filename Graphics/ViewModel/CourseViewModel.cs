using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.Data;
using TimetableDesigner.Model;


namespace TimetableDesigner.Graphics.ViewModel
{
    public class CourseViewModel : ViewModelBase
    {
        private const int MIN_PIXEL=20;
        public Course Model { get; }

        public CourseViewModel(Course course)
        {
            Model = course;
            Teacher = DataManager.Instance.FindTeacherByModel(Model.Teacher);
            Subject = DataManager.Instance.FindSubjectByModel(Model.Subject);

            this.PropertyChanged += CourseViewModel_PropertyChanged;
        }

        private void CourseViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ValidateChanges();
        }

        public CourseViewModel(Time from, Time to)
        {
            Model = null;
            start = from;
            end = to;
        }

        private Time start, end;


        public int Height
        {
            get
            {
                int ret;
                if (Model != null)
                    ret =  ((Model.End.ToMinutes() - Model.Start.ToMinutes()))/15*MIN_PIXEL;
                else
                    ret=  (end.ToMinutes() - start.ToMinutes()) / 15 * MIN_PIXEL;
                return ret;
            }
        }

        public string TimeString
        {
            get
            {
                if (Model != null)
                    return string.Format("{0} - {1}", Model.Start.ToString(), Model.End.ToString());
                return "";
            }
        }

        public string SubjectString
        {
            get{
                if (Model != null && Model.Subject !=null)
                    return Model.Subject.Name;
                return "";
            }
        }

        public string TeacherString
        {
            get
            {
                if (Model is null)
                    return "";
                if (Model.Teacher is null)
                    return "";
                return Model.Teacher.Name;
            }
        }

        public string Group
        {
            get
            {
                if (Model != null)
                    return Model.Group.Name;
                return "";
            }
        }

        public override string ToString()
        {
            return this.SubjectString + " " + this.TimeString;
        }

        public Day Day { get { return Model.Day; } }

        private SubjectViewModel subject;
        public SubjectViewModel Subject
        {
            get
            {
                return subject;
            }
            set
            {
                if(subject != value)
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
                if(teacher != value)
                {
                    teacher = value;
                    OnPropertyChanged();
                }
            }
        }


        private void ValidateChanges()
        {
            var errors = CourseManager.Instance.ValidateAll(Model, Model.Group, Teacher.Model, Subject.Model, Day, start, end);
            string errortext = "";
            foreach(var item in errors)
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
                if(validationerror != value)
                {
                    validationerror = value;
                    OnPropertyChanged();
                }

            }
        }



    }
}
