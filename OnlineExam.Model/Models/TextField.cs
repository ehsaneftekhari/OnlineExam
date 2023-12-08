namespace OnlineExam.Model.Models
{
    public class TextField : BaseModel
    {
        public TextFieldUIType TextFieldUIType { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int? AnswerLength { get; set; }
        public string? RegEx { get; set; }
    }
}
