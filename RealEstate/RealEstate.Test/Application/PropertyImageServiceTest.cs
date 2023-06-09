using NSubstitute;
using RealEstate.Application.Properties;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Test.Application
{
    [TestFixture]
    public class PropertyImageServiceTest
    {
        private IPropertyImageRepository _propertyImageRepository;
        private IFileStorage _fileStorage;
        private PropertyImageService _propertyImageService;

        [SetUp]
        public void SetUp()
        {
            _propertyImageRepository = Substitute.For<IPropertyImageRepository>();
            _fileStorage = Substitute.For<IFileStorage>();
            _propertyImageService = new PropertyImageService(_propertyImageRepository, _fileStorage);

        }

        [Test]
        public async Task SavePhotosAsync()
        {
            var property = Mocks.PropertyMocks.GetEntity();
            var image = new byte[] { 10, 20, 30, 40, 50 };
            var images = new List<byte[]>()
            {
                image
            };
            string url = "http:url.com";

            _fileStorage.SaveFileAsync(Arg.Any<byte[]>(), Arg.Any<string>(), Arg.Any<string>()).Returns(url);
            _propertyImageRepository.AddRangeAsync(Arg.Any<List<PropertyImage>>()).Returns(Task.CompletedTask);
            await _propertyImageService.SavePhotosAsync(property, images);
            Assert.True(true);
        }

    }
}
