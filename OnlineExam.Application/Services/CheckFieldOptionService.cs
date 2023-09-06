using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.IServices;

namespace OnlineExam.Application.Services
{
    public class CheckFieldOptionService : ICheckFieldOptionService
    {
        public ShowCheckFieldOptionDTO Add(int checkFieldId, AddCheckFieldOptionDTO dTO)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ShowCheckFieldOptionDTO> GetAllByCheckFieldId(int checkFieldId, int skip = 0, int take = 20)
        {
            throw new NotImplementedException();
        }

        public ShowCheckFieldOptionDTO? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, UpdateCheckFieldOptionDTO dTO)
        {
            throw new NotImplementedException();
        }

        private void ValidateAddDTO(AddCheckFieldOptionDTO dTO)
            => ValidateDTO(dTO.Order, dTO.Text);

        private void ValidateUpdateDTO(UpdateCheckFieldOptionDTO dTO)
            => ValidateDTO(dTO.Order, dTO.Text);
        
        private void ValidateDTO(int? Order, string? Text)
        {
            if (Order.HasValue && (Order < 1))
                throw new ApplicationValidationException("Order can not be less then 1");

            if (string.IsNullOrEmpty(Text) && (Text!.Length > 4000))
                throw new ApplicationValidationException("Text length can not be more than 4000 characters");
        }
    }
}
