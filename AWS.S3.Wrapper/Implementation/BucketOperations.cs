
namespace AWS.S3.Wrapper.Implementation;

public class BucketOperations : IBucketOperations
{
    private readonly IAmazonS3 _s3Client;

    public BucketOperations(IAmazonS3 s3Client)
    {
        this._s3Client = s3Client;
    }

    public void CreateBucket(string bucketName)
    {
        var putBucketRequest = new PutBucketRequest
        {
            BucketName = bucketName,
            UseClientRegion = true
        };
        _s3Client.PutBucketAsync(putBucketRequest).Wait();
    }

    public async Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken = default)
    {
        var putBucketRequest = new PutBucketRequest
        {
            BucketName = bucketName,
            UseClientRegion = true
        };
        await _s3Client.PutBucketAsync(putBucketRequest, cancellationToken);
    }

    public void DeleteBucket(string bucketName)
    {
        var deleteBucketRequest = new DeleteBucketRequest
        {
            BucketName = bucketName
        };
        _s3Client.DeleteBucketAsync(deleteBucketRequest).Wait();
    }

    public async Task DeleteBucketAsync(string bucketName, CancellationToken cancellationToken = default)
    {
        var deleteBucketRequest = new DeleteBucketRequest
        {
            BucketName = bucketName
        };
        await _s3Client.DeleteBucketAsync(deleteBucketRequest, cancellationToken);
    }

    public IEnumerable<string> ListBuckets()
    {
        var listBucketsRequest = new ListBucketsRequest();
        var listBucketsResponse = _s3Client.ListBucketsAsync(listBucketsRequest).Result;
        return listBucketsResponse.Buckets.Select(x => x.BucketName);
    }

    public async Task<IEnumerable<string>> ListBucketsAsync(CancellationToken cancellationToken = default)
    {
        var listBucketsRequest = new ListBucketsRequest();
        var response = await _s3Client.ListBucketsAsync(listBucketsRequest, cancellationToken);
        return response.Buckets.Select(x => x.BucketName);
    }
}
