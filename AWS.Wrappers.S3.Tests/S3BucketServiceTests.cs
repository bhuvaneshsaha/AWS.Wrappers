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
            var bucketName = "test-bucket-" + Guid.NewGuid().ToString();
            S3Service.CreateBucket(bucketName);
            var result = S3Service.IsBucketExists(bucketName);

            Assert.True(result, "CreateBucket - Bucket Creation failed");

            //Remove created bucket
            S3Service.DeleteBucket(bucketName);

        }

        [Fact]
        public void DeleteBucket_SendValidBucketName_BucketDeleted()
        {
            var bucketName = "test-bucket-delete-" + Guid.NewGuid().ToString();
            S3Service.CreateBucket(bucketName);
            var result = S3Service.IsBucketExists(bucketName);

            Assert.True(result, "DeleteBucket - Bucket Creation failed");

            S3Service.DeleteBucket(bucketName);

            result = S3Service.IsBucketExists(bucketName);

            Assert.False(result, "DeleteBucket - Bucket Deletion failed");
        }

        [Fact]
        public void IsBucketExists_SendValidBucketName_ReturnsTrue()
        {
            var bucketName = "test-bucket-existcheck-" + Guid.NewGuid().ToString();
            S3Service.CreateBucket(bucketName);
            var result = S3Service.IsBucketExists(bucketName);

            Assert.True(result, "IsBucketExists - Bucket Creation failed");

            S3Service.DeleteBucket(bucketName);

        }

        [Fact]
        public void IsBucketExists_SendInValidBucketName_ReturnsFalse()
        {
            var bucketName = "test-bucket-existcheck-not-exists-in-s3";
            var result = S3Service.IsBucketExists(bucketName);

            Assert.False(result, "IsBucketExists - Bucket should not exists");

        }

        [Fact]
        public void ClearBucket_Success()
        {
            S3Service.ClearBucket(_bucketName);

            var dir = "GetSubDirectories-" + Guid.NewGuid().ToString();
            S3Service.CreateDirectory(_bucketName, dir);
            S3Service.CreateDirectory(_bucketName, dir + "/fol1");
            S3Service.CreateDirectory(_bucketName, dir + "/fol2");
            S3Service.CreateDirectory(_bucketName, dir + "/fol3");
            
            var objects = S3Service.GetAllObjects(_bucketName, "");

            Assert.Equal(4, objects.Count);
            if (objects.Count != 4)
            {
                Assert.True(false, "");
            }
            S3Service.ClearBucket(_bucketName);

            objects.Clear();
            objects = S3Service.GetAllObjects(_bucketName, "");

            Assert.Empty(objects);

        }


    }
}
