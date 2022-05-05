using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AWS.Wrappers.S3.Tests
{
    public class S3BucketServiceAsyncTests: S3ServiceBase
    {
        [Fact]
        public async Task CreateBucketAsync_SendNewBucketName_BucketCreated()
        {
            var bucketName = "aws-s3-wrapper-unit-test-bucket-bkt01";
            await S3Service.CreateBucketAsync(bucketName);
            var result = await S3Service.IsBucketExistsAsync(bucketName);

            Assert.True(result, "Bucket Creation failed");
        }

    }
}
