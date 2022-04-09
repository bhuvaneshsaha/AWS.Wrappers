namespace AWS.Wrappers.S3.Interfaces;

public interface IS3DirectoryService
{
    /// <summary>
    /// Checks Folder(Key) exists in S3 Bucket
    /// </summary>
    /// <param name="bucketName"></param>
    /// <param name="path"></param>
    /// <returns>Exists (True) / Not exists (False)</returns>
    bool IsDirectoryExists(string bucketName, string path);

    /// <summary>
    /// Checks Folder(Key) exists in S3 Bucket
    /// </summary>
    /// <param name="bucketName"></param>
    /// <param name="path"></param>
    /// <returns>Exists (True) / Not exists (False)</returns>
    Task<bool> IsDirectoryExistsAsync(string bucketName, string path);

    /// <summary>
    /// Create directory inside give bucket if not exits.
    /// </summary>
    /// <param name="bucketName">Name of the bucket where the folder needs to create</param>
    /// <param name="path"></param>
    /// <exception cref="ArgumentException"/>
    Task CreateDirectoryAsync(string bucketName, string path);

    /// <summary>
    /// Create directory inside give bucket if not exits.
    /// </summary>
    /// <param name="bucketName">Name of the bucket where the folder needs to create</param>
    /// <param name="path"></param>
    /// <exception cref="ArgumentException"/>
    void CreateDirectory(string bucketName, string path);

    /// <summary>
    /// Delete Directory if exists
    /// </summary>
    /// <param name="bucketName"></param>
    /// <param name="path"></param>
    /// <param name="recursive"></param>
    /// <returns></returns>
    Task DeleteDirectoryAsync(string bucketName, string path, bool recursive = false);

    /// <summary>
    /// Delete Directory if exists
    /// </summary>
    /// <param name="bucketName"></param>
    /// <param name="path"></param>
    /// <param name="recursive"></param>
    /// <returns></returns>
    void DeleteDirectory(string bucketName, string path, bool recursive = false);

    /// <summary>
    /// It gets the list of subdirectories from the given path.
    /// </summary>
    /// <param name="bucketName"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    Task<List<string>> GetSubDirectoriesAsync(string bucketName, string path);


    /// <summary>
    /// It gets the list of subdirectories from the given path.
    /// </summary>
    /// <param name="bucketName"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    List<string> GetSubDirectories(string bucketName, string path);

    /// <summary>
    /// It gets the list of directories/sub-directories under the given path.
    /// </summary>
    /// <param name="bucketName"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    List<string> GetAllDirectoriesRecursive(string bucketName, string path);

    /// <summary>
    /// It gets the list of directories and sub-directories under the given path.
    /// </summary>
    /// <param name="bucketName"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    Task<List<string>> GetAllDirectoriesRecursiveAsync(string bucketName, string path);

}
