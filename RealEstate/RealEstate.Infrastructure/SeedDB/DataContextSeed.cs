using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.SeedDB
{
    public class DataContextSeed
    {
        private readonly DataContext _dataContext;
        private readonly IOwnerRepository _ownerRepository;

        public DataContextSeed(DataContext dataContext, IOwnerRepository ownerRepository)
        {
            _dataContext = dataContext;
            _ownerRepository = ownerRepository;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckUserAsync("brandon@yopmail.com");
        }

        private async Task CheckUserAsync(string email)
        {
            var owner = await _ownerRepository.GetUserAsync(email);
            if (owner == null)
            {
                owner = new Owner
                {
                    Name = "Brandon Montoya",
                    Email = email,
                    UserName = email,
                    Address = "Florida",
                    Birthday = DateTime.Now,
                };

                await _ownerRepository.AddUserAsync(owner, "123456");
                await _ownerRepository.AddUserToRoleAsync(owner, "Owner");
            }
        }

        private async Task CheckRolesAsync()
        {
            await _ownerRepository.CheckRoleAsync("Owner");
        }

    }
}
