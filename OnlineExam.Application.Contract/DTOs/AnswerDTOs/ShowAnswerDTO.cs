namespace OnlineExam.Application.Contract.DTOs.AnswerDTOs
{
    public class ShowAnswerDTO
    {
        public int Id { get; set; }
        public int ExamUserId { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public int? EarnedScore { get; set; }
    }
}
