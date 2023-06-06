using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Specifications;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public async Task<Property> FindByIdAsync(int id)
        {
            var property = await Context.Properties.FirstOrDefaultAsync(p => p!.Id == id);
            return property!;
        }

        public async Task<List<Property>> GetAllWithPaginateAsync(int page, int recordsNumber)
        {
            var query = Context.Properties.AsQueryable();

            return await query.OrderByDescending(p => p.Id).Paginate(page, recordsNumber).ToListAsync();
        }
    }
}
