namespace OnlineExam.Application.Contract.DTOs.ExamUserDTOs
{
    public class AddExamUserDTO
    {
        public string UserId { get; set; } = null!;
        public int ExamId { get; set; }
    }
}
