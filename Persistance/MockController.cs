﻿using System;
using System.Collections.Generic;
using System.Text;
using TimetableDesigner.Model;

namespace TimetableDesigner.Persistence
{
    public class MockController : IDataController
    {
        public ICourseRepo CourseRepo { get; set; } = new MockCourseRepo();

        public IGroupRepo GroupRepo { get; set; } = new MockGroupRepo();

        public ISubjectRepo SubjectRepo { get; set; } = new MockSubjectRepo();

        public ITeacherRepo TeacherRepo { get; set; } = new MockTeacherRepo();

        public MockController()
        {
            Teacher jteacher = new Teacher() { Name = "Példa János" }, mteacher = new Teacher() { Name = "Minta Ágnes" };
            Subject asubject = new Subject() { Name = "Árvíztűrő" }, tsubject = new Subject() { Name = "Tükörfúrógép" };
            jteacher.AddSubject(asubject);
            mteacher.AddSubject(tsubject);
            Group mgroup = new Group() { Name = "mérnökinfó" };
            IDataController controller = this;
            for (int i = 8; i < 16; i += 2)
            {
                Course course = new Course();
                CourseManager.ChangeGroup(course, mgroup);
                CourseManager.ChangeTeacher(course, mteacher);
                CourseManager.ChangeSubject(course, tsubject);
                CourseManager.ChangeTime(course, Day.Monday, new Time(i, 0), new Time(i + 1, 0));
                controller.CourseRepo.Store(course);
            }
            
            for (int i = 8; i < 16; i += 2)
            {
                Course course = new Course();
                CourseManager.ChangeGroup(course, mgroup);
                CourseManager.ChangeTeacher(course, jteacher);
                CourseManager.ChangeSubject(course, asubject);
                CourseManager.ChangeTime(course, Day.Tuesday, new Time(i, 0), new Time(i + 1, 0));
                controller.CourseRepo.Store(course);
            }
            //Szerda kész
            for (int i = 8; i < 16; i += 2)
            {
                Course course = new Course();
                CourseManager.ChangeGroup(course, mgroup);
                CourseManager.ChangeTeacher(course, jteacher);
                CourseManager.ChangeSubject(course, asubject);
                CourseManager.ChangeTime(course, Day.Wednesday, new Time(i, 0), new Time(i + 2, 0));
                controller.CourseRepo.Store(course);
            }
            for (int i = 8; i < 16; i += 2)
            {
                Course course = new Course();
                CourseManager.ChangeGroup(course, mgroup);
                CourseManager.ChangeTeacher(course, mteacher);
                CourseManager.ChangeSubject(course, tsubject);
                CourseManager.ChangeTime(course, Day.Thursday, new Time(i, 0), new Time(i + 1, 0));
                controller.CourseRepo.Store(course);
            }
            for (int i = 8; i < 16; i += 2)
            {
                Course course = new Course();
                CourseManager.ChangeGroup(course, mgroup);
                CourseManager.ChangeTeacher(course, mteacher);
                CourseManager.ChangeSubject(course, tsubject);
                CourseManager.ChangeTime(course, Day.Friday, new Time(i, 0), new Time(i + 1, 0));
                controller.CourseRepo.Store(course);
            }
            for (int i = 8; i < 16; i += 2)
            {
                Course course = new Course();
                CourseManager.ChangeGroup(course, mgroup);
                CourseManager.ChangeTeacher(course, jteacher);
                CourseManager.ChangeSubject(course, asubject);
                CourseManager.ChangeTime(course, Day.Friday, new Time(i+1, 0), new Time(i + 2, 0));
                controller.CourseRepo.Store(course);
            }
            controller.GroupRepo.Store(mgroup);
            controller.SubjectRepo.Store(tsubject);
            controller.SubjectRepo.Store(asubject);
            controller.TeacherRepo.Store(jteacher);
            controller.TeacherRepo.Store(mteacher);
        }
    }
}
