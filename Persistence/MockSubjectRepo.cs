using System;
using System.Collections.Generic;
using TimetableDesigner.Model;


namespace TimetableDesigner.Persistence
{
    public class MockSubjectRepo : MockRepoBase<Subject>, ISubjectRepo
    {
        /*
        List<Subject> subjects = new List<Subject>();
        
        public Subject Find(int id)
        {
            foreach(var item in subjects)
            {
                if (item.Id == id)
                    return item;
            }
            return null;
        }

        public Subject FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Subject> GetList()
        {
            throw new NotImplementedException();
        }

        public Subject New()
        {
            throw new NotImplementedException();
        }

        public void Remove(Subject t)
        {
            throw new NotImplementedException();
        }*/
        public override Subject New()
        {
            Subject subject = new Subject(next_id++);
            ts.Add(subject);
            return subject;
        }
    }
}
