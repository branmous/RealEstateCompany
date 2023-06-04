using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Interfaces.Services;

namespace RealEstate.Application.Properties
{
    public class PropertyImageService : IPropertyImageService
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IFileStorage _fileStorage;
        private readonly string _propertyContainer;

        public PropertyImageService(IPropertyImageRepository propertyImageRepository,
            IFileStorage fileStorage)
        {
            _propertyImageRepository = propertyImageRepository;
            _fileStorage = fileStorage;
            _propertyContainer = "properties";
        }

        public async Task SavePhotos(Property property, List<byte[]> images)
        {
            List<PropertyImage> propertyImages = new();
            foreach (var image in images)
            {
                var url = await _fileStorage.SaveFileAsync(image, ".jpg", _propertyContainer);
                propertyImages.Add(new PropertyImage
                {
                    Enabled = true,
                    File = url,
                    PropertyId = property.Id,
                });
            }

            await _propertyImageRepository.AddRangeAsync(propertyImages);
        }
    }
}
