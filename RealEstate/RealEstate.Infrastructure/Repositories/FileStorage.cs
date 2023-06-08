using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Infrastructure.Repositories
{
    public class FileStorage : IFileStorage
    {
        private readonly string _connectionString;
        private readonly IBlobContainerClientFactory _blobContainerClientFactory;

        public FileStorage(IConfiguration configuration, IBlobContainerClientFactory blobContainerClientFactory)
        {
            _connectionString = configuration.GetConnectionString("AzureStorage")!;
            _blobContainerClientFactory = blobContainerClientFactory;
        }
        public async Task RemoveFileAsync(string path, string containerName)
        {
            var client = _blobContainerClientFactory.CreateClient(_connectionString, containerName);
            await client.CreateIfNotExistsAsync();
            var fileName = Path.GetFileName(path);
            var blob = client.GetBlobClient(fileName);
            await blob.DeleteIfExistsAsync();

        }

        public async Task<string> SaveFileAsync(byte[] content, string extention, string containerName)
        {
            var client = _blobContainerClientFactory.CreateClient(_connectionString, containerName);
            await client.CreateIfNotExistsAsync();
            client.SetAccessPolicy(PublicAccessType.Blob);
            var fileName = $"{Guid.NewGuid()}{extention}";
            var blob = client.GetBlobClient(fileName);

            using (var ms = new MemoryStream(content))
            {
                await blob.UploadAsync(ms);
            }

            return blob.Uri.ToString();
        }

    }
}
