using OnlineExam.Application.Abstractions.InternalService;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.FileFieldServices
{
    public class AllowedFileTypesFieldInternalService : BaseInternalService<AllowedFileTypesField, IAllowedFileTypesFieldOptionRepository>
    {
        public AllowedFileTypesFieldInternalService(IAllowedFileTypesFieldOptionRepository repository) : base(repository) { }

        internal IEnumerable<AllowedFileTypesField> GetByIds(IEnumerable<int> allowedFileTypesFieldIds)
        {
            if (allowedFileTypesFieldIds == null)
                throw new ApplicationValidationException("allowedFileTypesFieldIds can not be null");

            var fetched =
                allowedFileTypesFieldIds.Any() ?
                GetIQueryable()
                .Where(x => allowedFileTypesFieldIds.Contains(x.Id))
                .ToList()
                : new List<AllowedFileTypesField>();

            if (allowedFileTypesFieldIds.Count() != fetched.Count())
            {
                var notFoundedIds = allowedFileTypesFieldIds
                    .Where(id => !fetched.Any(t => t.Id == id))
                    .ToList();

                string message;

                if (notFoundedIds.Count() > 1)
                {
                    message = "AllowedFileTypesField with ids: ";
                    notFoundedIds.ForEach(id => message += $"{id}, ");
                    message += " are not exists";
                }
                else
                {
                    message = $"AllowedFileTypesField with id: {notFoundedIds.First()} is not exists";
                }

                throw new ApplicationValidationException(message);
            }

            return fetched;
        }
    }
}
