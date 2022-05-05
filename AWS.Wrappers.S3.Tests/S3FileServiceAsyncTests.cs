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
    public class S3FileServiceAsyncTests: S3ServiceBase
    {
        [Fact]
        public async Task UploadFile_S3Upload()
        {
            var filePath = Directory.GetCurrentDirectory() + "\\Files";
            var s3Upload = new S3Upload
            {
                SourcePath = filePath + "\\Upload-File-1.txt",
                DestPath = "A/B/C",
                DestFileName = "UploadedFile.txt",
                BucketName = _bucketName

            };

            await S3Service.UploadFileAsync(s3Upload);
        }
    }
}
