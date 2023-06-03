using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Interfaces.Services;

namespace RealEstate.Application.Properties
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<List<Property>> GetAll()
        {
            return await _propertyRepository.All();
        }

        public async Task SaveProperty(Property property)
        {
            await _propertyRepository.AddAsync(property!);
        }


    }
}
