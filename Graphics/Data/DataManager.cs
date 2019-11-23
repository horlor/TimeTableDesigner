using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.ViewModel;
using TimetableDesigner.Model;
using TimetableDesigner.Persistence;

namespace TimetableDesigner.Graphics.Data
{
    public class DataManager : ITeacherManager, ISubjectManager, IGroupManager
    {
        private JsonController dataController;

        public CourseManager CourseManager { get; set; } = CourseManager.Instance;
        
        //Using singleton pattern to avoid multiple Data access
        private DataManager()
        {
            //Load();
            //TODO safe loading and writing
            //Posibbly i should use the new Library for runtime apps
            dataController = new JsonController
            {
                Path = "data.json"
            };
            dataController.Load();
            
            foreach (Teacher item in dataController.TeacherRepo.GetList())
            {
                Teachers.Add(new TeacherViewModel(item));
            }

            foreach(Subject item in dataController.SubjectRepo.GetList())
            {
                Subjects.Add(new SubjectViewModel(item));
            }
            foreach (Group item in dataController.GroupRepo.GetList())
            {
                Groups.Add(new GroupViewModel(item));
            }
        }

        private static DataManager instance;
        public static DataManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataManager();
                return instance;
            }
        }

        public async void Save()
        {
            dataController.Save("data_temp.json");
            System.IO.File.Move("data.json", "data_old.json");
            System.IO.File.Move("data_temp.json", "data.json");
        }

        private async void Load()
        {
            if (System.IO.File.Exists("data_temp.json"))
                throw new System.IO.FileLoadException();

        }

        public ObservableCollection<SubjectViewModel> Subjects { get; private set; }
    = new ObservableCollection<SubjectViewModel>();

        public SubjectViewModel CreateSubject()
        {
            var item = new Subject();
            var ret = new SubjectViewModel(item);
            dataController.SubjectRepo.Store(item);
            Subjects.Add(ret);
            return ret;
        }

        public void RemoveSubject(SubjectViewModel subject)
        {
            Subjects.Remove(subject);
            foreach(var course in dataController.CourseRepo.GetList())
            {
                if (course.Subject == subject.Model)
                    CourseManager.ChangeSubject(course, null);
            }
            dataController.SubjectRepo.Remove(subject.Model);
        }


        public SubjectViewModel FindSubjectByModel(Subject s)
        {
            foreach(var item in Subjects)
            {
                if (item.Model == s)
                    return item;
            }
            return null;
        }


        public ObservableCollection<TeacherViewModel> Teachers { get; private set; }
            = new ObservableCollection<TeacherViewModel>();



        public TeacherViewModel CreateTeacher()
        {
            var item = new Teacher();
            var ret = new TeacherViewModel(item);
            dataController.TeacherRepo.Store(item);
            Teachers.Add(ret);
            return ret;
        }

        public void RemoveTeacher(TeacherViewModel teacher)
        {
            teacher.Model.RemoveFromAllCourses();
            Teachers.Remove(teacher);
            dataController.TeacherRepo.Remove(teacher.Model);
        }

        public TeacherViewModel FindTeacherByModel(Teacher teacher)
        {
            foreach(var item in Teachers)
            {
                if (item.Model == teacher)
                    return item;
            }
            return null;
        }


        public ObservableCollection<GroupViewModel> Groups { get; private set; } = new ObservableCollection<GroupViewModel>();

        public GroupViewModel CreateGroup()
        {
            var item = new Group();
            var ret = new GroupViewModel(item);
            dataController.GroupRepo.Store(item);
            Groups.Add(ret);
            return ret;
        }

        public void RemoveGroup(GroupViewModel model)
        {
            Groups.Remove(model);
            foreach(var course in model.Model.Courses)
            {
                CourseManager.ChangeTeacher(course, null);
                dataController.CourseRepo.Remove(course);
            }
            dataController.GroupRepo.Remove(model.Model);
        }

        public GroupViewModel FindGroupByModel(Group group)
        {
            foreach (var item in Groups)
                if (item.Model == group)
                    return item;
            return null;
        }


        public void RemoveCourse(Course course)
        {
            dataController.CourseRepo.Remove(course);
            CourseManager.ChangeTeacher(course, null);
            CourseManager.ChangeGroup(course, null);
        }

        public Course NewCourse()
        {
            Course c = new Course();
            dataController.CourseRepo.Store(c);
            return c;
        }
    }
}
