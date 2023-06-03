using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces
{
    public interface IPropertyRepository : IBaseRepository<Property>
    {
        Task<Property> FindByIdAsync(int id);
    }
}
