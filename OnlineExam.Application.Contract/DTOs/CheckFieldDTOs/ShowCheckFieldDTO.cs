namespace OnlineExam.Application.Contract.DTOs.CheckFieldDTOs
{
    public class ShowCheckFieldDTO
    {
        public int Id { get; set; }
        public int CheckFieldUIType { get; set; }
        public string CheckFieldUITypeName { get; set; } = null!;
        public int QuestionId { get; set; }
        public bool RandomizeOptions { get; set; }
        public int MaximumSelection { get; set; }
    }
}
