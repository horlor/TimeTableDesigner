using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.Data;
using TimetableDesigner.Model;

namespace TimetableDesigner.Graphics.ViewModel
{
    public class TimetableViewModel : ViewModelBase
    {
        private const int NumOfDays = 5;
        DayCourseViewModel[] days = new DayCourseViewModel[NumOfDays];

        private ObservableCollection<CourseViewModel> courses;
        public ObservableCollection<CourseViewModel> Courses {
            get { return courses; }
            set { courses = value;
                foreach(var item in days)
                {
                    item.Courses = courses;
                }
                OnPropertyChanged();
            }
        }

        public TimetableViewModel()
        {
            for (int i = 0; i < NumOfDays; i++)
            {
                days[i] = new DayCourseViewModel();
                days[i].Day = (Day)i;
            }
            
        }

        public DayCourseViewModel Monday { get { return days[0]; } }
        public DayCourseViewModel Tuesday { get { return days[1]; } }
        public DayCourseViewModel Wednesday { get { return days[2]; } }
        public DayCourseViewModel Thursday { get { return days[3]; } }
        public DayCourseViewModel Friday { get { return days[4]; } }


    }
}
