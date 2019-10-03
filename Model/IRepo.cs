using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableDesigner.Model
{
    public interface IRepo<T>
    {
        public List<T> GetList();
        public void Remove(T t);
        public T FindByName(string name);
        public T Find(int id);
        public void Store(T t);


    }
}
