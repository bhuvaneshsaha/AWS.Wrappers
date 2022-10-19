using Amazon.S3.Model;
using Amazon.S3.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Wrappers.S3.Services;

public partial class S3Service
{
    public async Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken = default)
    {
        var respone = await _s3Client.PutBucketAsync(bucketName, cancellationToken);

        if (respone.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new ArgumentException(respone.ResponseMetadata.ToString());
        }
    }

    public async Task DeleteBucketAsync(string bucketName, CancellationToken cancellationToken = default)
    {
        if (await IsBucketExistsAsync(bucketName))
        {
            await _s3Client.DeleteBucketAsync(bucketName, cancellationToken);

            return;
        }

        throw new ArgumentException("Bucket not found");
    }

    public async Task<bool> IsBucketExistsAsync(string bucketName)
    {
        return await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
    }

    public async Task ClearBucketAsync(string bucketName)
    {
        ListObjectsV2Request request = new ListObjectsV2Request
        {
            BucketName = bucketName,
            MaxKeys = 500
        };

        ListObjectsV2Response response;
        do
        {
            response = await _s3Client.ListObjectsV2Async(request);

            foreach (var item in response.S3Objects)
            {
                await _s3Client.DeleteObjectAsync(new DeleteObjectRequest { BucketName = bucketName, Key = item.Key });
            }

            request.ContinuationToken = response.NextContinuationToken;

        } while (response.IsTruncated);

    }
}
