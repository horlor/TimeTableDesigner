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

        private IList<Course> courses;
        public IList<Course> Courses
        {
            get => courses;
            set
            {
                courses = value;
                ConvertCoursesToViews();
            }
        }
        public DayCourseViewModel()
        {
            this.Day = Day.Friday;
            Course c1 = new Course(), c2 = new Course();
            Teacher teacher = new Teacher { Name = "Teacher neve" };
            Subject subject = new Subject { Name = "Tantárgy" };
            teacher.AddSubject(subject);
            Group group = new Group() { Name = "Csoport" };
            CourseManager.ChangeTime(c1, Day.Friday, new Time(8, 0), new Time(9, 0));
            CourseManager.ChangeTime(c2, Day.Friday, new Time(10, 0), new Time(12, 0));
            CourseManager.ChangeGroup(c1, group);
            CourseManager.ChangeGroup(c2, group);
            CourseManager.ChangeTeacher(c1, teacher);
            CourseManager.ChangeTeacher(c2, teacher);
            CourseManager.ChangeSubject(c1, subject);
            CourseManager.ChangeSubject(c2, subject);
            List<Course> courses = new List<Course>
            {
                c1,
                c2
            };
            this.Courses = courses;
            ConvertCoursesToViews();
        }

        public DayCourseViewModel(IList<Course> courses)
        {
            this.courses = courses;
            ConvertCoursesToViews();
        }

        private void ConvertCoursesToViews()
        {
            try
            {
                List<Course> list = new List<Course>();
                Time time = From;
                foreach (Course c in courses)
                {
                    if (c.Day == this.Day)
                        list.Add(c);
                }
                list.Sort(new CourseComparer());
                //Erasing the former list
                courseviews = new ObservableCollection<CourseViewModel>();
                foreach (Course item in list)
                {
                    if (item.Start != time)
                        courseviews.Add(new CourseViewModel(time, item.Start));
                    time = item.End;
                    courseviews.Add(new CourseViewModel(item));
                }
                if (time < To)
                    courseviews.Add(new CourseViewModel(time, To));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public string DayString { get { return Day.ToString(); } }

        public Day  Day {get; set;}

        public Time From { get; set; } = new Time(6, 0);
        public Time To { get; set; } = new Time(20, 0);

        private class CourseComparer : IComparer<Course>
        {
            public int Compare(Course x, Course y)
            {
                return x.Start.ToMinutes() - y.Start.ToMinutes();
            }
        }
    }
}
