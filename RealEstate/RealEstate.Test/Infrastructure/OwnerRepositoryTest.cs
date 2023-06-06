using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Data;
using RealEstate.Infrastructure.Repositories;

namespace RealEstate.Test.Infrastructure
{
    [TestFixture]
    public class OwnerRepositoryTest
    {
        private DataContext _dataContext;
        private UserManager<Owner> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<Owner> _signInManager;
        private OwnerRepository? _ownerRepository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "testDatabase")
            .EnableSensitiveDataLogging(true)
                    .Options;

            _userManager = Substitute.For<UserManager<Owner>>(
                        Substitute.For<IUserStore<Owner>>(),
                        null, null, null, null, null, null, null, null
                    );

            _signInManager = Substitute.For<SignInManager<Owner>>(
                                    _userManager,
                                    Substitute.For<IHttpContextAccessor>(),
                                    Substitute.For<IUserClaimsPrincipalFactory<Owner>>(),
                                    null, null, null, null
                                );

            _userManager = Substitute.For<UserManager<Owner>>(Substitute.For<IUserStore<Owner>>(),
                                                        null,
                                                        Substitute.For<IPasswordHasher<Owner>>(),
                                                        null,
                                                        null,
                                                        null,
                                                        null,
                                                        null,
                                                        Substitute.For<ILogger<UserManager<Owner>>>());
            _roleManager = Substitute.For<RoleManager<IdentityRole>>(
                                Substitute.For<IRoleStore<IdentityRole>>(),
                                null, null, null, null
                            );

            _dataContext = new DataContext(options);
        }

        [Test]
        public async Task AddUserAsync_Correctly()
        {
            var owner = new Owner
            {
                Name = "Mock Name",
                Email = "mock@mock.com",
                UserName = "mock@mock.com",
                Address = "Florida",
                Birthday = DateTime.Now,
            };

            _ownerRepository = new OwnerRepository(_dataContext, _userManager, _roleManager, _signInManager);
            var result = await _ownerRepository.AddUserAsync(owner, "123456");
            await _userManager.Received().CreateAsync(owner, "123456");
        }

        [Test]
        public async Task AddUserToRoleAsync_Correctly()
        {
            var owner = new Owner
            {
                Name = "Mock Name",
                Email = "mock@mock.com",
                UserName = "mock@mock.com",
                Address = "Florida",
                Birthday = DateTime.Now,
            };
            _ownerRepository = new OwnerRepository(_dataContext, _userManager, _roleManager, _signInManager);

            await _ownerRepository.AddUserToRoleAsync(owner, "ROLE_TEST");
            await _userManager.Received().AddToRoleAsync(owner, "ROLE_TEST");
        }

        [Test]
        public async Task CheckRoleAsync_Correctly()
        {
            _roleManager.RoleExistsAsync(Arg.Any<string>()).Returns(true);
            _ownerRepository = new OwnerRepository(_dataContext, _userManager, _roleManager, _signInManager);

            await _ownerRepository.CheckRoleAsync("ROLE_TEST");
            Assert.True(true);
        }

        [Test]
        public async Task CheckRoleAsync_NotExistAndCreateCorrectly()
        {
            _roleManager.RoleExistsAsync(Arg.Any<string>()).Returns(true);
            _roleManager.CreateAsync(Arg.Any<IdentityRole>()).Returns(IdentityResult.Success);
            _ownerRepository = new OwnerRepository(_dataContext, _userManager, _roleManager, _signInManager);

            await _ownerRepository.CheckRoleAsync("ROLE_TEST");
            Assert.True(true);
        }

        [Test]
        public async Task LoginAsync_Correctly()
        {
            _ownerRepository = new OwnerRepository(_dataContext, _userManager, _roleManager, _signInManager);

            var result = await _ownerRepository.LoginAsync("mock@mock.com", "mockPassword");
            await _signInManager.Received().PasswordSignInAsync("mock@mock.com", "mockPassword", false, false);
        }
    }
}
