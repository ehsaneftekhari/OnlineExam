namespace OnlineExam.Model.Models
{
    public class ExamTag
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
