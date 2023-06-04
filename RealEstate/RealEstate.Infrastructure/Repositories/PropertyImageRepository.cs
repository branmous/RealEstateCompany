using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyImageRepository : BaseRepository<PropertyImage>, IPropertyImageRepository
    {
        public PropertyImageRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public async Task AddRangeAsync(List<PropertyImage> images)
        {
            Context.AddRange(images);
            await Context.SaveChangesAsync();
        }
    }
}
