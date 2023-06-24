
//  reference: https://docs.aws.amazon.com/AmazonS3/latest/userguide/example_s3_Scenario_PresignedUrl_section.html

namespace AWS.S3.Wrapper.Implementation
{
    public class PresignedUrlOperations : IPresignedUrlOperations
    {
        private readonly IAmazonS3 _s3Client;

        public PresignedUrlOperations(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public Task<string> GeneratePresignedUrlForGetAsync(string bucketName, string objectKey, DateTime expires, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GeneratePresignedUrlForPutAsync(string bucketName, string objectKey, DateTime expires, string contentType, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
