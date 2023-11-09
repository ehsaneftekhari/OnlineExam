namespace OnlineExam.Application.Contract.Exceptions
{
    public class ApplicationUnAuthenticateException : OEApplicationException
    {
        public ApplicationUnAuthenticateException(string message) : base(message) { }

        public ApplicationUnAuthenticateException(string message, Exception exception) : base(message, exception) { }
    }
}
