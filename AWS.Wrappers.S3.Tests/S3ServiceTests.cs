using Amazon.S3;
using AWS.Wrappers.S3.Interfaces;
using AWS.Wrappers.S3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AWS.Wrappers.S3.Tests
{
    public class S3ServiceTests
    {
        private readonly IS3Service _s3Service;
        private string _bucketName = "s3-unit-test-dotnet-temp";
        public S3ServiceTests()
        {
            var accessKey = "<AccessKey>";
            var secretKey = "<SecretKey>";
            _s3Service = new S3Service(accessKey, secretKey, S3Regions.USEast1);
        }

        #region Bucket Operations

        [Fact]
        public async Task CreateBucketAsync_SendNewBucketName_BucketCreated()
        {
            var bucketName = "aws-s3-wrapper-unit-test-bucket-bkt01";
            await _s3Service.CreateBucketAsync(bucketName);
            var result = await _s3Service.IsBucketExistsAsync(bucketName);

            Assert.True(result, "Bucket Creation failed");
        }

        [Fact]
        public void CreateBucket_SendNewBucketName_BucketCreated()
        {
            var bucketName = "aws-s3-wrapper-unit-test-bucket-bkt02";
            _s3Service.CreateBucket(_bucketName);
            var result = _s3Service.IsBucketExists(_bucketName);

            Assert.True(result, "Bucket Creation failed");
        }

        #endregion

        #region Directory Operations
        [Fact]
        public void IsDirectoryExists()
        {
            var path1 = "A/B/C/E";
            var result = _s3Service.IsDirectoryExists(_bucketName, path1);

            Assert.True(result);
        }

        [Fact]
        public async Task IsDirectoryExistsAsync()
        {
            var path1 = "A/B/C/";
            var result = await _s3Service.IsDirectoryExistsAsync(_bucketName, path1);

            Assert.True(result);
        }

        [Fact]
        public void CreateDirectory()
        {
            var folderToCreate = "FolACreate/FolBCreate/FolCCreate";
            _s3Service.CreateDirectory(_bucketName, folderToCreate);
            var result = _s3Service.IsDirectoryExists(_bucketName, folderToCreate);

            Assert.True(result);

            _s3Service.DeleteDirectory(_bucketName, folderToCreate);
        }

        [Fact]
        public async Task CreateDirectoryAsync()
        {
            var folderToCreate = "FolACreate/FolBCreate/FolCCreate";
            await _s3Service.CreateDirectoryAsync(_bucketName, folderToCreate);
            var result = await _s3Service.IsDirectoryExistsAsync(_bucketName, folderToCreate);

            Assert.True(result);

            await _s3Service.DeleteDirectoryAsync(_bucketName, folderToCreate);

            result = await _s3Service.IsDirectoryExistsAsync(_bucketName, folderToCreate);

            Assert.False(result);
        }

        [Fact]
        public async Task GetSubDirectoriesAsync()
        {
            var path1 = "A";
            var folders = await _s3Service.GetSubDirectoriesAsync(_bucketName, path1);
        }

        [Fact]
        public void GetSubDirectories()
        {
            var path1 = "A1";
            var folders = _s3Service.GetSubDirectories(_bucketName, path1);
        }

        [Fact]
        public void GetAllDirectoriesRecursive()
        {
            var path1 = "A";
            var folders = _s3Service.GetAllDirectoriesRecursive(_bucketName, path1);
        }

        #endregion

    }
}
