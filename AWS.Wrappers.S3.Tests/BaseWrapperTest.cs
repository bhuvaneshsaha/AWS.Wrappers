using AWS.Wrappers.S3.Interfaces;
using AWS.Wrappers.S3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Wrappers.S3.Tests
{
    public class BaseWrapperTest
    {
        private IS3Service _iS3Service;

        public IS3Service S3Service { get => _iS3Service ??= GetS3ClientUsingKeys(); }

        private S3Service GetS3ClientUsingKeys()
        {
            var publicKey = "";
            var secretKey = "";
            var region = "";
            return new S3Service(publicKey, secretKey, region);
        }
    }
}
