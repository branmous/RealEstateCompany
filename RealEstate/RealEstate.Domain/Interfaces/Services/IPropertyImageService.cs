using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces.Services
{
    public interface IPropertyImageService
    {
        Task SavePhotosAsync(Property property, List<byte[]> images);
    }
}
