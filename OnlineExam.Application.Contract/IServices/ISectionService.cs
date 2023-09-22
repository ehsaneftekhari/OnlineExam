using OnlineExam.Application.Contract.DTOs.SectionDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface ISectionService
    {
        ShowSectionDTO Add(int examId, AddSectionDTO dTO);

        IEnumerable<ShowSectionDTO> GetAllByExamId(int examId, int skip, int take);

        ShowSectionDTO? GetById(int id);

        void Update(int id, UpdateSectionDTO dTO);

        void Delete(int id);
    }
}
