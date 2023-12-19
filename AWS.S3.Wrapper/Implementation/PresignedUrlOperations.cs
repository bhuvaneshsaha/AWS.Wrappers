
//  reference: https://docs.aws.amazon.com/AmazonS3/latest/userguide/example_s3_Scenario_PresignedUrl_section.html

namespace AWS.S3.Wrapper.Implementation;
public class PresignedUrlOperations(IAmazonS3 s3Client) : IPresignedUrlOperations
{
    public async Task<string> GeneratePresignedUrlForGetAsync(string bucketName, string objectKey, DateTime expires, CancellationToken cancellationToken)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = bucketName,
            Key = objectKey,
            Expires = expires,
            Verb = HttpVerb.GET
        };

        return await Task.Run(()=>s3Client.GetPreSignedURL(request), cancellationToken);
    }

    public async Task<string> GeneratePresignedUrlForPutAsync(string bucketName, string objectKey, DateTime expires, string contentType, CancellationToken cancellationToken)
    {
        return await Task.Run(() => s3Client.GeneratePreSignedURL(bucketName, objectKey, expires, null));
    }
}
