using System;
using System.Collections.Generic;
using System.Text;
using TimetableDesigner.Model;

namespace TimetableDesigner.Persistence
{
    public class MockGroupRepo : MockRepoBase<Group>, IGroupRepo
    {
        public override Group New()
        {
            Group group = new Group(next_id++);
            ts.Add(group);
            return group;
        }
    }
}
