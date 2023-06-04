using Moq;
using RealEstate.Application.Properties;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Exceptions;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Test.Application
{
    [TestFixture]
    internal class PropertyServiceTest
    {
        private Mock<IPropertyRepository> _propertyRepository;
        private PropertyService? _propertyService;

        [SetUp]
        public void Setup()
        {
            _propertyRepository = new Mock<IPropertyRepository>();
        }

        [Test]
        public async Task FindByIdAsync_Correctly()
        {
            // Arrange
            int propertyId = 1;
            var property = new Property
            {
                Id = 1,
                Name = "My House",
                Address = "Florida",
                CodeInternal = "12345",
                Price = 2000,
                OwnerId = "211bd761-d46c-41b7-9c7f-301fb8239b73"
            };

            _propertyRepository.Setup(p => p.FindByIdAsync(propertyId)).ReturnsAsync(property);
            _propertyService = new PropertyService(_propertyRepository.Object);
            var result = await _propertyService.FindByIdAsync(propertyId);

            Assert.NotNull(result);
            Assert.That(property!.Id, Is.EqualTo(result.Id));
            Assert.That(property.Name, Is.EqualTo(result.Name));
        }

        [Test]
        public void FindByIdAsync_NotFoundException()
        {
            // Arrange
            int propertyId = 1;

            _propertyRepository.Setup(p => p.FindByIdAsync(propertyId));
            _propertyService = new PropertyService(_propertyRepository.Object);

            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await _propertyService.FindByIdAsync(propertyId));
            Assert.That(exception.Message, Is.EqualTo("Property not found!!!"));
        }

        [Test]
        public async Task GetAllAsync_Correctly()
        {
            // Arrange
            var property = new Property
            {
                Id = 1,
                Name = "My House",
                Address = "Florida",
                CodeInternal = "12345",
                Price = 2000,
                OwnerId = "211bd761-d46c-41b7-9c7f-301fb8239b73"
            };
            var list = new List<Property> { property };

            _propertyRepository.Setup(p => p.All()).ReturnsAsync(list);
            _propertyService = new PropertyService(_propertyRepository.Object);
            var result = await _propertyService.GetAllAsync();

            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(list.Count));
        }

        [Test]
        public async Task SavePropertyAsync_Correctly()
        {
            // Arrange
            var property = new Property
            {
                Id = 1,
                Name = "My House",
                Address = "Florida",
                CodeInternal = "12345",
                Price = 2000,
                OwnerId = "211bd761-d46c-41b7-9c7f-301fb8239b73"
            };

            _propertyRepository.Setup(p => p.AddAsync(It.IsAny<Property>())).ReturnsAsync(property);
            _propertyService = new PropertyService(_propertyRepository.Object);
            var result = await _propertyService.SavePropertyAsync(property);

            Assert.NotNull(result);
            Assert.That(property!.Id, Is.EqualTo(result.Id));
            Assert.That(property.Name, Is.EqualTo(result.Name));
        }

        [Test]
        public async Task UpdateAsync_Correctly()
        {
            // Arrange
            var propOld = new Property
            {
                Id = 1,
                Name = "My House",
                Address = "Florida",
                CodeInternal = "12345",
                Price = 2000,
                OwnerId = "211bd761-d46c-41b7-9c7f-301fb8239b73"
            };

            var proNew = new Property
            {
                Id = 1,
                Name = "My House 3",
                Address = "Miami",
                CodeInternal = "12345",
                Price = 2000,
                OwnerId = "211bd761-d46c-41b7-9c7f-301fb8239b73"
            };

            _propertyRepository.Setup(p => p.FindByIdAsync(propOld.Id)).ReturnsAsync(propOld);
            _propertyRepository.Setup(p => p.UpdateAsync(It.IsAny<Property>())).ReturnsAsync(proNew);
            _propertyService = new PropertyService(_propertyRepository.Object);
            var result = await _propertyService.UpdateAsync(proNew);

            Assert.NotNull(result);
            Assert.That(propOld.Id, Is.EqualTo(result.Id));
            Assert.That(propOld.Price, Is.EqualTo(result.Price));
            Assert.That(result.Name, Is.Not.EqualTo(propOld.Name));
        }

        [Test]
        public void UpdateAsync_NotFoundException()
        {
            // Arrange
            var propNew = new Property
            {
                Id = 1,
                Name = "My House",
                Address = "Florida",
                CodeInternal = "12345",
                Price = 2000,
                OwnerId = "211bd761-d46c-41b7-9c7f-301fb8239b73"
            };

            _propertyRepository.Setup(p => p.FindByIdAsync(propNew.Id));
            _propertyService = new PropertyService(_propertyRepository.Object);
            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await _propertyService.UpdateAsync(propNew));
            Assert.That(exception.Message, Is.EqualTo("Property not found!!!"));
        }
    }
}
