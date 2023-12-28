// reference: https://docs.aws.amazon.com/AmazonS3/latest/userguide/example_s3_MultipartCopy_section.html

namespace AWS.S3.Wrapper.Interfaces;
public interface ILargeFileOperations
{
    Task UploadFullDirectoryAsync(string bucketName, string keyPrefix, string localPath, CancellationToken cancellationToken);
}
