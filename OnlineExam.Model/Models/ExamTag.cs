namespace OnlineExam.Model.Models
{
    public class ExamTag
    {
        public int ExamId { get; set; }
        public Exam Exam { get; set; } = null!;
        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}
