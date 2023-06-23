
namespace AWS.S3.Wrapper.Tests;

[Collection("Our Test Collection #1")]
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

    [Fact]
    public async Task CreateDirectoryAsync_ShouldNotCreateMultipleDirectoryWithSameName()
    {
        // arrange
        var bucketName = $"{_bucketPrefix}-{Guid.NewGuid()}";
        await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);

        // act
        await _directoryOperations.CreateDirectoryAsync(bucketName, "my-directory", cancellationToken);
        await _directoryOperations.CreateDirectoryAsync(bucketName, "my-directory", cancellationToken);

        // assert
        var directories = await _directoryOperations.ListDirectoriesAsync(bucketName, "my-directory", cancellationToken);

        Assert.Single(directories);

        _createdBucketNames.Add(bucketName);
    }


    [Fact]
    public async Task DeleteDirectoryAsync_ShouldDeleteDirectory()
    {
        // arrange
        var bucketName = $"{_bucketPrefix}-{Guid.NewGuid()}";
        await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);
        await _directoryOperations.CreateDirectoryAsync(bucketName, "my-directory", cancellationToken);

        // act
        await _directoryOperations.DeleteDirectoryAsync(bucketName, "my-directory", cancellationToken);

        // assert
        var isDirectoryExists = await _directoryOperations.DirectoryExistsAsync(bucketName, "my-directory", cancellationToken);

        Assert.False(isDirectoryExists);

        _createdBucketNames.Add(bucketName);
    }

    [Fact]
    public async Task ListDirectoriesAsync_ShouldListDirectories()
    {
        // arrange
        var bucketName = $"{_bucketPrefix}-{Guid.NewGuid()}";
        await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);
        await _directoryOperations.CreateDirectoryAsync(bucketName, "my-directory", cancellationToken);
        await _directoryOperations.CreateDirectoryAsync(bucketName, "my-directory/child1", cancellationToken);
        await _directoryOperations.CreateDirectoryAsync(bucketName, "my-directory/child2", cancellationToken);

        // act
        var directories = await _directoryOperations.ListDirectoriesAsync(bucketName, "my-directory", cancellationToken);

        // assert
        Assert.Equal(2, directories.Count());

        _createdBucketNames.Add(bucketName);
    }

    [Fact]
    public async Task EmptyDirectoryAsync_ShouldRemoveSubDirectories()
    {
        // arrange
        var bucketName = $"{_bucketPrefix}-{Guid.NewGuid()}";
        await _bucketOperations.CreateBucketAsync(bucketName, cancellationToken);
        await _directoryOperations.CreateDirectoryAsync(bucketName, "my-directory", cancellationToken);
        await _directoryOperations.CreateDirectoryAsync(bucketName, "my-directory/child1", cancellationToken);
        await _directoryOperations.CreateDirectoryAsync(bucketName, "my-directory/child2", cancellationToken);

        // act
        await _directoryOperations.EmptyDirectoryAsync(bucketName, "my-directory", cancellationToken);

        // assert
        var directories = await _directoryOperations.ListDirectoriesAsync(bucketName, "my-directory", cancellationToken);

        Assert.Empty(directories);

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
