namespace OnlineExam.Model.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string CreatorUserId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Published { get; set; }
        public ICollection<Section> Sections { get; set; } = new List<Section>();
        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
    }
}
