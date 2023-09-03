using OnlineExam.Model.Models;

namespace OnlineExam.Application.Contract.DTOs.TextFieldDTOs
{
    public class ShowTextFieldDTO
    {
        public int Id { get; set; }
        public TextFieldUIType TextFieldUIType { get; set; }
        public int QuestionId { get; set; }
        public int? AnswerLength { get; set; }
        public string? RegEx { get; set; }
    }
}
