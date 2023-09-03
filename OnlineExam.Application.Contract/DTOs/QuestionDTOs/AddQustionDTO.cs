namespace OnlineExam.Application.Contract.DTOs.QuestionDTOs
{
    public class AddQuestionDTO
    {
        public string Text { get; set; }
        public string? ImageAddress { get; set; }
        public int Score { get; set; }
        public int DurationDays { get; set; }
        public int DurationHours { get; set; }
        public int DurationMinutes { get; set; }
        public int DurationSeconds { get; set; }
        public int Order { get; set; }
    }
}
