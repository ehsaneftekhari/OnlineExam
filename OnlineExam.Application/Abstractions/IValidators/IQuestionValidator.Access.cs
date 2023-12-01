using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IQuestionAccessValidator
    {
        public void ThrowIfUserIsNotExamCreator(int sectionId, string issuerUserId);

        public void ThrowIfUserIsNotExamCreatorOrExamUser(int sectionId, string issuerUserId);

        public void ThrowIfUserIsNotExamCreatorOrExamUser(string issuerUserId, Exam exam);

        public void ThrowIfUserIsNotExamCreator(string issuerUserId, Exam exam);
    }
}
