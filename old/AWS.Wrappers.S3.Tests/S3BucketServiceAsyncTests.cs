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
            var bucketName = "test-bucket-" + Guid.NewGuid().ToString();
            S3Service.CreateBucket(bucketName);
            var result = await S3Service.IsBucketExistsAsync(bucketName);

            Assert.True(result, "CreateBucketAsync - Bucket Creation failed");

            //Remove created bucket
            await S3Service.DeleteBucketAsync(bucketName);
        }

        [Fact]
        public async Task DeleteBucketAsync_SendExistingBucketName_BucketDeleted()
        {
            var bucketName = "test-bucket-delete-" + Guid.NewGuid().ToString();
            S3Service.CreateBucket(bucketName);
            var result = await S3Service.IsBucketExistsAsync(bucketName);

            Assert.True(result, "DeleteBucketAsync - Bucket Creation failed");

            await S3Service.DeleteBucketAsync(bucketName);

            result = await S3Service.IsBucketExistsAsync(bucketName);

            Assert.False(result, "DeleteBucketAsync - Bucket Deletion failed");


        }

        [Fact]
        public async Task IsBucketExistsAsync_SendValidBucketName_ReturnsTrue()
        {
            var bucketName = "test-bucket-existcheck-" + Guid.NewGuid().ToString();
            S3Service.CreateBucket(bucketName);
            var result = await S3Service.IsBucketExistsAsync(bucketName);

            Assert.True(result, "IsBucketExists - Bucket Creation failed");

            S3Service.DeleteBucket(bucketName);

        }

        [Fact]
        public async Task IsBucketExistsAsync_SendInValidBucketName_ReturnsFalse()
        {
            var bucketName = "test-bucket-existcheck-not-exists-in-s3";
            var result = await S3Service.IsBucketExistsAsync(bucketName);

            Assert.False(result, "IsBucketExists - Bucket should not exists");

        }

        [Fact]
        public async Task ClearBucketAsync_Success()
        {
            await S3Service.ClearBucketAsync(_bucketName);

            var dir = "ClearBucketAsync-" + Guid.NewGuid().ToString();
            S3Service.CreateDirectory(_bucketName, dir);
            S3Service.CreateDirectory(_bucketName, dir + "/fol1");
            S3Service.CreateDirectory(_bucketName, dir + "/fol2");
            S3Service.CreateDirectory(_bucketName, dir + "/fol3");

            var objects = await S3Service.GetAllObjectsAsync(_bucketName, "");

            Assert.Equal(4, objects.Count);
            if (objects.Count != 4)
            {
                Assert.True(false, "");
            }
            await S3Service.ClearBucketAsync(_bucketName);

            objects.Clear();
            objects = await S3Service.GetAllObjectsAsync(_bucketName, "");

            Assert.Empty(objects);

        }
    }

}
