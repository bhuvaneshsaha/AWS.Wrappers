
namespace AWS.S3.Wrapper.Tests;

public class PresignedUrlOperationsTests : TestBase, IDisposable
{
    private readonly List<string> _createdBucketNames = new();

    [Fact]
    public async Task GeneratePresignedUrlForGetAsync_ShouldGetPresignedURLOfUpload()
    {
        var bucketName = $"{_bucketPrefix}-{Guid.NewGuid()}";
        await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);

        var objectKey = "my-object";
        var content = new MemoryStream(Encoding.UTF8.GetBytes("Hello World!"));
        var contentType = "text/plain";

        // act
        await _fileOperations.UploadFileStreamAsync(bucketName, objectKey, content, contentType, cancellationToken);

        // assert
        var expires = DateTime.Now.AddMinutes(5);

        var preSignedGetURL = await _presignedUrlOperations.GeneratePresignedUrlForGetAsync(bucketName, objectKey, expires, cancellationToken);

        Assert.NotEmpty(preSignedGetURL);
    }

    [Fact]
    public async Task GeneratePresignedUrlForPutAsyncTest()
    {
        var bucketName = $"{_bucketPrefix}-{Guid.NewGuid()}";
        await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);

        var objectKey = "my-object";
        var content = new MemoryStream(Encoding.UTF8.GetBytes("Hello World!"));
        var contentType = "text/plain";

        // act
        await _fileOperations.UploadFileStreamAsync(bucketName, objectKey, content, contentType, cancellationToken);
        var expires = DateTime.Now.AddMinutes(5);

        var preSignedPutURL = await _presignedUrlOperations.GeneratePresignedUrlForPutAsync(bucketName, objectKey, expires, contentType, cancellationToken);

        Assert.NotEmpty(preSignedPutURL);
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
