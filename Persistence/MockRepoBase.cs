using System;
using System.Collections.Generic;
using System.Text;
using TimetableDesigner.Model;

namespace TimetableDesigner.Persistence
{
    public abstract class MockRepoBase<T> : IRepo<T> where T : EntityBase
    {
        protected List<T> ts;
        protected int next_id=0;
        public T Find(int id)
        {
            foreach(T item in ts)
            {
                if (item.Id == id)
                    return item;
            }
            return null;
        }

        public T FindByName(string name)
        {
            foreach (T item in ts)
            {
                if (item.Name == name)
                    return item;
            }
            return null;
        }

        public List<T> GetList()
        {
            return ts;
        }

        abstract public T New();

        public void Remove(T t)
        {
            ts.Remove(t);
        }
    }
}
