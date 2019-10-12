using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Model;

namespace TimetableDesigner.Graphics.ViewModel
{
    public class DayCourseViewModel
    {
        private ObservableCollection<CourseViewModel> courseviews = new ObservableCollection<CourseViewModel>();

        public ObservableCollection<CourseViewModel> CourseViews
        {
            get
            {
                return courseviews;
            }
        }

        public IList<Course> Courses { get; set; }
        public DayCourseViewModel()
        {
            CourseViews.Add(new CourseViewModel());
            CourseViewModel course = new CourseViewModel();
            CourseManager.ChangeTime(course.Course, Day.Friday, new Time(10, 0), new Time(12, 0));
            CourseViews.Add(course);
        }

        public string DayString { get { return Day.ToString(); } }

        public Day  Day {get; set;}

    }
}
