using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.S3.Wrapper.Interfaces
{
    public interface IFileMetadataOperations
    {
        Task<IDictionary<string, string>> GetObjectMetadataAsync(string bucketName, string objectKey, CancellationToken cancellationToken);
        Task UpdateObjectMetadataAsync(string bucketName, string objectKey, IDictionary<string, string> metadata, CancellationToken cancellationToken);
    }
}