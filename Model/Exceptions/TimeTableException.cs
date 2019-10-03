using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TimetableDesigner.Model
{
    public class TimeTableException : Exception
    {
        public TimeTableException()
        {
        }

        public TimeTableException(string message) : base(message)
        {
        }

        public TimeTableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TimeTableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
