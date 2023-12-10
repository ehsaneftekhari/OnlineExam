namespace OnlineExam.EndPoint.API.DTOs.ExamDTOs
{
    public class AddExamApiDTO
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Published { get; set; }
    }
}
