
namespace AWS.S3.Wrapper.Tests;
public class TestBase : IDisposable
{
    protected readonly IAmazonS3 client = new AmazonS3Client(Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID"), Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY"), Amazon.RegionEndpoint.USEast1);
    protected readonly IBucketOperations _bucketOperations;
    protected readonly IDirectoryOperations _directoryOperations;
    protected readonly IFileOperations _fileOperations;
    protected readonly IFileMetadataOperations _fileMetadataOperations;
    protected readonly ILargeFileOperations _largeFileOperations;
    protected readonly IPresignedUrlOperations _presignedUrlOperations;
    protected readonly string _bucketPrefix = "my-unit-test-bucket";
    protected readonly CancellationToken cancellationToken = default;

    protected readonly List<string> _createdBucketNames = [];
    private bool _disposedValue;
    
    public TestBase()
    {
        _bucketOperations = new BucketOperations(client);
        _directoryOperations = new DirectoryOperations(client);
        _fileOperations = new FileOperations(client);
        _fileMetadataOperations = new FileMetadataOperations(client);
        _largeFileOperations = new LargeFileOperations(client);
        _presignedUrlOperations = new PresignedUrlOperations(client);
    }

    // List and delete all buckets with the prefix my-unit-test-bucket
#pragma warning disable xUnit1004 // Test methods should not be skipped
    [Fact(Skip = "For cleanup only")]
#pragma warning restore xUnit1004 // Test methods should not be skipped
    protected async Task DeleteAllBucketsAsync()
    {
        var buckets = await _bucketOperations.ListBucketsAsync(cancellationToken);
        foreach (var bucket in buckets)
        {
            if (bucket.StartsWith(_bucketPrefix))
            {
                await _bucketOperations.EmptyBucketAsync(bucket, cancellationToken);
                await _bucketOperations.DeleteBucketAsync(bucket, cancellationToken);
            }
        }

        Assert.True(true);
    }

    protected virtual async Task Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                foreach (var bucketName in _createdBucketNames)
                {
                    await _bucketOperations.EmptyBucketAsync(bucketName, cancellationToken);
                    await _bucketOperations.DeleteBucketAsync(bucketName, cancellationToken);
                }
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true).Wait();
        GC.SuppressFinalize(this);
    }
}
