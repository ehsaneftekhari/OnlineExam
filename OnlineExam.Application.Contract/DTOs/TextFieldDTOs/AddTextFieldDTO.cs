using OnlineExam.Model.Models;

namespace OnlineExam.Application.Contract.DTOs.TextFieldDTOs
{
    public class AddTextFieldDTO
    {
        public int TextFieldUIType { get; set; }
        public int? AnswerLength { get; set; }
        public string? RegEx { get; set; }
    }
}
