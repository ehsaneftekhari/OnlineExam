namespace OnlineExam.Application.Contract.Exceptions
{
    public class ApplicationUnAuthorizedException : OEApplicationException
    {
        public ApplicationUnAuthorizedException(string message) : base(message) { }

        public ApplicationUnAuthorizedException(string message, Exception exception) : base(message, exception) { }
    }
}
