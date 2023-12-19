
using AWS.S3.Wrapper.Tests.Utilities;

namespace AWS.S3.Wrapper.Tests;

[Collection("Our Test Collection #1")]
public class LargeFileOperationsTests : TestBase, IDisposable
{
    private readonly List<string> _createdBucketNames = [];

    [Fact]
    public async Task UploadFullDirectoryAsync_ShouldUploadAllLocalFiles()
    {
        // arrange
        var bucketName = $"{_bucketPrefix}-{Guid.NewGuid()}";
        await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);

        var fileNames = new List<string> { "my-file1.txt", "my-file2.txt", "my-file3.txt" };
        var localPath = FileUtility.GetLocalTestFilesPathWithFolder("UploadFullDirectoryAsync");

        await FileUtility.CreateFileInLocal(fileNames, localPath);

        // act
        await _largeFileOperations.UploadFullDirectoryAsync(bucketName, "upload", localPath, cancellationToken);

        // assert
        foreach (var fileName in fileNames)
        {
            var objectKey = $"upload/{fileName}";
            var fileExists = await _fileOperations.DoseFileExistAsync(bucketName, objectKey, cancellationToken);
            Assert.True(fileExists);
        }

        // cleanup
        FileUtility.DeleteFilesInDirectory(localPath);

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
