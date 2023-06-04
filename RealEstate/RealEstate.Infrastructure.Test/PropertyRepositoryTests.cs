using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Data;
using RealEstate.Infrastructure.Repositories;
using RealEstate.Infrastructure.Test.Mocks;

namespace RealEstate.Infrastructure.Test
{
    [TestFixture]
    public class PropertyRepositoryTests
    {
        private readonly DataContext _context;

        public PropertyRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "testDatabase")
                .EnableSensitiveDataLogging(true)
                .Options;
            
            _context = new DataContext(options);
        }

        [SetUp]
        public async Task SeptUp()
        {
            _context.Properties.AddRange(PropertyMocks.GetList());
            await _context.SaveChangesAsync();
        }

        [Test]
        public async Task FindByIdAsync_ShouldReturnProperty()
        {
            // Arrange
            int propertyId = 1;
            var propertyRepository = new PropertyRepository(_context);
            // Act
            var property = await propertyRepository.FindByIdAsync(propertyId);

            // Asserts
            Assert.NotNull(property);
            Assert.That(property!.Id, Is.EqualTo(propertyId));
            Assert.That(property.Name, Is.EqualTo("My House"));
        }

        [Test]
        public async Task Save_Correctly()
        {
            var property = new Property
            {
                Name = "My House 3",
                Address = "Florida",
                CodeInternal = "12345",
                Price = 2000,
                OwnerId = "211bd761-d46c-41b7-9c7f-301fb8239b73"
            };
            var propertyRepository = new PropertyRepository(_context);
            var result = await propertyRepository.AddAsync(property!);
            Assert.NotNull(result);
            Assert.That(property.Name, Is.EqualTo(result.Name));
        }
    }
}
