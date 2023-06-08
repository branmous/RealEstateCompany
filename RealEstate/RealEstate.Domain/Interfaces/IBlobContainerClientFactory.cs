using Azure.Storage.Blobs;

namespace RealEstate.Domain.Interfaces
{
    public interface IBlobContainerClientFactory
    {
        BlobContainerClient CreateClient(string connectionString, string containerName);
    }
}
