using OnlineExam.Application.Contract.DTOs.AnswerDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IAnswerRalationValidator
    {
        void ValidateBeforeAdd(Exam exam, IEnumerable<ExamUser> examUsers);
        void ValidateBeforeUpdate(UpdateAnswerDTO dTO);
    }
}