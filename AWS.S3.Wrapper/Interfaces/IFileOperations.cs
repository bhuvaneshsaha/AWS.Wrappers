using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.S3.Wrapper.Interfaces
{
    public interface IFileOperations
    {
        void PutFile(string bucketName, string objectKey, Stream content, string contentType = null);
        Stream GetFile(string bucketName, string objectKey);
        void DeleteFile(string bucketName, string objectKey);
        void CopyFile(string sourceBucket, string sourceObjectKey, string destinationBucket, string destinationObjectKey);
        IEnumerable<string> ListObjects(string bucketName, string prefix = null, string delimiter = null, int maxKeys = 0);

        Task PutFileAsync(string bucketName, string objectKey, Stream content, string contentType = null, CancellationToken cancellationToken = default);
        Task<Stream> GetFileAsync(string bucketName, string objectKey, CancellationToken cancellationToken = default);
        Task DeleteFileAsync(string bucketName, string objectKey, CancellationToken cancellationToken = default);
        Task CopyFileAsync(string sourceBucket, string sourceObjectKey, string destinationBucket, string destinationObjectKey, CancellationToken cancellationToken = default);
        Task<IEnumerable<string>> ListObjectsAsync(string bucketName, string prefix = null, string delimiter = null, int maxKeys = 0, CancellationToken cancellationToken = default);
    }
}