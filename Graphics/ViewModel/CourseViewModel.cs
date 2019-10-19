using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Model;


namespace TimetableDesigner.Graphics.ViewModel
{
    public class CourseViewModel
    {
        private const int MIN_PIXEL=20;
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

        public CourseViewModel(Course course)
        {
            Course = course;
        }

        public CourseViewModel(Time from, Time to)
        {
            Course = null;
            start = from;
            end = to;
        }

        private Time start, end;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Height
        {
            get
            {
                int ret;
                if (Course != null)
                    ret =  ((Course.End.ToMinutes() - Course.Start.ToMinutes()))/15*MIN_PIXEL;
                else
                    ret=  (end.ToMinutes() - start.ToMinutes()) / 15 * MIN_PIXEL;
                //if (ret <= 0 || ret >500)
                //    return 100;
                return ret;
            }
        }

        public string TimeString
        {
            get
            {
                if (Course != null)
                    return string.Format("{0} - {1}", Course.Start.ToString(), Course.End.ToString());
                return "";
            }
        }

        public string Subject
        {
            get{ 
                if(Course !=null) 
                    return Course.Subject.Name;
                return "";
            }
        }

        public string Teacher
        {
            get
            {
                if(Course!=null)
                    return Course.Teacher.Name;
                return "";
            }
        }

        public string Group
        {
            get
            {
                if (Course != null)
                    return Course.Group.Name;
                return "";
            }
        }

        public override string ToString()
        {
            return this.Subject + " " + this.TimeString;
        }
    }
}
