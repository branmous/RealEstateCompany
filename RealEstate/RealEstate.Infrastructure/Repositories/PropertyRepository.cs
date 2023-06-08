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

        public async Task<List<Property>> GetAllWithPaginateAsync(string ownerId, int page, int recordsNumber, string filter = null!)
        {
            var query = Context.Properties.Where(q => q.OwnerId == ownerId).AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(q => q.OwnerId == ownerId
                && (q.Name.Contains(filter)
                || q.Address.Contains(filter)
                || q.CodeInternal.Contains(filter)));
            }

            return await query.OrderByDescending(p => p.Id).Paginate(page, recordsNumber).ToListAsync();
        }
    }
}
