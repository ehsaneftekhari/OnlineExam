namespace OnlineExam.Model.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<ExamTag> ExamTags { get; set; } = new List<ExamTag>();
    }
}
