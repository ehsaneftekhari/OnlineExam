namespace OnlineExam.Application.Contract.DTOs
{
    public class ValidateTokenResult
    {
        public bool IsAuthenticated { get; set; }
        public bool IsAuthorized { get; set; }
    }
}