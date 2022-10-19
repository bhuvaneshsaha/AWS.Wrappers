using Amazon.S3;
using AWS.Wrappers.S3.Interfaces;
using AWS.Wrappers.S3.Models;
using AWS.Wrappers.S3.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AWS.Wrappers.S3.Tests
{
    [Collection("Sequential")]
    public class S3ServiceBase
    {

        private IS3Service? _s3Service;
        public IS3Service S3Service
        {
            get
            {
                return _s3Service ??= GetS3ServiceObject();
            }
        }

        private IConfiguration? _configuration;
        public IConfiguration Configuration
        {
            get { return _configuration ??= InitConfiguration(); }
        }

        public string _bucketName;

        private IS3Service GetS3ServiceObject()
        {
            _bucketName = Configuration["s3:testBucket"];
            var accessKey = Configuration["awss3keys:accessKey"];
            var secretKey = Configuration["awss3keys:secretKey"];
            return new S3Service(accessKey, secretKey, S3Regions.USEast1);
        }



        private static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
               .AddUserSecrets<S3ServiceBase>(true)
                .Build();
            return config;
        }

        public S3ServiceBase()
        {
            if (S3Service.IsBucketExists(_bucketName))
                S3Service.ClearBucket(_bucketName);
            else
                S3Service.CreateBucket(_bucketName);

            var temp = "Temp";
            if (Directory.Exists(temp))
            {
                Directory.Delete(temp, true);
            }
            Directory.CreateDirectory(temp);
        }
    }
}
