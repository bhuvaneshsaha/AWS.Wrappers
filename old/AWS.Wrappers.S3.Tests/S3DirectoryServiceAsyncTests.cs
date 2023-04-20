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
        public async Task IsDirectoryExistsAsync_SendValidDirectory_ReturnsTrue()
        {
            var dir = "IsDirectoryExistsAsync" +  Guid.NewGuid().ToString();

            await S3Service.CreateDirectoryAsync(_bucketName,dir);

            var result = await S3Service.IsDirectoryExistsAsync(_bucketName, dir);

            Assert.True(result, "IsDirectoryExistsAsync - Directory not exists");

            await S3Service.DeleteDirectoryAsync(_bucketName, dir);
        }

        [Fact]
        public async Task IsDirectoryExistsAsync_SendInValidDirectory_ReturnsFalse()
        {
            var dir = "IsDirectoryExistsAsync-not-exists-" + Guid.NewGuid().ToString();
            
            var result = await S3Service.IsDirectoryExistsAsync(_bucketName, dir);

            Assert.False(result, "IsDirectoryExistsAsync - Directory exists");

        }


        [Fact]
        public async Task CreateDirectoryAsync()
        {
            var dir = "CreateDirectoryAsync" + Guid.NewGuid().ToString();

            await S3Service.CreateDirectoryAsync(_bucketName, dir);

            var result = await S3Service.IsDirectoryExistsAsync(_bucketName, dir);

            Assert.True(result, "CreateDirectoryAsync - Directory creation failed");

            await S3Service.DeleteDirectoryAsync(_bucketName, dir);
        }

        [Fact]
        public async Task GetSubDirectoriesAsync()
        {
            var dir = "GetSubDirectoriesAsync-" + Guid.NewGuid().ToString();
            S3Service.CreateDirectory(_bucketName ,dir);
            S3Service.CreateDirectory(_bucketName ,dir +"/fol1");
            S3Service.CreateDirectory(_bucketName ,dir +"/fol2");
            S3Service.CreateDirectory(_bucketName ,dir +"/fol3");
            var folders = await S3Service.GetSubDirectoriesAsync(_bucketName, dir);

            Assert.Equal(3, folders.Count);

            await S3Service.DeleteDirectoryAsync(_bucketName, dir, true);
        }

        [Fact]
        public async Task GetAllObjectsAsync_SendValidPath_GetListOfObjects()
        {
            var dir = "GetAllObjects-" + Guid.NewGuid().ToString();
            S3Service.CreateDirectory(_bucketName, dir);
            S3Service.CreateDirectory(_bucketName, dir + "/fol1");
            S3Service.CreateDirectory(_bucketName, dir + "/fol2");
            S3Service.CreateDirectory(_bucketName, dir + "/fol3/fol31/fol322");
            S3Service.CreateDirectory(_bucketName, dir + "/fol4");
            S3Service.CreateDirectory(_bucketName, dir + "/fol4/fol41");

            var objects = await S3Service.GetAllObjectsAsync(_bucketName, dir);

            Assert.Equal(6, objects.Count);

            S3Service.DeleteDirectory(_bucketName, dir, true); //Cleanup
        }

        [Fact]
        public async Task GetAllObjectsAsync_SendInValidPath_GetListOfObjects()
        {
            var objects = await S3Service.GetAllObjectsAsync(_bucketName, "path/not/exits");

            Assert.Empty(objects);
        }



        [Fact]
        public async Task DeleteDirectoryAsync_DeleteWithRecursiveTrue_Success()
        {
            var dir = "DeleteDirectoryAsync-" + Guid.NewGuid().ToString();
            S3Service.CreateDirectory(_bucketName, dir);
            S3Service.CreateDirectory(_bucketName, dir + "/fol1");
            S3Service.CreateDirectory(_bucketName, dir + "/fol2");
            S3Service.CreateDirectory(_bucketName, dir + "/fol3");
            S3Service.CreateDirectory(_bucketName, dir + "/fol3/fol31");

            var result = await S3Service.DeleteDirectoryAsync(_bucketName, dir, true);

            Assert.Equal(5, result);
        }

        [Fact]
        public async Task DeleteDirectoryAsync_DeleteWithRecursiveFalse_ThrowsError()
        {
            var dir = "DeleteDirectoryAsync-" + Guid.NewGuid().ToString();
            S3Service.CreateDirectory(_bucketName, dir);
            S3Service.CreateDirectory(_bucketName, dir + "/fol1");
            S3Service.CreateDirectory(_bucketName, dir + "/fol2");
            S3Service.CreateDirectory(_bucketName, dir + "/fol3");
            S3Service.CreateDirectory(_bucketName, dir + "/fol3/fol31");

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await
                S3Service.DeleteDirectoryAsync(_bucketName, dir));

            await S3Service.DeleteDirectoryAsync(_bucketName, dir, true); //Clean up

        }

    }
}
