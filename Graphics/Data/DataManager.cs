using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TimetableDesigner.Graphics.ViewModel;
using TimetableDesigner.Model;
using TimetableDesigner.Persistence;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Graphics;
using System.Threading;

namespace TimetableDesigner.Graphics.Data
{
    public class DataManager : ITeacherManager, ISubjectManager, IGroupManager
    {
        private JsonController dataController;

        public CourseManager CourseManager { get; set; } = CourseManager.Instance;

        //Using singleton pattern to avoid multiple Data access
        private DataManager()
        {

            Task.Run(() => Load()).Wait();
            //ars.WaitOne();

            foreach (Teacher item in dataController.TeacherRepo.GetList())
            {
                Teachers.Add(new TeacherViewModel(item));
            }

            foreach (Subject item in dataController.SubjectRepo.GetList())
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

        public async Task Save()
        {
            StorageFolder localfolder = ApplicationData.Current.LocalFolder;
            StorageFile tempfile = await localfolder.CreateFileAsync("data_temp.json",CreationCollisionOption.ReplaceExisting);
            var stream = await tempfile.OpenStreamForWriteAsync();
            dataController.Save(stream);
            var present = await localfolder.GetFileAsync("data.json");
            var old = await localfolder.TryGetItemAsync("data_old.json");
            if (old != null)
            {
               await old.DeleteAsync();
            }
            await present.RenameAsync("data_old.json");
            await tempfile.RenameAsync("data.json");
            System.Diagnostics.Debug.WriteLine("Save ended");
        }

        private async Task Load()
        {
            
            StorageFolder local = ApplicationData.Current.LocalFolder;
            if((await local.TryGetItemAsync("data_temp.json"))!=null && (await local.TryGetItemAsync("data_old.json")) != null)
            {
                var temp = await local.GetFileAsync("data_temp.json");
                var data = await local.TryGetItemAsync("data.json");
                if (data != null)
                    await data.DeleteAsync();
                await temp.RenameAsync("data.json");
            }
            try
            {
                dataController = new JsonController();
                var json = await local.GetFileAsync("data.json");
                var stream = await json.OpenStreamForReadAsync();
                dataController.Load(stream);
                System.Diagnostics.Debug.WriteLine("load success");
            }
            catch(Newtonsoft.Json.JsonException e)
            {
                var result = await JsonErrorDialog();
                if (result == ContentDialogResult.Primary)
                {
                    dataController = new JsonController();
                    return;
                }
                else
                {
                    App.Current.Exit();
                }
            }
            catch(FileNotFoundException e)
            {
                dataController = new JsonController();
            }
        }

        private async Task<ContentDialogResult> JsonErrorDialog()
        {
            ContentDialog SaveDialog = new ContentDialog
            {
                Title = "File error",
                Content = "The application data is corrupted, do you wish to try loading a previous version or create a new Applicationdata",
                CloseButtonText = "Cancel",
                PrimaryButtonText = "Create new"
            };

            return await SaveDialog.ShowAsync();
        }

        public static void Initialize()
        {
            instance = new DataManager();
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

        public void StoreCourse(Course c)
        {
            dataController.CourseRepo.Store(c);
        }
    }
}
