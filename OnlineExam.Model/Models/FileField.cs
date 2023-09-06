namespace OnlineExam.Model.Models
{
    public class FileField : BaseModel
    {
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
        public int KiloByteMaximumSize { get; set; }
        public ICollection<AllowedFileTypesField> AllowedFileTypes { get; set; } = new List<AllowedFileTypesField>();
    }
}
