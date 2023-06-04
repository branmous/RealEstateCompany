using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<Owner> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Owner> _signInManager;

        public OwnerRepository(DataContext dataContext,
            UserManager<Owner> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<Owner> signInManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddUserAsync(Owner owner, string password)
        {
            return await _userManager.CreateAsync(owner, password);
        }

        public async Task AddUserToRoleAsync(Owner owner, string roleName)
        {
            await _userManager.AddToRoleAsync(owner, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task<Owner> GetUserAsync(string email)
        {
            var owner = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email! == email);
            return owner!;
        }

        public async Task<SignInResult> LoginAsync(string email, string password)
        {
            return await _signInManager.PasswordSignInAsync(email, password, false, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
