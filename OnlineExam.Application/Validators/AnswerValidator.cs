using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.AnswerDTOs;
using OnlineExam.Application.Contract.Exceptions;

namespace OnlineExam.Application.Validators
{
    internal class AnswerValidator : IAnswerValidator
    {
        public void ValidateDTO(AddAnswerDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            if (dTO.ExamUserId < 1)
                throw new ApplicationValidationException("examUserId can not be less than 1");

            if (dTO.QuestionId < 1)
                throw new ApplicationValidationException("questionId can not be less than 1");

            if (dTO.Content != null && dTO.Content.Length > 4000)
                throw new ApplicationValidationException("Content can not be longer than 4000 characters");
        }

        public void ValidateDTO(UpdateAnswerDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            if (dTO.Id < 1)
                throw new ApplicationValidationException("Id can not be less than 1");

            if (dTO.EarnedScore < 0)
                throw new ApplicationValidationException("EarnedScore can not be less than 0");
        }
    }
}
