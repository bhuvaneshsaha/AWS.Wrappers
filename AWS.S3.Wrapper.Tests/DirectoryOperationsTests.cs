
namespace AWS.S3.Wrapper.Tests;


public class DirectoryOperationsTests : TestBase, IDisposable
{
    private readonly BucketOperations _bucketOperations;
    private readonly DirectoryOperations _directoryOperations;
    private readonly string _bucketPrefix = "unit-test-directory";

    private readonly List<string> _createdBucketNames = new();

    public DirectoryOperationsTests()
    {
        _bucketOperations = new(client);
        _directoryOperations = new(client);
    }

    #region sync methods
    [Fact]
    public void CreateDirectory_ShouldCreateDirectory()
    {
        // arrange
        var bucketName = $"{_bucketPrefix}-{Guid.NewGuid()}";
        _bucketOperations.CreateBucket(bucketName);

        // act
        _directoryOperations.CreateDirectory(bucketName, "my-directory");

        var isDirectoryExists = _directoryOperations.DirectoryExists(bucketName, "my-directory");

        Assert.True(isDirectoryExists);

        _createdBucketNames.Add(bucketName);
    }


    #endregion

    #region async methods
    [Fact]
    public async Task CreateDirectoryAsync_ShouldCreateDirectory()
    {
        // arrange
        var bucketName = $"{_bucketPrefix}-{Guid.NewGuid()}";
        await _bucketOperations.CreateBucketAsync(bucketName);

        // act
        await _directoryOperations.CreateDirectoryAsync(bucketName, "my-directory");

        // assert
        var isDirectoryExists = await _directoryOperations.DirectoryExistsAsync(bucketName, "my-directory");

        Assert.True(isDirectoryExists);

        _createdBucketNames.Add(bucketName);
    }
    #endregion

    public void Dispose()
    {
        foreach (var bucketName in _createdBucketNames)
        {
            _bucketOperations.EmptyBucket(bucketName);
            _bucketOperations.DeleteBucket(bucketName);
        }
    }

}
