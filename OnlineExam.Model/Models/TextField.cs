namespace OnlineExam.Model.Models
{
    public class TextField
    {
        public int Id { get; set; }
        public TextFieldUIType TextFieldUIType { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int? AnswerLength { get; set; }
        public string? RegEx { get; set; }
    }

    public enum TextFieldUIType
    {
        TextField = 1,
        TextArea = 2
    }
}
