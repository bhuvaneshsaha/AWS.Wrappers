
namespace AWS.S3.Wrapper.Tests;

[Collection("Our Test Collection #1")]
public class FileOperationsTests : TestBase
{
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
        await _fileOperations.UploadFileStreamAsync(bucketName, objectKey, content, contentType, cancellationToken);

        // assert
        var actualContent = await _fileOperations.GetFileAsync(bucketName, objectKey, cancellationToken);

        Assert.NotNull(actualContent);

        _createdBucketNames.Add(bucketName);
    }

    [Fact]
    public async Task CopyFileAsync_FileShouldCopy()
    {
        // arrange
        var sourceBucketName = $"{_bucketPrefix}-{Guid.NewGuid()}";
        await _bucketOperations.CreateBucketAsync(sourceBucketName, cancellationToken);

        var destinationBucketName = $"{_bucketPrefix}-{Guid.NewGuid()}";
        await _bucketOperations.CreateBucketAsync(destinationBucketName, cancellationToken);

        var objectKey = "my-object";
        var content = new MemoryStream(Encoding.UTF8.GetBytes("Hello World!"));
        var contentType = "text/plain";

        await _fileOperations.UploadFileStreamAsync(sourceBucketName, objectKey, content, contentType, cancellationToken);

        // act
        await _fileOperations.CopyFileAsync(sourceBucketName, objectKey, destinationBucketName, objectKey, cancellationToken);

        // assert
        var actualContent = await _fileOperations.GetFileAsync(destinationBucketName, objectKey, cancellationToken);

        Assert.NotNull(actualContent);

        _createdBucketNames.Add(sourceBucketName);
        _createdBucketNames.Add(destinationBucketName);
    }

    [Fact]
    public async Task DeleteFileAsync_FileShouldDelete()
    {
        // arrange
        var bucketName = $"{_bucketPrefix}-{Guid.NewGuid()}";
        await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);

        var objectKey = "my-object";
        var content = new MemoryStream(Encoding.UTF8.GetBytes("Hello World!"));
        var contentType = "text/plain";

        await _fileOperations.UploadFileStreamAsync(bucketName, objectKey, content, contentType, cancellationToken);

        // act
        await _fileOperations.DeleteFileAsync(bucketName, objectKey, cancellationToken);

        // assert
        var actualContent = await _fileOperations.DoseFileExistAsync(bucketName, objectKey, cancellationToken);

        Assert.False(actualContent);

        _createdBucketNames.Add(bucketName);
    }

    [Fact]
    public async Task ListObjectsAsync()
    {
        // arrange
        var bucketName = $"{_bucketPrefix}-{Guid.NewGuid()}";
        await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);

        var objectKey = "my-object";
        var content = new MemoryStream(Encoding.UTF8.GetBytes("Hello World!"));
        var contentType = "text/plain";

        await _fileOperations.UploadFileStreamAsync(bucketName, objectKey, content, contentType, cancellationToken);

        // act
        var actualContent = await _fileOperations.ListObjectsAsync(bucketName, "", "", 1000, cancellationToken);

        // assert
        Assert.NotNull(actualContent);
        Assert.Single(actualContent);

        _createdBucketNames.Add(bucketName);
    }

    [Fact]
    public async Task UploadSingleFileAsync_ShouldUploadFile()
    {
        // arrange
        var bucketName = $"{_bucketPrefix}-{Guid.NewGuid()}";
        await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);

        var fileName = "my-file.txt";
        var localPath = Path.Combine(Directory.GetCurrentDirectory());

        var fullPath = Path.Combine(localPath, fileName);

        // create text file and store it in the local path
        // create if not exists
        if (!File.Exists(fullPath))
        {
            await File.WriteAllTextAsync(fullPath, "Hello World!");
        }

        // act
        var actualContent = await _fileOperations.UploadSingleFileAsync(bucketName, sourceFileFullPath: fullPath, destKey: fileName, cancellationToken);

        // assert
        Assert.True(actualContent);

        // remove the file from the local path
        File.Delete(fullPath);

        _createdBucketNames.Add(bucketName);
    }

    [Fact]
    public async Task UploadSingleFileAsync_ShouldReturnFalse()
    {
        // arrange
        var bucketName = $"{_bucketPrefix}-{Guid.NewGuid()}";
        await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);

        var fileName = "my-file-not-exists.txt";
        var localPath = Path.Combine(Directory.GetCurrentDirectory());

        var fullPath = Path.Combine(localPath, fileName);

        // act
        var actualContent = await _fileOperations.UploadSingleFileAsync(bucketName, sourceFileFullPath: fullPath, destKey: fileName, cancellationToken);

        // assert
        Assert.False(actualContent);

        _createdBucketNames.Add(bucketName);
    }
}