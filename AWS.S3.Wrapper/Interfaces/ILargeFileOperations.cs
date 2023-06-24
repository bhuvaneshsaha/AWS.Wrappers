// reference: https://docs.aws.amazon.com/AmazonS3/latest/userguide/example_s3_MultipartCopy_section.html

namespace AWS.S3.Wrapper.Interfaces;

public interface ILargeFileOperations
{
    Task UploadLargeFileMultipartAsync(string bucketName, string objectKey, Stream inputStream, long partSize, CancellationToken cancellationToken);
    Task AbortMultipartUploadAsync(string bucketName, string objectKey, string uploadId, CancellationToken cancellationToken);
    Task<bool> UploadFullDirectoryAsync(string bucketName, string keyPrefix, string localPath, CancellationToken cancellationToken);
}
