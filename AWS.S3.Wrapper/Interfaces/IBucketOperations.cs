using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.S3.Wrapper.Interfaces
{
    public interface IBucketOperations
    {
        void CreateBucket(string bucketName);
        IEnumerable<string> ListBuckets();
        void DeleteBucket(string bucketName);

        Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken = default);
        Task<IEnumerable<string>> ListBucketsAsync(CancellationToken cancellationToken = default);
        Task DeleteBucketAsync(string bucketName, CancellationToken cancellationToken = default);
    }
}