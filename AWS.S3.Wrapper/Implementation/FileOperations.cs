using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.S3.Wrapper.Implementation
{
    public class FileOperations : IFileOperations
    {
        private readonly IAmazonS3 _s3Client;

        public FileOperations(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task CopyFileAsync(string sourceBucket, string sourceObjectKey, string destinationBucket, string destinationObjectKey, CancellationToken cancellationToken)
        {
            var request = new Amazon.S3.Model.CopyObjectRequest
            {
                SourceBucket = sourceBucket,
                SourceKey = sourceObjectKey,
                DestinationBucket = destinationBucket,
                DestinationKey = destinationObjectKey
            };

            await _s3Client.CopyObjectAsync(request, cancellationToken);
        }

        public async Task DeleteFileAsync(string bucketName, string objectKey, CancellationToken cancellationToken)
        {
            var request = new Amazon.S3.Model.DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = objectKey
            };

            await _s3Client.DeleteObjectAsync(request, cancellationToken);
        }

        public async Task<bool> DoseFileExistAsync(string bucketName, string objectKey, CancellationToken cancellationToken)
        {
            var request = new Amazon.S3.Model.GetObjectMetadataRequest
            {
                BucketName = bucketName,
                Key = objectKey
            };

            try
            {
            var response = await _s3Client.GetObjectMetadataAsync(request, cancellationToken);

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Amazon.S3.AmazonS3Exception ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

        }

        public async Task<Stream> GetFileAsync(string bucketName, string objectKey, CancellationToken cancellationToken)
        {
            // GET FILES FROM S3
            var request = new Amazon.S3.Model.GetObjectRequest
            {
                BucketName = bucketName,
                Key = objectKey
            };

            var response = await _s3Client.GetObjectAsync(request, cancellationToken);

            return response.ResponseStream;
        }

        public async Task<IEnumerable<string>> ListObjectsAsync(string bucketName, string prefix, string delimiter, int maxKeys, CancellationToken cancellationToken)
        {
            var request = new Amazon.S3.Model.ListObjectsRequest
            {
                BucketName = bucketName,
                Prefix = prefix,
                Delimiter = delimiter,
                MaxKeys = maxKeys
            };

            var response = await _s3Client.ListObjectsAsync(request, cancellationToken);

            return response.S3Objects.Select(x => x.Key);
        }


        public async Task PutFileAsync(string bucketName, string objectKey, Stream content, string contentType, CancellationToken cancellationToken)
        {
            var request = new Amazon.S3.Model.PutObjectRequest
            {
                BucketName = bucketName,
                Key = objectKey,
                InputStream = content,
                ContentType = contentType
            };

            await _s3Client.PutObjectAsync(request, cancellationToken);
        }
    }
}