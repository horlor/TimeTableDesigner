using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableDesigner.Model
{
    public abstract class EntityBase
    {
        public int Id { get; }
        public string Name { get; set; }

        public EntityBase(int id)
        {
            this.Id = id;
        }

    }
}
