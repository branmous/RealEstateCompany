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

        public async Task<Property> FindByIdAsync(int id)
        {
            return await _propertyRepository.FindByIdAsync(id);
        }

        public async Task<List<Property>> GetAllAsync()
        {
            return await _propertyRepository.All();
        }

        public async Task SavePropertyAsync(Property property)
        {
            await _propertyRepository.AddAsync(property!);
        }

        public async Task<Property> UpdateAsync(Property property)
        {
            var prop = await _propertyRepository.FindByIdAsync(property.Id);
            if (prop == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            await _propertyRepository.UpdateAsync(property);
        }
    }
}
