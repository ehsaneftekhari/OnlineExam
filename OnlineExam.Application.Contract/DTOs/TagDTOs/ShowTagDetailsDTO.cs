namespace OnlineExam.Application.Contract.DTOs.TagDTOs
{
    public class ShowTagDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
