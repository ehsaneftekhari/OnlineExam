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

        public IEnumerable<Tag> SyncTagsByNames(IEnumerable<Tag> tags)
        {
            if (tags != null)
                return tags.Select(t => SyncTagByName(t));

            return Array.Empty<Tag>();
        }

        public Tag SyncTagByName(Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException();

            var fromDbTag = _tagRepository.GetByName(tag!.Name);

            if (fromDbTag == null)
                _tagRepository.Add(tag!);
            else
                tag = fromDbTag;

            return tag;
        }

        public bool DeleteById(int id)
        {
            return _tagRepository.DeleteById(id) == 1;
        }

        public ShowTagDetailsDTO? GetById(int id)
        {
            return _tagMapper.EntityToShowTagDetailsDTO(_tagRepository.GetById(id));
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
