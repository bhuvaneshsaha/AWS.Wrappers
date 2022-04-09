using AWS.Wrappers.S3.Interfaces;
using AWS.Wrappers.S3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Wrappers.S3.Tests
{
    public class BaseTests
    {
        private IS3Service? _iS3Service;

        public IS3Service S3Service { get => _iS3Service ??= GetS3ClientUsingKeys(); }

        private S3Service GetS3ClientUsingKeys()
        {
            var publicKey = "AKIARPSFPGN4Y7MMCW6R";
            var secretKey = "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";
            var region = S3Regions.USEast1; //us-east-1
            return new S3Service(publicKey, secretKey, region);
        }



    }
}
