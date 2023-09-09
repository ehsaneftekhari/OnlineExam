namespace OnlineExam.Application.Contract.DTOs.ExamUserDTOs
{
    public class ShowExamUserDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int ExamId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int EarnedScore { get; set; }
    }
}
