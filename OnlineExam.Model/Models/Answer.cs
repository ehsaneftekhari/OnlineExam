namespace OnlineExam.Model.Models
{
    public class Answer : BaseModel
    {
        public int ExamUserId { get; set; }
        public ExamUser ExamUser { get; set; } = null!;
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public int EarnedScore { get; set; }
    }
}
