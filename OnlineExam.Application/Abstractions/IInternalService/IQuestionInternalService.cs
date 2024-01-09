using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IInternalService
{
    public interface IQuestionInternalService : IBaseInternalService<Question, int, Section, int>
    {

        public Question GetWith_Section_Exam_ExamUser_Included(int questionId);
    }
}
