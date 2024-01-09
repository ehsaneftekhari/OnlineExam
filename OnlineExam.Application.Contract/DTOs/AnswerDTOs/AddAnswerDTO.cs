namespace OnlineExam.Application.Contract.DTOs.AnswerDTOs
{
    public class AddAnswerDTO
    {
        public int QuestionId { get; set; }
        public string Content { get; set; } = null!;
    }
}
