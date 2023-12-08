using Microsoft.AspNetCore.Identity;
using OnlineExam.Application.Abstractions.BaseInternalServices;

namespace OnlineExam.Application.Abstractions.IInternalService
{
    public interface IUserInternalService : IBaseInternalService<IdentityUser, string>
    {
        internal IList<string> GetRoles(IdentityUser user);
        internal IdentityUser GetByName(string name);
        internal bool TryGetByName(string name, out IdentityUser result, out Exception exception);
        internal string HashPassword(IdentityUser user, string password);
        internal void AddToRolesAsync(IdentityUser user, IEnumerable<string> roles);
    }
}
