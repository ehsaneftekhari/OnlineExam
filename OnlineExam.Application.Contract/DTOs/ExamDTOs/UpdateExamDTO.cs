namespace OnlineExam.Application.Contract.DTOs.ExamDTOs
{
    public class UpdateExamDTO
    {
        public string? Title { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public bool? Published { get; set; }
    }
}
