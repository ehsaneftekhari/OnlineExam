namespace OnlineExam.Application.Contract.DTOs.TextFieldDTOs
{
    public class ShowTextFieldDTO
    {
        public int Id { get; set; }
        public ShowTextFieldTypeDTO UIType { get; set; }
        public int QuestionId { get; set; }
        public int? AnswerLength { get; set; }
        public string? RegEx { get; set; }
    }
}
