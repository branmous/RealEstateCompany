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
            var owner = await CheckUserAsync("brandon@yopmail.com");
            await CheckProperties(owner);
        }

        private async Task CheckProperties(Owner owner)
        {
            if (owner != null)
            {
                if (!_dataContext.Properties.Any())
                {
                    _dataContext.Properties.AddRange(new List<Property>
                    {
                        new Property {
                            Name = "My House",
                            Address = "Florida",
                            CodeInternal= "12345",
                            Price = 2000,
                            OwnerId = owner!.Id
                        },
                        new Property {
                            Name = "My house 2",
                            Address = "Florida",
                            CodeInternal= "12345",
                            Price = 2000,
                            OwnerId = owner!.Id
                        },
                         new Property {
                            Name = "My House 3",
                            Address = "Florida",
                            CodeInternal= "12345",
                            Price = 2000,
                            OwnerId = owner!.Id
                        },
                        new Property {
                            Name = "My house 4",
                            Address = "Florida",
                            CodeInternal= "12345",
                            Price = 2000,
                            OwnerId = owner!.Id
                        },
                        new Property {
                            Name = "My House 5",
                            Address = "Florida",
                            CodeInternal= "12345",
                            Price = 2000,
                            OwnerId = owner!.Id
                        },
                        new Property {
                            Name = "My house 6",
                            Address = "Florida",
                            CodeInternal= "12345",
                            Price = 2000,
                            OwnerId = owner!.Id
                        },
                        new Property {
                            Name = "My house 7",
                            Address = "Florida",
                            CodeInternal= "12345",
                            Price = 2000,
                            OwnerId = owner!.Id
                        },
                        new Property {
                            Name = "My house 8",
                            Address = "Florida",
                            CodeInternal= "12345",
                            Price = 2000,
                            OwnerId = owner!.Id
                        },
                        new Property {
                            Name = "My house 9",
                            Address = "Florida",
                            CodeInternal= "12345",
                            Price = 2000,
                            OwnerId = owner!.Id
                        },
                        new Property {
                            Name = "My house 10",
                            Address = "Florida",
                            CodeInternal= "12345",
                            Price = 2000,
                            OwnerId = owner!.Id
                        }
                    });
                }

                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task<Owner> CheckUserAsync(string email)
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

            return owner;
        }

        private async Task CheckRolesAsync()
        {
            await _ownerRepository.CheckRoleAsync("Owner");
        }

    }
}
