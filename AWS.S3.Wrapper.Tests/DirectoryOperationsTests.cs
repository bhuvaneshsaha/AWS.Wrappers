
namespace AWS.S3.Wrapper.Tests;

[Collection("Our Test Collection #1")]
public class DirectoryOperationsTests : TestBase
{
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
        await _directoryOperations.CreateDirectoryAsync(bucketName, "my-directory1", cancellationToken);
        await _directoryOperations.CreateDirectoryAsync(bucketName, "my-directory1", cancellationToken);

        // assert
        var isDirectoryExists = await _directoryOperations.DirectoryExistsAsync(bucketName, "my-directory1", cancellationToken);

        Assert.True(isDirectoryExists);

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
}
