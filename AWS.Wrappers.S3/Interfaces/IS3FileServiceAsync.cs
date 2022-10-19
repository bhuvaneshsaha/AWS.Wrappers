using AWS.Wrappers.S3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Wrappers.S3.Interfaces
{
    public interface IS3FileServiceAsync
    {
        /// <summary>
        /// Upload local file to S3 path
        /// </summary>
        /// <param name="s3Upload"></param>
        /// <returns>Success (True) or False</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="IOException"></exception>
        Task UploadFileAsync(S3Upload s3Upload, CancellationToken cancellationToken = default);

        /// <summary>
        /// Upload file bytes to S3 path
        /// </summary>
        /// <param name="s3Uploads"></param>
        /// <returns></returns>
        Task UploadFileAsync(S3UploadByBytes s3Upload, CancellationToken cancellationToken = default);

        /// <summary>
        /// Download the S3 file and save it in local path
        /// </summary>
        /// <param name="s3Download"></param>
        Task DownloadFileToLocalAsync(S3Download s3Download, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get Bytes of File from S3
        /// </summary>
        /// <param name="s3Download"></param>
        /// <returns></returns>
        Task<byte[]> GetFileBytesAsync(S3Download s3Download, CancellationToken cancellationToken = default);

        /// <summary>
        /// ​Checks file object exist in S3​
        /// </summary>
        /// <param name="s3Key"></param>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        Task<bool> IsFileExistsAsync(string bucketName, string s3Key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete file
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="s3Key"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteFileAsync(string bucketName, string s3Key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get list of files from path
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="path"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<string>> GetFilesAsync(string bucketName, string path, CancellationToken cancellationToken = default);
    }
}
