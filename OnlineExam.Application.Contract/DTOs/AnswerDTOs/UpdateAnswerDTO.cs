namespace OnlineExam.Application.Contract.DTOs.AnswerDTOs
{
    public class UpdateAnswerDTO
    {
        public int ExamUserId { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; } = null!;
    }
}
