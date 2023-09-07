using OnlineExam.Model.Models;
using OnlineExam.Model;

namespace OnlineExam.Application.Contract.DTOs.FileFieldDTOs
{
    public class AddFileFieldDTO
    {
        public int KiloByteMaximumSize { get; set; }
        public ICollection<int> AllowedFileTypesIds { get; set; } = new List<int>();
    }
}
