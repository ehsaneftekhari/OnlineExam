using Microsoft.AspNetCore.Identity;

namespace OnlineExam.Model.Models
{
    public class ExamUser : BaseModel
    {
        public string UserId { get; set; } = null!;
        public IdentityUser User { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
        public int? EarnedScore { get; set; }
    }
}
