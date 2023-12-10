using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.FileFieldServices
{
    public sealed class AllowedFileTypesFieldInternalService
        : BaseInternalService<AllowedFileTypesField, IAllowedFileTypesFieldOptionRepository>
        , IAllowedFileTypesFieldInternalService
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

        IEnumerable<AllowedFileTypesField> IAllowedFileTypesFieldInternalService.GetByIds(IEnumerable<int> allowedFileTypesFieldIds)
            => GetByIds(allowedFileTypesFieldIds);
    }
}
