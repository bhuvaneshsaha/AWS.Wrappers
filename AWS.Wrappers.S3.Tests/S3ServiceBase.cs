using Amazon.S3;
using AWS.Wrappers.S3.Interfaces;
using AWS.Wrappers.S3.Models;
using AWS.Wrappers.S3.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AWS.Wrappers.S3.Tests
{
    public class S3ServiceBase
    {
        private readonly IS3Service? _s3Service;
        public static string _bucketName = "s3-unit-test-dotnet-temp";

        public IS3Service S3Service
        {
            get { return _s3Service ?? GetS3ServiceObject(); }
        }

        private IS3Service GetS3ServiceObject()
        {
            var accessKey = "<AccessKey>";
            var secretKey = "<SecretKey>";

            return new S3Service(accessKey, secretKey, S3Regions.USEast1);
        }
        
    }
}
