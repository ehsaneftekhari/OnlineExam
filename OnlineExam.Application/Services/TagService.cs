using OnlineExam.Application.Contract.DTOs.TagDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.IMappers;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services
{
    public class TagService : ITagService
    {
        readonly ITagRepository _tagRepository;
        readonly ITagMapper _tagMapper;

        public TagService(ITagRepository tagRepository, ITagMapper tagMapper)
        {
            this._tagRepository = tagRepository;
            this._tagMapper = tagMapper;
        }

        public bool Add(AddTagDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            var newTag = _tagMapper.AddDTOToEntity(dTO);
            return _tagRepository.Add(newTag!) == 1;
        }

        public IEnumerable<ShowTagDTO> GetOrAdd(IEnumerable<AddTagDTO> dTOs)
        {
            List<ShowTagDTO> result = new List<ShowTagDTO>();
            if (dTOs != null)
                foreach (var tag in dTOs)
                {
                    result.Add(GetOrAdd(tag));
                }
            return result;
        }

        public ShowTagDTO GetOrAdd(AddTagDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            var tag = _tagMapper.AddDTOToEntity(dTO);

            var fromDbTag = _tagRepository.GetIQueryable().FirstOrDefault(x => x.Name == tag!.Name);

            if (fromDbTag == null)
                _tagRepository.Add(tag!);
            else
                tag = fromDbTag;

            return _tagMapper.EntityToShowDTO(tag)!;
        }

        public bool Delete(int id)
        {
            return _tagRepository.DeleteById(id) == 1;
        }

        public ShowTagDTO? GetById(int id)
        {
            return _tagMapper.EntityToShowDTO(_tagRepository.GetById(id));
        }

        public ShowTagDTO? GetByName(int id)
        {
            return _tagMapper.EntityToShowDTO(_tagRepository.GetById(id));
        }

        public bool Update(UpdateTagDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            var tag = _tagRepository.GetById(dTO.Id);

            var result = false;

            if (tag != null)
            {
                _tagMapper.UpdateEntityByDTO(tag, dTO);
                result = _tagRepository.Update(tag) == 1;
            }

            return result;
        }
    }
}
