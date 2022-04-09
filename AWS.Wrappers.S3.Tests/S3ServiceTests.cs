using Amazon.S3;
using AWS.Wrappers.S3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AWS.Wrappers.S3.Tests
{
    [Collection("Sequential")]
    public class S3ServiceTests: BaseTests
    {
        [Fact]
        public async Task CreateBucketAsync_SendNewBucketName_BucketCreated()
        {
            var bucketName = "aws-s3-wrapper-unit-test-bucket-bkt01";
            await S3Service.CreateBucketAsync(bucketName);
            var result = await S3Service.IsBucketExistsAsync(bucketName);

            Assert.True(result, "Bucket Creation failed");
        }

        [Fact]
        public void CreateBucket_SendNewBucketName_BucketCreated()
        {
            var bucketName = "aws-s3-wrapper-unit-test-bucket-bkt02";
            S3Service.CreateBucket(bucketName);
            var result = S3Service.IsBucketExists(bucketName);

            Assert.True(result, "Bucket Creation failed");
        }

    }
}
