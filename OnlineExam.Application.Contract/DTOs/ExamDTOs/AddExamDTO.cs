namespace OnlineExam.Application.Contract.DTOs.ExamDTOs
{
    public class AddExamDTO
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Published { get; set; }
        public string CreatorUserId { get; set; }
    }
}
