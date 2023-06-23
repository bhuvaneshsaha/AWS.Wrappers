using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.S3.Wrapper.Tests;

[Collection("Our Test Collection #1")]
public class FileOperationsTests : TestBase, IDisposable
{
    private readonly List<string> _createdBucketNames = new();

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

        await _fileOperations.PutFileAsync(sourceBucketName, objectKey, content, contentType, cancellationToken);

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

        await _fileOperations.PutFileAsync(bucketName, objectKey, content, contentType, cancellationToken);

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

        await _fileOperations.PutFileAsync(bucketName, objectKey, content, contentType, cancellationToken);

        // act
        var actualContent = await _fileOperations.ListObjectsAsync(bucketName, "", "", 1000, cancellationToken);

        // assert
        Assert.NotNull(actualContent);
        Assert.Single(actualContent);

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