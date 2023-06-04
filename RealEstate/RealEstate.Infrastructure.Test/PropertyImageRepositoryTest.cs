using Microsoft.EntityFrameworkCore;
using RealEstate.Infrastructure.Data;
using RealEstate.Infrastructure.Repositories;
using RealEstate.Infrastructure.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        
    }
}
