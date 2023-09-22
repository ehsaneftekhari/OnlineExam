namespace OnlineExam.EndPoint.API.Exceptions
{
    public class APIValidationException : APIException
    {
        public APIValidationException(string message) : base(message) { }
    }
}
