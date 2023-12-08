namespace OnlineExam.Application.Contract.DTOs.TextFieldDTOs
{
    public class AddTextFieldDTO
    {
        public int TextFieldUITypeId { get; set; }
        public int? AnswerLength { get; set; }
        public string? RegEx { get; set; }
    }
}
