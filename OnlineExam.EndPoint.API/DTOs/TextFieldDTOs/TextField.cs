using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;

namespace OnlineExam.EndPoint.API.DTOs.TextFieldDTOs
{
    public class TextFieldOptionsDTO
    {
        public IEnumerable<ShowTextFieldTypeDTO> TextFieldTypes { get; set; } = new List<ShowTextFieldTypeDTO>();
        public int AnswerMaxLength { get; set; }
    }
}
