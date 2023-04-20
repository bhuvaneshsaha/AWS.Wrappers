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
    /// Create directory inside give bucket if not exits.
    /// </summary>
    /// <param name="bucketName">Name of the bucket where the folder needs to create</param>
    /// <param name="path"></param>
    /// <exception cref="ArgumentException"/>
    void CreateDirectory(string bucketName, string path);

    /// <summary>
    /// Delete Directory if exists <br></br>
    /// recursive = false -> throws error if directory contains files or folder inside it <br></br>
    /// recursive = true -> deletes files or folder inside it <br></br>
    /// </summary>
    /// <param name="bucketName"></param>
    /// <param name="path"></param>
    /// <param name="recursive"></param>
    /// <returns>Delete Objects count</returns>
    int DeleteDirectory(string bucketName, string path, bool recursive = false);

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
    /// Gets all object under the path, both folder and files
    /// </summary>
    /// <returns></returns>
    List<string> GetAllObjects(string bucketName, string path);


}
