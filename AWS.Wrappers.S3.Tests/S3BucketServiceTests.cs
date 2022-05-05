using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AWS.Wrappers.S3.Tests
{
    public class S3BucketServiceTests: S3ServiceBase
    {
        
        [Fact]
        public void CreateBucket_SendNewBucketName_BucketCreated()
        {
            var bucketName = "aws-s3-wrapper-unit-test-bucket-bkt02";
            S3Service.CreateBucket(_bucketName);
            var result = S3Service.IsBucketExists(_bucketName);

            Assert.True(result, "Bucket Creation failed");
        }

    }
}
