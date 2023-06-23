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
    public void Dispose()
    {
        foreach (var bucketName in _createdBucketNames)
        {
            _bucketOperations.EmptyBucketAsync(bucketName, cancellationToken).Wait();
            _bucketOperations.DeleteBucketAsync(bucketName, cancellationToken).Wait();
        }
    }
}