using Microsoft.AspNetCore.Identity;
using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces
{
    public interface IOwnerRepository
    {
        Task<Owner> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(Owner owner, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(Owner owner, string roleName);

        Task<SignInResult> LoginAsync(string email, string password);

        Task LogoutAsync();

    }
}
