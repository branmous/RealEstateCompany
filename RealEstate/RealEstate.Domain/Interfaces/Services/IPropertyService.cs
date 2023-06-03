using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces.Services
{
    public interface IPropertyService
    {
        Task<Property> FindByIdAsync(int id);
        Task<List<Property>> GetAllAsync();
        Task SavePropertyAsync(Property property);
    }
}
