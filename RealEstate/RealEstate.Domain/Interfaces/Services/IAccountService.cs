using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces.Services
{
    public interface IAccountService
    {
        Task<Owner> GetUserAsyc(string email);
        Task<Owner> LoginAsync(string email, string password);
        Task LogoutAsync();
    }
}
