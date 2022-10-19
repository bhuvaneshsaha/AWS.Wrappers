using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Wrappers.S3.Models
{
    public class S3UploadByBytes
    {
        public byte[] SourceFileBytes { get; set; }
        public string DestPath { get; set; }
        public string DestFileName { get; set; }
        public string BucketName { get; set; }
        public bool CreatePathIfNotExists { get; set; } = true;
    }
}
