using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.S3.Wrapper.Interfaces
{
    public interface IPresignedUrlOperations
    {
        string GeneratePresignedUrlForGet(string bucketName, string objectKey, DateTime expires);
        string GeneratePresignedUrlForPut(string bucketName, string objectKey, DateTime expires, string contentType = null);

        Task<string> GeneratePresignedUrlForGetAsync(string bucketName, string objectKey, DateTime expires, CancellationToken cancellationToken = default);
        Task<string> GeneratePresignedUrlForPutAsync(string bucketName, string objectKey, DateTime expires, string contentType = null, CancellationToken cancellationToken = default);
    }
}