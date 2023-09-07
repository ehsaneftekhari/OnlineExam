namespace OnlineExam.Application.Contract.DTOs.FileFieldDTOs
{
    public class UpdateFileFieldDTO
    {
        public int KiloByteMaximumSize { get; set; }
        public ICollection<int> AllowedFileTypesIds { get; set; } = new List<int>();
    }
}
