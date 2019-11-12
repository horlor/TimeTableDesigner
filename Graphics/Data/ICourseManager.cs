using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.ViewModel;
using System.Collections.ObjectModel;
using TimetableDesigner.Model;

namespace TimetableDesigner.Graphics.Data
{
    public interface ICourseManager
    {
        CourseViewModel CreateCourse();
        void RemoveCourse(CourseViewModel course);
        ObservableCollection<CourseViewModel> Courses { get; }
        CourseViewModel FindCourseByModel(Course course);
    }
}
