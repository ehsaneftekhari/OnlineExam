namespace OnlineExam.Model.Models
{
    public class Section : BaseModel
    {
        public string Title { get; set; }
        public int Order { get; set; }
        public bool RandomizeQuestions { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public ICollection<Question> Question { get; set; } = new List<Question>();
    }
}
