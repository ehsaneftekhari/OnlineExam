namespace OnlineExam.Model.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Order { get; set; }
        public bool RandomizeQuestions { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; } = null!;

        public void Deconstruct(out int Id, out string Title, out int Order, out bool RandomizeQuestions, out int ExamId, out Exam Exam)
        {
            Id = this.Id;
            Title = this.Title;
            Order = this.Order;
            RandomizeQuestions = this.RandomizeQuestions;
            ExamId = this.ExamId;
            Exam = this.Exam;
        }
    }
}
