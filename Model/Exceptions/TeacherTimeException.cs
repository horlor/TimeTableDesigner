using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TimetableDesigner.Model
{
    public class TeacherTimeException : TimeTableException
    {
        public TeacherTimeException()
        {
        }

        public TeacherTimeException(string message) : base(message)
        {
        }

        public TeacherTimeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TeacherTimeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
