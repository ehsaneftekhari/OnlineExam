namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface ITextFieldUiTypeDTOValidator
    {
        void ThrowIfIdIsNotValid(int id);
    }
}