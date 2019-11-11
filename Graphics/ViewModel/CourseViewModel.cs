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
        public Course Model { get; set; }

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
            this.Model = course;
            
        }

        public CourseViewModel(Course course)
        {
            Model = course;
        }

        public CourseViewModel(Time from, Time to)
        {
            Model = null;
            start = from;
            end = to;
        }

        private Time start, end;


        public int Height
        {
            get
            {
                int ret;
                if (Model != null)
                    ret =  ((Model.End.ToMinutes() - Model.Start.ToMinutes()))/15*MIN_PIXEL;
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
                if (Model != null)
                    return string.Format("{0} - {1}", Model.Start.ToString(), Model.End.ToString());
                return "";
            }
        }

        public string SubjectString
        {
            get{ 
                if(Model !=null) 
                    return Model.Subject.Name;
                return "";
            }
        }

        public string TeacherString
        {
            get
            {
                if(Model!=null)
                    return Model.Teacher.Name;
                return "";
            }
        }

        public string Group
        {
            get
            {
                if (Model != null)
                    return Model.Group.Name;
                return "";
            }
        }

        public override string ToString()
        {
            return this.SubjectString + " " + this.TimeString;
        }

        public Day Day { get { return Model.Day; } }
    }
}
