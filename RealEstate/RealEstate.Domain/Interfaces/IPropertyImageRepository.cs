using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces
{
    public interface IPropertyImageRepository : IBaseRepository<PropertyImage>
    {
        Task AddRangeAsync(List<PropertyImage> images);
    }
}
