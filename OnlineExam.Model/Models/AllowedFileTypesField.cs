namespace OnlineExam.Model.Models
{
    public class AllowedFileTypesField : BaseModel
    {
        public string Name { get; set; } = null!;
        public string Extention { get; set; } = null!;
        public ICollection<FileField> FileFields { get; set; } = new List<FileField>();
    }
}
