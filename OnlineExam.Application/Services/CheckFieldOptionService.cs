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
    }
}
