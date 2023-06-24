
namespace AWS.S3.Wrapper.Tests;
public class TestBase
{
    protected readonly IAmazonS3 client = new AmazonS3Client(Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID"), Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY"), Amazon.RegionEndpoint.USEast1);
    protected readonly IBucketOperations _bucketOperations;
    protected readonly IDirectoryOperations _directoryOperations;
    protected readonly IFileOperations _fileOperations;
    protected readonly IFileMetadataOperations _fileMetadataOperations;
    protected readonly string _bucketPrefix = "my-unit-test-bucket";
    protected readonly CancellationToken cancellationToken = default;
    public TestBase()
    {
        _bucketOperations = new BucketOperations(client);
        _directoryOperations = new DirectoryOperations(client);
        _fileOperations = new FileOperations(client);
        _fileMetadataOperations = new FileMetadataOperations(client);
    }
}
