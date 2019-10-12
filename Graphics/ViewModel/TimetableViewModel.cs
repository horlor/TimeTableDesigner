using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Model;

namespace TimetableDesigner.Graphics.ViewModel
{
    public class TimetableViewModel
    {
        DayCourseViewModel[] days = new DayCourseViewModel[7];

        public TimetableViewModel()
        {
            for (int i = 0; i < 7; ++i)
            {
                days[i] = new DayCourseViewModel();
            }
        }
        public DayCourseViewModel GetModelOf(Day day)
        {
            return days[(int)day];
        }

        private IList<Course> courses = new List<Course>();
        public IList<Course> Courses
        {
            get => courses; set
            {
                courses = value;
                for (int i = 0; i < 7; i++)
                {
                    days[i].Day = (Day)i;
                    days[i].Courses = courses;
                }
            }
        }
    }
}
