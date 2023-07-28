using OnlineExam.Application.Contract.DTOs.TagDTOs;

namespace OnlineExam.Application.Contract.DTOs.ExamDTOs
{
    public class ExamFilterDTO
    {
        public string? Title { get; set; } = null!;
        public DateTime? StartFrom { get; set; }
        public DateTime? StartTo { get; set; }
        public DateTime? EndFrom { get; set; }
        public DateTime? EndTo { get; set; }
        public bool? Published { get; set; }
        public ICollection<string>? Tags { get; set; } = new List<string>();
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
