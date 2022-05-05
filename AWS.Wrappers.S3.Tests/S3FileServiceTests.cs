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
    public class S3FileServiceTests: S3ServiceBase
    {
        [Fact]
        public void UploadFile_S3Upload()
        {
            var filePath = Directory.GetCurrentDirectory() + "\\Files";
            var s3Upload = new S3Upload
            {
                SourcePath = filePath + "\\Upload-File-1.txt",
                DestPath = "A/B/C",
                DestFileName = "UploadedFile.txt",
                BucketName = _bucketName

            };

            S3Service.UploadFile(s3Upload);
        }

    }
}
