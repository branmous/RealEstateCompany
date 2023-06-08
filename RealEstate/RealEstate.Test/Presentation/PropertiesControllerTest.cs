using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;
using RealEstate.Application.Accounts;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces.Services;
using RealEstate.Presentation.Controllers;
using RealEstate.Presentation.DTOs;
using RealEstate.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Test.Presentation
{
    [TestFixture]
    public class PropertiesControllerTest
    {

        private Mock<IPropertyService> _propertyService;
        private Mock<IAccountService> _accountService;
        private PropertiesController _propertiesController;
        private Mock<HttpContext> _httpContextMock;

        [SetUp]
        public void Setup()
        {
            _httpContextMock = new Mock<HttpContext>();
            _httpContextMock.SetupGet(c => c.User.Identity!.Name).Returns("mock@mock.com");
            _propertyService = new Mock<IPropertyService>();
            _accountService = new Mock<IAccountService>();
            _propertiesController = new PropertiesController(_propertyService.Object, _accountService.Object);
            _propertiesController.ControllerContext = new ControllerContext
            {
                HttpContext = _httpContextMock.Object
            };

        }

        [Test]
        public async Task GetAll_Correctly()
        {
            // Arrage
            var owner = OwnerMock.GetEntity();

            List<Property> properties = PropertyMocks.GetList();

            _accountService.Setup(a => a.GetUserAsyc(It.IsAny<string>())).ReturnsAsync(owner);
            _propertyService.Setup(a => a.GetAllWithPaginateAsync(owner.Id, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(properties);

            var result = await _propertiesController.GetAsync(new PaginationDTO()) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task GetAll_ForID_Correctly()
        {
            // Arrage
            var property = PropertyMocks.GetEntity();

            _propertyService.Setup(a => a.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(property);

            var result = await _propertiesController.GetAsync(1) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task Post_Correctly()
        {
            // Arrage
            var property = PropertyMocks.GetEntity();
            var propertyDTO = PropertyMocks.GetDTO();
            var owner = OwnerMock.GetEntity();

            _accountService.Setup(a => a.GetUserAsyc(It.IsAny<string>())).ReturnsAsync(owner);
            _propertyService.Setup(a => a.SavePropertyAsync(It.IsAny<Property>())).ReturnsAsync(property);

            var result = await _propertiesController.PostAsync(propertyDTO) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(201));
            Assert.IsNotNull(result.Value);
        }
    }
}
