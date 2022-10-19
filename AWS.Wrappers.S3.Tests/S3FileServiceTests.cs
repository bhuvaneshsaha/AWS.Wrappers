using AWS.Wrappers.S3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AWS.Wrappers.S3.Tests
{
    public class S3FileServiceTests : S3ServiceBase
    {
        [Fact]
        public void UploadFile_S3Upload_Success()
        {
            var destPath = Guid.NewGuid().ToString() + "A/B/C";
            var filePath = "Files\\Upload-File-1.txt";
            var s3Upload = new S3Upload
            {
                SourcePath = filePath,
                DestPath = destPath,
                DestFileName = "UploadFile_S3Upload_Success.txt",
                BucketName = _bucketName
            };

            S3Service.UploadFile(s3Upload);

            var result = S3Service.IsFileExists(_bucketName, destPath + "/UploadFile_S3Upload_Success.txt");

            Assert.True(result);
            S3Service.DeleteFile(_bucketName, destPath);
        }

        [Fact]
        public void UploadFileAsync_SendFileBytes()
        {
            var destPath = Guid.NewGuid().ToString() + "A/B/C";
            var filePath = "Files\\Upload-File-1.txt";
            var s3Upload = new S3UploadByBytes
            {
                SourceFileBytes = File.ReadAllBytes(filePath),
                DestPath = destPath,
                DestFileName = "UploadFileAsync_SendFileBytes.txt",
                BucketName = _bucketName

            };

            S3Service.UploadFile(s3Upload);

            var result = S3Service.IsFileExists(_bucketName, destPath + "/UploadFileAsync_SendFileBytes.txt");

            Assert.True(result);

            S3Service.DeleteFile(_bucketName, destPath + "/UploadFileAsync_SendFileBytes.txt");
        }

        [Fact]
        public void DownloadFileToLocal()
        {
            var localPath = "Temp\\downloaded_DownloadFileToLocal.txt";
            var destPath = Guid.NewGuid().ToString() + "A/B/C";
            var filePath = "Files\\Upload-File-1.txt";

            var s3Upload = new S3UploadByBytes
            {
                SourceFileBytes = File.ReadAllBytes(filePath),
                DestPath = destPath,
                DestFileName = "UploadFileAsync_SendFileBytes.txt",
                BucketName = _bucketName

            };

            S3Service.UploadFile(s3Upload);

            var s3Download = new S3Download
            {
                BucketName = _bucketName,
                Key = destPath + "/UploadFileAsync_SendFileBytes.txt",
                LocalPath = localPath

            };

            S3Service.DownloadFileToLocal(s3Download);

            Assert.True(File.Exists(localPath), "File not downloaded");

            //Cleanup
            File.Delete(localPath);
            S3Service.DeleteFile(_bucketName, destPath);
        }

        [Fact]
        public void GetFileBytesAsync()
        {

            var localPath = "Temp\\downloaded_DownloadFileToLocal.txt";
            var destPath = Guid.NewGuid().ToString() + "A/B/C";
            var filePath = "Files\\Upload-File-1.txt";

            var s3Upload = new S3UploadByBytes
            {
                SourceFileBytes = File.ReadAllBytes(filePath),
                DestPath = destPath,
                DestFileName = "GetFileBytesAsync.txt",
                BucketName = _bucketName

            };

            S3Service.UploadFile(s3Upload);

            var s3Download = new S3Download
            {
                BucketName = _bucketName,
                Key = destPath + "/GetFileBytesAsync.txt",
            };

            var fileBytes = S3Service.GetFileBytes(s3Download);

            File.WriteAllBytes(localPath, fileBytes);

            Assert.True(File.Exists(localPath), "File not downloaded");

            //Cleanup
            File.Delete(localPath);
            S3Service.DeleteFile(_bucketName, destPath);
        }
        [Fact]
        public void IsFileExists_Success()
        {
            var destPath = Guid.NewGuid().ToString() + "A/B/C";
            var filePath = "Files\\Upload-File-1.txt";
            var s3Upload = new S3Upload
            {
                SourcePath = filePath,
                DestPath = destPath,
                DestFileName = "IsFileExists.txt",
                BucketName = _bucketName
            };

            S3Service.UploadFile(s3Upload);

            var result = S3Service.IsFileExists(_bucketName, destPath + "/IsFileExists.txt");

            Assert.True(result);
            S3Service.DeleteFile(_bucketName, destPath);
        }

        [Fact]
        public void DeleteFile_Success()
        {
            var destPath = Guid.NewGuid().ToString() + "A/B/C";
            var filePath = "Files\\Upload-File-1.txt";
            var s3Upload = new S3Upload
            {
                SourcePath = filePath,
                DestPath = destPath,
                DestFileName = "UploadedFileForDelete.txt",
                BucketName = _bucketName
            };

            S3Service.UploadFile(s3Upload);

            S3Service.DeleteFile(_bucketName, destPath + "/UploadedFileForDelete.txt");

            var result = S3Service.IsFileExists(_bucketName, destPath + "/UploadedFileForDelete.txt");

            Assert.False(result);

        }

        [Fact]
        public void GetFiles()
        {
            var destPath = Guid.NewGuid().ToString() + "A/B/C";
            var filePath = "Files\\Upload-File-1.txt";
            var s3Upload = new S3Upload
            {
                SourcePath = filePath,
                DestPath = destPath,
                DestFileName = "UploadedFile1.txt",
                BucketName = _bucketName
            };

            S3Service.UploadFile(s3Upload);
            s3Upload = new S3Upload
            {
                SourcePath = filePath,
                DestPath = destPath,
                DestFileName = "UploadedFile2.txt",
                BucketName = _bucketName
            };
            S3Service.UploadFile(s3Upload);

            var files = S3Service.GetFiles(_bucketName, destPath);

            Assert.Equal(2, files.Count);
        }

    }
}
