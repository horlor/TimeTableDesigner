using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableDesigner.Model
{
    /// <summary>
    /// Abstract class for the other classes, to reduce code duplicity
    /// </summary>
    public abstract class EntityBase
    {
        public int Id { get; }
        public string Name { get; set; }

        /// <summary>
        /// The constructor for the class
        /// </summary>
        /// <param name="id">The ID of the new class, it can be set only here</param>
        public EntityBase(int id)
        {
            this.Id = id;
        }

    }
}
