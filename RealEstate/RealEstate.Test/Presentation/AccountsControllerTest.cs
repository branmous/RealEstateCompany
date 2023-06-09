using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Exceptions;
using RealEstate.Domain.Interfaces.Services;
using RealEstate.Presentation.Controllers;
using RealEstate.Presentation.DTOs;
using RealEstate.Test.Mocks;

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
            var auth = OwnerMock.GetLogin();

            var owner = OwnerMock.GetEntity();

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
            AuthDTO auth = OwnerMock.GetLoginIncorrect();

            _accountsController.ModelState.AddModelError("Email", "The Email is invalid.");

            var result = await _accountsController.LoginAsync(auth) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task LoginAsync_BadRequest()
        {
            var auth = OwnerMock.GetLogin();

            _accountService.Setup(a => a.LoginAsync(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new AuthException("Incorrect email or password."));

            var result = await _accountsController.LoginAsync(auth) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(400));
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task LoginAsync_InternalServerError()
        {
            var auth = OwnerMock.GetLogin();

            _accountService.Setup(a => a.LoginAsync(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception("error"));

            var result = await _accountsController.LoginAsync(auth) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(500));
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task RegisterAsync_Correctly_WithoutPhoto()
        {
            var register = OwnerMock.RegisterWithoutPhoto();

            _accountService.Setup(a => a.Register(It.IsAny<byte[]>(), It.IsAny<Owner>(), It.IsAny<string>()));

            var result = await _accountsController.RegisterAsync(register) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(201));
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task RegisterAsync_ModelInvalid()
        {
            var register = OwnerMock.RegisterWithoutPhoto();
            register.Email = "email";

            _accountsController.ModelState.AddModelError("Email", "The Email is invalid.");

            var result = await _accountsController.RegisterAsync(register) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task RegisterAsync_Correctly_WithPhoto()
        {
            var register = OwnerMock.RegisterWithoutPhoto();
            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(f => f.FileName).Returns("example.jpg");
            formFileMock.Setup(f => f.Length).Returns(1000);
            IFormFile formFile = formFileMock.Object;
            register.Photo = formFile;

            _accountService.Setup(a => a.Register(It.IsAny<byte[]>(), It.IsAny<Owner>(), It.IsAny<string>()));

            var result = await _accountsController.RegisterAsync(register) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(201));
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task RegisterAsync_BadRequest()
        {
            var register = OwnerMock.RegisterWithoutPhoto();

            _accountService.Setup(a => a.Register(It.IsAny<byte[]>(), It.IsAny<Owner>(), It.IsAny<string>())).ThrowsAsync(new AuthException("error"));

            var result = await _accountsController.RegisterAsync(register) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(400));
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task RegisterAsync_InternalServerError()
        {
            var register = OwnerMock.RegisterWithoutPhoto();

            _accountService.Setup(a => a.Register(It.IsAny<byte[]>(), It.IsAny<Owner>(), It.IsAny<string>())).ThrowsAsync(new Exception("error"));

            var result = await _accountsController.RegisterAsync(register) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(500));
            Assert.IsNotNull(result.Value);
        }
    }
}
