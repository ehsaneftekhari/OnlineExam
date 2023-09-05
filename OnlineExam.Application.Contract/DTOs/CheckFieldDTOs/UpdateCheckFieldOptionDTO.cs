namespace OnlineExam.Application.Contract.DTOs.CheckFieldDTOs
{
    public class UpdateCheckFieldOptionDTO
    {
        public int? Order { get; set; }
        public int? CheckFieldId { get; set; }
        public int? Score { get; set; }
        public string? Text { get; set; }
    }
}
