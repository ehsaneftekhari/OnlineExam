using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface ITextFieldDTOValidator
    {
        public int AnswerMaxLength { get; }

        public void ValidateDTO(AddTextFieldDTO dTO);

        public void ValidateDTO(UpdateTextFieldDTO dTO);
    }
}
