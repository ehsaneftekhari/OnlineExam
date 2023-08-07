using System.Collections;

namespace OnlineExam.Application.Contract.Exceptions
{
    public class OEApplicationException : Exception
    {
        public OEApplicationException() : base()
        {
        }
        public OEApplicationException(string message) : base(message)
        {
        }
    }
}
