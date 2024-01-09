namespace OnlineExam.Application.Contract.Exceptions
{
    public class OEApplicationInvalidStateException : OEApplicationException
    {
        public OEApplicationInvalidStateException() : base() { }
        public OEApplicationInvalidStateException(string message) : base(message) { }
    }
}
