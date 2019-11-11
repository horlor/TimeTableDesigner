using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.Data;

namespace TimetableDesigner.Graphics.ViewModel
{
    public class CoursesPageViewModel
    {
        public IGroupManager GroupManager { get; set; } = DataManager.Instance;
        public ITeacherManager TeacherManager { get; set; } = DataManager.Instance;
        public ISubjectManager SubjectManager { get; set; } = DataManager.Instance;

        public CoursesPageViewModel()
        {

        }

        public GroupViewModel SelectedGroup { get; set; }

        public ObservableCollection<CourseViewModel> SCourses { get; private set; } = new ObservableCollection<CourseViewModel>();
    }
}
