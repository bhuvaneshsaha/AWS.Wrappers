using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.S3.Wrapper.Interfaces
{
    public interface IFileOperations
    {
        Task PutFileAsync(string bucketName, string objectKey, Stream content, string contentType, CancellationToken cancellationToken);
        Task<Stream> GetFileAsync(string bucketName, string objectKey, CancellationToken cancellationToken);
        Task DeleteFileAsync(string bucketName, string objectKey, CancellationToken cancellationToken);
        Task CopyFileAsync(string sourceBucket, string sourceObjectKey, string destinationBucket, string destinationObjectKey, CancellationToken cancellationToken);
        Task<IEnumerable<string>> ListObjectsAsync(string bucketName, string prefix, string delimiter, int maxKeys, CancellationToken cancellationToken);
    }
}