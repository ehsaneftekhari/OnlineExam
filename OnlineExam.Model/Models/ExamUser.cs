namespace OnlineExam.Model.Models
{
    public class ExamUser : BaseModel
    {
        public string UserId { get; set; } = null!;
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public int? EarnedScore { get; set; }
    }
}
