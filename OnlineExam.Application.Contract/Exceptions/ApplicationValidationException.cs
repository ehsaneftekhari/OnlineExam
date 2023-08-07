namespace OnlineExam.Application.Contract.Exceptions
{
    public class ApplicationValidationException : OEApplicationException
    {
        public ApplicationValidationException(string message) : base(message)
        {
        }
    }
}
