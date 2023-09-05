namespace OnlineExam.Model.Models
{
    public class CheckField
    {
        public int Id { get; set; }
        public CheckFieldUIType CheckFieldUIType { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public bool RandomizeOptions { get; set; }
        public int MaximumSelection { get; set; }
    }

    public enum CheckFieldUIType
    {
        TextField = 1,
        TextArea = 2
    }
}
