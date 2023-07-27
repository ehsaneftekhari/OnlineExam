namespace OnlineExam.Application.Contract.DTOs.ExamDTOs
{
    public class ShowExamDTO
    {
        public int Id { get; set; }
        public string CreatorUserId { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Published { get; set; }
    }
}
