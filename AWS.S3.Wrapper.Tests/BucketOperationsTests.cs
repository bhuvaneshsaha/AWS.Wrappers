namespace AWS.S3.Wrapper.Tests;
public class BucketOperationsTests
{
    private readonly Mock<IAmazonS3> _amazonS3Mock;
    private readonly BucketOperations _bucketOperations;

    public BucketOperationsTests()
    {
        _amazonS3Mock = new Mock<IAmazonS3>();
        _bucketOperations = new BucketOperations(_amazonS3Mock.Object);
    }

    // [Fact]
    // public void CreateBucket_CallsPutBucketAsync_WithBucketName()
    // {
    //     // Arrange
    //     var bucketName = "my-bucket";

    //     // Act
    //     _bucketOperations.CreateBucket(bucketName);

    //     // Assert
    //     _amazonS3Mock.Verify(x => x.PutBucketAsync(
    //         It.Is<PutBucketRequest>(y => y.BucketName == bucketName),
    //         It.IsAny<CancellationToken>()), Times.Once);
    // }

    [Fact]
    public void CreateBucket_ShouldCallPutBucketAsyncWithCorrectRequest()
    {
        // Arrange
        var bucketName = "my-bucket";
        var putBucketRequest = It.Is<PutBucketRequest>(r => r.BucketName == bucketName && r.UseClientRegion);

        // Act
        _bucketOperations.CreateBucket(bucketName);

        // Assert
        _amazonS3Mock.Verify(x => x.PutBucketAsync(
            It.Is<PutBucketRequest>(y => y.BucketName == bucketName),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateBucketAsync_ShouldCallPutBucketAsyncWithCorrectRequest()
    {
        // Arrange
        var bucketName = "my-bucket";
        var putBucketRequest = It.Is<PutBucketRequest>(r => r.BucketName == bucketName && r.UseClientRegion);
        var cancellationToken = new CancellationToken();

        // Act
        await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);

        // Assert
        _amazonS3Mock.Verify(x => x.PutBucketAsync(
            It.Is<PutBucketRequest>(y => y.BucketName == bucketName),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public void CreateBucket_ShouldThrowExceptionOnFailure()
    {
        // Arrange
        var bucketName = "my-bucket";
        var putBucketRequest = It.Is<PutBucketRequest>(r => r.BucketName == bucketName && r.UseClientRegion);

        _amazonS3Mock.Setup(x => x.PutBucketAsync(putBucketRequest, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new AmazonS3Exception("Error creating bucket", new Exception()));

        // Act and Assert
        Assert.Throws<AmazonS3Exception>(() => _bucketOperations.CreateBucket(bucketName));
    }


}

