namespace OnlineExam.Model.Models
{
    public class Section : BaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public bool RandomizeQuestions { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
    }
}
