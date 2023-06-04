using RealEstate.Domain.Entities;
using RealEstate.Domain.Exceptions;
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
            var property = await _propertyRepository.FindByIdAsync(id);
            if (property == null)
            {
                throw new NotFoundException("Property not found!!!");
            }

            return property;
        }

        public async Task<List<Property>> GetAllAsync()
        {
            return await _propertyRepository.All();
        }

        public async Task<Property> SavePropertyAsync(Property property)
        {
            return await _propertyRepository.AddAsync(property!);
        }

        public async Task<Property> UpdateAsync(Property property)
        {
            var prop = await _propertyRepository.FindByIdAsync(property.Id);
            if (prop == null)
            {
                throw new NotFoundException("Property not found!!!");
            }

            prop.Address = property.Address;
            prop.Price = property.Price;
            prop.CodeInternal = property.CodeInternal;
            prop.Year = property.Year;

            return await _propertyRepository.UpdateAsync(prop);
        }
    }
}
