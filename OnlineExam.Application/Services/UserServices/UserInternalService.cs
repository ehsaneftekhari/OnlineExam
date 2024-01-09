using Microsoft.AspNetCore.Identity;
using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Contract.Exceptions;
using System.Linq;
using System.Security.Claims;

namespace OnlineExam.Application.Services.UserServices
{

    public class UserInternalService : BaseInternalServiceBase<IdentityUser, string>, IUserInternalService
    {
        UserManager<IdentityUser> _userManager { get; set; }

        string IBaseInternalService<IdentityUser, string>.EntityName
            => "User";

        string IBaseInternalService<IdentityUser, string>.EntityIdName
            => "UserId";

        protected virtual OEApplicationException IdIsNotValidException
            => new ApplicationValidationException($"{EntityIdName} is not valid");

        public UserInternalService(UserManager<IdentityUser> userManager)
            => _userManager = userManager;

        protected virtual OEApplicationException CreateNameIsNotExistsException(string name)
            => new ApplicationSourceNotFoundException($"User with name:{name} is not exists");

        internal IQueryable<IdentityUser> GetIQueryable()
            => _userManager.Users;

        internal IdentityUser GetById(string id)
            => GetById(id, externQueryProvider : null);

        internal IdentityUser GetById(string id, IQueryable<IdentityUser>? queryable)
        {
            ThrowIfdIsNotValid(id);

            IdentityUser? record;

            if (queryable == null)
                record = _userManager.FindByIdAsync(id).GetAwaiter().GetResult();
            else
                record = queryable.FirstOrDefault(x => x.Id == id);

            if (record == null)
                throw IsNotExistsException(id);

            return record;
        }

        internal IdentityUser GetById(string id, Func<IQueryable<IdentityUser>, IQueryable<IdentityUser>>? externQueryProvider)
        {
            ThrowIfdIsNotValid(id);

            IdentityUser? record;

            if (externQueryProvider == null)
                record = _userManager.FindByIdAsync(id).GetAwaiter().GetResult();
            else
                record = externQueryProvider.Invoke(GetIQueryable()).FirstOrDefault(x => x.Id == id);

            if (record == null)
                throw IsNotExistsException(id);

            return record;
        }

        internal IdentityUser GetByName(string name)
        {
            if (!TryGetByName(name, out var record, out var exception))
                throw exception;

            return record;
        }

        internal bool TryGetByName(string name, out IdentityUser result, out Exception exception)
        {
            exception = null;
            result = _userManager.FindByNameAsync(name).GetAwaiter().GetResult();

            if (result == null)
            {
                exception = CreateNameIsNotExistsException(name);
                return false;
            }

            return true;
        }

        internal void ThrowIfdIsNotValid(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw IdIsNotValidException;
        }

        internal void ThrowExceptionIfEntityIsNotExists(string id)
            => GetById(id);

        internal void AddToRolesAsync(IdentityUser user, IEnumerable<string> roles)
        {
            var result = _userManager.AddToRolesAsync(user, roles).GetAwaiter().GetResult();

            //if(result.Errors.Any())
        }

        internal IdentityUser Add(IdentityUser newUser)
        {
            ThrowIfEntityIsNull(newUser);
            var addResult = _userManager.CreateAsync(newUser).GetAwaiter().GetResult();

            if (addResult.Errors.Any())
            {
                var messages = GetErrorMessages(addResult.Errors).Select(x => x.ToString()).Aggregate((f, s) => $"{f}, {s}");
                throw new OEApplicationException(messages);
            }

            return newUser;
        }

        internal IEnumerable<IdentityUser> GetAll(int skip, int take)
        {
            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var records =
                GetIQueryable()
                .Skip(skip)
                .Take(take)
                .ToList();

            if (!records.Any())
                throw ThereIsNoEntityException;

            return records!;
        }

        internal void Delete(string id)
            => Delete(GetById(id));

        internal void Delete(IdentityUser record)
        {
            var result = _userManager.DeleteAsync(record).GetAwaiter().GetResult();

            if (result.Errors.Any())
                throw DidNotDeletedException;
        }

        IQueryable<IdentityUser> IBaseInternalService<IdentityUser, string>.GetIQueryable()
            => GetIQueryable();

        void IUserInternalService.AddToRolesAsync(IdentityUser user, IEnumerable<string> roles)
            => AddToRolesAsync(user, roles);

        IdentityUser IBaseInternalService<IdentityUser, string>.Add(IdentityUser newUser)
            => Add(newUser);

        IdentityUser IBaseInternalService<IdentityUser, string>.GetById(string id)
            => GetById(id);

        IdentityUser IBaseInternalService<IdentityUser, string>.GetById(string id, IQueryable<IdentityUser>? queryable)
            => GetById(id, queryable);

        IEnumerable<IdentityUser> IBaseInternalService<IdentityUser, string>.GetAll(int skip, int take)
            => GetAll(skip, take);

        void IBaseInternalService<IdentityUser, string>.Update(IdentityUser user)
            => throw new NotImplementedException();

        void IBaseInternalService<IdentityUser, string>.Delete(string id)
            => Delete(id);

        IList<string> IUserInternalService.GetRoles(IdentityUser user)
            => _userManager.GetRolesAsync(user).GetAwaiter().GetResult();

        void IBaseInternalService<IdentityUser, string>.ThrowIfdIsNotValid(string id)
            => ThrowIfdIsNotValid(id);

        void IBaseInternalService<IdentityUser, string>.ThrowIfEntityIsNull(IdentityUser user)
            => ThrowIfEntityIsNull(user);

        void IBaseInternalService<IdentityUser, string>.ThrowExceptionIfEntityIsNotExists(string id)
            => ThrowExceptionIfEntityIsNotExists(id);

        string IUserInternalService.HashPassword(IdentityUser user, string password)
            => _userManager.PasswordHasher.HashPassword(user, password);

        IdentityUser IUserInternalService.GetByName(string name)
            => GetByName(name);

        bool IUserInternalService.TryGetByName(string name, out IdentityUser result, out Exception exception)
            => TryGetByName(name, out result, out exception);

        OEApplicationException IBaseInternalService<IdentityUser, string>.IsNotExistsException(string id)
            => IsNotExistsException(id);

        OEApplicationException IBaseInternalService<IdentityUser, string>.IsNotExistsException()
            => IsNotExistsException();

        IdentityUser IBaseInternalService<IdentityUser, string>.GetById(
            string id,
            Func<IQueryable<IdentityUser>, IQueryable<IdentityUser>>? externQueryProvider)
            => GetById(id, externQueryProvider);

        private IList<string> GetErrorMessages(IEnumerable<IdentityError> errors)
        {
            var messages = new List<string>();

            if (errors.Any(x => x.Code == "DuplicateUserName"))
                messages.Add(errors.First(x => x.Code == "DuplicateUserName").Description);

            return messages;
        }

        void IBaseInternalService<IdentityUser, string>.Delete(IdentityUser record)
        {
            throw new NotImplementedException();
        }
    }
}