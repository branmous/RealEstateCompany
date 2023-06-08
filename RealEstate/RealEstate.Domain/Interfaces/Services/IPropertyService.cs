using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces.Services
{
    public interface IPropertyService
    {
        Task<Property> FindByIdAsync(int id);
        Task<List<Property>> GetAllAsync();
        Task<List<Property>> GetAllWithPaginateAsync(string ownerId, int page, int recordsNumber, string filter = null!);
        Task<Property> SavePropertyAsync(Property property);
        Task<Property> UpdateAsync(Property property);
        Task UpdatePriceAsync(int id, decimal price);
    }
}
