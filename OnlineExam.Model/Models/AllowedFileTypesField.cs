namespace OnlineExam.Model.Models
{
    public class AllowedFileTypesField : BaseModel
    {
        public string Name { get; set; } = null!;
        public string Extension { get; set; } = null!;
        public ICollection<FileField> FileFields { get; set; } = new List<FileField>();
    }
}
