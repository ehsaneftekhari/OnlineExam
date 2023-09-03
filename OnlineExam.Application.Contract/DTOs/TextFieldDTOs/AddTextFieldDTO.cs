using OnlineExam.Model.Models;

namespace OnlineExam.Application.Contract.DTOs.TextFieldDTOs
{
    public class AddTextFieldDTO
    {
        public TextFieldUIType TextFieldUIType { get; set; }
        public int QuestionId { get; set; }
        public int? AnswerLength { get; set; }
        public string? RegEx { get; set; }
    }
}
