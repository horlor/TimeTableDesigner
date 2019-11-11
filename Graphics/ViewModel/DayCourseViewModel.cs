using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Model;

namespace TimetableDesigner.Graphics.ViewModel
{
    public class DayCourseViewModel : ViewModelBase
    {
        public ObservableCollection<CourseViewModel> CourseViews { get; private set; } = new ObservableCollection<CourseViewModel>();

        public Day Day { get; set; }

        private Time from = new Time(6, 0), to = new Time(20, 0);

        private ObservableCollection<CourseViewModel> courses;
        public ObservableCollection<CourseViewModel> Courses
        {
            get
            {
                return courses;
            }
            set
            {
                if(courses !=null)
                courses.CollectionChanged -= OnCollectionChanged;
                courses = value;
                if(courses !=null)
                courses.CollectionChanged += OnCollectionChanged;
                ConvertCourseViews();
            }
        }

        public DayCourseViewModel()
        {

        }

        private void ConvertCourseViews()
        {
            List<CourseViewModel> temp = new List<CourseViewModel>();
            foreach(var item in courses)
            {
                if (item.Day == this.Day)
                    temp.Add(item);
            }
            temp.Sort(new CourseViewModelComparer());
            Time last = from;
            CourseViews.Clear();
            foreach(var item in temp)
            {
                if (last < item.Model.Start)
                    CourseViews.Add(new CourseViewModel(last, item.Model.Start));
                CourseViews.Add(item);
                last = item.Model.End;
            }
            if(last < to)
            {
                CourseViews.Add(new CourseViewModel(last, to));
            }
        }


        

        public void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            ConvertCourseViews();
            /*switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    break;

                case NotifyCollectionChangedAction.Remove:
                    break;

            }*/
        }

        private class CourseViewModelComparer : Comparer<CourseViewModel>
        {
            public override int Compare(CourseViewModel x, CourseViewModel y)
            {
                return x.Model.Start.ToMinutes() - y.Model.Start.ToMinutes();
            }
        }

    }
}
