using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces.Services;
using RealEstate.Presentation.Controllers;
using RealEstate.Presentation.DTOs;

namespace RealEstate.Test.Presentation
{
    [TestFixture]
    public class AccountsControllerTest
    {

        private Mock<IConfiguration> _configuration;
        private Mock<IAccountService> _accountService;
        private AccountsController _accountsController;
        private string _key = "fhdsfhsdyfwdsmnfjhksdjhkfdsjnfjkhdsjfsdhjfhksdfhdsjfndsbhfjsjfrejkrekth";

        [SetUp]
        public void SetUp()
        {
            _configuration = new Mock<IConfiguration>();
            _accountService = new Mock<IAccountService>();
            _accountsController = new AccountsController(_configuration.Object, _accountService.Object);
        }

        [Test]
        public async Task LoginAsync_Correctly()
        {
            AuthDTO auth = new AuthDTO
            {
                Email = "mock@mock.com",
                Password = "test"
            };

            var owner = new Owner
            {
                Name = "Mock Name",
                Email = "mock@mock.com",
                UserName = "mock@mock.com",
                Address = "Florida",
                Birthday = DateTime.Now,
            };

            _configuration.Setup(c => c["jwtKey"]).Returns(_key);
            _accountService.Setup(a => a.LoginAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(owner);

            var result = await _accountsController.LoginAsync(auth) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task LoginAsync_ModelInvalid()
        {
            AuthDTO auth = new AuthDTO
            {
                Email = "mock",
                Password = ""
            };

            _accountsController.ModelState.AddModelError("Email", "The Email is invalid.");

            var result = await _accountsController.LoginAsync(auth) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(400));

        }
    }
}
