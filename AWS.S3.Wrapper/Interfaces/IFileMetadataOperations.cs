using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.S3.Wrapper.Interfaces
{
    public interface IFileMetadataOperations
    {
        IDictionary<string, string> GetObjectMetadata(string bucketName, string objectKey);
        void UpdateObjectMetadata(string bucketName, string objectKey, IDictionary<string, string> metadata);

        Task<IDictionary<string, string>> GetObjectMetadataAsync(string bucketName, string objectKey, CancellationToken cancellationToken = default);
        Task UpdateObjectMetadataAsync(string bucketName, string objectKey, IDictionary<string, string> metadata, CancellationToken cancellationToken = default);
    }
}