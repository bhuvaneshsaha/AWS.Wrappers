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
        public void IsDirectoryExists()
        {
            var path1 = "A/B/C/E";
            var result = S3Service.IsDirectoryExists(_bucketName, path1);

            Assert.True(result);
        }

        [Fact]
        public void CreateDirectory()
        {
            var folderToCreate = "FolACreate/FolBCreate/FolCCreate";
            S3Service.CreateDirectory(_bucketName, folderToCreate);
            var result = S3Service.IsDirectoryExists(_bucketName, folderToCreate);

            Assert.True(result);

            S3Service.DeleteDirectory(_bucketName, folderToCreate);
        }

        [Fact]
        public void GetSubDirectories()
        {
            var path1 = "A1";
            var folders = S3Service.GetSubDirectories(_bucketName, path1);
        }

        [Fact]
        public void GetAllDirectoriesRecursive()
        {
            var path1 = "A";
            var folders = S3Service.GetAllDirectoriesRecursive(_bucketName, path1);
        }

    }
}
