
//  reference: https://docs.aws.amazon.com/AmazonS3/latest/userguide/example_s3_Scenario_PresignedUrl_section.html

namespace AWS.S3.Wrapper.Implementation;
public class PresignedUrlOperations : IPresignedUrlOperations
{
    private readonly IAmazonS3 _s3Client;

    public PresignedUrlOperations(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }

    public async Task<string> GeneratePresignedUrlForGetAsync(string bucketName, string objectKey, DateTime expires, CancellationToken cancellationToken)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = bucketName,
            Key = objectKey,
            Expires = expires,
            Verb = HttpVerb.GET
        };

        return await Task.Run(()=>_s3Client.GetPreSignedURL(request), cancellationToken);
    }

    public async Task<string> GeneratePresignedUrlForPutAsync(string bucketName, string objectKey, DateTime expires, string contentType, CancellationToken cancellationToken)
    {
        return await Task.Run(() => _s3Client.GeneratePreSignedURL(bucketName, objectKey, expires, null));
    }
}
