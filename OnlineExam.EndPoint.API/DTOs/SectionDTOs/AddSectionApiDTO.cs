namespace OnlineExam.EndPoint.API.DTOs.SectionDTOs
{
    public class AddSectionApiDTO
    {
        public string Title { get; set; }
        public int Order { get; set; }
        public bool RandomizeQuestions { get; set; }
    }
}
