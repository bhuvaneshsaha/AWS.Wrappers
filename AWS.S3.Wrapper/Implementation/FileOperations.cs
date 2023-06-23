using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.S3.Wrapper.Implementation
{
    public class FileOperations: IFileOperations
    {
        private readonly IAmazonS3 _s3Client;

        public FileOperations(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public Task CopyFileAsync(string sourceBucket, string sourceObjectKey, string destinationBucket, string destinationObjectKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFileAsync(string bucketName, string objectKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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

        public Task<IEnumerable<string>> ListObjectsAsync(string bucketName, string prefix, string delimiter, int maxKeys, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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