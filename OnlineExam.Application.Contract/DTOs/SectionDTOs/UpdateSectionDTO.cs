namespace OnlineExam.Application.Contract.DTOs.SectionDTOs
{
    public class UpdateSectionDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? Order { get; set; }
        public bool? RandomizeQuestions { get; set; }
    }
}
