using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.AnswerDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    internal class AnswerDTOValidator : IAnswerDTOValidator
    {
        public void ValidateDTO(AddAnswerDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

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

        public void ThrowIfExamUsersAreNotValid(Exam parenExam)
        {
            if (!parenExam.ExamUsers.Any())
                throw new ApplicationValidationException($"User Did not registered for Exam");

            var unFinishedCount = parenExam.ExamUsers.Count(eu => !eu.End.HasValue);

            if (unFinishedCount > 1)
                throw new OEApplicationInvalidStateException($"Invalid State");


            var finishedCount = parenExam.ExamUsers.Count(eu => eu.End.HasValue);

            if (unFinishedCount == 0 && finishedCount > 0)
                throw new ApplicationValidationException($"User has finished the exam");
        }

        public void ThrowIfExamIsNotActive(DateTime now, Exam exam)
        {
            if (!exam.Published)
                throw new ApplicationValidationException($"Exam is not published yet");

            if (exam.Start > now)
                throw new ApplicationValidationException($"Exam is not started yet");

            if (exam.End < now)
                throw new ApplicationValidationException($"Exam has ended");
        }
    }
}
