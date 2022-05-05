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

        Task<List<string>> GetFilesAsync(string bucketName, string path, CancellationToken cancellationToken = default);
    }
}
