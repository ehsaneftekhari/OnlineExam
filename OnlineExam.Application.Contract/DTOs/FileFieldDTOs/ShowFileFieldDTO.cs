namespace OnlineExam.Application.Contract.DTOs.FileFieldDTOs
{
    public class ShowFileFieldDTO
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int KiloByteMaximumSize { get; set; }
        public IDictionary<int, string> AllowedFileTypesNameIdPairs { get; set; } = new Dictionary<int, string>();
    }
}
