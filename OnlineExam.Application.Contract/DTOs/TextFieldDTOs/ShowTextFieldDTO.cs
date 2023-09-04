using OnlineExam.Model.Models;

namespace OnlineExam.Application.Contract.DTOs.TextFieldDTOs
{
    public class ShowTextFieldDTO
    {
        public int Id { get; set; }
        public int TextFieldUIType { get; set; }
        public string TextFieldUITypeName { get; set; } = null!;
        public int QuestionId { get; set; }
        public int? AnswerLength { get; set; }
        public string? RegEx { get; set; }
    }
}
