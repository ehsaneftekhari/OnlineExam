using OnlineExam.Application.Contract.DTOs.SectionDTOs;
using OnlineExam.Application.Contract.Markers;

namespace OnlineExam.Application.Contract.IServices
{
    public interface ISectionService : IApplicationContractMarker
    {
        bool Add(AddSectionDTO dTO);

        ShowSectionDTO? GetById(int id);

        bool Update(UpdateSectionDTO dTO);

        bool Delete(int id);
    }
}
