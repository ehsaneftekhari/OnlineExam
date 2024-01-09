using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    internal class AnswerAccessValidator : IAnswerAccessValidator
    {
        readonly IExamAccessValidator _examAccessValidator;

        public AnswerAccessValidator(IExamAccessValidator examAccessValidator)
        {
            _examAccessValidator = examAccessValidator;
        }

        //TODO:need to review
        public bool IsUserExamUserOrExamCreator(ExamUser examUser, string userId)
        => examUser != null
            && (examUser.UserId == userId || _examAccessValidator.IsUserExamCreatorOrExamUser(userId, examUser.Exam));

        public bool IsUserExamCreator(ExamUser examUser, string userId)
            => examUser != null
            && (_examAccessValidator.IsUserExamCreator(userId, examUser.Exam));
    }
}
