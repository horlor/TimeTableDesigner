using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Model;


namespace TimetableDesigner.Graphics.ViewModel
{
    public class CourseViewModel
    {
        private const int MIN_PIXEL=40;
        public Course Course { get; set; }

        public CourseViewModel()
        {
            Course course = new Course();
            Teacher teacher = new Teacher()
            {
                Name = "Tanár"
            };
            Subject subject = new Subject { Name = "Tantárgy" };
            Group group = new Group();
            teacher.AddSubject(subject);
            CourseManager.ChangeGroup(course, group);
            CourseManager.ChangeTeacher(course, teacher);
            CourseManager.ChangeSubject(course, subject);
            CourseManager.ChangeTime(course,Day.Friday,new Time(8, 45), new Time(9, 45));
            this.Course = course;
        }

        public int Height
        {
            get
            {
                return ((Course.End.ToMinutes() - Course.Start.ToMinutes()))/15*MIN_PIXEL;
            }
        }
        public string TimeString
        {
            get
            {
                return string.Format("{0} - {1}", Course.Start.ToString(), Course.End.ToString());
            }
        }

        public string Subject
        {
            get { return Course.Subject.Name; }
        }

        public string Teacher
        {
            get
            {
                return Course.Teacher.Name;
            }
        }

        public string Group
        {
            get
            {
                return Course.Group.Name;
            }
        }
    }
}
