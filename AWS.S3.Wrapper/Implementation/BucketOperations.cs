
namespace AWS.S3.Wrapper.Implementation;
public class BucketOperations : IBucketOperations
{
    private readonly IAmazonS3 _s3Client;

    public BucketOperations(IAmazonS3 s3Client)
    {
        this._s3Client = s3Client;
    }

    public async Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken)
    {
        if (await DoesBucketExistAsync(bucketName, cancellationToken))
        {
            throw new ArgumentException("Bucket already exist");
        }

        var putBucketRequest = new PutBucketRequest
        {
            BucketName = bucketName,
            UseClientRegion = true
        };
        await _s3Client.PutBucketAsync(putBucketRequest, cancellationToken);
    }

    public async Task DeleteBucketAsync(string bucketName, CancellationToken cancellationToken)
    {
        var deleteBucketRequest = new DeleteBucketRequest
        {
            BucketName = bucketName
        };
        await _s3Client.DeleteBucketAsync(deleteBucketRequest, cancellationToken);
    }

    public async Task<bool> DoesBucketExistAsync(string bucketName, CancellationToken cancellationToken)
    {
        return await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
    }

    public async Task<IEnumerable<string>> ListBucketsAsync(CancellationToken cancellationToken)
    {
        var listBucketsRequest = new ListBucketsRequest();
        var response = await _s3Client.ListBucketsAsync(listBucketsRequest, cancellationToken);
        return response.Buckets.Select(x => x.BucketName);
    }

    public async Task EmptyBucketAsync(string bucketName, CancellationToken cancellationToken)
    {
        var listObjectsRequest = new ListObjectsV2Request
        {
            BucketName = bucketName
        };

        ListObjectsV2Response listObjectsResponse;

        do
        {
            listObjectsResponse = await _s3Client.ListObjectsV2Async(listObjectsRequest, cancellationToken);

            if (listObjectsResponse.KeyCount > 0)
            {
                var deleteObjectsRequest = new DeleteObjectsRequest
                {
                    BucketName = bucketName,
                    Objects = listObjectsResponse.S3Objects.Select(x => new KeyVersion { Key = x.Key }).ToList()
                };

                await _s3Client.DeleteObjectsAsync(deleteObjectsRequest, cancellationToken);
            }

            listObjectsRequest.ContinuationToken = listObjectsResponse.NextContinuationToken;
        } while (listObjectsResponse.IsTruncated);
    }
}
