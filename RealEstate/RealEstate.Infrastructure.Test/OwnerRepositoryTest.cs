using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Data;
using RealEstate.Infrastructure.Repositories;

namespace RealEstate.Infrastructure.Test
{
    [TestFixture]
    public class OwnerRepositoryTest
    {
        private DataContext _dataContext;
        private Mock<UserManager<Owner>> _userManager;
        private Mock<RoleManager<IdentityRole>> _roleManager;
        private Mock<SignInManager<Owner>> _signInManager;
        private OwnerRepository _ownerRepository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "testDatabase")
            .EnableSensitiveDataLogging(true)
                    .Options;

            var userStoreMock = new Mock<IUserStore<Owner>>();
            var optonMock = new Mock<IOptions<IdentityOptions>>();
            var passwordHasherMock = new Mock<IPasswordHasher<Owner>>();
            var userValidatorsMock = new Mock<IEnumerable<IUserValidator<Owner>>>();
            var passwordValidatorsMock = new Mock<IEnumerable<IPasswordValidator<Owner>>>();
            var keyNormalizerMock = new Mock<ILookupNormalizer>();
            var errorsMock = new Mock<IdentityErrorDescriber>();
            var servicesMock = new Mock<IServiceProvider>();
            var loggerMock = new Mock<ILogger<UserManager<Owner>>>();


            _userManager = new Mock<UserManager<Owner>>(Mock.Of<IUserStore<Owner>>(),
                                                        null,
                                                        Mock.Of<IPasswordHasher<Owner>>(),
                                                        null,
                                                        null,
                                                        null,
                                                        null,
                                                        null,
                                                        Mock.Of<ILogger<UserManager<Owner>>>());
            _roleManager = new Mock<RoleManager<IdentityRole>>(Mock.Of<IRoleStore<IdentityRole>>(),
                                                        null,
                                                        null,
                                                        null,
                                                        null);
            _signInManager = new Mock<SignInManager<Owner>>(
                                                        _userManager.Object,
                                                        Mock.Of<IHttpContextAccessor>(),
                                                        Mock.Of<IUserClaimsPrincipalFactory<Owner>>(),
                                                        null,
                                                        null,
                                                        null);
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


            _userManager.Setup(u => u.CreateAsync(It.IsAny<Owner>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _ownerRepository = new OwnerRepository(_dataContext, _userManager.Object, _roleManager.Object, _signInManager.Object);

            var result = await _ownerRepository.AddUserAsync(owner, "123456");
            Assert.That(result, Is.EqualTo(IdentityResult.Success));
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


            _userManager.Setup(u => u.AddToRoleAsync(It.IsAny<Owner>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _ownerRepository = new OwnerRepository(_dataContext, _userManager.Object, _roleManager.Object, _signInManager.Object);

            await _ownerRepository.AddUserToRoleAsync(owner, "ROLE_TEST");
            Assert.True(true);
        }

        [Test]
        public async Task CheckRoleAsync_Correctly()
        {
            _roleManager.Setup(u => u.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _ownerRepository = new OwnerRepository(_dataContext, _userManager.Object, _roleManager.Object, _signInManager.Object);

            await _ownerRepository.CheckRoleAsync("ROLE_TEST");
            Assert.True(true);
        }

        [Test]
        public async Task CheckRoleAsync_NotExistAndCreateCorrectly()
        {
            _roleManager.Setup(u => u.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
            _roleManager.Setup(u => u.CreateAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Success);
            _ownerRepository = new OwnerRepository(_dataContext, _userManager.Object, _roleManager.Object, _signInManager.Object);

            await _ownerRepository.CheckRoleAsync("ROLE_TEST");
            Assert.True(true);
        }

        [Test]
        public async Task LoginAsync_Correctly()
        {
            _signInManager.Setup(u => u.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(SignInResult.Success);
            _ownerRepository = new OwnerRepository(_dataContext, _userManager.Object, _roleManager.Object, _signInManager.Object);

            var result = await _ownerRepository.LoginAsync("mock@mock.com", "mockPassword");
            Assert.That(result, Is.EqualTo(SignInResult.Success));
        }
    }
}
