using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableDesigner.Model
{
    public interface IRepo<T>
    {
        List<T> GetList();
        void Remove(T t);
        T FindByName(string name);
        T Find(int id);
        void Store(T t);


    }
}
