namespace OnlineExam.Model.Models
{
    public class CheckFieldOption
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int CheckFieldId { get; set; }
        public CheckField CheckField { get; set; } = null!;
        public int Score { get; set; }
        public string? ImageAddress { get; set; }
        public string? Text { get; set; }
    }
}
