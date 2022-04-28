using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Wrappers.S3.Interfaces
{
    public interface IS3DirectoryServiceAsync
    {
        /// <summary>
        /// Checks Folder(Key) exists in S3 Bucket
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="path"></param>
        /// <returns>Exists (True) / Not exists (False)</returns>
        Task<bool> IsDirectoryExistsAsync(string bucketName, string path, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create directory inside give bucket if not exits.
        /// </summary>
        /// <param name="bucketName">Name of the bucket where the folder needs to create</param>
        /// <param name="path"></param>
        /// <exception cref="ArgumentException"/>
        Task CreateDirectoryAsync(string bucketName, string path, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete Directory if exists
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="path"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        Task DeleteDirectoryAsync(string bucketName, string path, bool recursive = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// It gets the list of subdirectories from the given path.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<List<string>> GetSubDirectoriesAsync(string bucketName, string path, CancellationToken cancellationToken = default);

        /// <summary>
        /// It gets the list of directories and sub-directories under the given path.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<List<string>> GetAllDirectoriesRecursiveAsync(string bucketName, string path, CancellationToken cancellationToken = default);


    }
}
