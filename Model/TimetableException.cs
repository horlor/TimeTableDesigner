using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TimetableDesigner.Model
{
    public class TimetableException : Exception
    {
        private List<TimetableError> errors = new List<TimetableError>();

        public IReadOnlyList<TimetableError> Errors
        {
            get
            {
                return errors.AsReadOnly();
            }
        }

        public TimetableException(TimetableError error = TimetableError.Unknown)
        {
            errors.Add(error);
        }

        public TimetableException(string message, TimetableError error = TimetableError.Unknown) : base(message)
        {
            errors.Add(error);
        }

        public TimetableException(string message, Exception innerException, TimetableError error = TimetableError.Unknown) : base(message, innerException)
        {
            errors.Add(error);
        }

        public TimetableException(IEnumerable<TimetableError> errors) : base()
        {
            this.errors.AddRange(errors);
        }
    }

    public enum TimetableError
    {
        TeacherSubject,
        GroupTime,
        TeacherTime,
        Time,
        Unknown
    }
}
