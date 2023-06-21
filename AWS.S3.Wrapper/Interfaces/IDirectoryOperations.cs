namespace AWS.S3.Wrapper.Interfaces;
public interface IDirectoryOperations
{
    // <summary>
    /// Creates a directory in the bucket
    /// </summary>
    void CreateDirectory(string bucketName, string directoryPath);

    // <summary>
    /// Is the directory exists
    /// </summary>
    bool DirectoryExists(string bucketName, string directoryPath);

    // <summary>
    /// Lists all directories in the bucket
    /// </summary>
    IEnumerable<string> ListDirectories(string bucketName, string parentDirectoryPath = "");

    // <summary>
    /// Deletes a directory in the bucket
    /// </summary>
    void DeleteDirectory(string bucketName, string directoryPath);

    // <summary>
    /// Deletes all objects in a directory in the bucket
    /// </summary>
    void EmptyDirectory(string bucketName, string directoryPath);

    // <summary>
    /// Creates a directory in the bucket
    /// </summary>
    Task CreateDirectoryAsync(string bucketName, string directoryPath, CancellationToken cancellationToken = default);

    // <summary>
    /// Is the directory exists
    /// </summary>
    Task<bool> DirectoryExistsAsync(string bucketName, string directoryPath, CancellationToken cancellationToken = default);

    // <summary>
    /// Lists all directories in the bucket
    /// </summary>
    Task<IEnumerable<string>> ListDirectoriesAsync(string bucketName, string parentDirectoryPath = "", CancellationToken cancellationToken = default);

    // <summary>
    /// Deletes a directory in the bucket
    /// </summary>
    Task DeleteDirectoryAsync(string bucketName, string directoryPath, CancellationToken cancellationToken = default);

    // <summary>
    /// Deletes all objects in a directory in the bucket
    /// </summary>
    Task EmptyDirectoryAsync(string bucketName, string directoryPath, CancellationToken cancellationToken = default);
}
