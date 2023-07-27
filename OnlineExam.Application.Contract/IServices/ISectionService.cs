using OnlineExam.Application.Contract.DTOs.SectionDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface ISectionService
    {
        bool Add(AddSectionDTO dTO);

        ShowSectionDTO? GetById(int id);

        bool Update(UpdateSectionDTO dTO);

        bool Delete(int id);
    }
}
