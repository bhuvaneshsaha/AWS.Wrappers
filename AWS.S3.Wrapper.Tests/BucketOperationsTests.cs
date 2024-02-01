using Xunit.Sdk;

namespace AWS.S3.Wrapper.Tests;

[Collection("Our Test Collection #1")]
public class BucketOperationsTests : TestBase
{
    [Fact]
    public async Task CreateBucketAsync_ShouldCreateBucketAsync()
    {
        // Arrange
        var bucketName = _bucketPrefix + "-" + Guid.NewGuid().ToString("N");

        // Act
        await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);

        // Assert
        Assert.True(await _bucketOperations.DoesBucketExistAsync(bucketName, cancellationToken));

        // for cleanup
        _createdBucketNames.Add(bucketName);
    }

    [Fact]
    public async Task CreateBucketAsync_ShouldThrowExceptionOnFailureAsync()
    {
        // Arrange
        var bucketName = _bucketPrefix + "-" + Guid.NewGuid().ToString("N");

        // Act
        await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken));

        // for cleanup
        _createdBucketNames.Add(bucketName);
    }

    [Fact]
    public async Task ListBucketsAsync_ShouldListBucketAsync()
    {
        // Arrange
        var bucketName = _bucketPrefix + "-" + Guid.NewGuid().ToString("N");
        await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);

        // Act
        var buckets = await _bucketOperations.ListBucketsAsync(cancellationToken);

        // Assert
        Assert.Contains(buckets, x => x == bucketName);

        // for cleanup
        _createdBucketNames.Add(bucketName);
    }

    [Fact]
    public async Task DeleteBucketAsync_ShouldDeleteBucketAsync()
    {
        // Arrange
        var bucketName = _bucketPrefix + "-" + Guid.NewGuid().ToString("N");
        await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);

        // Act
        await _bucketOperations.DeleteBucketAsync(bucketName, cancellationToken);

        // Assert
        Assert.False(await _bucketOperations.DoesBucketExistAsync(bucketName, cancellationToken));
    }
}
