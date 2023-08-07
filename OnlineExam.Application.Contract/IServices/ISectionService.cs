using OnlineExam.Application.Contract.DTOs.SectionDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface ISectionService
    {
        ShowSectionDTO Add(AddSectionDTO dTO);

        ShowSectionDTO? GetById(int id);

        void Update(UpdateSectionDTO dTO);

        void Delete(int id);
    }
}
