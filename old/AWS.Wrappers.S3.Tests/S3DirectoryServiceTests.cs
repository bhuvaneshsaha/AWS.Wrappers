using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AWS.Wrappers.S3.Tests
{
    public class S3DirectoryServiceTests: S3ServiceBase
    {
        [Fact]
        public void IsDirectoryExists_SendValidDirectory_ReturnsTrue()
        {
            var dir = "IsDirectoryExistsAsync" + Guid.NewGuid().ToString();

            S3Service.CreateDirectory(_bucketName, dir);

            var result = S3Service.IsDirectoryExists(_bucketName, dir);

            Assert.True(result, "IsDirectoryExists - Directory not exists");

            S3Service.DeleteDirectory(_bucketName, dir);
        }

        [Fact]
        public void IsDirectoryExists_SendInValidDirectory_ReturnsFalse()
        {
            var dir = "IsDirectoryExistsAsync-not-exists-" + Guid.NewGuid().ToString();

            var result = S3Service.IsDirectoryExists(_bucketName, dir);

            Assert.False(result, "IsDirectoryExistsAsync - Directory exists");

        }

        [Fact]
        public void CreateDirectory_SendNewDirectoryName_DirectoryCreated()
        {
            var dir = "CreateDirectory" + Guid.NewGuid().ToString();

            S3Service.CreateDirectory(_bucketName, dir);

            var result = S3Service.IsDirectoryExists(_bucketName, dir);

            Assert.True(result, "CreateDirectory - Directory creation failed");

            S3Service.DeleteDirectory(_bucketName, dir); //Cleanup
        }

        [Fact]
        public void GetSubDirectories()
        {
            var dir = "GetSubDirectories-" + Guid.NewGuid().ToString();
            S3Service.CreateDirectory(_bucketName, dir);
            S3Service.CreateDirectory(_bucketName, dir + "/fol1");
            S3Service.CreateDirectory(_bucketName, dir + "/fol2");
            S3Service.CreateDirectory(_bucketName, dir + "/fol3");
            var folders = S3Service.GetSubDirectories(_bucketName, dir);

            Assert.Equal(3, folders.Count);

            S3Service.DeleteDirectory(_bucketName, dir, true); //Cleanup
        }

        [Fact]
        public void GetAllObjects_SendValidPath_GetListOfObjects()
        {
            var dir = "GetAllObjects-" + Guid.NewGuid().ToString();
            S3Service.CreateDirectory(_bucketName, dir);
            S3Service.CreateDirectory(_bucketName, dir + "/fol1");
            S3Service.CreateDirectory(_bucketName, dir + "/fol2");
            S3Service.CreateDirectory(_bucketName, dir + "/fol3/fol31/fol322");
            S3Service.CreateDirectory(_bucketName, dir + "/fol4");
            S3Service.CreateDirectory(_bucketName, dir + "/fol4/fol41");

            var objects = S3Service.GetAllObjects(_bucketName, dir);

            Assert.Equal(6, objects.Count);

            S3Service.DeleteDirectory(_bucketName, dir, true); //Cleanup
        }

        [Fact]
        public void GetAllObjects_SendInValidPath_GetListOfObjects()
        {
            var objects = S3Service.GetAllObjects(_bucketName, "path/not/exits");

            Assert.Empty(objects);
        }

        [Fact]
        public void DeleteDirectory_DeleteWithRecursiveTrue_Success()
        {
            var dir = "DeleteDirectory-" + Guid.NewGuid().ToString();
            S3Service.CreateDirectory(_bucketName, dir);
            S3Service.CreateDirectory(_bucketName, dir + "/fol1");
            S3Service.CreateDirectory(_bucketName, dir + "/fol2");
            S3Service.CreateDirectory(_bucketName, dir + "/fol3");
            S3Service.CreateDirectory(_bucketName, dir + "/fol3/fol31");
            
            var result = S3Service.DeleteDirectory(_bucketName, dir, true);

            Assert.Equal(5, result);
        }

        [Fact]
        public void DeleteDirectory_DeleteWithRecursiveFalse_ThrowsError()
        {
            var dir = "DeleteDirectory-" + Guid.NewGuid().ToString();
            S3Service.CreateDirectory(_bucketName, dir);
            S3Service.CreateDirectory(_bucketName, dir + "/fol1");
            S3Service.CreateDirectory(_bucketName, dir + "/fol2");
            S3Service.CreateDirectory(_bucketName, dir + "/fol3");
            S3Service.CreateDirectory(_bucketName, dir + "/fol3/fol31");


            Assert.Throws<InvalidOperationException>(() =>
                S3Service.DeleteDirectory(_bucketName, dir));

            S3Service.DeleteDirectory(_bucketName, dir, true);//Clean up

        }

    }
}
