using Microsoft.AspNetCore.Identity;
using Moq;
using RealEstate.Application.Accounts;
using RealEstate.Application.Properties;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Exceptions;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Test
{
    [TestFixture]
    public class AccountServiceTest
    {
        private Mock<IOwnerRepository> _ownerRepository;
        private Mock<IFileStorage> _fileStorage;
        private AccountService _accountService;

        [SetUp]
        public void Setup()
        {
            _ownerRepository = new Mock<IOwnerRepository>();
            _fileStorage = new Mock<IFileStorage>();
        }

        [Test]
        public async Task LoginAsync_Correctly()
        {
            var owner = new Owner
            {
                Name = "Mock Name",
                Email = "mock@mock.com",
                UserName = "mock@mock.com",
                Address = "Florida",
                Birthday = DateTime.Now,
            };
            var email = "mock@mock.com";
            var password = "password";

            _ownerRepository.Setup(o => o.LoginAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(SignInResult.Success);
            _ownerRepository.Setup(o => o.GetUserAsync(It.IsAny<string>())).ReturnsAsync(owner);
            _accountService = new AccountService(_ownerRepository.Object, _fileStorage.Object);
            var result = await _accountService.LoginAsync(email, password);
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo(owner.Name));
        }

        [Test]
        public void LoginAsync_AuthException()
        {
            var email = "mock@mock.com";
            var password = "password";

            _ownerRepository.Setup(o => o.LoginAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(SignInResult.NotAllowed);
            _accountService = new AccountService(_ownerRepository.Object, _fileStorage.Object);
            var ex = Assert.ThrowsAsync<AuthException>(async () => await _accountService.LoginAsync(email, password));

            Assert.That(ex.Message, Is.EqualTo("Incorrect email or password."));
        }

        [Test]
        public async Task GetUserAsyc_Correctly()
        {
            var owner = new Owner
            {
                Name = "Mock Name",
                Email = "mock@mock.com",
                UserName = "mock@mock.com",
                Address = "Florida",
                Birthday = DateTime.Now,
            };
            var email = "mock@mock.com";

            _ownerRepository.Setup(o => o.GetUserAsync(It.IsAny<string>())).ReturnsAsync(owner);
            _accountService = new AccountService(_ownerRepository.Object, _fileStorage.Object);
            var result = await _accountService.GetUserAsyc(email);
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo(owner.Name));
        }

        [Test]
        public async Task GetUserAsyc_NotFoundException()
        {
            var email = "mock@mock.com";
            _ownerRepository.Setup(o => o.GetUserAsync(It.IsAny<string>()));
            _accountService = new AccountService(_ownerRepository.Object, _fileStorage.Object);
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _accountService.GetUserAsyc(email));

            Assert.That(ex.Message, Is.EqualTo("Owner not found"));
        }

        [Test]
        public async Task RegisterWithoutPhoto_Correctly()
        {
            var owner = new Owner
            {
                Name = "Mock Name",
                Email = "mock@mock.com",
                UserName = "mock@mock.com",
                Address = "Florida",
                Birthday = DateTime.Now,
            };
            var password = "password";

            _ownerRepository.Setup(o => o.AddUserAsync(It.IsAny<Owner>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _ownerRepository.Setup(o => o.AddUserToRoleAsync(It.IsAny<Owner>(), It.IsAny<string>()));
            _accountService = new AccountService(_ownerRepository.Object, _fileStorage.Object);
            await _accountService.Register(null!, owner, password);
            Assert.True(true);
        }

        [Test]
        public async Task RegisterWithPhoto_Correctly()
        {
            string url = "url_image.com";
            var owner = new Owner
            {
                Name = "Mock Name",
                Email = "mock@mock.com",
                UserName = "mock@mock.com",
                Address = "Florida",
                Birthday = DateTime.Now,
                Photo = url,
            };
            var password = "password";
            byte[] photo = new byte[] { 0x41, 0x42, 0x43, 0x44 };

            _fileStorage.Setup(f => f.SaveFileAsync(It.IsAny<byte[]>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(url);
            _ownerRepository.Setup(o => o.AddUserAsync(It.IsAny<Owner>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _ownerRepository.Setup(o => o.AddUserToRoleAsync(It.IsAny<Owner>(), It.IsAny<string>()));
            _accountService = new AccountService(_ownerRepository.Object, _fileStorage.Object);
            await _accountService.Register(photo, owner, password);
            Assert.True(true);
        }

        [Test]
        public void RegisterWithPhoto_AuthException()
        {
            string url = "url_image.com";
            var owner = new Owner
            {
                Name = "Mock Name",
                Email = "mock@mock.com",
                UserName = "mock@mock.com",
                Address = "Florida",
                Birthday = DateTime.Now,
                Photo = url,
            };
            var password = "password";
            byte[] photo = new byte[] { 0x41, 0x42, 0x43, 0x44 };
            var errorMessage = "Error";

            _fileStorage.Setup(f => f.SaveFileAsync(It.IsAny<byte[]>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(url);
            _ownerRepository.Setup(o => o.AddUserAsync(It.IsAny<Owner>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new[] { new IdentityError { Description = errorMessage } }));
            _accountService = new AccountService(_ownerRepository.Object, _fileStorage.Object);
            var ex = Assert.ThrowsAsync<AuthException>(async () => await _accountService.Register(photo, owner, password));
            Assert.That(ex.Message, Is.EqualTo("Error"));
        }
    }
}
