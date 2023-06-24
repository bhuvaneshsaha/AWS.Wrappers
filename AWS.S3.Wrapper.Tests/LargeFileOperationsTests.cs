using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.S3.Wrapper.Tests
{
    public class LargeFileOperationsTests : TestBase, IDisposable
    {
        private readonly List<string> _createdBucketNames = new();

        [Fact]
        public async Task UploadFullDirectoryAsync_ShouldUploadAllLocalFiles()
        {
            // arrange
            var bucketName = $"{_bucketPrefix}-{Guid.NewGuid()}";
            await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);

            var fileNames = new List<string> { "my-file1.txt", "my-file2.txt", "my-file3.txt" };
            var localPath = Directory.GetCurrentDirectory();
            var fullPathList = new List<string>();

            foreach (var fileName in fileNames)
            {
                var fullPath = Path.Combine(localPath, fileName);
                fullPathList.Add(fullPath);

                if (!File.Exists(fullPath))
                {
                    await File.WriteAllTextAsync(fullPath, $"Hello World {fileNames.IndexOf(fileName) + 1}!");
                }
            }

            // act
            var actualContent = await _largeFileOperations.UploadFullDirectoryAsync(bucketName, "upload", localPath, cancellationToken);

            // assert
            Assert.True(actualContent);

            // cleanup
            foreach (var fullPath in fullPathList)
            {
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }

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
