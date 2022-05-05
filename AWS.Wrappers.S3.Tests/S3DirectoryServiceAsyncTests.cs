using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AWS.Wrappers.S3.Tests
{
    public class S3DirectoryServiceAsyncTests: S3ServiceBase
    {
        [Fact]
        public async Task IsDirectoryExistsAsync()
        {
            var path1 = "A/B/C/";
            var result = await S3Service.IsDirectoryExistsAsync(_bucketName, path1);

            Assert.True(result);
        }

        [Fact]
        public async Task CreateDirectoryAsync()
        {
            var folderToCreate = "FolACreate/FolBCreate/FolCCreate";
            await S3Service.CreateDirectoryAsync(_bucketName, folderToCreate);
            var result = await S3Service.IsDirectoryExistsAsync(_bucketName, folderToCreate);

            Assert.True(result);

            await S3Service.DeleteDirectoryAsync(_bucketName, folderToCreate);

            result = await S3Service.IsDirectoryExistsAsync(_bucketName, folderToCreate);

            Assert.False(result);
        }

        [Fact]
        public async Task GetSubDirectoriesAsync()
        {
            var path1 = "A";
            var folders = await S3Service.GetSubDirectoriesAsync(_bucketName, path1);
        }

    }
}
