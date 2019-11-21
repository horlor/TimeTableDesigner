using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.Data;

namespace TimetableDesigner.Graphics.ViewModel
{
    public class CoursesPageViewModel : ViewModelBase
    {
        public IGroupManager GroupManager { get; set; } = DataManager.Instance;
        public ITeacherManager TeacherManager { get; set; } = DataManager.Instance;
        public ISubjectManager SubjectManager { get; set; } = DataManager.Instance;

        public CoursesPageViewModel()
        {

        }

        public TimetableViewModel Timetable { get;} = new TimetableViewModel();

        private GroupViewModel group;
        public GroupViewModel SelectedGroup {
            get
            {
                return group;
            }
            set
            {
                if(value != group)
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
    }
}
