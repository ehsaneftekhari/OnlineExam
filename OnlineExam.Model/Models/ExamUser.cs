namespace OnlineExam.Model.Models
{
    public class ExamUser : BaseModel
    {
        string UserId { get; set; } = null!;
        int ExamId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int EarnedScore { get; set; }
    }
}
