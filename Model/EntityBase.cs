using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableDesigner.Model
{
    /// <summary>
    /// Abstract class for the other classes, to reduce code duplicity
    /// </summary>
    [JsonObject]
    public abstract class EntityBase
    {
        [JsonProperty]
        private int _id;

        [JsonIgnore]
        public int Id { get { return _id; } }
        
        [JsonProperty]
        public string Name { get; set; }

        /// <summary>
        /// The constructor for the class
        /// </summary>
        /// <param name="id">The ID of the new class, it can be set only here</param>
        public EntityBase()
        {
            _id = -1;
        }

        public void SetId(int id)
        {
            _id = id;
        }

    }
}
