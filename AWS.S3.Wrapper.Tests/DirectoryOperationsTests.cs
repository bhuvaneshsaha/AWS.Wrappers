
namespace AWS.S3.Wrapper.Tests;

public class DirectoryOperationsTests : TestBase, IDisposable
{
    private readonly BucketOperations _bucketOperations;
    private readonly DirectoryOperations _directoryOperations;
    private readonly string _bucketPrefix = "unit-test-directory";
    private readonly CancellationToken cancellationToken = default;

    private readonly List<string> _createdBucketNames = new();

    public DirectoryOperationsTests()
    {
        _bucketOperations = new(client);
        _directoryOperations = new(client);
    }

    #region async methods
    [Fact]
    public async Task CreateDirectoryAsync_ShouldCreateDirectory()
    {
        // arrange
        var bucketName = $"{_bucketPrefix}-{Guid.NewGuid()}";
        await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);

        // act
        await _directoryOperations.CreateDirectoryAsync(bucketName, "my-directory", cancellationToken);

        // assert
        var isDirectoryExists = await _directoryOperations.DirectoryExistsAsync(bucketName, "my-directory", cancellationToken);

        Assert.True(isDirectoryExists);

        _createdBucketNames.Add(bucketName);
    }
    #endregion

    public void Dispose()
    {
        foreach (var bucketName in _createdBucketNames)
        {
            _bucketOperations.EmptyBucketAsync(bucketName, cancellationToken).Wait();
            _bucketOperations.DeleteBucketAsync(bucketName, cancellationToken).Wait();
        }
    }
}
