using OnlineExam.Application.Contract.DTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface IUserService
    {
        string LogIn(LogInDto logInDto);
    }
}