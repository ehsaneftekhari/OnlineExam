namespace OnlineExam.Application.Contract.DTOs
{
    public class AddExamDTO
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Published { get; set; }
    }
}
