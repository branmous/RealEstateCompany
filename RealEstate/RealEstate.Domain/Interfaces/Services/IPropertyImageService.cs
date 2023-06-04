using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces.Services
{
    public interface IPropertyImageService
    {
        Task SavePhotos(Property property, List<byte[]> images);
    }
}
