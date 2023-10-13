namespace OnlineExam.Application.Contract.DTOs.SectionDTOs
{
    public class UpdateSectionDTO
    {
        public string? Title { get; set; }
        public int? Order { get; set; }
        public bool? RandomizeQuestions { get; set; }
    }
}
