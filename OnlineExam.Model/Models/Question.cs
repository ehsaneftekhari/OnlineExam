namespace OnlineExam.Model.Models
{
    public class Question : BaseModel
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string? ImageAddress { get; set; }
        public int Score { get; set; }
        public TimeSpan Duration { get; set; }
        public int Order { get; set; }
    }
}
