using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces.Services
{
    public interface IPropertyService
    {
        Task<Property> FindByIdAsync(int id);
        Task<List<Property>> GetAllAsync();
        Task<List<Property>> GetAllWithPaginateAsync(int page, int recordsNumber);
        Task<Property> SavePropertyAsync(Property property);
        Task<Property> UpdateAsync(Property property);
        Task UpdatePriceAsync(int id, decimal price);
    }
}
