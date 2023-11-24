using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IQuestionValidator
    {
        public void ThrowIfUserIsNotExamCreator(int sectionId, string issuerUserId);

        public void ThrowIfUserIsNotExamCreatorOrExamUser(int sectionId, string issuerUserId);

        public void ThrowIfUserIsNotExamCreatorOrExamUser(string issuerUserId, Exam exam);

        public void ThrowIfUserIsNotExamCreator(string issuerUserId, Exam exam);
    }
}
