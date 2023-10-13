namespace OnlineExam.Application.Contract.Exceptions
{
    public class ApplicationSourceNotFoundException : OEApplicationException
    {
        public ApplicationSourceNotFoundException(string message) : base(message)
        {
        }
    }
}
