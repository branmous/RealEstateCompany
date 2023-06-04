using RealEstate.Domain.Entities;
using RealEstate.Domain.Exceptions;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Interfaces.Services;

namespace RealEstate.Application.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IOwnerRepository _ownerRepository;

        public AccountService(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<Owner> GetUserAsyc(string email)
        {
            var user = await _ownerRepository.GetUserAsync(email);
            if (user == null)
            {
                throw new NotFoundException("Owner not found");
            }

            return user;
        }

        public async Task<Owner> LoginAsync(string email, string password)
        {
            var result = await _ownerRepository.LoginAsync(email, password);
            if (!result.Succeeded)
            {
                throw new AuthException("Incorrect email or password.");
            }

            var user = await _ownerRepository.GetUserAsync(email);
            return user!;
        }

        public async Task LogoutAsync()
        {
            await _ownerRepository.LogoutAsync();
        }
    }
}
