
using Amazon.S3.Util;

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
        if (DoesBucketExist(bucketName))
        {
            throw new Exception("Bucket already exist");
        }

        var putBucketRequest = new PutBucketRequest
        {
            BucketName = bucketName,
            UseClientRegion = true
        };
        _s3Client.PutBucketAsync(putBucketRequest).Wait();
    }

    public async Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken = default)
    {
        if (await DoesBucketExistAsync(bucketName, cancellationToken))
        {
            throw new Exception("Bucket already exist");
        }

        var putBucketRequest = new PutBucketRequest
        {
            BucketName = bucketName,
            UseClientRegion = true
        };
        await _s3Client.PutBucketAsync(putBucketRequest, cancellationToken);
    }

    public void DeleteBucket(string bucketName)
    {
        if (!DoesBucketExist(bucketName))
        {
            throw new Exception("Bucket not exist");
        }

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

    public bool DoesBucketExist(string bucketName)
    {
        return AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName).Result;
    }

    public async Task<bool> DoesBucketExistAsync(string bucketName, CancellationToken cancellationToken = default)
    {
        return await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
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

    public void EmptyBucket(string bucketName)
    {
        var listObjectsRequest = new ListObjectsV2Request
        {
            BucketName = bucketName
        };

        ListObjectsV2Response listObjectsResponse;

        do
        {
            listObjectsResponse = _s3Client.ListObjectsV2Async(listObjectsRequest).Result;

            if (listObjectsResponse.KeyCount > 0)
            {
                var deleteObjectsRequest = new DeleteObjectsRequest
                {
                    BucketName = bucketName,
                    Objects = listObjectsResponse.S3Objects.Select(x => new KeyVersion { Key = x.Key }).ToList()
                };

                _s3Client.DeleteObjectsAsync(deleteObjectsRequest).Wait();
            }

            listObjectsRequest.ContinuationToken = listObjectsResponse.NextContinuationToken;
        } while (listObjectsResponse.IsTruncated);
        
    }

    public async Task EmptyBucketAsync(string bucketName, CancellationToken cancellationToken = default)
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
