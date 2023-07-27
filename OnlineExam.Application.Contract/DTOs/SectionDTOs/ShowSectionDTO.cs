using OnlineExam.Application.Contract.DTOs.ExamDTOs;

namespace OnlineExam.Application.Contract.DTOs.SectionDTOs
{
    public class ShowSectionDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Order { get; set; }
        public bool RandomizeQuestions { get; set; }
        public ShowExamDTO Exam { get; set; } = null!;
    }
}
