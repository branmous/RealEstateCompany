using Azure.Storage.Blobs;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Infrastructure.Repositories
{
    public class BlobContainerClientFactory : IBlobContainerClientFactory
    {
        public BlobContainerClient CreateClient(string connectionString, string containerName)
        {
            return new BlobContainerClient(connectionString, containerName);
        }
    }
}
