using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.S3.Wrapper.Interfaces
{
    public interface IBucketOperations
    {
        /// <summary>
        /// Creates a new S3 bucket with the given name.
        /// </summary>
        /// <param name="bucketName">The name of the new bucket to create.</param>
        void CreateBucket(string bucketName);

        /// <summary>
        /// Creates a new S3 bucket with the given name.
        /// </summary>
        /// <param name="bucketName">The name of the new bucket to create.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation.</param>
        Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists all S3 buckets.
        /// </summary>
        /// <returns>A list of bucket names.</returns>
        IEnumerable<string> ListBuckets();

        /// <summary>
        /// Lists all S3 buckets.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation.</param>
        /// <returns>A list of bucket names.</returns>
        Task<IEnumerable<string>> ListBucketsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the S3 bucket with the given name.
        /// </summary>
        /// <param name="bucketName">The name of the bucket to delete.</param>
        void DeleteBucket(string bucketName);

        /// <summary>
        /// Deletes the S3 bucket with the given name.
        /// </summary>
        /// <param name="bucketName">The name of the bucket to delete.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation.</param>
        Task DeleteBucketAsync(string bucketName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if the S3 bucket with the given name exists.
        /// </summary>
        /// <param name="bucketName">The name of the bucket to check.</param>
        /// <returns>True if the bucket exists, false otherwise.</returns>
        bool DoesBucketExist(string bucketName);

        /// <summary>
        /// Checks if the S3 bucket with the given name exists.
        /// </summary>
        /// <param name="bucketName">The name of the bucket to check.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation.</param>
        /// <returns>True if the bucket exists, false otherwise.</returns>
        Task<bool> DoesBucketExistAsync(string bucketName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Empty the S3 bucket with the given name.
        /// </summary>
        /// <param name="bucketName">The name of the bucket to empty.</param>
        void EmptyBucket(string bucketName);

        /// <summary>
        /// Empty the S3 bucket with the given name.
        /// </summary>
        /// <param name="bucketName">The name of the bucket to empty.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation.</param>
        Task EmptyBucketAsync(string bucketName, CancellationToken cancellationToken = default);
    }
}
