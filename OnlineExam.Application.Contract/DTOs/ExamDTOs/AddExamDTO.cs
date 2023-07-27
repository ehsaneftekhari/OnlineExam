using OnlineExam.Application.Contract.DTOs.TagDTOs;

namespace OnlineExam.Application.Contract.DTOs.ExamDTOs
{
    public class AddExamDTO
    {
        public string Title { get; set; } = null!;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Published { get; set; }
        public ICollection<AddTagDTO> Tags { get; set; } = new List<AddTagDTO>();
    }
}
