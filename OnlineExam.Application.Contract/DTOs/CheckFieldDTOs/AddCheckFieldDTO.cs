using OnlineExam.Model.Models;

namespace OnlineExam.Application.Contract.DTOs.CheckFieldDTOs
{
    public class AddCheckFieldDTO
    {
        public int CheckFieldUIType { get; set; }
        public bool RandomizeOptions { get; set; }
        public int MaximumSelection { get; set; }
    }
}
