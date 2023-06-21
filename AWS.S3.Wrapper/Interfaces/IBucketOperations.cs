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
        /// <exception cref="ArgumentException">Thrown when the bucket name is invalid.</exception>
        /// <exception cref="BucketAlreadyExistsException">Thrown when the bucket already exists.</exception>
        /// <exception cref="BucketAlreadyOwnedByYouException">Thrown when the bucket already exists and is owned by you.</exception>
        /// <exception cref="AmazonS3Exception">Thrown when the bucket creation fails.</exception>
        /// <exception cref="AmazonServiceException">Thrown when the bucket creation fails.</exception>
        /// <exception cref="AmazonClientException">Thrown when the bucket creation fails.</exception>
        void CreateBucket(string bucketName);

        /// <summary>
        /// Lists all S3 buckets.
        /// </summary>
        /// <returns>A list of bucket names.</returns>
        /// <exception cref="AmazonS3Exception">Thrown when the bucket listing fails.</exception>
        /// <exception cref="AmazonServiceException">Thrown when the bucket listing fails.</exception>
        /// <exception cref="AmazonClientException">Thrown when the bucket listing fails.</exception>
        IEnumerable<string> ListBuckets();

        /// <summary>
        /// Deletes the S3 bucket with the given name.
        /// </summary>
        /// <param name="bucketName">The name of the bucket to delete.</param>
        /// <exception cref="ArgumentException">Thrown when the bucket name is invalid.</exception>
        /// <exception cref="AmazonS3Exception">Thrown when the bucket deletion fails.</exception>
        /// <exception cref="AmazonServiceException">Thrown when the bucket deletion fails.</exception>
        /// <exception cref="AmazonClientException">Thrown when the bucket deletion fails.</exception>
        void DeleteBucket(string bucketName);

        /// <summary>
        /// Creates a new S3 bucket with the given name.
        /// </summary>
        /// <param name="bucketName">The name of the new bucket to create.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation.</param>
        /// <exception cref="ArgumentException">Thrown when the bucket name is invalid.</exception>
        /// <exception cref="BucketAlreadyExistsException">Thrown when the bucket already exists.</exception>
        /// <exception cref="BucketAlreadyOwnedByYouException">Thrown when the bucket already exists and is owned by you.</exception>
        /// <exception cref="AmazonS3Exception">Thrown when the bucket creation fails.</exception>
        /// <exception cref="AmazonServiceException">Thrown when the bucket creation fails.</exception>
        /// <exception cref="AmazonClientException">Thrown when the bucket creation fails.</exception>
        Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists all S3 buckets.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation.</param>
        /// <returns>A list of bucket names.</returns>
        /// <exception cref="AmazonS3Exception">Thrown when the bucket listing fails.</exception>
        /// <exception cref="AmazonServiceException">Thrown when the bucket listing fails.</exception>
        /// <exception cref="AmazonClientException">Thrown when the bucket listing fails.</exception>
        Task<IEnumerable<string>> ListBucketsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the S3 bucket with the given name.
        /// </summary>
        /// <param name="bucketName">The name of the bucket to delete.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation.</param>
        /// <exception cref="ArgumentException">Thrown when the bucket name is invalid.</exception>
        /// <exception cref="AmazonS3Exception">Thrown when the bucket deletion fails.</exception>
        /// <exception cref="AmazonServiceException">Thrown when the bucket deletion fails.</exception>
        /// <exception cref="AmazonClientException">Thrown when the bucket deletion fails.</exception>
        Task DeleteBucketAsync(string bucketName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if the S3 bucket with the given name exists.
        /// </summary>
        /// <param name="bucketName">The name of the bucket to check.</param>
        /// <returns>True if the bucket exists, false otherwise.</returns>
        /// <exception cref="ArgumentException">Thrown when the bucket name is invalid.</exception>
        /// <exception cref="AmazonS3Exception">Thrown when the bucket existence check fails.</exception>
        /// <exception cref="AmazonServiceException">Thrown when the bucket existence check fails.</exception>
        /// <exception cref="AmazonClientException">Thrown when the bucket existence check fails.</exception>
        bool DoesBucketExist(string bucketName);

        /// <summary>
        /// Checks if the S3 bucket with the given name exists.
        /// </summary>
        /// <param name="bucketName">The name of the bucket to check.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation.</param>
        /// <returns>True if the bucket exists, false otherwise.</returns>
        /// <exception cref="ArgumentException">Thrown when the bucket name is invalid.</exception>
        /// <exception cref="AmazonS3Exception">Thrown when the bucket existence check fails.</exception>
        /// <exception cref="AmazonServiceException">Thrown when the bucket existence check fails.</exception>
        /// <exception cref="AmazonClientException">Thrown when the bucket existence check fails.</exception>
        Task<bool> DoesBucketExistAsync(string bucketName, CancellationToken cancellationToken = default);
        
        
    }
}