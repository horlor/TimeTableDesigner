﻿using System;
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
    public class DataManager : ITeacherManager, ISubjectManager, ICourseManager, IGroupManager
    {
        public JsonController dataController;

        
        //Using singleton pattern to avoid multiple Data access
        private DataManager()
        {
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
            foreach (Course item in dataController.CourseRepo.GetList())
            {
                Courses.Add(new CourseViewModel(item));
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
            dataController.SubjectRepo.Remove(subject.Model);
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

        public ObservableCollection<CourseViewModel> Courses { get; private set; } = new ObservableCollection<CourseViewModel>();
        public CourseViewModel CreateCourse()
        {
            var item = new Course();
            var ret = new CourseViewModel(item);
            dataController.CourseRepo.Store(item);
            Courses.Add(ret);
            return ret;
        }

        public void RemoveCourse(CourseViewModel model)
        {
            Courses.Remove(model);
            model.Course.Teacher.RemoveCourse(model.Course);
            model.Course.Group.RemoveCourse(model.Course);
            dataController.CourseRepo.Remove(model.Course);
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
            model.Model.RemoveFromAllCourses();
            dataController.GroupRepo.Remove(model.Model);
        }


        

    }
}
