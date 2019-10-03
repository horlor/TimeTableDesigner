using System;
using System.Collections.Generic;
using System.Text;
using TimetableDesigner.Model;
using Newtonsoft.Json;

namespace TimetableDesigner.Persistence
{
    public class MockGroupRepo : MockRepoBase<Group>, IGroupRepo
    {
    }
}
