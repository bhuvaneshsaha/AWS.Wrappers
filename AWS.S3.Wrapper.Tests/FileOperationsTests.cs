using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.S3.Wrapper.Tests
{
    public class FileOperationsTests : TestBase, IDisposable
    {
        private readonly BucketOperations _bucketOperations;
        private readonly FileOperations _fileOperations;
        private readonly string _bucketPrefix = "my-unit-test-bucket";
        private readonly CancellationToken cancellationToken = default;
        private readonly List<string> _createdBucketNames = new();

        public FileOperationsTests()
        {
            _bucketOperations = new(client);
            _fileOperations = new(client);
        }

        [Fact]
        public async Task PutFileAsync_FileShouldUpload()
        {
            // arrange
            var bucketName = $"{_bucketPrefix}-{Guid.NewGuid()}";
            await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);

            var objectKey = "my-object";
            var content = new MemoryStream(Encoding.UTF8.GetBytes("Hello World!"));
            var contentType = "text/plain";

            // act
            await _fileOperations.PutFileAsync(bucketName, objectKey, content, contentType, cancellationToken);

            // assert
            var actualContent = await _fileOperations.GetFileAsync(bucketName, objectKey, cancellationToken);
                        
            _createdBucketNames.Add(bucketName);

        }
        public void Dispose()
        {
            foreach (var bucketName in _createdBucketNames)
            {
                _bucketOperations.EmptyBucketAsync(bucketName, cancellationToken).Wait();
                _bucketOperations.DeleteBucketAsync(bucketName, cancellationToken).Wait();
            }
        }
    }
}