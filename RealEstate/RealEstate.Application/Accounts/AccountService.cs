using RealEstate.Domain.Entities;
using RealEstate.Domain.Exceptions;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Interfaces.Services;

namespace RealEstate.Application.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IFileStorage _fileStorage;
        private readonly string _ownerContainer;

        public AccountService(IOwnerRepository ownerRepository,
            IFileStorage fileStorage)
        {
            _ownerRepository = ownerRepository;
            _fileStorage = fileStorage;
            _ownerContainer = "owners";
        }

        public async Task Register(byte[] photo, Owner owner, string password)
        {
            if (photo != null)
            {
                var url = await _fileStorage.SaveFileAsync(photo, ".jpg", _ownerContainer);
                owner.Photo = url;
            }

            var result = await _ownerRepository.AddUserAsync(owner, password);
            if (!result.Succeeded)
            {
                throw new AuthException(result.Errors!.FirstOrDefault()!.Description);
            }

            await _ownerRepository.AddUserToRoleAsync(owner, "Owner");
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
