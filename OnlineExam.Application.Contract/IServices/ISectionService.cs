using OnlineExam.Application.Contract.DTOs.SectionDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface ISectionService
    {
        ShowSectionDTO Add(int examId, string issuerUserId, AddSectionDTO dTO);

        IEnumerable<ShowSectionDTO> GetAllByExamId(int examId, string issuerUserId, int skip, int take);

        ShowSectionDTO? GetById(int id, string issuerUserId);

        void Update(int id, string issuerUserId, UpdateSectionDTO dTO);

        void Delete(int id, string issuerUserId);
    }
}
