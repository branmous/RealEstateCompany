using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Repositories;
using System.Drawing;

namespace RealEstate.Test.Infrastructure
{
    [TestFixture]
    public class FileStorageTest
    {
        private IBlobContainerClientFactory _factory;
        private IConfiguration _configuration;
        private FileStorage? _fileStorage;

        [SetUp]
        public void SetUp()
        {
            _configuration = Substitute.For<IConfiguration>();
            _factory = Substitute.For<IBlobContainerClientFactory>();
            _configuration.GetConnectionString("AzureStorage").Returns("AzureStorageConnection");
            
        }

        [Test]
        public async Task RemoveFileAsync_Correctly()
        {
            string path = "path/to/file";
            string container = "container_name";
            var blobContainerClientMock = Substitute.For<BlobContainerClient>();
            var blobClientMock = Substitute.For<BlobClient>();
            blobContainerClientMock.GetBlobClient(Arg.Any<string>()).Returns(blobClientMock);
            _factory.CreateClient(Arg.Any<string>(), Arg.Any<string>()).Returns(blobContainerClientMock);
            _fileStorage = new FileStorage(_configuration, _factory);

            await _fileStorage.RemoveFileAsync(path, container);

            Assert.True(true);
        }

        [Test]
        public async Task SaveFileAsync_Correctly()
        {
            string url = "http://url/imagen.jpg";
            string container = "container_name";
            var image = new byte[] { 10, 20, 30, 40, 50 };

            var blobContainerClientMock = Substitute.For<BlobContainerClient>();
            var blobClientMock = Substitute.For<BlobClient>();
            blobContainerClientMock.GetBlobClient(Arg.Any<string>()).Returns(blobClientMock);
            blobClientMock.Uri.Returns(new Uri(url));
            _factory.CreateClient(Arg.Any<string>(), Arg.Any<string>()).Returns(blobContainerClientMock);
            _fileStorage = new FileStorage(_configuration, _factory);

            var result = await _fileStorage.SaveFileAsync(image, ".jpg", container);

            Assert.That(result, Is.EqualTo(url));
        }
    }
}
