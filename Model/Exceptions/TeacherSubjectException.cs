using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TimetableDesigner.Model
{
    public class TeacherSubjectException : TimeTableException
    {
        public TeacherSubjectException()
        {
        }

        public TeacherSubjectException(string message) : base(message)
        {
        }

        public TeacherSubjectException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TeacherSubjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
