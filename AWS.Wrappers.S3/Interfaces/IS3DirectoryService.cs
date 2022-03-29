namespace AWS.Wrappers.S3.Interfaces;

public interface IS3DirectoryService
{
    /// <summary>
    /// Checks Folder(Key) exists in S3 Bucket
    /// </summary>
    /// <param name="bucketName"></param>
    /// <param name="folderName"></param>
    /// <returns>Exists (True) / Not exists (False)</returns>
    bool IsDirectoryExists(string bucketName, string folderName);

    /// <summary>
    /// Create directory if not exits
    /// </summary>
    /// <param name="bucketName">Name of the bucket where the folder needs to create</param>
    /// <param name="folderName"></param>
    /// <exception cref="ArgumentException"/>
    void CreateDirectory(string bucketName, string folderName);

    /// <summary>
    /// Delete Directory if exists
    /// </summary>
    /// <param name="bucketName"></param>
    /// <param name="folderName"></param>
    /// <param name="recursive"></param>
    /// <returns></returns>
    bool DeleteDirectory(string bucketName, string folderName, bool recursive = false);
}
