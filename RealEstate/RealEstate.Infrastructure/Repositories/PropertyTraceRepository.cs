using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyTraceRepository : BaseRepository<PropertyTrace>, IPropertyTraceRepository
    {
        public PropertyTraceRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
