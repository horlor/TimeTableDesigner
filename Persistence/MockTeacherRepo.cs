using System;
using System.Collections.Generic;
using System.Text;
using TimetableDesigner.Model;

namespace TimetableDesigner.Persistence
{
    public class MockTeacherRepo : Model.ITeacherRepo
    {
        private List<Teacher> teachers = new List<Teacher>();
        private int next_id = 0;
        public Teacher Find(int id)
        {
            foreach (var item in teachers)
            {
                if (item.Id == id)
                    return item;
            }
            return null;
        }

        public Teacher FindByName(string name)
        {
            foreach(var item in teachers)
            {
                if (item.Name == name)
                    return item;
            }
            return null;
        }

        public List<Teacher> GetList()
        {
            return teachers;
        }

        public Teacher New()
        {
            Teacher teacher = new Teacher(next_id);
            next_id++;
            teachers.Add(teacher);
            return teacher;
        }

        public void Remove(Teacher t)
        {
            teachers.Remove(t);
        }
    }
}
