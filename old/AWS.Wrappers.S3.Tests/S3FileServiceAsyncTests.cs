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
    public class S3FileServiceAsyncTests : S3ServiceBase
    {
        [Fact]
        public async Task UploadFile_S3Upload_Success()
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

            var result = await S3Service.IsFileExistsAsync(_bucketName, destPath + "/UploadFile_S3Upload_Success.txt");

            //Cleanup
            Assert.True(result);
            S3Service.DeleteFile(_bucketName, destPath);
        }

        [Fact]
        public async Task UploadFileAsync_SendFileBytes()
        {
            var destPath = Guid.NewGuid().ToString() + "A/B/C";
            var filePath = "Files\\Upload-File-1.txt";
            var s3Upload = new S3Upload
            {
                SourcePath = filePath,
                DestPath = destPath,
                DestFileName = "UploadFileAsync_SendFileBytes.txt",
                BucketName = _bucketName
            };

            await S3Service.UploadFileAsync(s3Upload);

            var result = await S3Service.IsFileExistsAsync(_bucketName, destPath + "/UploadFileAsync_SendFileBytes.txt");

            //Cleanup
            Assert.True(result);
            await S3Service.DeleteFileAsync(_bucketName, destPath + "/UploadFileAsync_SendFileBytes.txt");
        }

        [Fact]
        public async Task DownloadFileToLocalAsync()
        {
            var localPath = "Temp\\DownloadFileToLocalAsync.txt";
            var destPath = Guid.NewGuid().ToString() + "A/B/C";
            var filePath = "Files\\Upload-File-1.txt";

            var s3Upload = new S3UploadByBytes
            {
                SourceFileBytes = await File.ReadAllBytesAsync(filePath),
                DestPath = destPath,
                DestFileName = "DownloadFileToLocalAsync.txt",
                BucketName = _bucketName

            };

            await S3Service.UploadFileAsync(s3Upload);

            var s3Download = new S3Download
            {
                BucketName = _bucketName,
                Key = destPath + "/DownloadFileToLocalAsync.txt",
                LocalPath = localPath

            };

            await S3Service.DownloadFileToLocalAsync(s3Download);

            Assert.True(File.Exists(localPath), "File not downloaded");

            //Cleanup
            File.Delete(localPath);
            await S3Service.DeleteFileAsync(_bucketName, destPath);
        }

        [Fact]
        public async Task GetFileBytesAsync()
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

            await S3Service.UploadFileAsync(s3Upload);

            var s3Download = new S3Download
            {
                BucketName = _bucketName,
                Key = destPath + "/GetFileBytesAsync.txt",
            };

            var fileBytes = await S3Service.GetFileBytesAsync(s3Download);

            File.WriteAllBytes(localPath, fileBytes);

            Assert.True(File.Exists(localPath), "File not downloaded");

            //Cleanup
            File.Delete(localPath);
            await S3Service.DeleteFileAsync(_bucketName, destPath);
        }

        [Fact]
        public async Task IsFileExistsAsync()
        {
            var destPath = Guid.NewGuid().ToString() + "A/B/C";
            var filePath = "Files\\Upload-File-1.txt";
            var s3Upload = new S3Upload
            {
                SourcePath = filePath,
                DestPath = destPath,
                DestFileName = "UploadedFile.txt",
                BucketName = _bucketName
            };

            S3Service.UploadFile(s3Upload);

            var result = await S3Service.IsFileExistsAsync(_bucketName, destPath + "/UploadedFile.txt");

            Assert.True(result);
            await S3Service.DeleteFileAsync(_bucketName, destPath);
        }
    }
}
