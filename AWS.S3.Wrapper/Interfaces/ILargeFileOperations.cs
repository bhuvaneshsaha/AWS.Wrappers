using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.S3.Wrapper.Interfaces
{
    public interface ILargeFileOperations
    {
        Task UploadLargeFileMultipartAsync(string bucketName, string objectKey, Stream inputStream, long partSize = 5 * 1024 * 1024, CancellationToken cancellationToken = default);
        Task AbortMultipartUploadAsync(string bucketName, string objectKey, string uploadId, CancellationToken cancellationToken = default);
    }
}