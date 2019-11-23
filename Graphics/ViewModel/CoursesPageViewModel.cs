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
            this.PropertyChanged += CoursesPageViewModel_PropertyChanged;
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
            }
            else {
                Subject = SubjectManager.FindSubjectByModel(course.Subject);
                Teacher = TeacherManager.FindTeacherByModel(course.Teacher);
                Start = course.Start;
                End = course.End;
                Day = course.Day;
                EditEnabled = true;
                ValidateChanges();
            }

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

        private void CoursesPageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName=="Teacher"||e.PropertyName=="Subject"||e.PropertyName == "Start" || e.PropertyName =="End" || e.PropertyName =="Day")
                ValidateChanges();
        }

        private void ValidateChanges()
        {
            if (course is null || Teacher == null || Subject == null || SelectedGroup == null)
                return;
            List<string> errortexts = new List<string>();
            //Local Validation for Time, and Timetable
            if (Start >= End)
                errortexts.Add("The given end time is before the start.");
            if (Start < new Time(6, 0))
                errortexts.Add("The given start time is before 6am - Limits of timetable.");
            if (End > new Time(20, 0))
                errortexts.Add("The given end time is later than 8pm - Limits of timetable.");
            IList<TimetableError> errors = CourseManager.Instance.ValidateAll(course,SelectedGroup.Model,Teacher.Model,Subject.Model,Day,start,end) ;
            foreach(var item in errors)
            {
                switch (item)
                {
                    case TimetableError.TeacherSubject: errortexts.Add("The selected Teacher does not teach the given subject."); break;
                    case TimetableError.TeacherTime: errortexts.Add("The selected Teacher has another course in the given time."); break;
                    case TimetableError.GroupTime: errortexts.Add("The selected Group has another course in the given time"); break;
                }
            }
            StringBuilder stringBuilder = new StringBuilder();
            for(int i=0; i < errortexts.Count; i++)
            {
                stringBuilder.Append(errortexts[i]);
                if (i !=( errortexts.Count - 1))
                    stringBuilder.Append("\n");
            }
            ValidationError = stringBuilder.ToString();

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
