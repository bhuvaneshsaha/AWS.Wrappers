using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.S3.Wrapper.Interfaces
{
    public interface IFileOperations
    {
        Task UploadFileStreamAsync(string bucketName, string objectKey, Stream content, string contentType, CancellationToken cancellationToken);
        Task<bool> DoseFileExistAsync(string bucketName, string objectKey, CancellationToken cancellationToken);
        Task<Stream> GetFileAsync(string bucketName, string objectKey, CancellationToken cancellationToken);
        Task DeleteFileAsync(string bucketName, string objectKey, CancellationToken cancellationToken);
        Task CopyFileAsync(string sourceBucket, string sourceObjectKey, string destinationBucket, string destinationObjectKey, CancellationToken cancellationToken);
        Task<IEnumerable<string>> ListObjectsAsync(string bucketName, string prefix, string delimiter, int maxKeys, CancellationToken cancellationToken);
        public Task<bool> UploadSingleFileAsync(string bucketName, string sourceFileFullPath, string destKey, CancellationToken cancellationToken);

    }
}
