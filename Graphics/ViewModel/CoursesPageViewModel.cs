using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.Data;
using TimetableDesigner.Graphics.View;
using TimetableDesigner.Model;
using Windows.UI.Xaml.Controls;

namespace TimetableDesigner.Graphics.ViewModel
{
    public class CoursesPageViewModel : ViewModelBase
    {
        public readonly Day[] Days = { Day.Monday, Day.Tuesday, Day.Wednesday, Day.Thursday, Day.Friday };

        public IGroupManager GroupManager { get; set; } = DataManager.Instance;
        public ITeacherManager TeacherManager { get; set; } = DataManager.Instance;
        public ISubjectManager SubjectManager { get; set; } = DataManager.Instance;

        public CoursesPageViewModel()
        {

        }

        public TimetableViewModel Timetable { get; } = new TimetableViewModel();

        private GroupViewModel group;
        public GroupViewModel SelectedGroup {
            get
            {
                return group;
            }
            set
            {
                if (value != group)
                {
                    group = value;
                    Timetable.Courses = group.Courses;
                    OnPropertyChanged();
                }

            }
        }

        private CourseViewModel selectedCourse;
        public CourseViewModel SelectedCourse
        {
            get
            {
                return selectedCourse;
            }

            set
            {
                if (value != selectedCourse)
                {
                    selectedCourse = value;
                    OnPropertyChanged();
                }
            }
        }

        private Course course;

        public void Timetable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            course = (sender as TimetableView).Selected.Model;
            if(course is null)
            {
                EditEnabled = false;
                ValidationError = "The Clicked item is not a Course, please choose another, or add a new one";
                return;
            }
            ValidationError = "";
            Subject = SubjectManager.FindSubjectByModel(course.Subject);
            Teacher = TeacherManager.FindTeacherByModel(course.Teacher);
            Start = course.Start;
            End = course.End;
            Day = course.Day;
            EditEnabled = true;
        }

        private bool editEnabled=false;
        public bool EditEnabled
        {
            get { return editEnabled; }
            set
            {
                if (editEnabled != value)
                {
                    editEnabled = value;
                    OnPropertyChanged();
                }
            }
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
        public Day Day
        {
            get
            {
                return day;
            }
            set
            {
                if (day != value)
                {
                    day = value;
                    OnPropertyChanged();
                }
            }
        }

        private Time start, end;

        public Time Start
        {
            get
            {
                return start;
            }
            set
            {
                if (start != value) {
                    start = value;
                    OnPropertyChanged();
                }

            }
        }

        public Time End
        {
            get
            {
                return end;
            }
            set
            {
                if(end != value)
                {
                    end = value;
                    OnPropertyChanged();
                }
            }
        }

        private void ValidateChanges()
        {
            IList<TimetableError> errors = CourseManager.Instance.ValidateAll(course,SelectedGroup.Model,Teacher.Model,Subject.Model,Day,start,end) ;
            string errortext="";
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
