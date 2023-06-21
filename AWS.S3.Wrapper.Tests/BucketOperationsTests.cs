using Xunit.Sdk;

namespace AWS.S3.Wrapper.Tests;
public class BucketOperationsTests : TestBase, IDisposable
{
    private readonly BucketOperations _bucketOperations;
    private readonly string _bucketPrefix = "my-unit-test-bucket";

    private readonly List<string> _createdBucketNames = new();

    public BucketOperationsTests()
    {
        _bucketOperations = new BucketOperations(client);
    }

    #region Sync Methods Tests
    [Fact]
    public void CreateBucket_ShouldCreateBucket()
    {
        // Arrange
        var bucketName = _bucketPrefix + "-" + Guid.NewGuid().ToString("N");

        // Act
        _bucketOperations.CreateBucket(bucketName);

        // Assert
        Assert.True(_bucketOperations.DoesBucketExist(bucketName));

        // for cleanup
        _createdBucketNames.Add(bucketName);
    }

    [Fact]
    public void CreateBucket_ShouldThrowExceptionOnFailure()
    {

        // Arrange
        var bucketName = _bucketPrefix + "-" + Guid.NewGuid().ToString("N");

        // Act
        _bucketOperations.CreateBucket(bucketName);

        // Assert
        Assert.Throws<Exception>(() => _bucketOperations.CreateBucket(bucketName));

        // for cleanup
        _createdBucketNames.Add(bucketName);

    }


    #endregion

    #region Async Methods Tests
    [Fact]
    public async Task CreateBucketAsync_ShouldCreateBucketAsync()
    {
        // Arrange
        var bucketName = _bucketPrefix + "-" + Guid.NewGuid().ToString("N");

        // Act
        await _bucketOperations.CreateBucketAsync(bucketName);

        // Assert
        Assert.True(_bucketOperations.DoesBucketExist(bucketName));

        // for cleanup
        _createdBucketNames.Add(bucketName);
    }

    [Fact]
    public async Task CreateBucketAsync_ShouldThrowExceptionOnFailureAsync()
    {

        // Arrange
        var bucketName = _bucketPrefix + "-" + Guid.NewGuid().ToString("N");

        // Act
        await _bucketOperations.CreateBucketAsync(bucketName);

        // Assert
        await Assert.ThrowsAsync<Exception>(async () => await _bucketOperations.CreateBucketAsync(bucketName));

        // for cleanup
        _createdBucketNames.Add(bucketName);

    }

    #endregion

    public void Dispose()
    {
        foreach (var bucketName in _createdBucketNames)
        {
            _bucketOperations.DeleteBucket(bucketName);
        }
    }

}