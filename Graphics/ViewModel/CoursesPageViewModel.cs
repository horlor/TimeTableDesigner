﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.Commands;
using TimetableDesigner.Graphics.Data;
using TimetableDesigner.Graphics.View;
using TimetableDesigner.Model;
using Windows.UI.Xaml.Controls;

namespace TimetableDesigner.Graphics.ViewModel
{
    public class CoursesPageViewModel : ViewModelBase
    {
        public readonly Day[] Days = { Model.Day.Monday, Model.Day.Tuesday, Model.Day.Wednesday, Model.Day.Thursday, Model.Day.Friday };

        public IGroupManager GroupManager { get; set; } = DataManager.Instance;
        public ITeacherManager TeacherManager { get; set; } = DataManager.Instance;
        public ISubjectManager SubjectManager { get; set; } = DataManager.Instance;

        public CoursesPageViewModel()
        {
            //Subscribing for changes in order to validate them
            Teacher.PropertyChanged += CoursesPageViewModel_PropertyChanged;
            Subject.PropertyChanged += CoursesPageViewModel_PropertyChanged;
            Day.PropertyChanged += CoursesPageViewModel_PropertyChanged;
            Start.PropertyChanged += CoursesPageViewModel_PropertyChanged;
            End.PropertyChanged += CoursesPageViewModel_PropertyChanged;

            //
            NewCommand = new CommandBase(o => NewCourse(), o => true);
            SaveCommand = new CommandBase(o => Save(),o=> isValid());
            UndoCommand = new CommandBase(o => Undo(), o=> { return EditEnabled; });
            DeleteCommand = new CommandBase(o => DeleteCourse(), o => { return EditEnabled; });
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
                    if(group!=null)
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
        private bool isnewcourse = false;

        public async Task Timetable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (course != null && isCourseChanged())
            {
                bool res = await showUnsavedChangesDialog();
                if (res)
                    Save();
            } 
            course = (sender as TimetableView).Selected.Model;
            LoadCourse();
        }

        private void LoadCourse()
        {
            setPropertiesValid();
            if (course is null)
            {
                EditEnabled = false;
                ValidationError = "The Clicked item is not a Course, please choose another, or add a new one";
            }
            else
            {
                Subject.Value = SubjectManager.FindSubjectByModel(course.Subject);
                Teacher.Value = TeacherManager.FindTeacherByModel(course.Teacher);
                Start.Value = course.Start;
                End.Value = course.End;
                Day.Value = course.Day;
                EditEnabled = true;
            }
            RefreshCommands();
        }

        private async Task<bool> showUnsavedChangesDialog()
        {
            ContentDialog SaveDialog = new ContentDialog
            {
                Title = "Not saved modifications",
                Content = "There are unsaved modifications on the selected item. Do you want to save them?",
                CloseButtonText = "No",
                PrimaryButtonText = "Yes"
            };

            ContentDialogResult result = await SaveDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
                return true;
            return false;
        }

        private bool isCourseChanged()
        {
            return Day.Value != course.Day ||
                Start.Value != course.Start ||
                End.Value != course.End ||
                Subject.Value.Model != course.Subject ||
                Teacher.Value.Model != course.Teacher;

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

        public ValidablePropertyBase<SubjectViewModel> Subject { get; } = new ValidablePropertyBase<SubjectViewModel>();

        public ValidablePropertyBase<TeacherViewModel> Teacher { get; } = new ValidablePropertyBase<TeacherViewModel>();

        public ValidablePropertyBase<Day> Day { get; } = new ValidablePropertyBase<Day>();

        public ValidablePropertyBase<Time> Start { get; } = new ValidablePropertyBase<Time>();
        public ValidablePropertyBase<Time> End { get; } = new ValidablePropertyBase<Time>();

        private void setPropertiesValid()
        {
            Teacher.Validity = true;
            Subject.Validity = true;
            Day.Validity = true;
            Start.Validity = true;
            End.Validity = true;
        }

        private void CoursesPageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName!="Validity")
                ValidateChanges();
            RefreshCommands();
        }

        private void ValidateChanges()
        {
            if (course is null || SelectedGroup == null)
                return;
            List<string> errortexts = new List<string>();
            setPropertiesValid();
            //Local Validation for Time, and Timetable
            if (Start.Value >= End.Value)
            {
                errortexts.Add("The given end time is before the start.");
                End.Validity = false;
            }  
            if (Start.Value < new Time(6, 0))
            {
                errortexts.Add("The given start time is before 6am - Limits of timetable.");
                Start.Validity = false;
            }
            if (End.Value > new Time(20, 0))
            {
                errortexts.Add("The given end time is later than 8pm - Limits of timetable.");
                Start.Validity = false;
                End.Validity = false;
            }
            Subject subject = Subject.Value is null ? null : Subject.Value.Model;
            Teacher teacher = Teacher.Value is null ? null : Teacher.Value.Model;
            IList<TimetableError> errors = CourseManager.Instance.ValidateAll(course,SelectedGroup.Model,teacher,subject,Day.Value,Start.Value,End.Value) ;
            foreach(var item in errors)
            {
                switch (item)
                {
                    case TimetableError.TeacherSubject: errortexts.Add("The selected Teacher does not teach the given subject.");
                            Teacher.Validity = false; Subject.Validity = false; break;
                    case TimetableError.TeacherTime: errortexts.Add("The selected Teacher has another course in the given time.");
                        Teacher.Validity = false; Start.Validity = false; End.Validity = false; Day.Validity = false; break;
                    case TimetableError.GroupTime: errortexts.Add("The selected Group has another course in the given time");
                        Start.Validity = false; End.Validity = false; Day.Validity = false; break;
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

        public CommandBase NewCommand { get; }

        private void NewCourse()
        {
            course = new Course();
            Teacher.Value = null;
            Subject.Value = null;
            Start.Value = new Time(11, 0);
            End.Value = new Time(12, 0);
            isnewcourse = true;
            EditEnabled = true;
        }
        public CommandBase DeleteCommand { get; }

        private void DeleteCourse()
        {
            DataManager.Instance.RemoveCourse(course);
            UpdateTimetable();
        }

        public CommandBase SaveCommand { get; }

        private void Save()
        {

            CourseManager manager = CourseManager.Instance;
            System.Diagnostics.Debug.WriteLine(Start.Value + "-" + End.Value);
            manager.ChangeAll(course, SelectedGroup.Model, Teacher.Value.Model, Subject.Value.Model, Day.Value, Start.Value, End.Value);
            UpdateTimetable();
            if (isnewcourse)
            {
                DataManager.Instance.StoreCourse(course);
                isnewcourse = false;
            }
        }

        public CommandBase UndoCommand { get; }

        private void Undo()
        {
            LoadCourse();
        } 

        private bool  isValid()
        {
                return EditEnabled && Teacher.Validity && Subject.Validity && Start.Validity && End.Validity && Day.Validity;
        }
        private void RefreshCommands()
        {
            NewCommand.ExecutionChanged();
            DeleteCommand.ExecutionChanged();
            SaveCommand.ExecutionChanged();
            UndoCommand.ExecutionChanged();
        }

        private void UpdateTimetable()
        {
            Timetable.Courses = SelectedGroup.Courses;
        }
    }
}
