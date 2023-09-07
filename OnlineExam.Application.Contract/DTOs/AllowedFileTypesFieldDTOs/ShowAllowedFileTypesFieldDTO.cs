namespace OnlineExam.Application.Contract.DTOs.AllowedFileTypesFieldDTOs
{
    public class ShowAllowedFileTypesFieldDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Extension { get; set; } = null!;
    }
}
