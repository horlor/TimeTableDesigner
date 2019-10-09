using System;
using System.Collections.Generic;
using System.Text;
using TimetableDesigner.Model;
using Newtonsoft.Json;

namespace TimetableDesigner.Persistence
{
    public abstract class JsonRepoBase<T> : IRepo<T> where T : EntityBase
    {
        private int next_id = 0;
        [JsonProperty]
        protected List<T> ts = new List<T>();
        public T Find(int id)
        {
            foreach(T t in ts){
                if (t.Id == id)
                    return t;
            }
            return null;
        }

        public T FindByName(string name)
        {
            foreach(T t in ts)
            {
                if (t.Name == name)
                    return t;
            }
            return null;
        }

        public List<T> GetList()
        {
            return ts;
        }

        public void Remove(T t)
        {
            ts.Remove(t);
        }

        public void Store(T t)
        {
            t.SetId(next_id++);
            ts.Add(t);
        }

    }
}
