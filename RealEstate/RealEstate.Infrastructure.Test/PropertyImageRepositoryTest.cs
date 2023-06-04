using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Data;
using RealEstate.Infrastructure.Repositories;

namespace RealEstate.Infrastructure.Test
{
    [TestFixture]
    public class PropertyImageRepositoryTest
    {
        private readonly DataContext _context;
        private PropertyImageRepository propertyImageRepository;

        public PropertyImageRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "testDatabase")
                .Options;

            _context = new DataContext(options);
        }

        [SetUp]
        public async Task SeptUp()
        {
            propertyImageRepository = new PropertyImageRepository(_context);
        }

        [Test]
        public async Task CreateList_Correctly()
        {
            List<PropertyImage> images = new List<PropertyImage>
            {
                new PropertyImage
                {
                    Enabled = true,
                    File = "url",
                    PropertyId = 1,
                }
            };

            await propertyImageRepository.AddRangeAsync(images);
            Assert.True(true);
        }

        [Test]
        public async Task GetList_Correctly()
        {
            List<PropertyImage> images = new List<PropertyImage>
            {
                new PropertyImage
                {
                    Enabled = true,
                    File = "url",
                    PropertyId = 1,
                }
            };

            await propertyImageRepository.AddRangeAsync(images);

            var result = await propertyImageRepository.All();

            Assert.NotNull(result);
        }

    }
}
