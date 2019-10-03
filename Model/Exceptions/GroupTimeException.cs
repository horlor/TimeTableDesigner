using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TimetableDesigner.Model
{
    public class GroupTimeException : TimeTableException
    {
        public GroupTimeException()
        {
        }

        public GroupTimeException(string message) : base(message)
        {
        }

        public GroupTimeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GroupTimeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
