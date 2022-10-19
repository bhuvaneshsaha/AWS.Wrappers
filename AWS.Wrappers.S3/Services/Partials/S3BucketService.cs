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
    public void CreateBucket(string bucketName)
    {
        var response = _s3Client.PutBucketAsync(bucketName).GetAwaiter().GetResult();

        if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new ArgumentException(response.ResponseMetadata.ToString());
        }
    }

    public void DeleteBucket(string bucketName)
    {
        if (IsBucketExists(bucketName))
        {
            _s3Client.DeleteBucketAsync(bucketName).GetAwaiter().GetResult();

            return;
        }

        throw new ArgumentException("Bucket not found");
    }

    public bool IsBucketExists(string bucketName)
    {
        return AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName).GetAwaiter().GetResult();
    }

    public void ClearBucket(string bucketName)
    {
        ListObjectsV2Request request = new ListObjectsV2Request
        {
            BucketName = bucketName,
            MaxKeys = 500
        };

        ListObjectsV2Response response;
        do
        {
            response = _s3Client.ListObjectsV2Async(request).GetAwaiter().GetResult();

            foreach (var item in response.S3Objects)
            {
                _s3Client.DeleteObjectAsync(new DeleteObjectRequest { BucketName = bucketName, Key = item.Key }).GetAwaiter().GetResult();
            }

            request.ContinuationToken = response.NextContinuationToken;

        } while (response.IsTruncated);

    }
}
