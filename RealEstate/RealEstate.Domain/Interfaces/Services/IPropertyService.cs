using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces.Services
{
    public interface IPropertyService
    {
        Task<List<Property>> GetAll();
        Task SaveProperty(Property property);
    }
}
