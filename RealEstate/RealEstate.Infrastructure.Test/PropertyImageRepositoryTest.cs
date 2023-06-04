using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Data;
using RealEstate.Infrastructure.Repositories;

namespace RealEstate.Infrastructure.Test
{
    [TestFixture]
    public class PropertyImageRepositoryTest
    {
        private DataContext _context;
        private PropertyImageRepository _propertyImageRepository;


        [SetUp]
        public void SeptUp()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "testDatabase")
                .Options;

            _context = new DataContext(options);
            _propertyImageRepository = new PropertyImageRepository(_context);
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

            await _propertyImageRepository.AddRangeAsync(images);
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

            await _propertyImageRepository.AddRangeAsync(images);

            var result = await _propertyImageRepository.All();

            Assert.NotNull(result);
        }

    }
}
