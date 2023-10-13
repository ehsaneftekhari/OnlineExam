namespace OnlineExam.Application.Contract.DTOs.CheckFieldDTOs
{
    public class ShowCheckFieldOptionDTO
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int CheckFieldId { get; set; }
        public int Score { get; set; }
        public string? ImageAddress { get; set; }
        public string? Text { get; set; }
    }
}
