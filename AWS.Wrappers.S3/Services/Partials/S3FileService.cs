using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using AWS.Wrappers.S3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Wrappers.S3.Services;

public partial class S3Service
{
    public void UploadFile(S3UploadByBytes s3Upload)
    {
        using (var fsSource = new MemoryStream(s3Upload.SourceFileBytes))
        {
            s3Upload.DestPath = PathFormat(s3Upload.DestPath);
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = fsSource,
                Key = s3Upload.DestPath + s3Upload.DestFileName,
                BucketName = s3Upload.BucketName,
                CannedACL = S3CannedACL.BucketOwnerFullControl
            };

            var fileTransferUtility = new TransferUtility(_s3Client);
            fileTransferUtility.UploadAsync(uploadRequest).GetAwaiter().GetResult();
        }
    }

    public void UploadFile(S3Upload s3Upload)
    {
        using (FileStream fsSource = new FileStream(s3Upload.SourcePath, FileMode.Open, FileAccess.Read))
        {
            s3Upload.DestPath = PathFormat(s3Upload.DestPath);
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = fsSource,
                Key = s3Upload.DestPath + s3Upload.DestFileName,
                BucketName = s3Upload.BucketName,
                CannedACL = S3CannedACL.BucketOwnerFullControl
            };

            var fileTransferUtility = new TransferUtility(_s3Client);
            fileTransferUtility.UploadAsync(uploadRequest).GetAwaiter().GetResult();
        }
    }

    public void DeleteFile(string bucketName, string s3Key)
    {
        _s3Client.DeleteObjectAsync(new Amazon.S3.Model.DeleteObjectRequest()
        {
            BucketName = bucketName,
            Key = s3Key
        }).GetAwaiter().GetResult();
    }

    public void DownloadFileToLocal(S3Download s3Download)
    {
        MemoryStream ms = null;
        GetObjectRequest getObjectRequest = new GetObjectRequest
        {
            BucketName = s3Download.BucketName,
            Key = s3Download.Key
        };

        using (var response = _s3Client.GetObjectAsync(getObjectRequest).GetAwaiter().GetResult())
        {
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                using (ms = new MemoryStream())
                {
                    response.ResponseStream.CopyTo(ms);
                }
            }
        }

        File.WriteAllBytes(s3Download.LocalPath, ms?.ToArray());

    }

    public byte[] GetFileBytes(S3Download s3Download)
    {
        MemoryStream ms = null;
        GetObjectRequest getObjectRequest = new GetObjectRequest
        {
            BucketName = s3Download.BucketName,
            Key = s3Download.Key
        };

        using (var response = _s3Client.GetObjectAsync(getObjectRequest).GetAwaiter().GetResult())
        {
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                using (ms = new MemoryStream())
                {
                    response.ResponseStream.CopyTo(ms);
                }
            }
        }

        return ms?.ToArray();
    }

    public bool IsFileExists(string bucketName, string s3Key)
    {
        var request = new ListObjectsV2Request()
        {
            BucketName = bucketName,
            Prefix = s3Key
        };

        var listResponse = _s3Client.ListObjectsV2Async(request).GetAwaiter().GetResult();
        return listResponse.S3Objects.Any(o => o.Key == s3Key && o.BucketName == bucketName);
    }

    public List<string> GetFiles(string bucketName, string path)
    {
        var request = new ListObjectsV2Request { BucketName = bucketName, Prefix = path, StartAfter = path };
        var lists = _s3Client.ListObjectsV2Async(request).GetAwaiter().GetResult();

        var files = new List<string>();
        var file = "";
        foreach (var item in lists.S3Objects)
        {
            if (item.Size > 0)
            {
                file = item.Key.Replace(path + "/", "");
                if (!file.Contains('/'))
                {
                    files.Add(file);
                }
            }
        }

        return files;

    }

}
