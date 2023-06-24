namespace AWS.S3.Wrapper.Interfaces;
public interface IDirectoryOperations
{
    // <summary>
    /// Creates a directory in the bucket
    /// </summary>
    Task CreateDirectoryAsync(string bucketName, string directoryPath, CancellationToken cancellationToken);

    // <summary>
    /// Is the directory exists
    /// </summary>
    Task<bool> DirectoryExistsAsync(string bucketName, string directoryPath, CancellationToken cancellationToken);

    // <summary>
    /// Lists all directories in the bucket
    /// </summary>
    Task<IEnumerable<string>> ListDirectoriesAsync(string bucketName, string parentDirectoryPath, CancellationToken cancellationToken);

    // <summary>
    /// Deletes a directory in the bucket
    /// </summary>
    Task DeleteDirectoryAsync(string bucketName, string directoryPath, CancellationToken cancellationToken);

    // <summary>
    /// Deletes all objects in a directory in the bucket
    /// </summary>
    Task EmptyDirectoryAsync(string bucketName, string directoryPath, CancellationToken cancellationToken);
}
