namespace OnlineExam.Application.Contract.DTOs
{
    public class ShowExamDTO
    {
        public string CreatorUserId { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Published { get; set; }
    }
}
