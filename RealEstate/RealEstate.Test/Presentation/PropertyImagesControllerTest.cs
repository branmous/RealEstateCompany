using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealEstate.Domain.Exceptions;
using RealEstate.Domain.Interfaces.Services;
using RealEstate.Presentation.Controllers;
using RealEstate.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Test.Presentation
{
    internal class PropertyImagesControllerTest
    {
        private Mock<IPropertyService> _propertyService;
        private Mock<IPropertyImageService> _propertyImageService;
        private PropertyImagesController _controller;
        private Mock<HttpContext> _httpContextMock;

        [SetUp]
        public void Setup()
        {
            _httpContextMock = new Mock<HttpContext>();
            _httpContextMock.SetupGet(c => c.User.Identity!.Name).Returns("mock@mock.com");
            _propertyService = new Mock<IPropertyService>();
            _propertyImageService = new Mock<IPropertyImageService>();
            _controller = new PropertyImagesController(_propertyService.Object, _propertyImageService.Object);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = _httpContextMock.Object
            };

        }

        [Test]
        public async Task PostSetImagesAsync_Correctly()
        {
            int id = 1;
            var formFiles = new List<IFormFile>()
            {
                CreateMockFormFile("image1.jpg"),
                CreateMockFormFile("image2.jpg")
            };
            var property = PropertyMocks.GetEntity();

            _propertyService.Setup(p => p.FindByIdAsync(property.Id)).ReturnsAsync(property);
            _propertyImageService.Setup(p => p.SavePhotosAsync(property, It.IsAny<List<byte[]>>()));

            var result = await _controller.PostSetImagesAsync(id, formFiles) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task PostSetImagesAsync_NotFoundException()
        {
            int id = 1;
            var formFiles = new List<IFormFile>()
            {
                CreateMockFormFile("image1.jpg"),
                CreateMockFormFile("image2.jpg")
            };
            var property = PropertyMocks.GetEntity();

            _propertyService.Setup(p => p.FindByIdAsync(property.Id)).ThrowsAsync(new NotFoundException("error"));

            var result = await _controller.PostSetImagesAsync(id, formFiles) as NotFoundObjectResult;
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task PostSetImagesAsync_BadRequest()
        {
            int id = 1;
            var formFiles = new List<IFormFile>();

            var result = await _controller.PostSetImagesAsync(id, formFiles) as BadRequestObjectResult;
            
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task PostSetImagesAsync_InternalError()
        {
            int id = 1;
            var formFiles = new List<IFormFile>();
            _propertyService.Setup(p => p.FindByIdAsync(id)).ThrowsAsync(new Exception("error"));

            var result = await _controller.PostSetImagesAsync(id, formFiles) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(400));
        }

        private IFormFile CreateMockFormFile(string fileName)
        {
            var fileMock = new Mock<IFormFile>();
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write("Mock file content");
            writer.Flush();
            stream.Position = 0;

            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.Length).Returns(stream.Length);
            fileMock.Setup(f => f.OpenReadStream()).Returns(stream);

            return fileMock.Object;
        }
    }
}
