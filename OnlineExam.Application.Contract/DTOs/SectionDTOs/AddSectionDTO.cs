namespace OnlineExam.Application.Contract.DTOs.SectionDTOs
{
    public class AddSectionDTO
    {
        public string Title { get; set; }
        public int Order { get; set; }
        public bool RandomizeQuestions { get; set; }
        public int ExamId { get; set; }
    }
}
