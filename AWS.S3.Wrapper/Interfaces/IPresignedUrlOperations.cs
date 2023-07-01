
namespace AWS.S3.Wrapper.Interfaces;
public interface IPresignedUrlOperations
{
    Task<string> GeneratePresignedUrlForGetAsync(string bucketName, string objectKey, DateTime expires, CancellationToken cancellationToken);
    Task<string> GeneratePresignedUrlForPutAsync(string bucketName, string objectKey, DateTime expires, string contentType, CancellationToken cancellationToken);
}
